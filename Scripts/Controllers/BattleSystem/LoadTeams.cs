using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadTeams
{
    public List<CardBase> LoadPlayerTeamCard(string team_id)
    {
        var cardHeroesList = UserCardHeroesService.Create().GetUserCardHeroesTeamWithoutPosition(User.CurrentUserId, team_id);
        var cardCaptainsList = UserCardCaptainsService.Create().GetUserCardCaptainsTeamWithoutPosition(User.CurrentUserId, team_id);
        var cardColonelsList = UserCardColonelsService.Create().GetUserCardColonelsTeamWithoutPosition(User.CurrentUserId, team_id);
        var cardGeneralsList = UserCardGeneralsService.Create().GetUserCardGeneralsTeamWithoutPosition(User.CurrentUserId, team_id);
        var cardAdmiralsList = UserCardAdmiralsService.Create().GetUserCardAdmiralsTeamWithoutPosition(User.CurrentUserId, team_id);
        var cardMonstersList = UserCardMonstersService.Create().GetUserCardMonstersTeamWithoutPosition(User.CurrentUserId, team_id);
        var cardMilitaryList = UserCardMilitaryService.Create().GetUserCardMilitaryTeamWithoutPosition(User.CurrentUserId, team_id);
        var cardSpellList = UserCardSpellService.Create().GetUserCardSpellTeamWithoutPosition(User.CurrentUserId, team_id);

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
}
