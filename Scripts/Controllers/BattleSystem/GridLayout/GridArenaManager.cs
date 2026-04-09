using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GridArenaManager : MonoBehaviour
{
    public static GridArenaManager Instance { get; private set; }

    [Header("Settings")]
    public GameObject cardPrefab;

    // Danh sách tất cả các ô trong Arena
    private List<GridCell> allCells = new List<GridCell>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Thu thập tất cả GridCell có trong Scene
        allCells = FindObjectsOfType<GridCell>().ToList();
    }

    /// <summary>
    /// Tìm một ô trống gần nhất hoặc ô cụ thể dựa trên logic game
    /// </summary>
    public GridCell GetCellByMainPosition(int mainPos)
    {
        // Ưu tiên tìm ô đang hiển thị teamNumber này
        foreach (var cell in allCells)
        {
            if (cell.teamNumber == mainPos) return cell;
        }
        return null;
    }

    /// <summary>
    /// Hàm quan trọng: Sinh Card tiếp theo khi Card cũ chết
    /// </summary>
    /// <param name="mainPos">ID luồng vị trí</param>
    /// <param name="nextSubIndex">Thứ tự tiếp theo trong danh sách chờ</param>
    public void SpawnNextCardAtCell(int mainPos, int nextSubIndex)
    {
        // 1. Tìm ô mà Card cũ vừa chết (hoặc ô mặc định cho MainPos này)
        GridCell targetCell = GetCellByMainPosition(0); // Tìm ô trống bất kỳ hoặc theo logic của bạn

        // 2. Lấy dữ liệu từ Database (giả lập)
        CardBase nextData = GetCardDataFromDatabase(mainPos, nextSubIndex);

        if (nextData != null && targetCell != null)
        {
            GameObject cardObj = Instantiate(cardPrefab, targetCell.transform.position, Quaternion.identity);
            CardController controller = cardObj.GetComponent<CardController>();

            // Thiết lập Card mới
            controller.Setup(nextData, mainPos, nextSubIndex, targetCell);

            Debug.Log($"Spawned Hero: {nextData.Name} at Position: {mainPos} (Index: {nextSubIndex})");
        }
        else
        {
            Debug.LogWarning($"Không còn Card nào cho MainPosition: {mainPos} hoặc không tìm thấy ô trống!");
        }
    }

    /// <summary>
    /// Giả lập việc truy vấn Database 70,000 hero của bạn
    /// </summary>
    private CardBase GetCardDataFromDatabase(int mainPos, int subIndex)
    {
        // Trong thực tế, bạn sẽ dùng SQL hoặc tìm trong List<CardBase> 
        // dựa trên logic: MainPosition == team và SubIndex == thứ tự chờ.

        // Ví dụ: return MyDatabase.HeroList.Find(h => h.mainPos == mainPos && h.subIndex == subIndex);
        return null;
    }

    // ================= LOGIC ĐIỀU KHIỂN CHUỘT (CHỌN & DI CHUYỂN) =================

    private CardController selectedCard;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
    }

    private void HandleSelection()
    {
        // 1. Kiểm tra xem có đang trong Phase cho phép di chuyển không
        if (!(TurnManager.Instance.CurrentPhase is StartPhase ||
              TurnManager.Instance.CurrentPhase is PreparationPhase))
        {
            Debug.Log("Không thể di chuyển trong Phase này!");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GridCell clickedCell = hit.collider.GetComponentInParent<GridCell>();
            if (clickedCell != null)
            {
                if (selectedCard != null)
                {
                    // Thực hiện di chuyển nếu ô trống và trong phạm vi 8 hướng
                    if (selectedCard.CanMoveTo(clickedCell))
                    {
                        selectedCard.MoveTo(clickedCell);
                        selectedCard = null;
                    }
                }
                else if (clickedCell.CurrentCard != null)
                {
                    selectedCard = clickedCell.CurrentCard;
                }
            }
        }
    }
}