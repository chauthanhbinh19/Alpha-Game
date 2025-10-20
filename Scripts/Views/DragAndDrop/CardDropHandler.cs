using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;
using System.Collections.Generic;

public class CardDropHandler : MonoBehaviour, IDropHandler
{
    public GameObject teamsObject;
    public string position_id;
    public string card_id;
    public double card_power;
    public System.Action OnDropEnd;
    
    UserCardHeroesService userCardHeroesService;
    UserCardCaptainsService userCardCaptainsService;
    UserCardColonelsService userCardColonelsService;
    UserCardGeneralsService userCardGeneralsService;
    UserCardAdmiralsService userCardAdmiralsService;
    UserCardMonstersService userCardMonstersService;
    UserCardMilitaryService userCardMilitaryService;
    UserCardSpellService userCardSpellService;
    TeamsService teamsService;
    public void OnDrop(PointerEventData eventData)
    {
        userCardHeroesService = UserCardHeroesService.Create();
        userCardCaptainsService = UserCardCaptainsService.Create();
        userCardColonelsService = UserCardColonelsService.Create();
        userCardGeneralsService = UserCardGeneralsService.Create();
        userCardAdmiralsService = UserCardAdmiralsService.Create();
        userCardMonstersService = UserCardMonstersService.Create();
        userCardMilitaryService = UserCardMilitaryService.Create();
        userCardSpellService = UserCardSpellService.Create();
        teamsService = TeamsService.Create();

        CardDragHandler draggedCard = eventData.pointerDrag.GetComponent<CardDragHandler>();
        TextMeshProUGUI powerText = teamsObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        if (draggedCard != null)
        {
            // Kiểm tra xem liệu có texture được gán không
            RawImage positionImage = gameObject.transform.Find("Image").GetComponent<RawImage>();
            if (positionImage != null)
            {
                positionImage.texture = draggedCard.cardTexture;
                // Debug.Log("Card dropped: " + draggedCard.gameObject.name + " at position: " + gameObject.name);
            }
            object obj = draggedCard.obj;
            string position = draggedCard.mainPosition + "-" + position_id;
            Teams teams = new Teams();
            if (obj is CardHeroes cardHeroes)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardHeroesService.UpdateTeamCardHeroes(null, null, card_id);
                    userCardHeroesService.UpdateTeamCardHeroes(draggedCard.team_id, position, cardHeroes.Id);
                    if (cardHeroes.Power >= card_power)
                    {
                        double newPower = cardHeroes.Power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardHeroes.Power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardHeroesService.UpdateTeamCardHeroes(draggedCard.team_id, position, cardHeroes.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardHeroes.Power, 1);
                }
            }
            else if (obj is CardCaptains cardCaptains)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardCaptainsService.UpdateTeamCardCaptains(null, null, card_id);
                    userCardCaptainsService.UpdateTeamCardCaptains(draggedCard.team_id, position, cardCaptains.Id);
                    if (cardCaptains.Power >= card_power)
                    {
                        double newPower = cardCaptains.Power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardCaptains.Power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardCaptainsService.UpdateTeamCardCaptains(draggedCard.team_id, position, cardCaptains.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardCaptains.Power, 1);
                }
            }
            else if (obj is CardColonels cardColonels)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardColonelsService.UpdateTeamCardColonels(null, null, card_id);
                    userCardColonelsService.UpdateTeamCardColonels(draggedCard.team_id, position, cardColonels.Id);
                    if (cardColonels.Power >= card_power)
                    {
                        double newPower = cardColonels.Power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardColonels.Power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardColonelsService.UpdateTeamCardColonels(draggedCard.team_id, position, cardColonels.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardColonels.Power, 1);
                }
            }
            else if (obj is CardGenerals cardGenerals)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardGeneralsService.UpdateTeamCardGenerals(null, null, card_id);
                    userCardGeneralsService.UpdateTeamCardGenerals(draggedCard.team_id, position, cardGenerals.Id);
                    if (cardGenerals.Power >= card_power)
                    {
                        double newPower = cardGenerals.Power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardGenerals.Power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardGeneralsService.UpdateTeamCardGenerals(draggedCard.team_id, position, cardGenerals.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardGenerals.Power, 1);
                }
            }
            else if (obj is CardAdmirals cardAdmirals)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardAdmiralsService.UpdateTeamCardAdmirals(null, null, card_id);
                    userCardAdmiralsService.UpdateTeamCardAdmirals(draggedCard.team_id, position, cardAdmirals.Id);
                    if (cardAdmirals.Power >= card_power)
                    {
                        double newPower = cardAdmirals.Power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardAdmirals.Power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardAdmiralsService.UpdateTeamCardAdmirals(draggedCard.team_id, position, cardAdmirals.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardAdmirals.Power, 1);
                }
            }
            else if (obj is CardMonsters cardMonsters)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardMonstersService.UpdateTeamCardMonsters(null, null, card_id);
                    userCardMonstersService.UpdateTeamCardMonsters(draggedCard.team_id, position, cardMonsters.Id);
                    if (cardMonsters.Power >= card_power)
                    {
                        double newPower = cardMonsters.Power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardMonsters.Power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardMonstersService.UpdateTeamCardMonsters(draggedCard.team_id, position, cardMonsters.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardMonsters.Power, 1);
                }
            }
            else if (obj is CardMilitaries cardMilitary)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardMilitaryService.UpdateTeamCardMilitary(null, null, card_id);
                    userCardMilitaryService.UpdateTeamCardMilitary(draggedCard.team_id, position, cardMilitary.Id);
                    if (cardMilitary.Power >= card_power)
                    {
                        double newPower = cardMilitary.Power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardMilitary.Power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardMilitaryService.UpdateTeamCardMilitary(draggedCard.team_id, position, cardMilitary.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardMilitary.Power, 1);
                }
            }
            else if (obj is CardSpells cardSpell)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardSpellService.UpdateTeamCardSpell(null, null, card_id);
                    userCardSpellService.UpdateTeamCardSpell(draggedCard.team_id, position, cardSpell.Id);
                    if (cardSpell.Power >= card_power)
                    {
                        double newPower = cardSpell.Power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardSpell.Power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardSpellService.UpdateTeamCardSpell(draggedCard.team_id, position, cardSpell.Id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardSpell.Power, 1);
                }
            }
        }
        OnDropEnd.Invoke();
    }

}
