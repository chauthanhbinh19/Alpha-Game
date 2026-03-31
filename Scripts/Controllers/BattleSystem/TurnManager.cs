using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class TurnManager
{
    private readonly List<BattleTurn> turns;
    private int currentTurnIndex;
    private readonly int maxTurn = 8;

    public TurnManager(PlayerController attacker, PlayerController defender, int maxTurn, CardDisplayManager displayManager)
    {
        this.maxTurn = maxTurn;
        this.currentTurnIndex = 0;
        turns = new List<BattleTurn>();

        for (int turnId = 1; turnId <= maxTurn; turnId++)
        {
            turns.Add(CreateTurn(turnId, displayManager));
        }
    }

    private BattleTurn CreateTurn(int turnId, CardDisplayManager displayManager)
    {
        return new BattleTurn
        {
            TurnId = turnId,
            Phases = new List<BattlePhaseInfo>
            {
                new BattlePhaseInfo(1, "Start", new StartPhase(displayManager)),
                new BattlePhaseInfo(2, "Battle", new BattlePhase()),
                new BattlePhaseInfo(3, "Battle", new BattlePhase()),
                new BattlePhaseInfo(4, "Battle", new BattlePhase()),
                new BattlePhaseInfo(5, "Battle", new BattlePhase()),
                new BattlePhaseInfo(6, "Battle", new BattlePhase()),
                new BattlePhaseInfo(7, "End", new EndPhase())
            }
        };
    }

    public IEnumerator RunTurns(PlayerController attacker, PlayerController defender)
    {
        while (currentTurnIndex < turns.Count)
        {
            BattleTurn currentTurn = turns[currentTurnIndex];
            Debug.Log($"===== TURN {currentTurn.TurnId} START =====");

            foreach (var phaseInfo in currentTurn.Phases)
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