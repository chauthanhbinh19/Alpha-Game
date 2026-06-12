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

        var cardHeroList = heroTask.Result;
        var cardCaptainList = captainTask.Result;
        var cardColonelList = colonelTask.Result;
        var cardGeneralList = generalTask.Result;
        var cardAdmiralList = admiralTask.Result;
        var cardMonsterList = monsterTask.Result;
        var cardMilitaryList = militaryTask.Result;
        var cardSpellList = spellTask.Result;

        List<CardBase> allCards = new List<CardBase>();

        foreach (var entity in cardHeroList)
        {
            CardHero card = new CardHero();
            card.Initialize(entity);
            allCards.Add(card);
        }

        foreach (var entity in cardCaptainList)
        {
            CardCaptain card = new CardCaptain();
            card.Initialize(entity);
            allCards.Add(card);
        }

        foreach (var entity in cardColonelList)
        {
            CardColonel card = new CardColonel();
            card.Initialize(entity);
            allCards.Add(card);
        }

        foreach (var entity in cardGeneralList)
        {
            CardGeneral card = new CardGeneral();
            card.Initialize(entity);
            allCards.Add(card);
        }

        foreach (var entity in cardAdmiralList)
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

        foreach (var entity in cardMonsterList)
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
