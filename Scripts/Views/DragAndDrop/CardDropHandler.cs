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
    public GameObject TeamsObject;
    public string PositionId;
    public string CardId;
    public double CardPower;
    public System.Action OnDropEnd;

    UserCardHeroesService UserCardHeroesService;
    UserCardCaptainsService UserCardCaptainsService;
    UserCardColonelsService UserCardColonelsService;
    UserCardGeneralsService UserCardGeneralsService;
    UserCardAdmiralsService UserCardAdmiralsService;
    UserCardMonstersService UserCardMonstersService;
    UserCardMilitariesService UserCardMilitaryService;
    UserCardSpellsService UserCardSpellService;
    TeamsService TeamsService;
    public void OnDrop(PointerEventData eventData)
    {
        // gọi async method nhưng không await
        _ = HandleDropAsync(eventData);
    }
    public async Task HandleDropAsync(PointerEventData eventData)
    {
        UserCardHeroesService = UserCardHeroesService.Create();
        UserCardCaptainsService = UserCardCaptainsService.Create();
        UserCardColonelsService = UserCardColonelsService.Create();
        UserCardGeneralsService = UserCardGeneralsService.Create();
        UserCardAdmiralsService = UserCardAdmiralsService.Create();
        UserCardMonstersService = UserCardMonstersService.Create();
        UserCardMilitaryService = UserCardMilitariesService.Create();
        UserCardSpellService = UserCardSpellsService.Create();
        TeamsService = TeamsService.Create();

        CardDragHandler draggedCard = eventData.pointerDrag.GetComponent<CardDragHandler>();
        TextMeshProUGUI powerText = TeamsObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
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
            string position = draggedCard.mainPosition + "-" + PositionId;
            double currentPower = User.CurrentUserPower;
            if (obj is Equipments cardHero)
            {
                if (!string.IsNullOrEmpty(CardId))
                {
                    await UserCardHeroesService.UpdateTeamCardHeroAsync(null, null, CardId);
                    await UserCardHeroesService.UpdateTeamCardHeroAsync(draggedCard.team_id, position, cardHero.Id);
                    if (cardHero.Power >= CardPower)
                    {
                        double diffPower = cardHero.Power - CardPower;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = CardPower - cardHero.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await UserCardHeroesService.UpdateTeamCardHeroAsync(draggedCard.team_id, position, cardHero.Id);
                    double updatedPower = currentPower + cardHero.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardHero.Power, 1);
                }
            }
            else if (obj is CardCaptains cardCaptain)
            {
                if (!string.IsNullOrEmpty(CardId))
                {
                    await UserCardCaptainsService.UpdateTeamCardCaptainAsync(null, null, CardId);
                    await UserCardCaptainsService.UpdateTeamCardCaptainAsync(draggedCard.team_id, position, cardCaptain.Id);
                    if (cardCaptain.Power >= CardPower)
                    {
                        double diffPower = cardCaptain.Power - CardPower;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = CardPower - cardCaptain.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await UserCardCaptainsService.UpdateTeamCardCaptainAsync(draggedCard.team_id, position, cardCaptain.Id);
                    double updatedPower = currentPower + cardCaptain.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardCaptain.Power, 1);
                }
            }
            else if (obj is CardColonels cardColonel)
            {
                if (!string.IsNullOrEmpty(CardId))
                {
                    await UserCardColonelsService.UpdateTeamCardColonelAsync(null, null, CardId);
                    await UserCardColonelsService.UpdateTeamCardColonelAsync(draggedCard.team_id, position, cardColonel.Id);
                    if (cardColonel.Power >= CardPower)
                    {
                        double diffPower = cardColonel.Power - CardPower;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = CardPower - cardColonel.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await UserCardColonelsService.UpdateTeamCardColonelAsync(draggedCard.team_id, position, cardColonel.Id);
                    double updatedPower = currentPower + cardColonel.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardColonel.Power, 1);
                }
            }
            else if (obj is CardGenerals cardGeneral)
            {
                if (!string.IsNullOrEmpty(CardId))
                {
                    await UserCardGeneralsService.UpdateTeamCardGeneralAsync(null, null, CardId);
                    await UserCardGeneralsService.UpdateTeamCardGeneralAsync(draggedCard.team_id, position, cardGeneral.Id);
                    if (cardGeneral.Power >= CardPower)
                    {
                        double diffPower = cardGeneral.Power - CardPower;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = CardPower - cardGeneral.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await UserCardGeneralsService.UpdateTeamCardGeneralAsync(draggedCard.team_id, position, cardGeneral.Id);
                    double updatedPower = currentPower + cardGeneral.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardGeneral.Power, 1);
                }
            }
            else if (obj is CardAdmirals cardAdmiral)
            {
                if (!string.IsNullOrEmpty(CardId))
                {
                    await UserCardAdmiralsService.UpdateTeamCardAdmiralAsync(null, null, CardId);
                    await UserCardAdmiralsService.UpdateTeamCardAdmiralAsync(draggedCard.team_id, position, cardAdmiral.Id);
                    if (cardAdmiral.Power >= CardPower)
                    {
                        double diffPower = cardAdmiral.Power - CardPower;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = CardPower - cardAdmiral.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await UserCardAdmiralsService.UpdateTeamCardAdmiralAsync(draggedCard.team_id, position, cardAdmiral.Id);
                    double updatedPower = currentPower + cardAdmiral.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardAdmiral.Power, 1);
                }
            }
            else if (obj is CardMonsters cardMonster)
            {
                if (!string.IsNullOrEmpty(CardId))
                {
                    await UserCardMonstersService.UpdateTeamCardMonsterAsync(null, null, CardId);
                    await UserCardMonstersService.UpdateTeamCardMonsterAsync(draggedCard.team_id, position, cardMonster.Id);
                    if (cardMonster.Power >= CardPower)
                    {
                        double diffPower = cardMonster.Power - CardPower;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = CardPower - cardMonster.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await UserCardMonstersService.UpdateTeamCardMonsterAsync(draggedCard.team_id, position, cardMonster.Id);
                    double updatedPower = currentPower + cardMonster.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardMonster.Power, 1);
                }
            }
            else if (obj is CardMilitaries cardMilitary)
            {
                if (!string.IsNullOrEmpty(CardId))
                {
                    await UserCardMilitaryService.UpdateTeamCardMilitaryAsync(null, null, CardId);
                    await UserCardMilitaryService.UpdateTeamCardMilitaryAsync(draggedCard.team_id, position, cardMilitary.Id);
                    if (cardMilitary.Power >= CardPower)
                    {
                        double diffPower = cardMilitary.Power - CardPower;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = CardPower - cardMilitary.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await UserCardMilitaryService.UpdateTeamCardMilitaryAsync(draggedCard.team_id, position, cardMilitary.Id);
                    double updatedPower = currentPower + cardMilitary.Power;
                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, cardMilitary.Power, 1);
                }
            }
            else if (obj is CardSpells cardSpell)
            {
                if (!string.IsNullOrEmpty(CardId))
                {
                    await UserCardSpellService.UpdateTeamCardSpellAsync(null, null, CardId);
                    await UserCardSpellService.UpdateTeamCardSpellAsync(draggedCard.team_id, position, cardSpell.Id);
                    if (cardSpell.Power >= CardPower)
                    {
                        double diffPower = cardSpell.Power - CardPower;
                        double updatedPower = currentPower + diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                    }
                    else
                    {
                        double diffPower = CardPower - cardSpell.Power;
                        double updatedPower = currentPower - diffPower;

                        await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                        User.CurrentUserPower = updatedPower;

                        FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                    }
                }
                else
                {
                    await UserCardSpellService.UpdateTeamCardSpellAsync(draggedCard.team_id, position, cardSpell.Id);
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
