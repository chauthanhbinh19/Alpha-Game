using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleSpawnController : MonoBehaviour
{
    [Header("References")]
    public GridManager GridManager;
    public GameObject CardVisualPrefab; // Kéo Prefab hiển thị của thẻ quân cờ vào đây

    private LoadTeams LoadTeams = new LoadTeams();

    // Hệ thống Bản đồ Map: Key = Vị trí Slot (1-10) | Value = Ô cờ trên Grid tương ứng
    private Dictionary<int, GridCell> AlphaSlotToCellMap = new Dictionary<int, GridCell>();
    private Dictionary<int, GridCell> OmegaSlotToCellMap = new Dictionary<int, GridCell>();

    // Danh sách lưu trữ Runtime trong trận theo hệ Alpha / Omega
    private List<CardBase> ActiveAlphaHeroes = new List<CardBase>();
    private List<CardBase> ActiveOmegaHeroes = new List<CardBase>();

    private List<CardBase> AlphaBenchCards = new List<CardBase>();
    private List<CardBase> OmegaBenchCards = new List<CardBase>();

    async void Start()
    {
        // if (gridManager == null)
        // {
        //     Debug.LogError("Chưa kéo GridManager vào BattleSpawnController!");
        //     return;
        // }

        // // Bước 1: Ánh xạ 10 ô cờ của mỗi bên thành các Slot logic từ 1 -> 10
        // MapSlotsToGridCells();

        // // Bước 2: Chuẩn bị thông tin nạp song song 2 đội hình Alpha và Omega từ DB
        // string alphaUserId = "639167826246347876"; 
        // string alphaTeamId = "1";

        // string omegaUserId = "639169852484092591"; // ID của đối thủ / AI
        // string omegaTeamId = "1";

        // Debug.Log("Đang tải dữ liệu đội hình Alpha và Omega từ Database...");
        
        // Task<TeamDeploymentResult> alphaLoadTask = loadTeams.LoadAndSortTeamAsync(alphaUserId, alphaTeamId);
        // Task<TeamDeploymentResult> omegaLoadTask = loadTeams.LoadAndSortTeamAsync(omegaUserId, omegaTeamId);

        // // Đợi cả 2 phe cùng nạp xong dữ liệu mạng
        // await Task.WhenAll(alphaLoadTask, omegaLoadTask);

        // TeamDeploymentResult alphaData = alphaLoadTask.Result;
        // TeamDeploymentResult omegaData = omegaLoadTask.Result;

        // // Lưu trữ lại danh sách Bench làm dữ liệu tạm trong bộ nhớ
        // alphaBenchCards = alphaData.BenchCards;
        // omegaBenchCards = omegaData.BenchCards;
        // Debug.Log($"[Dữ liệu nền]: Đã nạp {alphaBenchCards.Count} thẻ chờ Alpha và {omegaBenchCards.Count} thẻ chờ Omega.");

        // // Bước 3: Khởi tạo Visual cho các CardHero có vị trí nằm trên sân
        // // Deploy phe Ta (Alpha)
        // DeployTeam(alphaData.OnFieldCards, isAlpha: true);

        // // Deploy phe Địch (Omega)
        // DeployTeam(omegaData.OnFieldCards, isAlpha: false);
    }

    public void MapSlotsToGridCells()
    {
        // Vẫn lấy từ GridManager (Nếu trong GridManager bạn cũng muốn đổi tên list, hãy đổi tương ứng nhé)
        List<GridCell> alphaCells = GridManager.PlayerSpawnCells; 
        List<GridCell> omegaCells = GridManager.EnemySpawnCells;

        // Đảm bảo GridManager đã tìm đủ ô
        if (alphaCells.Count < 10 || omegaCells.Count < 10)
        {
            Debug.LogWarning("Số lượng ô Spawn khởi tạo trên bàn cờ đang nhỏ hơn 10!");
        }

        // Ánh xạ cho phe Alpha
        for (int i = 0; i < alphaCells.Count; i++)
        {
            AlphaSlotToCellMap.Add(i + 1, alphaCells[i]); // Slot 1 -> 10
        }

        // Ánh xạ cho phe Omega
        for (int i = 0; i < omegaCells.Count; i++)
        {
            OmegaSlotToCellMap.Add(i + 1, omegaCells[i]); // Slot 1 -> 10
        }
    }

    public void DeployTeam(List<CardBase> cards, bool isAlpha)
    {
        // Chọn đúng Dictionary map vị trí dựa theo phe Alpha / Omega
        var slotMap = isAlpha ? AlphaSlotToCellMap : OmegaSlotToCellMap;

        foreach (CardBase hero in cards)
        {
            // TÌM Ô CỜ TRÙNG KHỚP: Tìm trực tiếp trong slotMap bằng MainPosition (Tốc độ O(1))
            if (slotMap.TryGetValue(hero.MainPosition, out GridCell targetCell))
            {
                // Kiểm tra xem ô này đã có ai đứng chưa để tránh chồng lên nhau
                if (targetCell.OccupiedCard != null)
                {
                    // Debug.LogWarning($"Ô số {hero.MainPosition} của bên {(isAlpha ? "Alpha" : "Omega")} đã bị chiếm bởi {targetCell.occupiedCard.Name}. Không thể đặt {hero.Name} vào!");
                    continue;
                }

                // Tạo Visual và làm con trực tiếp của displayCardPanel trên ô cờ đó
                GameObject cardObj = Instantiate(CardVisualPrefab, targetCell.DisplayCardPanel);

                cardObj.name = $"{(isAlpha ? "Alpha" : "Omega")}_Pos_{hero.MainPosition}_{hero.Name}";

                CardVisual visualScript = cardObj.GetComponent<CardVisual>();
                if (visualScript != null)
                {
                    visualScript.SetupVisual(hero);
                }

                // Đồng bộ gán ngược tham chiếu dữ liệu vào ô cờ
                targetCell.OccupiedCard = hero;

                // Lưu vào mảng hoạt động tương ứng của từng hệ
                if (isAlpha)
                {
                    ActiveAlphaHeroes.Add(hero);
                }
                else
                {
                    ActiveOmegaHeroes.Add(hero);
                }

                // Debug.Log($"<color=cyan>[Spawn]</color> Đã đặt {hero.Name} vào ô cờ số {targetCell.MainPosition} của phe {(isAlpha ? "Alpha" : "Omega")} thành công!");
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy ô cờ nào có MainPosition = {hero.MainPosition} tương thích với {hero.Name} trong hệ thống Map!");
            }
        }
    }
}