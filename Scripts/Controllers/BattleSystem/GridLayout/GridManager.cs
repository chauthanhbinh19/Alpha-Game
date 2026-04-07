using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public int width = 7;
    public int height = 7;

    public GridCell[,] grid;
    public List<GridCell> allCells = new List<GridCell>();

    private void Awake()
    {
        InitGrid();
    }

    void InitGrid()
    {
        grid = new GridCell[width, height];

        GridCell[] cells = GetComponentsInChildren<GridCell>();

        int index = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (index >= cells.Length) return;

                GridCell cell = cells[index];

                cell.Setup(index, new Vector2Int(x, y));

                grid[x, y] = cell;
                allCells.Add(cell);

                index++;
            }
        }
    }

    // Lấy cell theo tọa độ
    public GridCell GetCell(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
            return null;

        return grid[x, y];
    }

    // Kiểm tra có đi được không
    public bool CanMoveTo(int x, int y)
    {
        GridCell cell = GetCell(x, y);
        return cell != null && cell.IsWalkable;
    }
}