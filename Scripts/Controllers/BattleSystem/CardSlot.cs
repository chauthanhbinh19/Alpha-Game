using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lớp này gắn vào từng GameObject Platform trên sân
public class CardSlot : MonoBehaviour
{
    // Thuộc tính để lưu trữ thẻ bài hiện đang chiếm vị trí này (Có thể là null)
    public CardBase currentCard { get; private set; } 
    
    // Vị trí logic (sẽ được gán thủ công hoặc lúc khởi tạo)
    public int slotIndex; 

    // Hàm đặt thẻ bài vào vị trí
    public void PlaceCard(CardBase card)
    {
        // Gắn đối tượng logic
        currentCard = card;
        
        // Cập nhật vị trí 3D của thẻ bài (nếu thẻ bài là một GameObject)
        if (card.gameObject != null)
        {
            card.gameObject.transform.SetParent(this.transform);
            card.gameObject.transform.localPosition = Vector3.zero; // Đặt về gốc của slot
        }
    }
    
    // Hàm xóa thẻ bài khỏi vị trí
    public CardBase RemoveCard()
    {
        CardBase removedCard = currentCard;
        currentCard = null; // Đặt lại về null (trống)
        return removedCard;
    }
}
