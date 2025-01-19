using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardDropHandler : MonoBehaviour, IDropHandler
{
    public GameObject teamsObject;
    public int position_id;
    public int card_id;
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
            if (obj is CardHeroes cardHeroes)
            {
                if (card_id != 0)
                {
                    cardHeroes.UpdateTeamFactCardHeroes(null, null, card_id);
                    cardHeroes.UpdateTeamFactCardHeroes(draggedCard.team_id, position, cardHeroes.id);
                }
                cardHeroes.UpdateTeamFactCardHeroes(draggedCard.team_id, position, cardHeroes.id);
            }
            else if (obj is CardCaptains cardCaptains)
            {
                if (card_id != 0)
                {
                    cardCaptains.UpdateTeamFactCardCaptains(null, null, card_id);
                }
                cardCaptains.UpdateTeamFactCardCaptains(draggedCard.team_id, position, cardCaptains.id);
            }
            else if (obj is CardColonels cardColonels)
            {
                if (card_id != 0)
                {
                    cardColonels.UpdateTeamFactCardColonels(null, null, card_id);
                }
                cardColonels.UpdateTeamFactCardColonels(draggedCard.team_id, position, cardColonels.id);
            }
            else if (obj is CardGenerals cardGenerals)
            {
                if (card_id != 0)
                {
                    cardGenerals.UpdateTeamFactCardGenerals(null, null, card_id);
                }
                cardGenerals.UpdateTeamFactCardGenerals(draggedCard.team_id, position, cardGenerals.id);
            }
            else if (obj is CardAdmirals cardAdmirals)
            {
                if (card_id != 0)
                {
                    cardAdmirals.UpdateTeamFactCardAdmirals(null, null, card_id);
                }
                cardAdmirals.UpdateTeamFactCardAdmirals(draggedCard.team_id, position, cardAdmirals.id);
            }
            else if (obj is CardMonsters cardMonsters)
            {
                if (card_id != 0)
                {
                    cardMonsters.UpdateTeamFactCardMonsters(null, null, card_id);
                }
                cardMonsters.UpdateTeamFactCardMonsters(draggedCard.team_id, position, cardMonsters.id);
            }
            else if (obj is CardMilitary cardMilitary)
            {
                if (card_id != 0)
                {
                    cardMilitary.UpdateTeamFactCardMilitary(null, null, card_id);
                }
                cardMilitary.UpdateTeamFactCardMilitary(draggedCard.team_id, position, cardMilitary.id);
            }
            else if (obj is CardSpell cardSpell)
            {
                if (card_id != 0)
                {
                    cardSpell.UpdateTeamFactCardSpell(null, null, card_id);
                }
                cardSpell.UpdateTeamFactCardSpell(draggedCard.team_id, position, cardSpell.id);
            }
        }
        OnDropEnd.Invoke();
    }


}
