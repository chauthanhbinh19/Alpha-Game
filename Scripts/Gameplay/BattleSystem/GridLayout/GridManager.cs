using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 12;
    public int height = 12;
    public float cellSize = 1.1f; 
    public GameObject cellPrefab;  

    private Dictionary<Vector2Int, GridCell> gridDict = new Dictionary<Vector2Int, GridCell>();

    [Header("Spawn Positions (10 Cells Each)")]
    public List<GridCell> playerSpawnCells = new List<GridCell>();
    public List<GridCell> enemySpawnCells = new List<GridCell>();

    void Awake()
    {
        GenerateGrid();
    }

    void GenerateGrid()
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
                Vector3 spawnPos = new Vector3(x * cellSize, 0, z * cellSize);
                GameObject newCellObj = Instantiate(cellPrefab, spawnPos, Quaternion.identity, this.transform);
                newCellObj.name = $"Cell ({x}, {z})";

                GridCell cellScript = newCellObj.GetComponent<GridCell>();
                Vector2Int gridPosition = new Vector2Int(x, z);
                
                cellScript.Setup(idCounter, gridPosition);

                // Tạo vài địa hình mẫu
                if (x == -2 && z == -2) cellScript.SetEnvironment(CellEnvironment.Hazard);
                if (x == 3 && z == 3) cellScript.SetEnvironment(CellEnvironment.Structure);

                gridDict.Add(gridPosition, cellScript);
                idCounter++;
            }
        }

        SetupSpawnPositions(minX, maxX, minZ, maxZ);
    }

    void SetupSpawnPositions(int minX, int maxX, int minZ, int maxZ)
    {
        playerSpawnCells.Clear();
        enemySpawnCells.Clear();

        // Lấy 10 ô chính giữa đối xứng để đội hình 2 bên nhìn thẳng vào nhau cân đối
        int startX = minX + 1; // -5
        int endX = maxX - 1;   // 5

        for (int x = startX; x < endX; x++)
        {
            // Phe mình: Hàng dưới cùng (z = -6)
            GridCell pCell = GetCellAt(x, minZ);
            if (pCell != null && pCell.IsWalkable && playerSpawnCells.Count < 10)
            {
                playerSpawnCells.Add(pCell);
            }

            // Phe địch: Hàng trên cùng (z = 5)
            GridCell eCell = GetCellAt(x, maxZ - 1);
            if (eCell != null && eCell.IsWalkable && enemySpawnCells.Count < 10)
            {
                enemySpawnCells.Add(eCell);
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