using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    // Quản lý 10 vị trí trên sân, mỗi vị trí có một hàng đợi tối đa 10 thẻ
    private Dictionary<int, Queue<CardBase>> positionQueues = new Dictionary<int, Queue<CardBase>>();
    
    // Lưu tham chiếu đến các CardController đang hiển thị trên sân để dễ truy xuất
    private Dictionary<int, CardController> activeCardsOnField = new Dictionary<int, CardController>();

    private void Awake() => Instance = this;

    public void SetupBattle(List<CardBase> allCards)
    {
        // Giả sử bạn truyền vào 100 card đã load từ LoadPlayerTeamCardAsync
        // Chúng ta chia 100 card vào 10 vị trí (mỗi vị trí 10 card)
        for (int i = 0; i < 10; i++)
        {
            positionQueues[i] = new Queue<CardBase>();
            for (int j = 0; j < 10; j++)
            {
                int index = i * 10 + j;
                if (index < allCards.Count)
                    positionQueues[i].Enqueue(allCards[index]);
            }
        }

        // Sau khi chia xong, bắt đầu gọi 10 card đầu tiên ra sân
        for (int i = 0; i < 10; i++)
        {
            SpawnNextCardAtPosition(i);
        }
    }

    public void SpawnNextCardAtPosition(int positionIndex)
    {
        if (positionQueues[positionIndex].Count > 0)
        {
            CardBase nextData = positionQueues[positionIndex].Dequeue();
            
            // Gọi ArenaManager để hiển thị lên Grid 7x7
            // ArenaManager.Instance.PlaceCardOnGrid(nextData, positionIndex);
        }
        else
        {
            Debug.Log($"Vị trí {positionIndex} đã hết thẻ dự phòng!");
            activeCardsOnField.Remove(positionIndex);
        }
    }
}

