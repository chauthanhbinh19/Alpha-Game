using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private CardDisplayManager displayManager;
    [SerializeField] private int maxTurns = 8;

    private PlayerController attacker;
    private PlayerController defender;
    private TurnManager turnManager;

    public void InitializeBattle(PlayerController attacker, PlayerController defender)
    {
        this.attacker = attacker;
        this.defender = defender;

        displayManager?.InitializeBattleGrid();

        AssignDefaultGridPositions(attacker.GetCards(), BattleCellType.Player);
        AssignDefaultGridPositions(defender.GetCards(), BattleCellType.Enemy);

        turnManager = new TurnManager(attacker, defender, maxTurns, displayManager);
    }

    public void StartBattle()
    {
        if (attacker == null || defender == null)
        {
            Debug.LogError("BattleManager requires attacker and defender before starting the battle.");
            return;
        }

        StartCoroutine(turnManager.RunTurns(attacker, defender));
    }

    private void AssignDefaultGridPositions(List<CardModel> cards, BattleCellType owner)
    {
        var availablePositions = displayManager?.GetAvailablePositions(owner);
        if (availablePositions == null || availablePositions.Count == 0)
        {
            Debug.LogWarning($"No available grid slots defined for owner {owner}.");
            return;
        }

        int index = 0;
        foreach (var card in cards)
        {
            if (card == null || !card.IsAlive)
                continue;

            if (card.CellPosition >= 0 && availablePositions.Contains(card.CellPosition))
            {
                if (displayManager?.PlaceCardModel(card, card.CellPosition, owner) == true)
                    continue;
            }

            if (index >= availablePositions.Count)
                break;

            int position = availablePositions[index];
            displayManager?.PlaceCardModel(card, position, owner);
            index++;
        }
    }
}

