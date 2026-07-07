using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BattleState
{
    GameStart,
    Playing,
    GameOver
}

public enum BattlePhaseType
{
    Start,
    Preparation,
    Shopping,
    Battle,
    Boss,
    Reward,
    End,
    Custom
}

public enum BattlePhaseActionType
{
    Log,
    Wait,
    ResetCardsForTurn,
    OpenShop,
    CloseShop,
    BuildActionQueue,
    ExecuteBattleQueue,
    CleanupTurn,
    TriggerPassiveEffects
}

[Serializable]
public class BattlePhaseAction
{
    public BattlePhaseActionType ActionType;
    public string Label;
    public float Duration;
    public string PhaseCode = "MAIN";
    public string TriggerCondition;
}

[Serializable]
public class BattlePhaseDefinition
{
    public BattlePhaseType PhaseType = BattlePhaseType.Custom;
    public string PhaseName = "Phase";
    public float Duration;
    public bool WaitForDuration;
    public List<BattlePhaseAction> Actions = new List<BattlePhaseAction>();
}

[Serializable]
public class BattleTurnDefinition
{
    public int TurnNumber = 1;
    public List<BattlePhaseDefinition> Phases = new List<BattlePhaseDefinition>();
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    [Header("Game Loop Settings")]
    public int CurrentTurnNumber = 1;
    public bool AutoStartBattle = false;
    public bool RepeatLastTurnPlan = true;

    [Header("Turn Plans")]
    public List<BattleTurnDefinition> TurnPlans = new List<BattleTurnDefinition>();

    [Header("Card & Battle Reference")]
    public List<CardBase> AllCards = new List<CardBase>();

    public BattleState CurrentBattleState { get; private set; }
    public BattlePhaseDefinition CurrentPhase { get; private set; }

    public event Action<int, BattleTurnDefinition> OnTurnStarted;
    public event Action<int, BattlePhaseDefinition> OnPhaseStarted;
    public event Action<int, BattlePhaseDefinition> OnPhaseEnded;

    private readonly Queue<CardBase> actionQueue = new Queue<CardBase>();
    private CardBase currentActiveCard;
    private Coroutine battleLoopCoroutine;

    private void Awake()
    {
        Instance = this;
        EnsureDefaultTurnPlans();
    }

    private void Start()
    {
        if (AutoStartBattle)
        {
            StartBattle();
        }
    }

    public void StartBattle()
    {
        if (battleLoopCoroutine != null)
        {
            StopCoroutine(battleLoopCoroutine);
        }

        CurrentTurnNumber = Mathf.Max(1, CurrentTurnNumber);
        CurrentPhase = null;
        CurrentBattleState = BattleState.GameStart;
        TriggerPassiveEffects("START", AppConstants.TriggerCondition.ON_BATTLE_START);
        battleLoopCoroutine = StartCoroutine(BattleLoop());
    }

    public void AdvanceToNextPhase()
    {
        BattleTurnDefinition turnPlan = GetCurrentTurnPlan();
        if (turnPlan == null || turnPlan.Phases == null || turnPlan.Phases.Count == 0)
        {
            return;
        }

        BattlePhaseDefinition previousPhase = CurrentPhase;
        if (previousPhase != null)
        {
            OnPhaseEnded?.Invoke(CurrentTurnNumber, previousPhase);
        }

        int nextPhaseIndex = 0;
        if (CurrentPhase != null)
        {
            int currentIndex = turnPlan.Phases.FindIndex(phase => ReferenceEquals(phase, CurrentPhase));
            if (currentIndex >= 0 && currentIndex + 1 < turnPlan.Phases.Count)
            {
                nextPhaseIndex = currentIndex + 1;
            }
            else
            {
                CurrentTurnNumber++;
                turnPlan = GetCurrentTurnPlan();
                if (turnPlan == null || turnPlan.Phases == null || turnPlan.Phases.Count == 0)
                {
                    CurrentPhase = null;
                    CurrentBattleState = BattleState.GameOver;
                    return;
                }

                nextPhaseIndex = 0;
                OnTurnStarted?.Invoke(CurrentTurnNumber, turnPlan);
            }
        }

        CurrentPhase = turnPlan.Phases[nextPhaseIndex];
        OnPhaseStarted?.Invoke(CurrentTurnNumber, CurrentPhase);
    }

