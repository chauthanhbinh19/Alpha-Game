using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class PlayerController
{
    private List<CardModel> allCards;
    private LoadTeams _loadTeamService;

    public PlayerController()
    {
        allCards = new List<CardModel>();
        _loadTeamService = new LoadTeams();
    }

    public async Task GetPlayerCardAsync(string userId, string teamId)
    {
        var loadedCards = await _loadTeamService.LoadPlayerTeamCardAsync(userId, teamId);
        allCards = loadedCards.Select(CardModel.CreateFrom).ToList();
    }

    public void Attack(PlayerController opponent)
    {
        foreach (var card in allCards)
        {
            if (card == null || !card.IsAlive)
            {
                continue;
            }

            card.PerformAction(opponent);
        }
    }

    public bool IsFieldEmpty()
    {
        return allCards.Count == 0;
    }

    public List<CardModel> GetCards()
    {
        return allCards;
    }
}
