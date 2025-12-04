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
            if (obj is CardHeroes cardHeroes)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardHeroesService.UpdateTeamCardHeroAsync(null, null, card_id);
                    await userCardHeroesService.UpdateTeamCardHeroAsync(draggedCard.team_id, position, cardHeroes.Id);
                    if (cardHeroes.Power >= card_power)
                    {
                        double diffPower = cardHeroes.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardHeroes.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardHeroesService.UpdateTeamCardHeroAsync(draggedCard.team_id, position, cardHeroes.Id);
                    double updatedPower = currentPower + cardHeroes.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardHeroes.Power, 1);
                }
            }
            else if (obj is CardCaptains cardCaptains)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardCaptainsService.UpdateTeamCardCaptainAsync(null, null, card_id);
                    await userCardCaptainsService.UpdateTeamCardCaptainAsync(draggedCard.team_id, position, cardCaptains.Id);
                    if (cardCaptains.Power >= card_power)
                    {
                        double diffPower = cardCaptains.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardCaptains.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardCaptainsService.UpdateTeamCardCaptainAsync(draggedCard.team_id, position, cardCaptains.Id);
                    double updatedPower = currentPower + cardCaptains.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardCaptains.Power, 1);
                }
            }
            else if (obj is CardColonels cardColonels)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardColonelsService.UpdateTeamCardColonelAsync(null, null, card_id);
                    await userCardColonelsService.UpdateTeamCardColonelAsync(draggedCard.team_id, position, cardColonels.Id);
                    if (cardColonels.Power >= card_power)
                    {
                        double diffPower = cardColonels.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardColonels.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardColonelsService.UpdateTeamCardColonelAsync(draggedCard.team_id, position, cardColonels.Id);
                    double updatedPower = currentPower + cardColonels.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardColonels.Power, 1);
                }
            }
            else if (obj is CardGenerals cardGenerals)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardGeneralsService.UpdateTeamCardGeneralAsync(null, null, card_id);
                    await userCardGeneralsService.UpdateTeamCardGeneralAsync(draggedCard.team_id, position, cardGenerals.Id);
                    if (cardGenerals.Power >= card_power)
                    {
                        double diffPower = cardGenerals.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardGenerals.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardGeneralsService.UpdateTeamCardGeneralAsync(draggedCard.team_id, position, cardGenerals.Id);
                    double updatedPower = currentPower + cardGenerals.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardGenerals.Power, 1);
                }
            }
            else if (obj is CardAdmirals cardAdmirals)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(null, null, card_id);
                    await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(draggedCard.team_id, position, cardAdmirals.Id);
                    if (cardAdmirals.Power >= card_power)
                    {
                        double diffPower = cardAdmirals.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardAdmirals.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(draggedCard.team_id, position, cardAdmirals.Id);
                    double updatedPower = currentPower + cardAdmirals.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardAdmirals.Power, 1);
                }
            }
            else if (obj is CardMonsters cardMonsters)
            {
                if (!string.IsNullOrEmpty(card_id))
                {
                    await userCardMonstersService.UpdateTeamCardMonsterAsync(null, null, card_id);
                    await userCardMonstersService.UpdateTeamCardMonsterAsync(draggedCard.team_id, position, cardMonsters.Id);
                    if (cardMonsters.Power >= card_power)
                    {
                        double diffPower = cardMonsters.Power - card_power;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = card_power - cardMonsters.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await userCardMonstersService.UpdateTeamCardMonsterAsync(draggedCard.team_id, position, cardMonsters.Id);
                    double updatedPower = currentPower + cardMonsters.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardMonsters.Power, 1);
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
