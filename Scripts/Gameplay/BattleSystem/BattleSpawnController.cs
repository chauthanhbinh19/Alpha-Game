using System.Collections.Generic;
using UnityEngine;

public class BattleSpawnController : MonoBehaviour
{
    [Header("References")]
    public GridManager gridManager;
    public GameObject cardVisualPrefab; // Kéo Prefab hiển thị của thẻ quân cờ vào đây

    private LoadTeams loadTeams = new LoadTeams();

    // Hệ thống Bản đồ Map: Key = Vị trí Slot (1-10) | Value = Ô cờ trên Grid tương ứng
    private Dictionary<int, GridCell> playerSlotToCellMap = new Dictionary<int, GridCell>();
    private Dictionary<int, GridCell> enemySlotToCellMap = new Dictionary<int, GridCell>();

    // Danh sách lưu trữ Runtime trong trận
    private List<CardBase> activePlayerHeroes = new List<CardBase>();
    private List<CardBase> backEndBenchCards = new List<CardBase>();

    async void Start()
    {
        if (gridManager == null)
        {
            Debug.LogError("Chưa kéo GridManager vào BattleSpawnController!");
            return;
        }

        // Bước 1: Ánh xạ 10 ô cờ của mỗi bên thành các Slot logic từ 1 -> 10
        MapSlotsToGridCells();

        // Bước 2: Load dữ liệu đội hình từ các Service bằng hàm Async
        string mockUserId = "639167826246347876";
        string mockTeamId = "1";

        Debug.Log("Đang tải dữ liệu đội hình từ Database...");
        TeamDeploymentResult teamData = await loadTeams.LoadAndSortTeamAsync(mockUserId, mockTeamId);

        // Lưu trữ lại danh sách Bench làm dữ liệu tạm trong bộ nhớ (Không tạo GameObject)
        backEndBenchCards = teamData.BenchCards;
        Debug.Log($"[Dữ liệu nền]: Đã nạp {backEndBenchCards.Count} thẻ vào trạng thái chờ đợi (SubIndex).");

        // Bước 3: Khởi tạo Visual cho các CardHero có MainPosition nằm trên sân
        DeployTeam(teamData.OnFieldCards, isPlayer: true);
    }

    public void MapSlotsToGridCells()
    {
        List<GridCell> pCells = gridManager.playerSpawnCells;
        List<GridCell> eCells = gridManager.enemySpawnCells;

        // Đảm bảo GridManager đã tìm đủ ô
        if (pCells.Count < 10 || eCells.Count < 10)
        {
            Debug.LogWarning("Số lượng ô Spawn khởi tạo trên bàn cờ đang nhỏ hơn 10!");
        }

        // Ánh xạ cho phe Ta (Player)
        for (int i = 0; i < pCells.Count; i++)
        {
            playerSlotToCellMap.Add(i + 1, pCells[i]); // Slot 1 -> 10
        }

        // Ánh xạ cho phe Địch (Enemy)
        for (int i = 0; i < eCells.Count; i++)
        {
            enemySlotToCellMap.Add(i + 1, eCells[i]); // Slot 1 -> 10
        }
    }

    public void DeployTeam(List<CardBase> cards, bool isPlayer)
    {
        // TỐI ƯU: Chọn đúng Dictionary map vị trí đã tạo ở Bước 1 dựa theo phe
        var slotMap = isPlayer ? playerSlotToCellMap : enemySlotToCellMap;

        foreach (CardBase hero in cards)
        {
            // TÌM Ô CỜ TRÙNG KHỚP: Tìm trực tiếp trong slotMap bằng MainPosition (Tốc độ O(1))
            if (slotMap.TryGetValue(hero.MainPosition, out GridCell targetCell))
            {
                // Kiểm tra xem ô này đã có ai đứng chưa để tránh chồng lên nhau
                if (targetCell.OccupiedCard != null)
                {
                    Debug.LogWarning($"Ô số {hero.MainPosition} đã bị chiếm bởi {targetCell.OccupiedCard.Name}. Không thể đặt {hero.Name} vào!");
                    continue;
                }

                // Tạo Visual và làm con trực tiếp của displayCardPanel trên ô cờ đó
                GameObject cardObj = Instantiate(cardVisualPrefab, targetCell.DisplayCardPanel);
                cardObj.transform.localPosition = Vector3.zero;
                cardObj.transform.localRotation = Quaternion.identity;

                cardObj.name = $"{(isPlayer ? "Player" : "Enemy")}_Pos_{hero.MainPosition}_{hero.Name}";

                CardVisual visualScript = cardObj.GetComponent<CardVisual>();
                if (visualScript != null)
                {
                    visualScript.SetupVisual(hero);
                }

                // Đồng bộ gán ngược tham chiếu dữ liệu vào ô cờ
                targetCell.OccupiedCard = hero;

                if (isPlayer) activePlayerHeroes.Add(hero);

                Debug.Log($"<color=cyan>[Khớp Vị Trí]</color> Đã đặt {hero.Name} vào ô cờ số {targetCell.MainPosition} thành công!");
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy ô cờ nào có MainPosition = {hero.MainPosition} tương thích với {hero.Name} trong hệ thống Map!");
            }
        }
    }
}