    public void TriggerGameOver()
    {
        CurrentBattleState = BattleState.GameOver;
        Debug.Log("<color=red>=== BATTLE ENDED ===</color>");
    }

    private IEnumerator BattleLoop()
    {
        Debug.Log("=== BATTLE START ===");
        CurrentBattleState = BattleState.Playing;

        while (CurrentBattleState == BattleState.Playing)
        {
            BattleTurnDefinition turnPlan = GetTurnPlan(CurrentTurnNumber);
            if (turnPlan == null || turnPlan.Phases.Count == 0)
            {
                Debug.LogWarning($"[TurnManager] No phase plan found for turn {CurrentTurnNumber}. Ending battle loop.");
                TriggerGameOver();
                yield break;
            }

            Debug.Log($"<color=yellow>=== TURN {CurrentTurnNumber} START ===</color>");
            OnTurnStarted?.Invoke(CurrentTurnNumber, turnPlan);

            foreach (BattlePhaseDefinition phase in turnPlan.Phases)
            {
                if (CurrentBattleState == BattleState.GameOver)
                {
                    yield break;
                }

                yield return RunPhase(phase);
            }

            Debug.Log($"<color=yellow>=== TURN {CurrentTurnNumber} END ===</color>");
            TriggerPassiveEffects("END", AppConstants.TriggerCondition.ON_TURN_END);
            CurrentTurnNumber++;
        }
    }

    private IEnumerator RunPhase(BattlePhaseDefinition phase)
    {
        CurrentPhase = phase;
        string phaseName = GetPhaseDisplayName(phase);

        Debug.Log($"<color=cyan>[Turn {CurrentTurnNumber}] Start Phase: {phaseName}</color>");
        OnPhaseStarted?.Invoke(CurrentTurnNumber, phase);

        foreach (BattlePhaseAction action in phase.Actions)
        {
            if (CurrentBattleState == BattleState.GameOver)
            {
                yield break;
            }

            yield return ExecutePhaseAction(action, phase);
        }

        if (phase.WaitForDuration && phase.Duration > 0f)
        {
            yield return new WaitForSeconds(phase.Duration);
        }

        Debug.Log($"<color=cyan>[Turn {CurrentTurnNumber}] End Phase: {phaseName}</color>");
        OnPhaseEnded?.Invoke(CurrentTurnNumber, phase);
    }

    public string GetCurrentPhaseDisplayName()
    {
        return GetPhaseDisplayName(CurrentPhase);
    }

    public string GetPhaseDisplayName(BattlePhaseDefinition phase)
    {
        if (phase == null)
        {
            return "Unnamed Phase";
        }

        if (!string.IsNullOrWhiteSpace(phase.PhaseName) && phase.PhaseType == BattlePhaseType.Custom)
        {
            return phase.PhaseName;
        }

        if (!string.IsNullOrWhiteSpace(phase.PhaseName) && phase.PhaseName != "Phase")
        {
            return $"{phase.PhaseType} - {phase.PhaseName}";
        }

        return phase.PhaseType.ToString();
    }

