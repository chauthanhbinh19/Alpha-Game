// using System.Collections.Generic;
// using UnityEngine;

// public class ArenaManager : MonoBehaviour
// {
//     public static ArenaManager Instance;
    
//     // Danh sách thẻ bài còn dư sau khi đã LoadPlayerTeamCardAsync
//     private List<CardBase> waitingQueue = new List<CardBase>();
    
//     // Các vị trí Transform trên Arena
//     public Transform[] slotPositions; 

//     private void Awake() => Instance = this;

//     public void InitializeArena(List<CardBase> allLoadedCards)
//     {
//         // Giả sử sân có 4 chỗ, lấy 4 thẻ đầu tiên ra sân
//         for (int i = 0; i < slotPositions.Length; i++)
//         {
//             if (allLoadedCards.Count > 0)
//             {
//                 SpawnCard(allLoadedCards[0], i);
//                 allLoadedCards.RemoveAt(0);
//             }
//         }
//         // Các thẻ còn lại cho vào hàng chờ
//         waitingQueue = allLoadedCards;
//     }

//     public void SpawnCard(CardBase data, int slotIndex)
//     {
//         GameObject cardObj = Instantiate(cardPrefab, slotPositions[slotIndex].position, Quaternion.identity);
//         var controller = cardObj.GetComponent<CardController>();
//         controller.Setup(data, slotIndex);
//     }

//     public void HandleCardDeath(CardController deadCard)
//     {
//         int pos = deadCard.CurrentPositionIndex;

//         if (waitingQueue.Count > 0)
//         {
//             // Lấy thẻ tiếp theo trong hàng chờ
//             CardBase nextCard = waitingQueue[0];
//             waitingQueue.RemoveAt(0);

//             // Sinh ra tại đúng vị trí cũ
//             SpawnCard(nextCard, pos);
//             Debug.Log($"Thẻ tại vị trí {pos} đã chết, thay bằng thẻ mới.");
//         }
//         else
//         {
//             Debug.Log("Hết thẻ dự phòng cho vị trí này.");
//         }
//     }
// }