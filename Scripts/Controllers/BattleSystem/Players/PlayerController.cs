using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class PlayerController
{
    private List<CardBase> allCards;
    private LoadTeams _loadTeamService;

    public PlayerController()
    {
        allCards = new List<CardBase>();
        _loadTeamService = new LoadTeams();
    }

    public async Task GetPlayerCardAsync(string userId, string teamId)
    {
        allCards = await _loadTeamService.LoadPlayerTeamCardAsync(userId, teamId);
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
