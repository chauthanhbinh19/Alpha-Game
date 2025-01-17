using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardDropHandler : MonoBehaviour, IDropHandler
{
    public GameObject teamsObject;
    public int position_id;
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
            Debug.Log(position_id);
            object obj = draggedCard.obj;
            string position="F"+position_id;
            if(obj is CardHeroes cardHeroes){
                cardHeroes.UpdateTeamFactCardHeroes(draggedCard.team_id, position,cardHeroes.id);
            }else if(obj is CardCaptains cardCaptains){
                cardCaptains.UpdateTeamFactCardCaptains(draggedCard.team_id, position,cardCaptains.id);
            }else if(obj is CardColonels cardColonels){
                cardColonels.UpdateTeamFactCardColonels(draggedCard.team_id, position,cardColonels.id);
            }else if(obj is CardGenerals cardGenerals){
                cardGenerals.UpdateTeamFactCardGenerals(draggedCard.team_id, position,cardGenerals.id);
            }else if(obj is CardAdmirals cardAdmirals){
                cardAdmirals.UpdateTeamFactCardAdmirals(draggedCard.team_id, position,cardAdmirals.id);
            }else if(obj is CardMonsters cardMonsters){
                cardMonsters.UpdateTeamFactCardMonsters(draggedCard.team_id, position,cardMonsters.id);
            }else if(obj is CardMilitary cardMilitary){
                cardMilitary.UpdateTeamFactCardMilitary(draggedCard.team_id, position,cardMilitary.id);
            }else if(obj is CardSpell cardSpell){
                
            }
        }
        else
        {
            Debug.LogWarning("No CardDragHandler found on the dropped object.");
        }
    }


}
