using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    [Header("Grid Settings")]
    public int width = 12;
    public int height = 12;
    public float cellSize = 1.1f;
    public GameObject cellPrefab;
    public Transform gridParent;

    [Header("Materials")]
    public Material EmptyPositionMaterial;
    public Material PlayerPositionMaterial;
    public Material EnemyPositionMaterial;

    private Dictionary<Vector2Int, GridCell> gridDict = new Dictionary<Vector2Int, GridCell>();

    [Header("Spawn Positions (10 Cells Each)")]
    public List<GridCell> playerSpawnCells = new List<GridCell>();
    public List<GridCell> enemySpawnCells = new List<GridCell>();

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
        
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        int idCounter = 0;
        int minX = -width / 2;
        int maxX = width / 2;
        int minZ = -height / 2;
        int maxZ = height / 2;

        for (int x = minX; x < maxX; x++)
        {
            for (int z = minZ; z < maxZ; z++)
            {
                GameObject newCellObj = Instantiate(cellPrefab, gridParent);
                newCellObj.name = $"Cell ({x}, {z})";
                newCellObj.transform.localPosition = new Vector3(x * cellSize, 0, z * cellSize);
                newCellObj.transform.localRotation = Quaternion.identity;

                GridCell cellScript = newCellObj.GetComponent<GridCell>();
                Vector2Int gridPosition = new Vector2Int(x, z);

                cellScript.Setup(idCounter, gridPosition); // Giữ nguyên hàm Setup cũ của bạn

                // --- BƯỚC 1: TẤT CẢ 144 Ô ĐỀU ĐƯỢC GÁN MATERIAL TRỐNG ---
                cellScript.SetDefaultMaterial(EmptyPositionMaterial);

                gridDict.Add(gridPosition, cellScript);
                idCounter++;
            }
        }

        // Sau khi tạo xong 12x12, tiến hành ghi đè màu cho các ô Spawn
        // SetupSpawnPositions(minX, maxX, minZ, maxZ);
        SetupRandomDoubleRowSpawnPositions(minX, maxX, minZ, maxZ);
    }

    // Hàm xóa hiển thị range của TẤT CẢ các ô trên bàn cờ
    public void ClearAllMovementRanges()
    {
        foreach (var cell in gridDict.Values)
        {
            cell.HideMovementRange();
        }
    }

    public void SetupSpawnPositions(int minX, int maxX, int minZ, int maxZ)
    {
        playerSpawnCells.Clear();
        enemySpawnCells.Clear();

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
                playerSpawnCells.Add(pCell);
                playerIndex++;
            }

            // 2. PHE ENEMY (Bây giờ lấy ở hàng DƯỚI CÙNG của ma trận)
            int enemyXRow = minX + 1;
            GridCell eCell = GetCellAt(enemyXRow, z);
            if (eCell != null && enemyIndex <= 10)
            {
                eCell.SetAsSpawnCell(enemyIndex, isPlayer: false, EnemyPositionMaterial);
                enemySpawnCells.Add(eCell);
                enemyIndex++;
            }
        }
    }

    public void SetupRandomDoubleRowSpawnPositions(int minX, int maxX, int minZ, int maxZ)
    {
        playerSpawnCells.Clear();
        enemySpawnCells.Clear();

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
            playerSpawnCells.Add(cell);
        }

        // Cấu hình ngẫu nhiên cho Enemy
        int enemyMaxCount = Mathf.Min(10, potentialEnemyCells.Count);
        for (int i = 0; i < enemyMaxCount; i++)
        {
            GridCell cell = potentialEnemyCells[i];
            int enemyIndex = i + 1; // Số từ 1 -> 10

            cell.SetAsSpawnCell(enemyIndex, isPlayer: false, EnemyPositionMaterial);
            enemySpawnCells.Add(cell);
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

    // Hàm tìm và hiển thị vùng di chuyển theo bán kính (Dùng thuật toán BFS)
    public void ShowMovementRangeAt(Vector2Int centerPos, int range)
    {
        // Bước 1: Xóa sạch các ô đang hiện từ trước để tránh bị đè
        ClearAllMovementRanges();

        if (range <= 0) return;

        // Bước 2: Thuật toán loang BFS để tìm các ô trong tầm
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, int> visited = new Dictionary<Vector2Int, int>(); // Lưu ô đã đi qua và khoảng cách của nó

        queue.Enqueue(centerPos);
        visited.Add(centerPos, 0);

        // 4 hướng di chuyển cơ bản (Lên, Xuống, Trái, Phải)
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),  // Lên
            new Vector2Int(0, -1), // Xuống
            new Vector2Int(-1, 0), // Trái
            new Vector2Int(1, 0)   // Phải
        };

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            int currentDistance = visited[current];

            // Nếu khoảng cách vượt quá MovementRange thì dừng loang hướng đó
            if (currentDistance >= range) continue;

            foreach (Vector2Int dir in directions)
            {
                Vector2Int neighborPos = current + dir;

                // Kiểm tra xem ô hàng xóm có tồn tại trên bàn cờ không
                if (gridDict.ContainsKey(neighborPos) && !visited.ContainsKey(neighborPos))
                {
                    GridCell neighborCell = gridDict[neighborPos];

                    // Điều kiện phụ (Tùy chọn): Bạn có thể chặn không cho hiện range nếu ô đó có chướng ngại vật hoặc có tướng khác đứng
                    // if (neighborCell.OccupiedCard != null) continue; 

                    visited.Add(neighborPos, currentDistance + 1);
                    queue.Enqueue(neighborPos);

                    // Bật ô xanh hiển thị lên!
                    neighborCell.ShowMovementRange();
                }
            }
        }
    }

    public GridCell GetCellAt(int x, int z)
    {
        Vector2Int pos = new Vector2Int(x, z);
        if (gridDict.TryGetValue(pos, out GridCell cell))
        {
            return cell;
        }
        return null;
    }
}