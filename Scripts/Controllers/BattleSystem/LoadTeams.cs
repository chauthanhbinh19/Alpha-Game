using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadTeams
{
    public List<CardBase> LoadPlayerTeamCard(string userId, string teamId)
    {
        var cardHeroesList = UserCardHeroesService.Create().GetUserCardHeroesTeamWithoutPosition(userId, teamId);
        var cardCaptainsList = UserCardCaptainsService.Create().GetUserCardCaptainsTeamWithoutPosition(userId, teamId);
        var cardColonelsList = UserCardColonelsService.Create().GetUserCardColonelsTeamWithoutPosition(userId, teamId);
        var cardGeneralsList = UserCardGeneralsService.Create().GetUserCardGeneralsTeamWithoutPosition(userId, teamId);
        var cardAdmiralsList = UserCardAdmiralsService.Create().GetUserCardAdmiralsTeamWithoutPosition(userId, teamId);
        var cardMonstersList = UserCardMonstersService.Create().GetUserCardMonstersTeamWithoutPosition(userId, teamId);
        var cardMilitaryList = UserCardMilitaryService.Create().GetUserCardMilitaryTeamWithoutPosition(userId, teamId);
        var cardSpellList = UserCardSpellService.Create().GetUserCardSpellTeamWithoutPosition(userId, teamId);

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
