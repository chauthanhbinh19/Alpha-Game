using UnityEngine;

public class CardController : MonoBehaviour 
{
    public CardBase CardData { get; private set; }
    public int CurrentPositionIndex { get; private set; }

    // Hàm này sẽ gọi khi bạn khởi tạo Card vào Arena
    public void Setup(CardBase data, int positionIndex)
    {
        this.CardData = data;
        this.CurrentPositionIndex = positionIndex;

        // Cập nhật UI hoặc Model 3D dựa trên CardData
        // Ví dụ: UpdateHealthBar(data.HP);
        // LoadModel(data.ModelPath);
    }

    // public void OnCardDie()
    // {
    //     // Gọi lên ArenaManager để yêu cầu thay thế
    //     ArenaManager.Instance.HandleCardDeath(this);
    //     Destroy(gameObject); 
    // }
}