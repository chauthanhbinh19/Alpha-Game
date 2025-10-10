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
                    userCardHeroesService.UpdateTeamCardHeroes(draggedCard.team_id, position, cardHeroes.id);
                    if (cardHeroes.power >= card_power)
                    {
                        double newPower = cardHeroes.power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardHeroes.power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardHeroesService.UpdateTeamCardHeroes(draggedCard.team_id, position, cardHeroes.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardHeroes.power, 1);
                }
            }
            else if (obj is CardCaptains cardCaptains)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardCaptainsService.UpdateTeamCardCaptains(null, null, card_id);
                    userCardCaptainsService.UpdateTeamCardCaptains(draggedCard.team_id, position, cardCaptains.id);
                    if (cardCaptains.power >= card_power)
                    {
                        double newPower = cardCaptains.power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardCaptains.power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardCaptainsService.UpdateTeamCardCaptains(draggedCard.team_id, position, cardCaptains.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardCaptains.power, 1);
                }
            }
            else if (obj is CardColonels cardColonels)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardColonelsService.UpdateTeamCardColonels(null, null, card_id);
                    userCardColonelsService.UpdateTeamCardColonels(draggedCard.team_id, position, cardColonels.id);
                    if (cardColonels.power >= card_power)
                    {
                        double newPower = cardColonels.power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardColonels.power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardColonelsService.UpdateTeamCardColonels(draggedCard.team_id, position, cardColonels.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardColonels.power, 1);
                }
            }
            else if (obj is CardGenerals cardGenerals)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardGeneralsService.UpdateTeamCardGenerals(null, null, card_id);
                    userCardGeneralsService.UpdateTeamCardGenerals(draggedCard.team_id, position, cardGenerals.id);
                    if (cardGenerals.power >= card_power)
                    {
                        double newPower = cardGenerals.power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardGenerals.power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardGeneralsService.UpdateTeamCardGenerals(draggedCard.team_id, position, cardGenerals.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardGenerals.power, 1);
                }
            }
            else if (obj is CardAdmirals cardAdmirals)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardAdmiralsService.UpdateTeamCardAdmirals(null, null, card_id);
                    userCardAdmiralsService.UpdateTeamCardAdmirals(draggedCard.team_id, position, cardAdmirals.id);
                    if (cardAdmirals.power >= card_power)
                    {
                        double newPower = cardAdmirals.power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardAdmirals.power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardAdmiralsService.UpdateTeamCardAdmirals(draggedCard.team_id, position, cardAdmirals.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardAdmirals.power, 1);
                }
            }
            else if (obj is CardMonsters cardMonsters)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardMonstersService.UpdateTeamCardMonsters(null, null, card_id);
                    userCardMonstersService.UpdateTeamCardMonsters(draggedCard.team_id, position, cardMonsters.id);
                    if (cardMonsters.power >= card_power)
                    {
                        double newPower = cardMonsters.power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardMonsters.power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardMonstersService.UpdateTeamCardMonsters(draggedCard.team_id, position, cardMonsters.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardMonsters.power, 1);
                }
            }
            else if (obj is CardMilitary cardMilitary)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardMilitaryService.UpdateTeamCardMilitary(null, null, card_id);
                    userCardMilitaryService.UpdateTeamCardMilitary(draggedCard.team_id, position, cardMilitary.id);
                    if (cardMilitary.power >= card_power)
                    {
                        double newPower = cardMilitary.power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardMilitary.power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardMilitaryService.UpdateTeamCardMilitary(draggedCard.team_id, position, cardMilitary.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardMilitary.power, 1);
                }
            }
            else if (obj is CardSpell cardSpell)
            {
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                if (!string.IsNullOrEmpty(card_id))
                {
                    userCardSpellService.UpdateTeamCardSpell(null, null, card_id);
                    userCardSpellService.UpdateTeamCardSpell(draggedCard.team_id, position, cardSpell.id);
                    if (cardSpell.power >= card_power)
                    {
                        double newPower = cardSpell.power - card_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                    }
                    else
                    {
                        double newPower = card_power - cardSpell.power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                    }
                }
                else
                {
                    userCardSpellService.UpdateTeamCardSpell(draggedCard.team_id, position, cardSpell.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardSpell.power, 1);
                }
            }
        }
        OnDropEnd.Invoke();
    }

}
