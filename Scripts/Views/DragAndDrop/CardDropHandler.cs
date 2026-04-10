using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    UserCardMilitariesService userCardMilitaryService;
    UserCardSpellsService userCardSpellService;
    TeamsService teamsService;
    public void OnDrop(PointerEventData eventData)
    {
        // gọi async method nhưng không await
        _ = HandleDropAsync(eventData);
    }
    public async Task HandleDropAsync(PointerEventData eventData)
    {
        userCardHeroesService = UserCardHeroesService.Create();
        userCardCaptainsService = UserCardCaptainsService.Create();
        userCardColonelsService = UserCardColonelsService.Create();
        userCardGeneralsService = UserCardGeneralsService.Create();
        userCardAdmiralsService = UserCardAdmiralsService.Create();
        userCardMonstersService = UserCardMonstersService.Create();
        userCardMilitaryService = UserCardMilitariesService.Create();
        userCardSpellService = UserCardSpellsService.Create();
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
            double currentPower = User.CurrentUserPower;
            if (obj is CardHeroes cardHero)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardHeroesService.UpdateTeamCardHeroAsync(null, null, card_id);
                    await userCardHeroesService.UpdateTeamCardHeroAsync(draggedCard.team_id, position, cardHero.Id);
                    if (cardHero.Power >= card_power)
                    {
                        double diffPower = cardHero.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardHero.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardHeroesService.UpdateTeamCardHeroAsync(draggedCard.team_id, position, cardHero.Id);
                    double updatedPower = currentPower + cardHero.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardHero.Power, 1);
                }
            }
            else if (obj is CardCaptains cardCaptain)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardCaptainsService.UpdateTeamCardCaptainAsync(null, null, card_id);
                    await userCardCaptainsService.UpdateTeamCardCaptainAsync(draggedCard.team_id, position, cardCaptain.Id);
                    if (cardCaptain.Power >= card_power)
                    {
                        double diffPower = cardCaptain.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardCaptain.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardCaptainsService.UpdateTeamCardCaptainAsync(draggedCard.team_id, position, cardCaptain.Id);
                    double updatedPower = currentPower + cardCaptain.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardCaptain.Power, 1);
                }
            }
            else if (obj is CardColonels cardColonel)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardColonelsService.UpdateTeamCardColonelAsync(null, null, card_id);
                    await userCardColonelsService.UpdateTeamCardColonelAsync(draggedCard.team_id, position, cardColonel.Id);
                    if (cardColonel.Power >= card_power)
                    {
                        double diffPower = cardColonel.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardColonel.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardColonelsService.UpdateTeamCardColonelAsync(draggedCard.team_id, position, cardColonel.Id);
                    double updatedPower = currentPower + cardColonel.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardColonel.Power, 1);
                }
            }
            else if (obj is CardGenerals cardGeneral)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardGeneralsService.UpdateTeamCardGeneralAsync(null, null, card_id);
                    await userCardGeneralsService.UpdateTeamCardGeneralAsync(draggedCard.team_id, position, cardGeneral.Id);
                    if (cardGeneral.Power >= card_power)
                    {
                        double diffPower = cardGeneral.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardGeneral.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardGeneralsService.UpdateTeamCardGeneralAsync(draggedCard.team_id, position, cardGeneral.Id);
                    double updatedPower = currentPower + cardGeneral.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardGeneral.Power, 1);
                }
            }
            else if (obj is CardAdmirals cardAdmiral)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(null, null, card_id);
                    await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(draggedCard.team_id, position, cardAdmiral.Id);
                    if (cardAdmiral.Power >= card_power)
                    {
                        double diffPower = cardAdmiral.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardAdmiral.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(draggedCard.team_id, position, cardAdmiral.Id);
                    double updatedPower = currentPower + cardAdmiral.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardAdmiral.Power, 1);
                }
            }
            else if (obj is CardMonsters cardMonster)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardMonstersService.UpdateTeamCardMonsterAsync(null, null, card_id);
                    await userCardMonstersService.UpdateTeamCardMonsterAsync(draggedCard.team_id, position, cardMonster.Id);
                    if (cardMonster.Power >= card_power)
                    {
                        double diffPower = cardMonster.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardMonster.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardMonstersService.UpdateTeamCardMonsterAsync(draggedCard.team_id, position, cardMonster.Id);
                    double updatedPower = currentPower + cardMonster.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardMonster.Power, 1);
                }
            }
            else if (obj is CardMilitaries cardMilitary)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardMilitaryService.UpdateTeamCardMilitaryAsync(null, null, card_id);
                    await userCardMilitaryService.UpdateTeamCardMilitaryAsync(draggedCard.team_id, position, cardMilitary.Id);
                    if (cardMilitary.Power >= card_power)
                    {
                        double diffPower = cardMilitary.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardMilitary.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardMilitaryService.UpdateTeamCardMilitaryAsync(draggedCard.team_id, position, cardMilitary.Id);
                    double updatedPower = currentPower + cardMilitary.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardMilitary.Power, 1);
                }
            }
            else if (obj is CardSpells cardSpell)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardSpellService.UpdateTeamCardSpellAsync(null, null, card_id);
                    await userCardSpellService.UpdateTeamCardSpellAsync(draggedCard.team_id, position, cardSpell.Id);
                    if (cardSpell.Power >= card_power)
                    {
                        double diffPower = cardSpell.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardSpell.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardSpellService.UpdateTeamCardSpellAsync(draggedCard.team_id, position, cardSpell.Id);
                    double updatedPower = currentPower + cardSpell.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardSpell.Power, 1);
                }
            }
        }
        OnDropEnd.Invoke();
    }

}
