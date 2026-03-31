using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class LoadTeams
{
    public async Task<List<CardBase>> LoadPlayerTeamCardAsync(string userId, string teamId)
    {
        var heroTask = UserCardHeroesService.Create().GetUserCardHeroesTeamWithoutPositionAsync(userId, teamId);
        var captainTask = UserCardCaptainsService.Create().GetUserCardCaptainsTeamWithoutPositionAsync(userId, teamId);
        var colonelTask = UserCardColonelsService.Create().GetUserCardColonelsTeamWithoutPositionAsync(userId, teamId);
        var generalTask = UserCardGeneralsService.Create().GetUserCardGeneralsTeamWithoutPositionAsync(userId, teamId);
        var admiralTask = UserCardAdmiralsService.Create().GetUserCardAdmiralsTeamWithoutPositionAsync(userId, teamId);
        var monsterTask = UserCardMonstersService.Create().GetUserCardMonstersTeamWithoutPositionAsync(userId, teamId);
        var militaryTask = UserCardMilitariesService.Create().GetUserCardMilitariesTeamWithoutPositionAsync(userId, teamId);
        var spellTask = UserCardSpellsService.Create().GetUserCardSpellsTeamWithoutPositionAsync(userId, teamId);

        await Task.WhenAll(heroTask, captainTask, colonelTask, generalTask, admiralTask, monsterTask, militaryTask, spellTask);

        var cardHeroesList = heroTask.Result;
        var cardCaptainsList = captainTask.Result;
        var cardColonelsList = colonelTask.Result;
        var cardGeneralsList = generalTask.Result;
        var cardAdmiralsList = admiralTask.Result;
        var cardMonstersList = monsterTask.Result;
        var cardMilitaryList = militaryTask.Result;
        var cardSpellList = spellTask.Result;

        List<CardBase> allCards = new List<CardBase>();

        foreach (var entity in cardHeroesList)
        {
            CardHero card = new CardHero();
            card.Initialize(entity);
            allCards.Add(card);
        }

        foreach (var entity in cardCaptainsList)
        {
            CardCaptain card = new CardCaptain();
            card.Initialize(entity);
            allCards.Add(card);
        }

        foreach (var entity in cardColonelsList)
        {
            CardColonel card = new CardColonel();
            card.Initialize(entity);
            allCards.Add(card);
        }

        foreach (var entity in cardGeneralsList)
        {
            CardGeneral card = new CardGeneral();
            card.Initialize(entity);
            allCards.Add(card);
        }

        foreach (var entity in cardAdmiralsList)
        {
            CardAdmiral card = new CardAdmiral();
            card.Initialize(entity);
            allCards.Add(card);
        }

        foreach (var entity in cardMilitaryList)
        {
            CardMilitary card = new CardMilitary();
            card.Initialize(entity);
            allCards.Add(card);
        }

        foreach (var entity in cardMonstersList)
        {
            CardMonster card = new CardMonster();
            card.Initialize(entity);
            allCards.Add(card);
        }

        foreach (var entity in cardSpellList)
        {
            CardSpell card = new CardSpell();
            card.Initialize(entity);
            allCards.Add(card);
        }
        return allCards;
    }
    // Hàm hỗ trợ: Chuyển đổi CardPenalty List sang Dictionary để tra cứu nhanh O(1)

}
