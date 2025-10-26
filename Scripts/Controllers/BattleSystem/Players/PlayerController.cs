using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController
{
    private List<CardBase> allCards;
    private LoadTeams _loadTeamService;

    public PlayerController()
    {
        allCards = new List<CardBase>();
        _loadTeamService = new LoadTeams();
    }

    public void GetPlayerCard(string userId, string teamId)
    {
        allCards = _loadTeamService.LoadPlayerTeamCard(userId, teamId);
    }

    public void Attack(PlayerController opponent)
    {
        foreach (var card in allCards)
        {
            card.PerformAction(opponent);
        }
    }

    public bool IsFieldEmpty()
    {
        return allCards.Count == 0;
    }

    public List<CardBase> GetCards()
    {
        return allCards;
    }
}