    private IEnumerator ExecutePhaseAction(BattlePhaseAction action, BattlePhaseDefinition phase)
    {
        if (action == null)
        {
            yield break;
        }

        switch (action.ActionType)
        {
            case BattlePhaseActionType.Log:
                Debug.Log(string.IsNullOrWhiteSpace(action.Label)
                    ? $"[TurnManager] {phase.PhaseName}"
                    : action.Label);
                break;

            case BattlePhaseActionType.Wait:
                yield return new WaitForSeconds(Mathf.Max(0f, action.Duration));
                break;

            case BattlePhaseActionType.ResetCardsForTurn:
                ResetCardsForTurn();
                break;

            case BattlePhaseActionType.OpenShop:
                OpenShopPhase(action.Duration);
                if (action.Duration > 0f)
                {
                    yield return new WaitForSeconds(action.Duration);
                }
                break;

            case BattlePhaseActionType.CloseShop:
                CloseShopPhase();
                break;

            case BattlePhaseActionType.BuildActionQueue:
                BuildActionQueue();
                break;

            case BattlePhaseActionType.ExecuteBattleQueue:
                yield return ExecuteBattleQueue();
                break;

            case BattlePhaseActionType.CleanupTurn:
                yield return CleanupTurn();
                break;

            case BattlePhaseActionType.TriggerPassiveEffects:
                TriggerPassiveEffects(action.PhaseCode, action.TriggerCondition);
                break;
        }
    }

    public BattleTurnDefinition GetCurrentTurnPlan()
    {
        return GetTurnPlan(CurrentTurnNumber);
    }

    public BattleTurnDefinition GetTurnPlan(int turnNumber)
    {
        BattleTurnDefinition exactPlan = TurnPlans.FirstOrDefault(plan => plan.TurnNumber == turnNumber);
        if (exactPlan != null)
        {
            return exactPlan;
        }

        if (!RepeatLastTurnPlan || TurnPlans.Count == 0)
        {
            return null;
        }

        return TurnPlans
            .Where(plan => plan.TurnNumber <= turnNumber)
            .OrderByDescending(plan => plan.TurnNumber)
            .FirstOrDefault() ?? TurnPlans.OrderBy(plan => plan.TurnNumber).FirstOrDefault();
    }

    private void ResetCardsForTurn()
    {
        foreach (CardBase card in AllCards)
        {
            if (card == null)
            {
                continue;
            }

            card.hasActedThisturn = false;

            if (card.Class != null)
            {
                card.CurrentMovementPoint = card.Class.MovementPoint;
            }
        }
    }

    private void OpenShopPhase(float duration)
    {
        Debug.Log(duration > 0f
            ? $"Shop opened for {duration:0.#} seconds."
            : "Shop opened.");

        // Hook shop UI here.
    }

    private void CloseShopPhase()
    {
        Debug.Log("Shop closed. Preparing battle actions.");

        // Hook shop UI lock/close here.
    }

    private void BuildActionQueue()
    {
        actionQueue.Clear();

        List<CardBase> sortedList = AllCards
            .Where(card => card != null && card.IsAlive && !card.hasActedThisturn)
            .OrderByDescending(card => card.CurrentSpeed > 0 ? card.CurrentSpeed : card.Speed)
            .ThenByDescending(card => card.Power)
            .ThenBy(card => card.Team)
            .ToList();

        foreach (CardBase card in sortedList)
        {
            actionQueue.Enqueue(card);
        }

        Debug.Log($"[Battle] Built action queue with {actionQueue.Count} cards.");
    }

    private IEnumerator ExecuteBattleQueue()
    {
        Debug.Log("--- BATTLE ACTIONS START ---");

        while (actionQueue.Count > 0 && CurrentBattleState == BattleState.Playing)
        {
            currentActiveCard = actionQueue.Dequeue();

            if (currentActiveCard == null || !currentActiveCard.IsAlive || currentActiveCard.hasActedThisturn)
            {
                continue;
            }

            Debug.Log($"-> Action turn: {currentActiveCard.Name} ({currentActiveCard.Team})");
            yield return ExecuteCardAction(currentActiveCard);
        }

        currentActiveCard = null;
        Debug.Log("--- BATTLE ACTIONS END ---");
    }

