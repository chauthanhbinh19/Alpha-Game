using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-198)]
public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    [Header("Grid Settings")]
    public int Width = 12;
    public int Height = 12;
    public float CellSize = 1.1f;
    public GameObject CellPrefab;
    public Transform GridParent;

    [Header("Materials")]
    public Material EmptyPositionMaterial;
    public Material PlayerPositionMaterial;
    public Material EnemyPositionMaterial;
    public Material SelectedPositionMaterial;

    [Header("New Movement Range Materials")]
    [Tooltip("Màu dành cho các ô CÓ THỂ đi tới đầu lượt này (Đủ điểm)")]
    public Material WalkableRangeMaterial;

    [Tooltip("Màu dành cho các ô nằm trong tầm di chuyển gốc nhưng do vướng vật cản/đi vòng nên KHÔNG ĐỦ điểm tới")]
    public Material OutOfPointsMaterial;

    [Header("Pathfinding Materials")]
    [Tooltip("Màu dành cho ô Đích đến cuối cùng")]
    public Material DestinationMaterial;
    [Tooltip("Màu dành cho các ô Trung gian nằm trên đường đi")]
    public Material PathNodeMaterial;

    [Header("Attack Range Materials")]
    [Tooltip("Màu dành cho các ô nằm trong phạm vi tấn công")]
    public Material AttackRangeMaterial;

    private Dictionary<Vector2Int, GridCell> GridDict = new Dictionary<Vector2Int, GridCell>();
    private bool isGridReady;
    private System.Action onGridReadyCallback;

    // Lưu trữ danh sách đường đi hiện tại để di chuyển hoặc xóa
    private List<GridCell> CurrentCalculatedPath = new List<GridCell>();
    private GridCell CurrentlySelectedCell;
    private GridCell TargetDestinationCell; // Ô đích được chọn
    // Các hàm tương tác phục vụ nút bấm UI
    public List<GridCell> GetCurrentPath() => CurrentCalculatedPath;
    public void ClearCurrentPathData() => CurrentCalculatedPath.Clear();


    [Header("Spawn Positions (10 Cells Each)")]
    public List<GridCell> PlayerSpawnCells = new List<GridCell>();
    public List<GridCell> EnemySpawnCells = new List<GridCell>();


    void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public bool IsGridReady()
    {
        return isGridReady;
    }

    public void InitializeGridAsync(System.Action onComplete)
    {
        if (isGridReady)
        {
            onComplete?.Invoke();
            return;
        }

        onGridReadyCallback = onComplete;
        if (GridDict.Count == 0 && GridParent != null)
        {
            GenerateGrid();
        }
        else if (GridDict.Count > 0)
        {
            FinishGridInitialization();
        }
    }

    private void FinishGridInitialization()
    {
        isGridReady = true;
        onGridReadyCallback?.Invoke();
        onGridReadyCallback = null;
    }

    /// <summary>
    /// Thực hiện lệnh tấn công thực tế khi người chơi click chọn mục tiêu hợp lệ
    /// </summary>
    public void ExecuteAttack(CardBase attacker, GridCell targetCell)
    {
        if (targetCell == null || targetCell.OccupiedCard == null || attacker == null) return;

        CardBase enemy = targetCell.OccupiedCard;

        // Chỉ tấn công nếu là kẻ địch (khác Type hoặc phe) [cite: 182]
        if (!attacker.Type.Equals(enemy.Type))
        {
            UnityEngine.Debug.Log($"[Battle] {attacker.Name} bắt đầu tấn công {enemy.Name}");

            Effects attackEffect = new Effects
            {
                Id = 0,
                Name = "Normal Attack",
                Description = "Đòn tấn công thường của quân bài.",
                EffectAction = new EffectAction
                {
                    ActionCode = AppConstants.EffectAction.DAMAGE,
                    ActionName = "DAMAGE",
                    Description = "Gây sát thương theo thông số tấn công.",
                    ActionId = 0
                },
                TriggerPhase = "MAIN",
                TriggerCondition = AppConstants.TriggerCondition.ON_ATTACK,
                Target = new Targets { Id = "CAST_TARGET" }
            };

            CombatEffectProcessor.ApplyEffects(new List<Effects> { attackEffect }, attacker, enemy);

            // Xóa vùng hiển thị tầm đánh sau khi kết thúc hành động
            ClearAllMovementRanges();
        }
    }

    public void GenerateGrid()
    {
        int idCounter = 0;
        int minX = -Width / 2;
        int maxX = Width / 2;
        int minZ = -Height / 2;
        int maxZ = Height / 2;

        for (int x = minX; x < maxX; x++)
        {
            for (int z = minZ; z < maxZ; z++)
            {
                GameObject newCellObject = Instantiate(CellPrefab, GridParent);
                newCellObject.name = $"Cell ({x}, {z})";
                newCellObject.transform.localPosition = new Vector3(x * CellSize, 0, z * CellSize);
                newCellObject.transform.localRotation = Quaternion.identity;

                GridCell cellScript = newCellObject.GetComponent<GridCell>();
                Vector2Int gridPosition = new Vector2Int(x, z);

                cellScript.Setup(idCounter, gridPosition); // Giữ nguyên hàm Setup cũ của bạn

                // --- BƯỚC 1: TẤT CẢ 144 Ô ĐỀU ĐƯỢC GÁN MATERIAL TRỐNG ---
                cellScript.SetDefaultMaterial(EmptyPositionMaterial);

                GridDict.Add(gridPosition, cellScript);
                idCounter++;
            }
        }

        // Sau khi tạo xong 12x12, tiến hành ghi đè màu cho các ô Spawn
        // SetupSpawnPositions(minX, maxX, minZ, maxZ);
        SetupCenteredDoubleRowSpawnPositions(minX, maxX, minZ, maxZ);
        FinishGridInitialization();
    }

    // Hàm xóa hiển thị range của TẤT CẢ các ô trên bàn cờ
    public void ClearAllMovementRanges()
    {
        foreach (var cell in GridDict.Values)
        {
            cell.HideMovementRange();
            cell.HideAttackRange();

            // Tắt sạch các Target UI đang mở trên ô celer
            TargetUI cellTargetUI = cell.GetComponent<TargetUI>();
            if (cellTargetUI != null)
            {
                cellTargetUI.HideDamageTargetPanelUI();
                cellTargetUI.HideHealTargetPanelUI();
                cellTargetUI.HideBuffTargetPanelUI();
                cellTargetUI.HideDebuffTargetPanelUI();
                cellTargetUI.HideCleanseTargetPanelUI();
                cellTargetUI.HideSummonTargetPanelUI();
                cellTargetUI.HideControlTargetPanelUI();
            }
        }

        // // Hoàn tác luôn ô cờ đang chọn về màu mặc định khi clear range
        // if (currentlySelectedCell != null)
        // {
        //     currentlySelectedCell.ResetToDefaultMaterial();
        //     currentlySelectedCell = null;
        // }
    }

    public void SetupSpawnPositions(int minX, int maxX, int minZ, int maxZ)
    {
        PlayerSpawnCells.Clear();
        EnemySpawnCells.Clear();

        // Giữ nguyên dải ô chính giữa theo trục Z (từ trái sang phải màn hình)
        int startZ = minZ + 1; // -5
        int endZ = maxZ - 1;   // 5

        int playerIndex = 1;
        int enemyIndex = 1;

        for (int z = startZ; z < endZ; z++)
        {
            // --- ĐÃ ĐỔI NGƯỢC LẠI ---

            // 1. PHE PLAYER (Bây giờ lấy ở hàng TRÊN CÙNG của ma trận)
            int playerXRow = maxX - 2;
            GridCell pCell = GetCellAt(playerXRow, z);
            if (pCell != null && playerIndex <= 10)
            {
                pCell.SetAsSpawnCell(playerIndex, isPlayer: true, PlayerPositionMaterial);
                PlayerSpawnCells.Add(pCell);
                playerIndex++;
            }

            // 2. PHE ENEMY (Bây giờ lấy ở hàng DƯỚI CÙNG của ma trận)
            int enemyXRow = minX + 1;
            GridCell eCell = GetCellAt(enemyXRow, z);
            if (eCell != null && enemyIndex <= 10)
            {
                eCell.SetAsSpawnCell(enemyIndex, isPlayer: false, EnemyPositionMaterial);
                EnemySpawnCells.Add(eCell);
                enemyIndex++;
            }
        }
    }

    public void SetupRandomDoubleRowSpawnPositions(int minX, int maxX, int minZ, int maxZ)
    {
        PlayerSpawnCells.Clear();
        EnemySpawnCells.Clear();

        // Giới hạn trục Z để chừa 2 ô biên ngoài cùng (trái/phải màn hình) tương tự như cũ
        int startZ = minZ + 1; // -5
        int endZ = maxZ - 1;   // 5

        // Danh sách tạm để gom toàn bộ ô cờ hợp lệ của 2 hàng mỗi bên
        List<GridCell> potentialPlayerCells = new List<GridCell>();
        List<GridCell> potentialEnemyCells = new List<GridCell>();

        // ------------------------------------------------------------------
        // BƯỚC 1: GOM Ô CỜ TỪ 2 HÀNG CỦA MỖI PHE
        // ------------------------------------------------------------------
        for (int z = startZ; z < endZ; z++)
        {
            // --- PHE PLAYER (Phía Trên) ---
            // Gom hàng sát rìa (nhưng đã lùi 1): maxX - 2
            GridCell pCellRow1 = GetCellAt(maxX - 2, z);
            if (pCellRow1 != null) potentialPlayerCells.Add(pCellRow1);

            // Gom thêm hàng thứ 2 (lùi sâu vào trong hơn): maxX - 3
            GridCell pCellRow2 = GetCellAt(maxX - 3, z);
            if (pCellRow2 != null) potentialPlayerCells.Add(pCellRow2);


            // --- PHE ENEMY (Phía Dưới) ---
            // Gom hàng sát rìa (nhưng đã lùi 1): minX + 1
            GridCell eCellRow1 = GetCellAt(minX + 1, z);
            if (eCellRow1 != null) potentialEnemyCells.Add(eCellRow1);

            // Gom thêm hàng thứ 2 (lùi sâu vào trong hơn): minX + 2
            GridCell eCellRow2 = GetCellAt(minX + 2, z);
            if (eCellRow2 != null) potentialEnemyCells.Add(eCellRow2);
        }

        // ------------------------------------------------------------------
        // BƯỚC 2: TRỘN NGẪU NHIÊN (SHUFFLE) DANH SÁCH BẰNG THUẬT TOÁN FISHER-YATES
        // ------------------------------------------------------------------
        ShuffleList(potentialPlayerCells);
        ShuffleList(potentialEnemyCells);

        // ------------------------------------------------------------------
        // BƯỚC 3: BỐC RA 10 Ô ĐẦU TIÊN VÀ GÁN MAIN POSITION (1 ĐẾN 10)
        // ------------------------------------------------------------------

        // Cấu hình ngẫu nhiên cho Player
        int playerMaxCount = Mathf.Min(10, potentialPlayerCells.Count);
        for (int i = 0; i < playerMaxCount; i++)
        {
            GridCell cell = potentialPlayerCells[i];
            int playerIndex = i + 1; // Số từ 1 -> 10

            cell.SetAsSpawnCell(playerIndex, isPlayer: true, PlayerPositionMaterial);
            PlayerSpawnCells.Add(cell);
        }

        // Cấu hình ngẫu nhiên cho Enemy
        int enemyMaxCount = Mathf.Min(10, potentialEnemyCells.Count);
        for (int i = 0; i < enemyMaxCount; i++)
        {
            GridCell cell = potentialEnemyCells[i];
            int enemyIndex = i + 1; // Số từ 1 -> 10

            cell.SetAsSpawnCell(enemyIndex, isPlayer: false, EnemyPositionMaterial);
            EnemySpawnCells.Add(cell);
        }
    }

    public void SetupCenteredDoubleRowSpawnPositions(int minX, int maxX, int minZ, int maxZ)
    {
        PlayerSpawnCells.Clear();
        EnemySpawnCells.Clear();

        // Với Width = 12, Height = 12: 
        // minX = -6, maxX = 6 | minZ = -6, maxZ = 6
        // Vòng lặp chạy từ min cho đến < max (tức là từ -6 đến 5)

        // ------------------------------------------------------------------
        // BƯỚC 1: XÁC ĐỊNH DÒNG X THEO HỆ TỌA ĐỘ ĐỐI XỨNG (NÉ X = -1 VÀ X = 0)
        // ------------------------------------------------------------------
        // Phe Player (Phía trên - vùng chỉ số X dương)
        int playerRow1X = 2;  // Hàng chứa vị trí 1 -> 5
        int playerRow2X = 3;  // Hàng chứa vị trí 6 -> 10

        // Phe Enemy (Phía dưới - vùng chỉ số X âm)
        int enemyRow1X = -3; // Hàng chứa vị trí 1 -> 5
        int enemyRow2X = -4; // Hàng chứa vị trí 6 -> 10


        // ------------------------------------------------------------------
        // BƯỚC 2: CẤU HÌNH CỘT Z XEN KẼ (CÁCH NHAU 1 Ô NGANG)
        // Các ô Z: -4, -2, 0, 2, 4 cách nhau đúng 1 ô trống ở giữa và căn giữa sàn (-6 đến 5)
        // ------------------------------------------------------------------
        int[] activeZColumns = new int[] { -4, -2, 0, 2, 4 };


        // ------------------------------------------------------------------
        // BƯỚC 3: ĐẶT VỊ TRÍ PHE PLAYER (X DƯƠNG)
        // ------------------------------------------------------------------
        // Hàng 1 (Vị trí 1 đến 5)
        for (int i = 0; i < activeZColumns.Length; i++)
        {
            int z = activeZColumns[i];
            GridCell cell = GetCellAt(playerRow1X, z);
            if (cell != null)
            {
                int playerIndex = i + 1; // 1, 2, 3, 4, 5
                cell.SetAsSpawnCell(playerIndex, isPlayer: true, PlayerPositionMaterial);
                PlayerSpawnCells.Add(cell);
            }
        }

        // Hàng 2 (Vị trí 6 đến 10)
        for (int i = 0; i < activeZColumns.Length; i++)
        {
            int z = activeZColumns[i];
            GridCell cell = GetCellAt(playerRow2X, z);
            if (cell != null)
            {
                int playerIndex = i + 6; // 6, 7, 8, 9, 10
                cell.SetAsSpawnCell(playerIndex, isPlayer: true, PlayerPositionMaterial);
                PlayerSpawnCells.Add(cell);
            }
        }


        // ------------------------------------------------------------------
        // BƯỚC 4: ĐẶT VỊ TRÍ PHE ENEMY (X ÂM)
        // ------------------------------------------------------------------
        // Hàng 1 (Vị trí 1 đến 5)
        for (int i = 0; i < activeZColumns.Length; i++)
        {
            int z = activeZColumns[i];
            GridCell cell = GetCellAt(enemyRow1X, z);
            if (cell != null)
            {
                int enemyIndex = i + 1; // 1, 2, 3, 4, 5
                cell.SetAsSpawnCell(enemyIndex, isPlayer: false, EnemyPositionMaterial);
                EnemySpawnCells.Add(cell);
            }
        }

        // Hàng 2 (Vị trí 6 đến 10)
        for (int i = 0; i < activeZColumns.Length; i++)
        {
            int z = activeZColumns[i];
            GridCell cell = GetCellAt(enemyRow2X, z);
            if (cell != null)
            {
                int enemyIndex = i + 6; // 6, 7, 8, 9, 10
                cell.SetAsSpawnCell(enemyIndex, isPlayer: false, EnemyPositionMaterial);
                EnemySpawnCells.Add(cell);
            }
        }
    }
    
    // Hàm phụ trợ dùng để trộn bài / trộn list ngẫu nhiên cực chuẩn
    private void ShuffleList<T>(List<T> list)
    {
        System.Random rand = new System.Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public void SelectCell(GridCell targetCell)
    {
        // Trả ô cũ về màu mặc định (nếu có ô cũ và khác ô mới)
        if (CurrentlySelectedCell != null && CurrentlySelectedCell != targetCell)
        {
            CurrentlySelectedCell.ResetToDefaultMaterial();
        }

        CurrentlySelectedCell = targetCell;

        if (CurrentlySelectedCell != null)
        {
            // Đổi màu nền của ô được chọn sang màu Vàng (Selected)
            CurrentlySelectedCell.ChangeRuntimeMaterial(SelectedPositionMaterial);
        }
    }

    // Hàm phụ trợ tính khoảng cách Manhattan (Đường ngắn nhất tuyệt đối)
    // Hàm tính khoảng cách Manhattan chuẩn xác
    // Hàm phụ trợ tính khoảng cách Manhattan (Đường ngắn nhất tuyệt đối)
    // Hàm tính khoảng cách Manhattan chuẩn xác
    // Hàm phụ trợ tính khoảng cách Manhattan (Đường ngắn nhất tuyệt đối)
    // Hàm phụ trợ tính khoảng cách Manhattan (Đường ngắn nhất tuyệt đối)
    private int GetAbsoluteDistance(Vector2Int start, Vector2Int end)
    {
        return Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
    }

    /// <summary>
    /// Hàm tự động kiểm tra xem ĐƯỜNG NGẮN NHẤT (đường thẳng toán học) từ start đến end có bị VẬT CẢN chặn hay không.
    /// Trả về true nếu đường đi thẳng hoàn toàn thông thoáng, trả về false nếu vướng vật cản/Card khác.
    /// </summary>
    private bool IsShortestPathClear(Vector2Int start, Vector2Int end)
    {
        Vector2Int current = start;

        // Tịnh tiến từng bước theo trục X và Y để mô phỏng đường đi ngắn nhất lý tưởng
        while (current != end)
        {
            if (current.x != end.x)
            {
                current.x += (end.x > current.x) ? 1 : -1;
            }
            else if (current.y != end.y)
            {
                current.y += (end.y > current.y) ? 1 : -1;
            }

            // Nếu chưa tới đích mà ô dọc đường thẳng đã bị chặn hoặc có quân cờ đứng chiếm chỗ
            if (current != end)
            {
                if (GridDict.TryGetValue(current, out GridCell stepCell))
                {
                    if (!stepCell.IsWalkable || stepCell.OccupiedCard != null)
                    {
                        return false; // Phát hiện có vật cản chặn trên đường ngắn nhất
                    }
                }
            }
        }
        return true; // Đường thẳng tắp hoàn toàn sạch bóng vật cản
    }

    public void ShowMovementRangeAt(Vector2Int centerPos, int maxRange, int movementPoint)
    {
        // 1. Luôn xóa sạch các ô hiển thị màu và ẩn UI cảnh báo cũ trên toàn lưới trước
        ClearAllMovementRanges();

        // BỎ CHẶN RETURN Ở ĐÂY để cho phép xử lý hiển thị WarningUI khi movementPoint = 0

        Vector2Int[] directions =
        {
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0)
        };

        // BFS tính khoảng cách thực tế ngắn nhất (theo cấu trúc sạch sẽ của bạn)
        Dictionary<Vector2Int, int> realDistances = new Dictionary<Vector2Int, int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        // CHỈ CHẠY BFS LOANG THỰC TẾ NẾU ĐIỂM DI CHUYỂN LỚN HƠN 0
        if (movementPoint > 0)
        {
            queue.Enqueue(centerPos);
            realDistances[centerPos] = 0;

            while (queue.Count > 0)
            {
                Vector2Int currentPos = queue.Dequeue();
                int currentDistance = realDistances[currentPos];

                // Giới hạn tầm loang tối đa bằng maxRange hình học để thuật toán không chạy tràn biên
                if (currentDistance >= maxRange) continue;

                foreach (var dir in directions)
                {
                    Vector2Int nextPos = currentPos + dir;

                    if (!GridDict.TryGetValue(nextPos, out GridCell nextCell))
                        continue;

                    // Né chướng ngại vật hoặc ô có card khác đứng chiếm chỗ
                    if (!nextCell.IsWalkable || nextCell.OccupiedCard != null)
                        continue;

                    int nextDistance = currentDistance + 1;

                    if (!realDistances.ContainsKey(nextPos))
                    {
                        realDistances[nextPos] = nextDistance;
                        queue.Enqueue(nextPos);
                    }
                }
            }
        }

        // Bước 2: Duyệt danh sách toàn lưới và phân loại hiển thị dựa trên maxRange và movementPoint
        foreach (var pair in GridDict)
        {
            GridCell cell = pair.Value;

            // Xử lý ô gốc nơi tướng đang đứng khi movementPoint <= 0
            if (cell.GridPosition == centerPos)
            {
                if (movementPoint <= 0)
                {
                    MovementWarningUI ui = cell.GetComponent<MovementWarningUI>();
                    if (ui != null) ui.ShowUI();
                }
                continue;
            }

            // Khoảng cách đường chim bay toán học thô (Manhattan)
            int manhattanDistance = GetAbsoluteDistance(centerPos, cell.GridPosition);

            // QUY TẮC PHẠM VI: Nếu ô nằm ngoài tầm maxRange hình học của tướng -> Ẩn sạch, không xử lý gì hết
            if (manhattanDistance > maxRange)
            {
                cell.HideMovementRange();
                continue;
            }

            // Kiểm tra thực tế xem BFS có tìm được đường đi né vật cản đến ô này hay không
            bool hasRealPath = realDistances.TryGetValue(cell.GridPosition, out int realDistance);

            // Kiểm tra xem đường ngắn nhất (đường thẳng thô) đến vị trí đó có vật cản hay không
            bool isShortestPathClear = IsShortestPathClear(centerPos, cell.GridPosition);

            // ==========================================
            // VÙNG XANH - Đi tới được thực tế
            // Có đường đi thực tế VÀ điểm năng lượng hiện tại ĐỦ (movementPoint >= realDistance)
            // (Nếu movementPoint = 0 thì điều kiện này luôn sai, không bao giờ hiện màu xanh bậy)
            // ==========================================
            if (hasRealPath && realDistance <= movementPoint && movementPoint > 0)
            {
                cell.ShowMovementRange(WalkableRangeMaterial, true);
                continue;
            }

            // ==========================================
            // KHÔNG ĐỦ ĐIỂM HOẶC BỊ CHẶN TRÊN THỰC TẾ (Bao gồm cả trường hợp movementPoint = 0):
            // ==========================================

            // ĐIỀU KIỆN 1: Đường ngắn nhất đến vị trí đó CÓ VẬT CẢN và Point gốc ĐỦ (manhattanDistance <= movementPoint)
            // (Nếu movementPoint = 0, điều kiện manhattanDistance <= 0 sẽ sai đối với các ô xung quanh, nên chúng sẽ không bị hiện màu Đỏ nhầm)
            if (!isShortestPathClear && manhattanDistance <= movementPoint && movementPoint > 0)
            {
                cell.ShowMovementRange(OutOfPointsMaterial, false);
            }
            // ĐIỀU KIỆN 2: Ô nằm trong phạm vi maxRange (manhattanDistance <= maxRange) nhưng điểm di chuyển không đủ (movementPoint < thực tế hoặc bằng 0)
            // VÀ đường thẳng ngắn nhất không bị vật cản chặn
            else if (isShortestPathClear)
            {
                cell.HideMovementRange(); // Tắt Platform màu xanh đi

                MovementWarningUI ui = cell.GetComponent<MovementWarningUI>();
                if (ui != null)
                {
                    ui.ShowUI(); // Hiện WarningUI sọc vàng tại ô này
                }
            }
            else
            {
                // Các ô bị chướng ngại vật cản hoàn toàn mà điểm hiện tại không đủ để tính toán đi vòng
                cell.HideMovementRange();
                MovementWarningUI ui = cell.GetComponent<MovementWarningUI>();
                if (ui != null) ui.ShowUI();
            }
        }
    }

    public GridCell GetAllCells()
    {
        return null;
    }
    
    public GridCell GetCellAt(int x, int z)
    {
        Vector2Int pos = new Vector2Int(x, z);
        if (GridDict.TryGetValue(pos, out GridCell cell))
        {
            return cell;
        }
        return null;
    }

    /// <summary>
    /// Gọi hàm này khi người chơi Click vào quân cờ (CardVisual)
    /// </summary>
    public void HandleCardClick(GridCell cardCell, int maxRange)
    {
        if (cardCell == null || cardCell.OccupiedCard == null) return;

        // Lưu ô đang chọn làm ô xuất phát
        SelectCell(cardCell);
        CurrentlySelectedCell = cardCell;

        // Hiển thị vùng di chuyển dựa trên vị trí quân cờ và tầm đi (maxRange)
        // ShowMovementRangeAt(cardCell.GridPosition, maxRange);
    }

    /// <summary>
    /// Thuật toán tìm đường đi ngắn nhất dựa trên dữ liệu loang BFS đã chạy trước đó
    /// </summary>
    public List<GridCell> FindPath(Vector2Int startPos, Vector2Int endPos)
    {
        List<GridCell> path = new List<GridCell>();

        if (!GridDict.ContainsKey(startPos) || !GridDict.ContainsKey(endPos)) return path;

        GridCell startCell = GridDict[startPos];
        GridCell endCell = GridDict[endPos];

        // Chạy lại một vòng BFS ngầm hoặc tận dụng để gán ParentNode cho các ô
        // Để đơn giản và chính xác nhất, ta chạy BFS từ startPos để thiết lập liên kết dòng họ (Parent)
        Queue<GridCell> queue = new Queue<GridCell>();
        HashSet<GridCell> visited = new HashSet<GridCell>();

        foreach (var cell in GridDict.Values) cell.ParentNode = null; // Reset vết cũ

        queue.Enqueue(startCell);
        visited.Add(startCell);

        Vector2Int[] directions = new Vector2Int[] { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(-1, 0), new Vector2Int(1, 0) };

        bool found = false;
        while (queue.Count > 0)
        {
            GridCell current = queue.Dequeue();
            if (current == endCell) { found = true; break; }

            foreach (Vector2Int dir in directions)
            {
                Vector2Int nextPos = current.GridPosition + dir;
                if (GridDict.TryGetValue(nextPos, out GridCell neighbor))
                {
                    // Chỉ đi qua các ô Walkable, không bị chặn và NẰM TRONG VÙNG ĐI ĐƯỢC (Đã check ở hàm show range)
                    if (!visited.Contains(neighbor) && neighbor.IsWalkable && neighbor.OccupiedCard == null && neighbor.IsWalkableRange)
                    {
                        neighbor.ParentNode = current;
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        // Nếu tìm thấy đường, tiến hành hồi vết ngược từ Đích về Gốc
        if (found)
        {
            GridCell curr = endCell;
            while (curr != null)
            {
                path.Add(curr);
                curr = curr.ParentNode;
            }
            path.Reverse(); // Đảo ngược lại để có thứ tự từ Start -> End
        }

        return path;
    }

    /// <summary>
    /// Hàm xử lý khi người chơi Click vào một ô bất kỳ trên lưới
    /// </summary>
    public void HandleCellClick(GridCell clickedCell)
    {
        if (CurrentlySelectedCell == null) return; // Chưa chọn tướng xuất phát thì không làm gì cả

        // Lấy dữ liệu quân cờ đang chọn để biết tầm đi gốc
        CardBase selectedCard = CurrentlySelectedCell.OccupiedCard;
        if (selectedCard == null || selectedCard.Class == null) return;
        int maxMoveRange = selectedCard.Class.MovementRange;

        // ================= TRƯỜNG HỢP 1: CLICK VÀO Ô TRONG TẦM DI CHUYỂN HỢP LỆ =================
        if (clickedCell.IsWalkableRange)
        {
            if (CurrentCalculatedPath != null && CurrentCalculatedPath.Count > 0)
            {
                HighlightPath(false);
            }

            TargetDestinationCell = clickedCell;
            CurrentCalculatedPath = FindPath(CurrentlySelectedCell.GridPosition, TargetDestinationCell.GridPosition);

            if (CurrentCalculatedPath.Count > 0)
            {
                HighlightPath(true);
                MovementConfirmUI cellUI = TargetDestinationCell.GetComponent<MovementConfirmUI>();
                if (cellUI != null)
                {
                    cellUI.ShowUI(true);
                }
            }
        }
        // ================= TRƯỜNG HỢP 2: CLICK VÀO Ô TRONG TẦM TẤN CÔNG =================
        else if (clickedCell.IsAttackRange)
        {
            if (clickedCell.OccupiedCard != null && clickedCell.IsPlayerSpawnCell != CurrentlySelectedCell.IsPlayerSpawnCell)
            {
                AttackConfirmUI attackUI = clickedCell.GetComponent<AttackConfirmUI>();
                if (attackUI != null)
                {
                    attackUI.ShowUI(true, CurrentlySelectedCell, clickedCell);
                }
            }
        }
        // ================= TRƯỜNG HỢP 3: KHÔNG ĐỦ ĐIỂM (HIỂN THỊ MÀU ĐỎ OUT OF POINT) =================
        else if (clickedCell.MovementPlatform != null && clickedCell.MovementPlatform.gameObject.activeSelf && clickedCell.MovementPlatform.material == OutOfPointsMaterial)
        {
            HandleOutOfPointClick(clickedCell);
        }
        // ================= TRƯỜNG HỢP 4 (MỚI): CHECK ĐƯỜNG NGẮN NHẤT BỊ VẬT CẢN CHẶN =================
        else
        {
            // Tính khoảng cách ngắn nhất lý tưởng tuyệt đối (bỏ qua mọi vật cản)
            int absoluteDist = GetAbsoluteDistance(CurrentlySelectedCell.GridPosition, clickedCell.GridPosition);

            // Nếu khoảng cách lý tưởng nằm trong tầm đi gốc của Card, chứng tỏ dọc đường đi có vật cản/card khác chặn
            if (absoluteDist <= maxMoveRange)
            {
                if (MovementWarningUI.Instance != null)
                {
                    MovementWarningUI.Instance.ShowUI($"Ô này nằm trong tầm đi {maxMoveRange} ô nhưng lộ trình đã bị chướng ngại vật hoặc quân cờ khác chặn đường!");
                }
                else
                {
                    Debug.LogWarning("Chưa khởi tạo MovementWarningUI trong Scene!");
                }
            }
        }
    }

    /// <summary>
    /// Nơi để bạn tự gán thêm logic khi click vào ô Out Of Point (Để trống theo yêu cầu của bạn)
    /// </summary>
    private void HandleOutOfPointClick(GridCell clickedCell)
    {
        Debug.LogWarning($"[GridManager] Ô {clickedCell.GridPosition} không đủ điểm di chuyển tới!");

        // --- KÍCH HOẠT UI WARNING TẠI ĐÂY ---
        if (MovementWarningUI.Instance != null)
        {
            // Bạn có thể truyền text linh hoạt tùy ý thích
            MovementWarningUI.Instance.ShowUI("Không đủ Điểm Di Chuyển!");
        }
        else
        {
            Debug.LogError("Chưa kéo thả hoặc thiếu script MovementWarningUI trong Scene!");
        }
    }

    /// <summary>
    /// Vẽ màu hoặc xóa màu đường đi lộ trình
    /// </summary>
    public void HighlightPath(bool state)
    {
        if (CurrentCalculatedPath.Count == 0) return;

        for (int i = 0; i < CurrentCalculatedPath.Count; i++)
        {
            GridCell cell = CurrentCalculatedPath[i];

            if (state)
            {
                if (i == CurrentCalculatedPath.Count - 1)
                {
                    // Ô cuối cùng -> Đổi sang màu ĐÍCH ĐẾN (Destination)
                    cell.SetAsDestination(DestinationMaterial);
                }
                else if (i > 0) // i = 0 là ô gốc tướng đang đứng, không cần đổi
                {
                    // Các ô ở giữa -> Đổi sang màu TRUNG GIAN (Path)
                    cell.SetAsPathNode(PathNodeMaterial);
                }
            }
            else
            {
                // Hoàn tác: trả lại màu loang Range ban đầu
                cell.ResetToRangeMaterial();
            }
        }

        if (!state) CurrentCalculatedPath.Clear();
    }

    /// <summary>
    /// Cập nhật lại ô gốc khi quân cờ đổi vị trí đứng
    /// </summary>
    public void SetOriginCell(GridCell newOrigin)
    {
        CurrentlySelectedCell = newOrigin;
    }

    // --- HÀM THÊM MỚI CHUYÊN DỤNG CHO ATTACK ---
    /// <summary>
    /// Tìm và hiển thị vùng tấn công theo bán kính (Bỏ qua chướng ngại vật và đồng minh)
    /// </summary>
    public void ShowAttackRangeAt(Vector2Int centerPos, int attackRange, bool isPlayerAttacking)
    {
        // 1. Dọn dẹp tất cả các vùng hiển thị cũ trước khi vẽ vùng tấn công
        ClearAllMovementRanges();
        if (attackRange <= 0) return;

        // Cấu trúc BFS lưu: <Vị trí ô, Tầm đánh còn lại>
        Queue<KeyValuePair<Vector2Int, int>> queue = new Queue<KeyValuePair<Vector2Int, int>>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        queue.Enqueue(new KeyValuePair<Vector2Int, int>(centerPos, attackRange));
        visited.Add(centerPos);

        Vector2Int[] directions = new Vector2Int[]
        {
        new Vector2Int(0, 1),  // Lên
        new Vector2Int(0, -1), // Xuống
        new Vector2Int(-1, 0), // Trái
        new Vector2Int(1, 0)   // Phải
        };

        while (queue.Count > 0)
        {
            var currentData = queue.Dequeue();
            Vector2Int currentPos = currentData.Key;
            int remainingRange = currentData.Value;

            if (remainingRange <= 0) continue;

            foreach (Vector2Int dir in directions)
            {
                Vector2Int neighborPos = currentPos + dir;

                // Kiểm tra ô hàng xóm tồn tại trên lưới không
                if (GridDict.TryGetValue(neighborPos, out GridCell neighborCell))
                {
                    if (visited.Contains(neighborPos)) continue;

                    // --- LUẬT KIỂM TRA CHƯỚNG NGẠI VẬT VÀ ĐỒNG MINH ---
                    // 1. Không thể bắn xuyên qua tường/vật cản cứng (!IsWalkable)
                    if (!neighborCell.IsWalkable) continue;

                    // 2. Nếu có quân cờ đang đứng, kiểm tra xem có phải ĐỒNG MINH không
                    if (neighborCell.OccupiedCard != null)
                    {
                        // SỬA TẠI ĐÂY: Dựa vào thuộc tính IsPlayerSpawnCell của chính ô đó để nhận diện phe
                        // Nếu ô đó là ô của Player (IsPlayerSpawnCell == true) và người đang đánh cũng là Player -> ĐỒNG MINH -> CHẶN
                        if (neighborCell.IsPlayerSpawnCell == isPlayerAttacking)
                        {
                            continue;
                        }
                    }

                    // Nếu hợp lệ, đánh dấu đã duyệt và đưa vào hàng đợi loang tiếp
                    visited.Add(neighborPos);
                    queue.Enqueue(new KeyValuePair<Vector2Int, int>(neighborPos, remainingRange - 1));

                    // Sơn màu hiển thị tầm đánh đỏ lên ô này (Ngoại trừ ô gốc của bản thân)
                    if (neighborPos != centerPos)
                    {
                        neighborCell.ShowAttackRange(AttackRangeMaterial);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Hiển thị tầm đánh và các Panel TargetUI tương ứng cho từng ô dựa theo loại Skill được click.
    /// </summary>
    /// <param name="centerPos">Vị trí quân cờ ra chiêu</param>
    /// <param name="attackRange">Tầm xa của skill</param>
    /// <param name="skillSubType">Mã loại skill (DAMAGE, HEAL, BUFF, DEBUFF, CLEANSE, CONTROL, SUMMON...)</param>
    /// <param name="isPlayerOwner">Quân cờ ra chiêu thuộc phe Player (true) hay Enemy (false)</param>
    public void ShowSkillAttackRange(Vector2Int centerPos, int attackRange, string skillSubType, bool isPlayerOwner)
    {
        // 1. Dọn dẹp sạch các hiển thị cũ (vùng di chuyển, tầm đánh thường...)
        ClearAllMovementRanges();
        if (attackRange <= 0) return;

        // Xác định mục tiêu hướng tới của Skill
        bool targetIsEnemy = (skillSubType == "DAMAGE" || skillSubType == "DEBUFF" || skillSubType == "CONTROL");
        bool targetIsAlly = (skillSubType == "HEAL" || skillSubType == "BUFF" || skillSubType == "CLEANSE");

        // Thuật toán BFS loang tìm phạm vi ô trong tầm skill
        Queue<KeyValuePair<Vector2Int, int>> queue = new Queue<KeyValuePair<Vector2Int, int>>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        queue.Enqueue(new KeyValuePair<Vector2Int, int>(centerPos, attackRange));
        visited.Add(centerPos);

        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        while (queue.Count > 0)
        {
            var currentData = queue.Dequeue();
            Vector2Int currentPos = currentData.Key;
            int remainingRange = currentData.Value;

            if (remainingRange <= 0) continue;

            foreach (Vector2Int dir in directions)
            {
                Vector2Int neighborPos = currentPos + dir;

                if (GridDict.TryGetValue(neighborPos, out GridCell neighborCell))
                {
                    if (visited.Contains(neighborPos)) continue;

                    // Không loang qua chướng ngại vật cứng
                    if (!neighborCell.IsWalkable) continue;

                    visited.Add(neighborPos);
                    queue.Enqueue(new KeyValuePair<Vector2Int, int>(neighborPos, remainingRange - 1));

                    // Bỏ qua ô gốc của bản thân, chỉ xét các ô xung quanh
                    if (neighborPos == centerPos) continue;

                    // Sơn nền đỏ hiển thị tầm đánh kỹ năng chung cho người chơi dễ nhìn
                    neighborCell.ShowAttackRange(AttackRangeMaterial);

                    // --- LOGIC PHÂN LOẠI PHE ĐỂ HIỂN THỊ TARGET UI TRÊN Ô CỜ ---
                    TargetUI cellTargetUI = neighborCell.GetComponent<TargetUI>();
                    if (cellTargetUI == null) continue;

                    // Kiểm tra xem ô hàng xóm này có quân cờ nào đang đứng không
                    if (neighborCell.OccupiedCard != null)
                    {
                        // Xác định phe của quân cờ đang đứng trên ô này bằng thuộc tính IsPlayerSpawnCell của ô
                        bool isTargetCellPlayer = neighborCell.IsPlayerSpawnCell;

                        if (targetIsEnemy)
                        {
                            // SKILL HẠI: Chỉ hiện UI nếu quân cờ trên ô ĐỐI LẬP phe với người ra chiêu (Kẻ địch)
                            if (isTargetCellPlayer != isPlayerOwner)
                            {
                                TriggerTargetUI(cellTargetUI, skillSubType);
                            }
                        }
                        else if (targetIsAlly)
                        {
                            // SKILL LỢI: Chỉ hiện UI nếu quân cờ trên ô CÙNG phe với người ra chiêu (Đồng minh)
                            if (isTargetCellPlayer == isPlayerOwner)
                            {
                                TriggerTargetUI(cellTargetUI, skillSubType);
                            }
                        }
                    }
                    else
                    {
                        // Trường hợp đặc biệt: Skill SUMMON (Triệu hồi) thường nhắm vào các ô TRỐNG
                        if (skillSubType == AppConstants.SkillType.SUMMON)
                        {
                            cellTargetUI.ShowSummonTargetPanelUI();
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Hàm phụ trợ kích hoạt chính xác Panel UI tương ứng trên TargetUI của ô cờ
    /// </summary>
    private void TriggerTargetUI(TargetUI targetUI, string skillSubType)
    {
        switch (skillSubType)
        {
            case AppConstants.SkillType.DAMAGE: targetUI.ShowDamageTargetPanelUI(); break;
            case AppConstants.SkillType.HEAL: targetUI.ShowHealTargetPanelUI(); break;
            case AppConstants.SkillType.BUFF: targetUI.ShowBuffTargetPanelUI(); break;
            case AppConstants.SkillType.DEBUFF: targetUI.ShowDebuffTargetPanelUI(); break;
            case AppConstants.SkillType.CLEANSE: targetUI.ShowCleanseTargetPanelUI(); break;
            case AppConstants.SkillType.CONTROL: targetUI.ShowControlTargetPanelUI(); break;
            case AppConstants.SkillType.SUMMON: targetUI.ShowSummonTargetPanelUI(); break;
        }
    }

    /// <summary>
    /// Tìm kiếm ô cờ GridCell đang chứa dữ liệu của thẻ bài CardBase được chỉ định
    /// </summary>
    public GridCell GetCellOfCard(CardBase card)
    {
        if (card == null) return null;

        // Duyệt qua tất cả các ô cờ đang được GridManager quản lý trong Dictionary
        foreach (var cell in GridDict.Values)
        {
            // Nếu ô cờ đó đang có quân cờ đứng và trùng khớp dữ liệu Instance/Id với quân cờ cần tìm
            if (cell.OccupiedCard != null && cell.OccupiedCard == card)
            {
                return cell;
            }
        }

        Debug.LogWarning($"[GridManager] Không tìm thấy quân cờ {card.Name} trên bất kỳ ô cờ nào!");
        return null;
    }

    public void SpawnDamagePopup(CardBase target, double amount, DamageTextType type = DamageTextType.Default, bool healing = false, float factor = 1f)
    {
        if (target == null || amount == 0) return;

        GameObject prefab = UIManager.Instance?.Get("FlyTextPrefab");
        if (prefab == null)
        {
            Debug.LogWarning("[GridManager] Không tìm thấy FlyTextPrefab từ UIManager.");
            return;
        }

        Transform spawnParent = null;
        GridCell targetCell = GetCellOfCard(target);
        if (targetCell != null)
        {
            spawnParent = targetCell.DamagePopupAnchor != null ? targetCell.DamagePopupAnchor : targetCell.transform;
        }
        else
        {
            spawnParent = GridParent != null ? GridParent : transform;
        }

        GameObject popupObj = Instantiate(prefab, spawnParent);
        FlyDamageText flyDamageText = popupObj.GetComponent<FlyDamageText>();
        if (flyDamageText != null)
        {
            flyDamageText.Init(amount, type, healing, factor);
        }
        else
        {
            Debug.LogWarning("[GridManager] FlyTextPrefab không có component FlyDamageText.");
        }
    }
}
