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
    public int position_id;
    public int card_id;
    public double card_all_power;
    public System.Action OnDropEnd;
    public void OnDrop(PointerEventData eventData)
    {
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
            string position = "F" + position_id;
            Teams teams = new Teams();
            if (obj is CardHeroes cardHeroes)
            {
                double currentPower = teams.GetTeamsPower();
                if (card_id != 0)
                {
                    cardHeroes.UpdateTeamFactCardHeroes(null, null, card_id);
                    cardHeroes.UpdateTeamFactCardHeroes(draggedCard.team_id, position, cardHeroes.id);
                    if(cardHeroes.all_power >= card_all_power){
                        double newPower = cardHeroes.all_power - card_all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,1);
                    }else{
                        double newPower = card_all_power - cardHeroes.all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,0);
                    }
                }else{
                    cardHeroes.UpdateTeamFactCardHeroes(draggedCard.team_id, position, cardHeroes.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardHeroes.all_power,1);
                }
            }
            else if (obj is CardCaptains cardCaptains)
            {
                double currentPower = teams.GetTeamsPower();
                if (card_id != 0)
                {
                    cardCaptains.UpdateTeamFactCardCaptains(null, null, card_id);
                    cardCaptains.UpdateTeamFactCardCaptains(draggedCard.team_id, position, cardCaptains.id);
                    if(cardCaptains.all_power >= card_all_power){
                        double newPower = cardCaptains.all_power - card_all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,1);
                    }else{
                        double newPower = card_all_power - cardCaptains.all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,0);
                    }
                }else{
                    cardCaptains.UpdateTeamFactCardCaptains(draggedCard.team_id, position, cardCaptains.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardCaptains.all_power,1);
                }
            }
            else if (obj is CardColonels cardColonels)
            {
                double currentPower = teams.GetTeamsPower();
                if (card_id != 0)
                {
                    cardColonels.UpdateTeamFactCardColonels(null, null, card_id);
                    cardColonels.UpdateTeamFactCardColonels(draggedCard.team_id, position, cardColonels.id);
                    if(cardColonels.all_power >= card_all_power){
                        double newPower = cardColonels.all_power - card_all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,1);
                    }else{
                        double newPower = card_all_power - cardColonels.all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,0);
                    }
                }else{
                    cardColonels.UpdateTeamFactCardColonels(draggedCard.team_id, position, cardColonels.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardColonels.all_power,1);
                }
            }
            else if (obj is CardGenerals cardGenerals)
            {
                double currentPower = teams.GetTeamsPower();
                if (card_id != 0)
                {
                    cardGenerals.UpdateTeamFactCardGenerals(null, null, card_id);
                    cardGenerals.UpdateTeamFactCardGenerals(draggedCard.team_id, position, cardGenerals.id);
                    if(cardGenerals.all_power >= card_all_power){
                        double newPower = cardGenerals.all_power - card_all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,1);
                    }else{
                        double newPower = card_all_power - cardGenerals.all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,0);
                    }
                }else{
                    cardGenerals.UpdateTeamFactCardGenerals(draggedCard.team_id, position, cardGenerals.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardGenerals.all_power,1);
                }
            }
            else if (obj is CardAdmirals cardAdmirals)
            {
                double currentPower = teams.GetTeamsPower();
                if (card_id != 0)
                {
                    cardAdmirals.UpdateTeamFactCardAdmirals(null, null, card_id);
                    cardAdmirals.UpdateTeamFactCardAdmirals(draggedCard.team_id, position, cardAdmirals.id);
                    if(cardAdmirals.all_power >= card_all_power){
                        double newPower = cardAdmirals.all_power - card_all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,1);
                    }else{
                        double newPower = card_all_power - cardAdmirals.all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,0);
                    }
                }else{
                    cardAdmirals.UpdateTeamFactCardAdmirals(draggedCard.team_id, position, cardAdmirals.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardAdmirals.all_power,1);
                }
            }
            else if (obj is CardMonsters cardMonsters)
            {
                double currentPower = teams.GetTeamsPower();
                if (card_id != 0)
                {
                    cardMonsters.UpdateTeamFactCardMonsters(null, null, card_id);
                    cardMonsters.UpdateTeamFactCardMonsters(draggedCard.team_id, position, cardMonsters.id);
                    if(cardMonsters.all_power >= card_all_power){
                        double newPower = cardMonsters.all_power - card_all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,1);
                    }else{
                        double newPower = card_all_power - cardMonsters.all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,0);
                    }
                }else{
                    cardMonsters.UpdateTeamFactCardMonsters(draggedCard.team_id, position, cardMonsters.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardMonsters.all_power,1);
                }
            }
            else if (obj is CardMilitary cardMilitary)
            {
                double currentPower = teams.GetTeamsPower();
                if (card_id != 0)
                {
                    cardMilitary.UpdateTeamFactCardMilitary(null, null, card_id);
                    cardMilitary.UpdateTeamFactCardMilitary(draggedCard.team_id, position, cardMilitary.id);
                    if(cardMilitary.all_power >= card_all_power){
                        double newPower = cardMilitary.all_power - card_all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,1);
                    }else{
                        double newPower = card_all_power - cardMilitary.all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,0);
                    }
                }else{
                    cardMilitary.UpdateTeamFactCardMilitary(draggedCard.team_id, position, cardMilitary.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardMilitary.all_power,1);
                }
            }
            else if (obj is CardSpell cardSpell)
            {
                double currentPower = teams.GetTeamsPower();
                if (card_id != 0)
                {
                    cardSpell.UpdateTeamFactCardSpell(null, null, card_id);
                    cardSpell.UpdateTeamFactCardSpell(draggedCard.team_id, position, cardSpell.id);
                    if(cardSpell.all_power >= card_all_power){
                        double newPower = cardSpell.all_power - card_all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,1);
                    }else{
                        double newPower = card_all_power - cardSpell.all_power;
                        FindObjectOfType<Power>().ShowPower(currentPower, newPower,0);
                    }
                }else{
                    cardSpell.UpdateTeamFactCardSpell(draggedCard.team_id, position, cardSpell.id);
                    FindObjectOfType<Power>().ShowPower(currentPower, cardSpell.all_power,1);
                }
            }
        }
        OnDropEnd.Invoke();
    }

}