    private IEnumerator ExecuteCardAction(CardBase character)
    {
        // Replace this prototype routine with player input, AI, skill selection, or movement controller.
        yield return new WaitForSeconds(0.8f);

        if (character.CurrentMovementPoint > 0)
        {
            character.MoveTo(new Vector3(UnityEngine.Random.Range(0, 5), 0, UnityEngine.Random.Range(0, 5)));
        }

        yield return new WaitForSeconds(0.8f);
        character.EndAction();
    }

    private IEnumerator CleanupTurn()
    {
        Debug.Log("--- TURN CLEANUP ---");

        foreach (CardBase card in AllCards)
        {
            if (card == null)
            {
                continue;
            }

            TickEffectDurations(card, card.FriendlyEffects);
            TickEffectDurations(card, card.HostileEffects);
        }

        yield return new WaitForSeconds(0.5f);
    }

    private void TriggerPassiveEffects(string phaseCode, string triggerCondition)
    {
        if (CombatEngagementEngine.Instance == null || string.IsNullOrWhiteSpace(phaseCode) || string.IsNullOrWhiteSpace(triggerCondition))
        {
            return;
        }

        foreach (CardBase card in AllCards)
        {
            if (card == null || !card.IsAlive)
            {
                continue;
            }

            CombatEngagementEngine.Instance.TriggerPassiveEffects(card, null, phaseCode, triggerCondition);
        }
    }

    private void TickEffectDurations(CardBase owner, List<RuntimeEffectInstance> effects)
    {
        if (effects == null)
        {
            return;
        }

        for (int i = effects.Count - 1; i >= 0; i--)
        {
            RuntimeEffectInstance instance = effects[i];
            if (instance == null || !instance.IsActive)
            {
                effects.RemoveAt(i);
                continue;
            }

            instance.RemainingDuration--;
            if (instance.RemainingDuration <= 0)
            {
                CombatEffectProcessor.RevertExpiredEffect(owner, instance.SourceEffect);
                instance.IsActive = false;
                effects.RemoveAt(i);
            }
        }
    }

    private void EnsureDefaultTurnPlans()
    {
        if (TurnPlans.Count > 0)
        {
            return;
        }

        TurnPlans.Add(new BattleTurnDefinition
        {
            TurnNumber = 1,
            Phases = new List<BattlePhaseDefinition>
            {
                new BattlePhaseDefinition
                {
                    PhaseType = BattlePhaseType.Start,
                    PhaseName = "Start",
                    Actions = new List<BattlePhaseAction>
                    {
                        new BattlePhaseAction { ActionType = BattlePhaseActionType.ResetCardsForTurn },
                        new BattlePhaseAction { ActionType = BattlePhaseActionType.TriggerPassiveEffects, PhaseCode = "START", TriggerCondition = "TURN_START" },
                        new BattlePhaseAction { ActionType = BattlePhaseActionType.Wait, Duration = 1f }
                    }
                },
                new BattlePhaseDefinition
                {
                    PhaseType = BattlePhaseType.Shopping,
                    PhaseName = "Shopping",
                    Actions = new List<BattlePhaseAction>
                    {
                        new BattlePhaseAction { ActionType = BattlePhaseActionType.OpenShop, Duration = 5f },
                        new BattlePhaseAction { ActionType = BattlePhaseActionType.CloseShop }
                    }
                },
                new BattlePhaseDefinition
                {
                    PhaseType = BattlePhaseType.Battle,
                    PhaseName = "Battle",
                    Actions = new List<BattlePhaseAction>
                    {
                        new BattlePhaseAction { ActionType = BattlePhaseActionType.BuildActionQueue },
                        new BattlePhaseAction { ActionType = BattlePhaseActionType.ExecuteBattleQueue }
                    }
                },
                new BattlePhaseDefinition
                {
                    PhaseType = BattlePhaseType.End,
                    PhaseName = "End",
                    Actions = new List<BattlePhaseAction>
                    {
                        new BattlePhaseAction { ActionType = BattlePhaseActionType.CleanupTurn },
                        new BattlePhaseAction { ActionType = BattlePhaseActionType.Wait, Duration = 1f }
                    }
                }
            }
        });
    }
}
