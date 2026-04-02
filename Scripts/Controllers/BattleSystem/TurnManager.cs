using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class TurnManager
{
    private readonly List<BattleTurn> turns;
    private int currentTurnIndex;
    private readonly int maxTurn;
    private readonly Func<int, List<BattlePhaseInfo>> phaseFactory;

    public TurnManager(PlayerController attacker, PlayerController defender, int maxTurn, CardDisplayManager displayManager, Func<int, List<BattlePhaseInfo>> phaseFactory = null)
    {
        this.maxTurn = maxTurn;
        this.currentTurnIndex = 0;
        this.phaseFactory = phaseFactory ?? (turnId => new List<BattlePhaseInfo>());
        turns = new List<BattleTurn>();

        for (int turnId = 1; turnId <= maxTurn; turnId++)
        {
            turns.Add(CreateTurn(turnId));
        }
    }

    private BattleTurn CreateTurn(int turnId)
    {
        return new BattleTurn
        {
            TurnId = turnId,
            Phases = phaseFactory(turnId) ?? new List<BattlePhaseInfo>()
        };
    }

    public TurnPhaseBuilder InitiateTurn(int turnId, bool clearExisting = true)
    {
        BattleTurn turn = turns.Find(t => t.TurnId == turnId);
        if (turn == null)
            return new TurnPhaseBuilder(null);

        if (clearExisting)
            turn.Phases = new List<BattlePhaseInfo>();

        return new TurnPhaseBuilder(turn);
    }

    public void SetPhasesForTurn(int turnId, List<BattlePhaseInfo> phases)
    {
        BattleTurn turn = turns.Find(t => t.TurnId == turnId);
        if (turn == null)
            return;

        turn.Phases = phases?.OrderBy(p => p.PhaseId).ToList() ?? new List<BattlePhaseInfo>();
    }

    public void AddPhase(int turnId, int order, IBattlePhase phase, string name = null)
    {
        if (phase == null)
            return;

        BattleTurn turn = turns.Find(t => t.TurnId == turnId);
        if (turn == null)
            return;

        turn.Phases.Add(new BattlePhaseInfo(order, name ?? phase.GetType().Name, phase));
        SortTurnPhases(turn);
    }

    public void AddPhaseToTurn(int turnId, BattlePhaseInfo phase)
    {
        if (phase == null)
            return;

        BattleTurn turn = turns.Find(t => t.TurnId == turnId);
        if (turn == null)
            return;

        turn.Phases.Add(phase);
        SortTurnPhases(turn);
    }

    public void InsertPhaseToTurn(int turnId, int index, BattlePhaseInfo phase)
    {
        if (phase == null)
            return;

        BattleTurn turn = turns.Find(t => t.TurnId == turnId);
        if (turn == null)
            return;

        index = Mathf.Clamp(index, 0, turn.Phases.Count);
        turn.Phases.Insert(index, phase);
        SortTurnPhases(turn);
    }

    public void RemovePhaseFromTurn(int turnId, int phaseId)
    {
        BattleTurn turn = turns.Find(t => t.TurnId == turnId);
        if (turn == null)
            return;

        BattlePhaseInfo phase = turn.Phases.Find(p => p.PhaseId == phaseId);
        if (phase == null)
            return;

        turn.Phases.Remove(phase);
        SortTurnPhases(turn);
    }

    private void SortTurnPhases(BattleTurn turn)
    {
        turn.Phases.Sort((a, b) => a.PhaseId.CompareTo(b.PhaseId));
    }

    public class TurnPhaseBuilder
    {
        private readonly BattleTurn turn;
        private readonly List<BattlePhaseInfo> phases = new List<BattlePhaseInfo>();

        internal TurnPhaseBuilder(BattleTurn turn)
        {
            this.turn = turn;
        }

        public TurnPhaseBuilder Add(int order, IBattlePhase phase, string name = null)
        {
            if (phase == null)
                return this;

            phases.Add(new BattlePhaseInfo(order, name ?? phase.GetType().Name, phase));
            return this;
        }

        public TurnPhaseBuilder Add(IBattlePhase phase, string name = null)
        {
            return Add(phases.Count + 1, phase, name);
        }

        public TurnPhaseBuilder Commit()
        {
            if (turn == null)
                return this;

            if (turn.Phases == null)
                turn.Phases = new List<BattlePhaseInfo>();

            turn.Phases.AddRange(phases);
            turn.Phases.Sort((a, b) => a.PhaseId.CompareTo(b.PhaseId));
            return this;
        }

        public List<BattlePhaseInfo> Build()
        {
            return phases.OrderBy(p => p.PhaseId).ToList();
        }
    }

    public IEnumerator RunTurns(PlayerController attacker, PlayerController defender)
    {
        while (currentTurnIndex < turns.Count)
        {
            BattleTurn currentTurn = turns[currentTurnIndex];
            Debug.Log($"===== TURN {currentTurn.TurnId} START =====");

            foreach (var phaseInfo in currentTurn.Phases.OrderBy(p => p.PhaseId))
            {
                Debug.Log($"--- TURN {currentTurn.TurnId} - PHASE {phaseInfo.PhaseId}: {phaseInfo.Name} ---");
                yield return phaseInfo.Phase.ExecutePhase(attacker, defender);
            }

            // Kiểm tra thắng thua
            List<CardModel> attackerCards = attacker.GetCards();
            List<CardModel> defenderCards = defender.GetCards();
            // Đếm số thẻ còn sống
            int aliveAttackerCount = attackerCards.Count(card => card.IsAlive);
            int aliveDefenderCount = defenderCards.Count(card => card.IsAlive);

            if (aliveAttackerCount <= 0)
            {
                // Debug.Log("⚔️ Attacker has no alive cards left. Defender wins!");
                yield break;
            }

            if (aliveDefenderCount <= 0)
            {
                Debug.Log("Defender has no alive cards left. Attacker wins!");
                yield break;
            }

            Debug.Log($"===== TURN {currentTurn.TurnId} END =====");
            currentTurnIndex++;
        }

        Debug.Log("Max turns reached.");
    }
}

public class BattleTurn
{
    public int TurnId { get; set; }
    public List<BattlePhaseInfo> Phases { get; set; }
}

public class BattlePhaseInfo
{
    public int PhaseId { get; set; }
    public string Name { get; set; }
    public IBattlePhase Phase { get; set; }

    public BattlePhaseInfo(int phaseId, string name, IBattlePhase phase)
    {
        PhaseId = phaseId;
        Name = name;
        Phase = phase;
    }
}

public class SimpleBattlePhase : IBattlePhase
{
    private readonly string phaseName;

    public SimpleBattlePhase(string phaseName)
    {
        this.phaseName = phaseName;
    }

    public IEnumerator ExecutePhase(PlayerController attacker, PlayerController defender)
    {
        Debug.Log($"=== {phaseName} ===");
        yield return new WaitForSeconds(0.5f);
        Debug.Log($"{phaseName} Complete");
    }
}