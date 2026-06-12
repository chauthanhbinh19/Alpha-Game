using UnityEngine;

public enum CellEnvironment { Normal, Hazard, Structure }

public class GridCell : MonoBehaviour
{
    public int CellId { get; private set; }
    public Vector2Int GridPos { get; private set; }
    public bool IsWalkable { get; set; } = true;
    public CellEnvironment EnvironmentType { get; private set; } = CellEnvironment.Normal;

    // Lưu trữ quân cờ đang đứng trên ô này (nếu có) để quản lý chồng lấn
    public CardBase OccupiedCard { get; set; }

    public void Setup(int id, Vector2Int position)
    {
        CellId = id;
        GridPos = position;
    }

    public void SetEnvironment(CellEnvironment type)
    {
        EnvironmentType = type;
        if (type == CellEnvironment.Structure)
        {
            IsWalkable = false; // Ô có công trình thì không đi vào được
        }
    }
}