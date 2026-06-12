using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 12;
    public int height = 12;
    public float cellSize = 1.1f; // Khoảng cách giữa các ô (tránh bị khít quá)
    public GameObject cellPrefab;  // Kéo Prefab ô cờ chứa script GridCell vào đây

    // Lưu trữ toàn bộ các ô để dễ dàng truy xuất bằng Tọa độ
    private Dictionary<Vector2Int, GridCell> gridDict = new Dictionary<Vector2Int, GridCell>();

    [Header("Spawn Positions")]
    public List<GridCell> playerSpawnCells = new List<GridCell>();
    public List<GridCell> enemySpawnCells = new List<GridCell>();

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        int idCounter = 0;
        
        // Tính toán độ lệch để gốc tọa độ (0,0,0) nằm chính giữa bàn cờ
        // Với 12x12, tọa độ Grid sẽ chạy từ -6 đến 5
        int minX = -width / 2;
        int maxX = width / 2;
        int minZ = -height / 2;
        int maxZ = height / 2;

        for (int x = minX; x < maxX; x++)
        {
            for (int z = minZ; z < maxZ; z++)
            {
                // Tính tọa độ Vector3 thực tế trong không gian Unity (Y mặc định = 0)
                Vector3 spawnPos = new Vector3(x * cellSize, 0, z * cellSize);
                
                // Sinh ra ô cờ
                GameObject newCellObj = Instantiate(cellPrefab, spawnPos, Quaternion.identity, this.transform);
                newCellObj.name = $"Cell ({x}, {z})";

                GridCell cellScript = newCellObj.GetComponent<GridCell>();
                
                // Vector2Int lưu tọa độ Grid (X, Z). Trong Vector2Int, ta dùng .y thay cho Z
                Vector2Int gridPosition = new Vector2Int(x, z);
                
                cellScript.Setup(idCounter, gridPosition);

                // --- VÍ DỤ TẠO ĐỊA HÌNH NGẪU NHIÊN ---
                if (x == -2 && z == -2) cellScript.SetEnvironment(CellEnvironment.Hazard);
                if (x == 3 && z == 3) cellScript.SetEnvironment(CellEnvironment.Structure);

                // Lưu vào Dictionary để tìm kiếm nhanh sau này
                gridDict.Add(gridPosition, cellScript);
                idCounter++;
            }
        }

        SetupSpawnPositions(minX, maxX, minZ, maxZ);
    }

    // Chọn ra 10 vị trí đặt Card cho mỗi phe
    void SetupSpawnPositions(int minX, int maxX, int minZ, int maxZ)
    {
        // Thuật toán ví dụ: Lấy 2 hàng cuối cùng của mỗi bên làm khu vực chọn vị trí
        // Phe mình (Z âm): Hàng z = -6 và z = -5
        // Phe địch (Z dương): Hàng z = 5 và z = 4

        foreach (var kvp in gridDict)
        {
            GridCell cell = kvp.Value;

            // Tìm 10 ô cho Player (Z nằm ở sâu bên phía âm)
            if (cell.GridPos.y <= -5 && playerSpawnCells.Count < 10)
            {
                if (cell.IsWalkable) playerSpawnCells.Add(cell);
            }

            // Tìm 10 ô cho Enemy (Z nằm ở sâu bên phía dương)
            if (cell.GridPos.y >= 4 && enemySpawnCells.Count < 10)
            {
                if (cell.IsWalkable) enemySpawnCells.Add(cell);
            }
        }

        Debug.Log($"Đã thiết lập {playerSpawnCells.Count} ô nạp quân ta và {enemySpawnCells.Count} ô nạp quân địch.");
    }

    // Hàm tiện ích giúp lấy nhanh 1 ô bất kỳ dựa trên tọa độ Grid (X, Z)
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