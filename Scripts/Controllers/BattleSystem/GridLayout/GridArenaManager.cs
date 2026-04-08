using UnityEngine;
using System.Collections.Generic;

public class GridArenaManager : MonoBehaviour
{
    public static GridArenaManager Instance;

    [Header("Grid Settings")]
    public GridCell[,] AllCells = new GridCell[7, 7];
    public List<GridCell> PlayableCells = new List<GridCell>(); // Danh sách 10 ô được chọn để đặt card

    [Header("Prefabs")]
    public GameObject CardPrefab;

    // Hàng đợi logic: 10 vị trí, mỗi vị trí 10 thẻ bài
    private Dictionary<int, Queue<CardBase>> positionQueues = new Dictionary<int, Queue<CardBase>>();

    private void Awake() => Instance = this;

    // Khởi tạo trận đấu từ list 100 card đã load
    public void InitializeBattle(List<CardBase> allLoadedCards)
    {
        // 1. Chia 100 card vào 10 hàng đợi (mỗi hàng 10 card)
        for (int i = 0; i < 10; i++)
        {
            positionQueues[i] = new Queue<CardBase>();
            for (int j = 0; j < 10; j++)
            {
                int index = i * 10 + j;
                if (index < allLoadedCards.Count)
                    positionQueues[i].Enqueue(allLoadedCards[index]);
            }
        }

        // 2. Đưa 10 card đầu tiên ra 10 ô PlayableCells đã chỉ định
        for (int i = 0; i < PlayableCells.Count; i++)
        {
            if (i < 10) // Giới hạn 10 vị trí hiển thị
            {
                SpawnNextCardAtCell(i);
            }
        }
    }

    public void SpawnNextCardAtCell(int queueIndex)
    {
        if (positionQueues[queueIndex].Count > 0)
        {
            CardBase nextData = positionQueues[queueIndex].Dequeue();
            GridCell targetCell = PlayableCells[queueIndex];

            // Tạo Object 3D
            GameObject cardObj = Instantiate(CardPrefab, targetCell.transform.position, Quaternion.identity);
            CardController controller = cardObj.GetComponent<CardController>();

            // Gán dữ liệu và vị trí hàng đợi cho controller
            controller.Setup(nextData, queueIndex, targetCell);
            
            // Cập nhật trạng thái ô Grid
            targetCell.SetType(CellType.Player); 
        }
        else
        {
            Debug.Log($"Hàng đợi tại vị trí {queueIndex} đã hết thẻ!");
            PlayableCells[queueIndex].SetType(CellType.Empty);
        }
    }
}