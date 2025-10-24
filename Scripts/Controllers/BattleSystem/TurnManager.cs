using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TurnManager
{
    private ICardDisplayManager _displayManager;
    private List<IBattlePhase> phases;
    private int currentTurn;
    private int maxTurn;

    public TurnManager(int maxTurn, ICardDisplayManager displayManager)
    {
        this.maxTurn = maxTurn;
        this.currentTurn = 1;
        this._displayManager = displayManager;

        phases = new List<IBattlePhase>
        {
            new StartPhase(displayManager),
            // new BattlePhase(),
            // new EndPhase()
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
            if (attacker.IsFieldEmpty())
            {
                // Debug.Log("Attacker has no cards left. Defender wins!");
                yield break;
            }

            if (defender.IsFieldEmpty())
            {
                // Debug.Log("Defender has no cards left. Attacker wins!");
                yield break;
            }

            // Debug.Log($"===== TURN {currentTurn} END =====");
            currentTurn++;
        }

        // Debug.Log("Max turns reached. Attacker loses, Defender wins!");
    }
}