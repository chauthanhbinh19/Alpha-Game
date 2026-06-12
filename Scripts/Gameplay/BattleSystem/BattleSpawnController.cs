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
    private List<CardHero> activePlayerHeroes = new List<CardHero>();
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
        string mockUserId = "user_player_01";
        string mockTeamId = "team_main_setup";

        Debug.Log("Đang tải dữ liệu đội hình từ Database...");
        TeamDeploymentResult teamData = await loadTeams.LoadAndSortTeamAsync(mockUserId, mockTeamId);

        // Lưu trữ lại danh sách Bench làm dữ liệu tạm trong bộ nhớ (Không tạo GameObject)
        backEndBenchCards = teamData.BenchCards;
        Debug.Log($"[Dữ liệu nền]: Đã nạp {backEndBenchCards.Count} thẻ vào trạng thái chờ đợi (SubIndex).");

        // Bước 3: Khởi tạo Visual cho các CardHero có MainPosition nằm trên sân
        DeployTeam(teamData.OnFieldCards, isPlayer: true);
    }

    void MapSlotsToGridCells()
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

    void DeployTeam(List<CardHero> heroes, bool isPlayer)
    {
        var slotMap = isPlayer ? playerSlotToCellMap : enemySlotToCellMap;

        foreach (CardHero hero in heroes)
        {
            // Chuyển đổi thuộc tính dữ liệu MainPosition (string) sang SlotIndex (int)
            if (int.TryParse(hero.MainPosition, out int slotIndex))
            {
                // Tìm kiếm ô cờ tương ứng với số Slot quy định
                if (slotMap.TryGetValue(slotIndex, out GridCell targetCell))
                {
                    // Tạo Visual hiển thị trên không gian Unity3D
                    GameObject cardObj = Instantiate(cardVisualPrefab, targetCell.transform.position, Quaternion.identity);
                    cardObj.name = $"{(isPlayer ? "Player" : "Enemy")}_Slot_{slotIndex}_{hero.Name}";

                    CardVisual visualScript = cardObj.GetComponent<CardVisual>();
                    if (visualScript != null)
                    {
                        visualScript.SetupVisual(hero); // Truyền dữ liệu CardHero vào để lấy SubImage gán cho Plane
                    }
                    else
                    {
                        Debug.LogWarning($"Prefab {cardVisualPrefab.name} thiếu script CardVisual!");
                    }

                    // Đồng bộ gán tham chiếu vào ô cờ
                    targetCell.OccupiedCard = hero;

                    // Lưu vào list quản lý cục bộ của controller để dùng cho TurnManager sau này
                    if (isPlayer) activePlayerHeroes.Add(hero);

                    Debug.Log($"<color=green>[Spawn Thành Công]</color> {hero.Name} đặt tại Slot logic {slotIndex} (Tọa độ ô cờ: {targetCell.GridPos})");
                }
                else
                {
                    Debug.LogWarning($"Dữ liệu tướng {hero.Name} yêu cầu Slot {slotIndex}, nhưng hệ thống Map không có ô cờ tương ứng!");
                }
            }
        }
    }
}