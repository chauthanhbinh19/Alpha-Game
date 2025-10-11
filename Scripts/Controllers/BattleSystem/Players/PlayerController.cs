using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController
{
    private List<CardBase> fieldCards;

    public PlayerController()
    {
        fieldCards = new List<CardBase>();
    }

    public void AddCard(CardBase card)
    {
        fieldCards.Add(card);
    }

    public void Attack(PlayerController opponent)
    {
        foreach (var card in fieldCards)
        {
            card.PerformAction(opponent);
        }
    }

    public bool IsFieldEmpty()
    {
        return fieldCards.Count == 0;
    }

    public List<CardBase> GetFieldCards()
    {
        return fieldCards;
    }
}
