using UnityEngine;

public enum CellType
{
    Empty,
    Player,
    Enemy
}

public class GridCell : MonoBehaviour
{
    public int ID;
    public Vector2Int GridPos;

    public CellType CellType;
    public bool IsWalkable = true;

    public void Setup(int id, Vector2Int pos)
    {
        ID = id;
        GridPos = pos;
    }

    public void SetType(CellType type)
    {
        CellType = type;
    }
}