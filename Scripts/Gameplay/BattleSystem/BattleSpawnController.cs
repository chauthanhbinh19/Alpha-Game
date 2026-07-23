using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class BattleSpawnController : MonoBehaviour
{
    public static BattleSpawnController Instance { get; private set; }

    [Header("References")]
    public GridManager GridManager;
    public GameObject CardVisualPrefab; // Kéo Prefab hiển thị của thẻ quân cờ vào đây

    private LoadTeams LoadTeams = new LoadTeams();
    private string AlphaUserId = "639186151469501545";
    private string OmegaUserId = "639186151625575788";

    // Hệ thống Bản đồ Map: Key = Vị trí Slot (1-10) | Value = Ô cờ trên Grid tương ứng
    private Dictionary<int, GridCell> AlphaSlotToCellMap = new Dictionary<int, GridCell>();
    private Dictionary<int, GridCell> OmegaSlotToCellMap = new Dictionary<int, GridCell>();

    // Danh sách lưu trữ Runtime trong trận theo hệ Alpha / Omega
    private List<CardBase> ActiveAlphaHeroes = new List<CardBase>();
    private List<CardBase> ActiveOmegaHeroes = new List<CardBase>();

    private List<CardBase> AlphaBenchCards = new List<CardBase>();
    private List<CardBase> OmegaBenchCards = new List<CardBase>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    public void StartBattle(string alphaUserId = "639186151469501545", string omegaUserId = "639186151625575788")
    {
        AlphaUserId = alphaUserId;
        OmegaUserId = omegaUserId;

        if (LoadingManager.Instance != null)
        {
            LoadingManager.Instance.ShowLoading();
            LoadingManager.Instance.SetProgress(0f, "0%", "Preparing battlefield");
        }

        if (GridManager == null)
        {
            GridManager = FindObjectOfType<GridManager>();
        }

        if (GridManager == null)
        {
            Debug.LogWarning("[BattleSpawnController] GridManager chưa được tìm thấy, chờ khởi tạo tiếp theo.");
            return;
        }

        bool prepareScheduled = false;
        if (!GridManager.IsGridReady())
        {
            prepareScheduled = true;
            // Debug.Log("[BattleSpawnController] Grid chưa sẵn sàng, đang chờ khởi tạo trước khi chuẩn bị sân đấu...");
            GridManager.InitializeGridAsync(() =>
            {
                // After grid initialization, continue loading teams and deploying
                _ = ContinuePrepareAndDeployAsync();
            });
        }

        Transform RootPanel = UIManager.Instance.GetTransform("RootPanel");
        Transform WaitingPanel = UIManager.Instance.GetTransform("WaitingPanel");
        Transform MainPanel = UIManager.Instance.GetTransform("MainPanel");

        RootPanel.gameObject.SetActive(false);
        WaitingPanel.gameObject.SetActive(false);
        MainPanel.gameObject.SetActive(true);

        // If grid was not ready, ContinuePrepareAndDeployAsync will be invoked after initialization.
        if (!prepareScheduled && GridManager.IsGridReady())
        {
            // Grid is ready now: start loading and deploying
            _ = ContinuePrepareAndDeployAsync();
        }
    }

    private bool isBattlePrepared = false;

    private void PrepareBattleField()
    {
        if (GridManager == null)
        {
            return;
        }

        // Idempotent guard: avoid running prepare logic multiple times
        if (isBattlePrepared)
        {
            return;
        }

        isBattlePrepared = true;

        MapSlotsToGridCells();

        Debug.Log("[BattleSpawnController] Battle field ready.");
    }

    private async System.Threading.Tasks.Task ContinuePrepareAndDeployAsync()
    {
        if (LoadingManager.Instance != null)
        {
            LoadingManager.Instance.ShowLoading();
            LoadingManager.Instance.SetProgress(0f, "0%", "Loading battle data");
        }

        try
        {
            // Load teams from server (use AlphaUserId/OmegaUserId as userIds and default teamId = "1")
            string teamId = "1";
            System.Threading.Tasks.Task<TeamDeploymentResult> alphaTask = LoadTeams.LoadAndSortTeamAsync(AlphaUserId, teamId);
            // System.Threading.Tasks.Task<TeamDeploymentResult> omegaTask = LoadTeams.LoadAndSortTeamAsync(OmegaUserId, teamId);

            await System.Threading.Tasks.Task.WhenAll(alphaTask
            // , omegaTask
            );

            TeamDeploymentResult alphaData = alphaTask.Result;
            // TeamDeploymentResult omegaData = omegaTask.Result;

            // Debug.Log($"[BattleSpawnController] Loaded battle teams: Alpha on-field={alphaData?.OnFieldCards?.Count ?? 0}, Alpha bench={alphaData?.BenchCards?.Count ?? 0}, Omega on-field={omegaData?.OnFieldCards?.Count ?? 0}, Omega bench={omegaData?.BenchCards?.Count ?? 0}");
            // LogTeamDeploymentDetails(alphaData, "Alpha");
            // LogTeamDeploymentDetails(omegaData, "Omega");

            if (LoadingManager.Instance != null)
            {
                LoadingManager.Instance.SetProgress(0.5f, "50%", "Loading teams");
            }

            // Store bench lists
            AlphaBenchCards = alphaData.BenchCards;
            // OmegaBenchCards = omegaData.BenchCards;

            // Ensure grid-cell mapping exists
            MapSlotsToGridCells();

            // Deploy on-field cards visually
            if (alphaData.OnFieldCards != null && alphaData.OnFieldCards.Count > 0)
            {
                DeployTeam(alphaData.OnFieldCards, true);
            }
            else
            {
                Debug.LogWarning("[BattleSpawnController] Alpha team has no on-field cards to deploy.");
            }

            // if (omegaData.OnFieldCards != null && omegaData.OnFieldCards.Count > 0)
            // {
            //     DeployTeam(omegaData.OnFieldCards, false);
            // }
            // else
            // {
            //     Debug.LogWarning("[BattleSpawnController] Omega team has no on-field cards to deploy.");
            // }

            if (LoadingManager.Instance != null)
            {
                LoadingManager.Instance.SetProgress(1f, "100%", "Battle ready");
            }

            if (ActionMenuUI.Instance != null)
            {
                ActionMenuUI.Instance.ShowBattleTimeline();
            }

            // Debug.Log("[BattleSpawnController] Teams loaded and deployed to battlefield.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[BattleSpawnController] Error loading battle teams: {ex}");
        }
        finally
        {
            if (LoadingManager.Instance != null)
            {
                LoadingManager.Instance.HideLoading();
            }
        }
    }

    private void LogTeamDeploymentDetails(TeamDeploymentResult teamResult, string teamName)
    {
        if (teamResult == null)
        {
            Debug.LogWarning($"[BattleSpawnController] {teamName} team result is null.");
            return;
        }

        if (teamResult.OnFieldCards == null || teamResult.OnFieldCards.Count == 0)
        {
            Debug.LogWarning($"[BattleSpawnController] {teamName} has no on-field cards.");
            return;
        }

        foreach (var card in teamResult.OnFieldCards)
        {
            if (card == null) continue;
            Debug.Log($"[BattleSpawnController] {teamName} on-field card '{card.Name}' position='{card.Position}' mainPosition={card.MainPosition}");
        }
    }

    public void MapSlotsToGridCells()
    {
        if (GridManager == null)
        {
            return;
        }

        // Clear existing mappings to avoid duplicate key exceptions if this is called multiple times
        AlphaSlotToCellMap.Clear();
        OmegaSlotToCellMap.Clear();

        List<GridCell> alphaCells = GridManager.PlayerSpawnCells;
        List<GridCell> omegaCells = GridManager.EnemySpawnCells;

        // Đảm bảo GridManager đã tìm đủ ô
        if (alphaCells.Count < 10 || omegaCells.Count < 10)
        {
            Debug.LogWarning("Số lượng ô Spawn khởi tạo trên bàn cờ đang nhỏ hơn 10!");
        }

        // Ánh xạ cho phe Alpha bằng MainPosition thực tế của mỗi ô
        foreach (GridCell cell in alphaCells)
        {
            if (cell == null) continue;

            if (cell.MainPosition >= 1 && cell.MainPosition <= 10)
            {
                if (!AlphaSlotToCellMap.ContainsKey(cell.MainPosition))
                {
                    AlphaSlotToCellMap.Add(cell.MainPosition, cell);
                }
                else
                {
                    Debug.LogWarning($"[BattleSpawnController] Alpha spawn cell duplicate MainPosition {cell.MainPosition} detected.");
                }
            }
            else
            {
                Debug.LogWarning($"[BattleSpawnController] Alpha spawn cell has invalid MainPosition {cell.MainPosition}.");
            }
        }

        // Ánh xạ cho phe Omega bằng MainPosition thực tế của mỗi ô
        foreach (GridCell cell in omegaCells)
        {
            if (cell == null) continue;

            if (cell.MainPosition >= 1 && cell.MainPosition <= 10)
            {
                if (!OmegaSlotToCellMap.ContainsKey(cell.MainPosition))
                {
                    OmegaSlotToCellMap.Add(cell.MainPosition, cell);
                }
                else
                {
                    Debug.LogWarning($"[BattleSpawnController] Omega spawn cell duplicate MainPosition {cell.MainPosition} detected.");
                }
            }
            else
            {
                Debug.LogWarning($"[BattleSpawnController] Omega spawn cell has invalid MainPosition {cell.MainPosition}.");
            }
        }
    }

    // private void ShowMockTestIdsOnBoard(string alphaId, string omegaId)
    // {
    //     if (GridManager == null)
    //     {
    //         return;
    //     }

    //     Transform anchorParent = GridManager.GridParent != null ? GridManager.GridParent : GridManager.transform;

    //     if (GridManager.PlayerSpawnCells.Count > 0)
    //     {
    //         CreateWorldText(anchorParent, $"Alpha: {alphaId}", GridManager.PlayerSpawnCells[0].transform.position + Vector3.up * 1.4f + Vector3.forward * 0.8f, Color.cyan);
    //     }

    //     if (GridManager.EnemySpawnCells.Count > 0)
    //     {
    //         CreateWorldText(anchorParent, $"Omega: {omegaId}", GridManager.EnemySpawnCells[0].transform.position + Vector3.up * 1.4f + Vector3.back * 0.8f, Color.magenta);
    //     }
    // }

    // private void CreateWorldText(Transform parent, string text, Vector3 worldPosition, Color color)
    // {
    //     GameObject labelObject = new GameObject($"BattleId_{text}", typeof(TextMeshPro));
    //     labelObject.transform.SetParent(parent, false);
    //     labelObject.transform.position = worldPosition;
    //     labelObject.transform.rotation = Quaternion.Euler(45f, 0f, 0f);

    //     TextMeshPro textMesh = labelObject.GetComponent<TextMeshPro>();
    //     textMesh.text = text;
    //     textMesh.fontSize = 2.2f;
    //     textMesh.color = color;
    //     textMesh.alignment = TextAlignmentOptions.Center;
    //     textMesh.enableWordWrapping = false;
    // }

    public void DeployTeam(List<CardBase> cards, bool isAlpha)
    {
        // Chọn đúng Dictionary map vị trí dựa theo phe Alpha / Omega
        var slotMap = isAlpha ? AlphaSlotToCellMap : OmegaSlotToCellMap;

        foreach (CardBase hero in cards)
        {
            if (hero == null)
            {
                Debug.LogWarning("[BattleSpawnController] Một hero null xuất hiện trong danh sách đội hình.");
                continue;
            }

            hero.Team = isAlpha ? Team.Alpha : Team.Omega;

            if (hero.MainPosition < 1 || hero.MainPosition > 10)
            {
                Debug.LogWarning($"[BattleSpawnController] {hero.Name} có MainPosition không hợp lệ: {hero.MainPosition} (raw: '{hero.Position}')");
                continue;
            }

            // Debug.Log($"[BattleSpawnController] Deploying {(isAlpha ? "Alpha" : "Omega")} hero '{hero.Name}' pos={hero.MainPosition} rawPosition='{hero.Position}'");

            if (hero.MainPosition < 1 || hero.MainPosition > 10)
            {
                Debug.LogWarning($"[BattleSpawnController] {hero.Name} có MainPosition không hợp lệ: {hero.MainPosition}");
                continue;
            }

            if (slotMap.TryGetValue(hero.MainPosition, out GridCell targetCell))
            {
                if (targetCell == null)
                {
                    // Debug.LogWarning($"[BattleSpawnController] Slot map trả về GridCell null cho vị trí {hero.MainPosition}.");
                    continue;
                }

                    Transform parentPanel = targetCell.DisplayCardPanel != null ? targetCell.DisplayCardPanel : targetCell.transform;
                if (targetCell.DisplayCardPanel == null)
                {
                    // Debug.LogWarning($"[BattleSpawnController] Target cell DisplayCardPanel null for {hero.Name} at slot {hero.MainPosition}. Falling back to cell transform.");
                    targetCell.DisplayCardPanel = parentPanel;
                }

                if (targetCell.OccupiedCard != null)
                {
                    // Debug.LogWarning($"[BattleSpawnController] Ô số {hero.MainPosition} của bên {(isAlpha ? "Alpha" : "Omega")} đã bị chiếm bởi {targetCell.OccupiedCard.Name}. Không thể đặt {hero.Name} vào!");
                    continue;
                }

                GameObject cardObj = Instantiate(CardVisualPrefab, parentPanel);
                cardObj.name = $"{(isAlpha ? "Alpha" : "Omega")}_Pos_{hero.MainPosition}_{hero.Name}";

                CardVisual visualScript = cardObj.GetComponent<CardVisual>();
                if (visualScript != null)
                {
                    visualScript.SetupVisual(hero);
                }
                else
                {
                    Debug.LogWarning($"[BattleSpawnController] CardVisual prefab thiếu CardVisual component: {cardObj.name}");
                }

                targetCell.OccupiedCard = hero;

                if (isAlpha)
                {
                    ActiveAlphaHeroes.Add(hero);
                }
                else
                {
                    ActiveOmegaHeroes.Add(hero);
                }

                // Debug.Log($"[BattleSpawnController] Spawned {hero.Name} into {(isAlpha ? "Alpha" : "Omega")} cell #{hero.MainPosition}.");
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy ô cờ nào có MainPosition = {hero.MainPosition} tương thích với {hero.Name} trong hệ thống Map! Alpha={isAlpha}");
            }
        }
    }
}