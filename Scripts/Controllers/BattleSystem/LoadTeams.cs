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
        var cardHeroesList = await UserCardHeroesService.Create().GetUserCardHeroesTeamWithoutPositionAsync(userId, teamId);
        var cardCaptainsList = await UserCardCaptainsService.Create().GetUserCardCaptainsTeamWithoutPositionAsync(userId, teamId);
        var cardColonelsList = await UserCardColonelsService.Create().GetUserCardColonelsTeamWithoutPositionAsync(userId, teamId);
        var cardGeneralsList = await UserCardGeneralsService.Create().GetUserCardGeneralsTeamWithoutPositionAsync(userId, teamId);
        var cardAdmiralsList = await UserCardAdmiralsService.Create().GetUserCardAdmiralsTeamWithoutPositionAsync(userId, teamId);
        var cardMonstersList = await UserCardMonstersService.Create().GetUserCardMonstersTeamWithoutPositionAsync(userId, teamId);
        var cardMilitaryList = await UserCardMilitariesService.Create().GetUserCardMilitariesTeamWithoutPositionAsync(userId, teamId);
        var cardSpellList = await UserCardSpellsService.Create().GetUserCardSpellsTeamWithoutPositionAsync(userId, teamId);

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
