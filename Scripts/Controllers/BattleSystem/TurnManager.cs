using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class TurnManager
{
    private List<IBattlePhase> phases;
    private int currentTurn;
    private int maxTurn;

    public TurnManager(PlayerController attacker, PlayerController defender, int maxTurn)
    {
        this.maxTurn = maxTurn;
        this.currentTurn = 1;

        phases = new List<IBattlePhase>
        {
            new StartPhase(),
            new BattlePhase(),
            new EndPhase()
        };
    }

    public IEnumerator RunTurns(PlayerController attacker, PlayerController defender)
    {
        while (currentTurn <= maxTurn)
        {
            // Debug.Log($"===== TURN {currentTurn} START =====");

            foreach (var phase in phases)
            {
                yield return phase.ExecutePhase(attacker, defender);
            }

            // Kiểm tra thắng thua
            List<CardBase> attackerCards = attacker.GetCards();
            List<CardBase> defenderCards = defender.GetCards();
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
                // Debug.Log("🛡️ Defender has no alive cards left. Attacker wins!");
                yield break;
            }

            // Debug.Log($"===== TURN {currentTurn} END =====");
            currentTurn++;
        }

        // Debug.Log("Max turns reached. Attacker loses, Defender wins!");
    }
}