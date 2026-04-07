using UnityEngine;

public class GridCellMaterialAssigner : MonoBehaviour
{
    public Material isWalkable1Material;
    public Material playerMaterial;
    public Material enemyMaterial;
    public Material isWalkable0Material;

    [ContextMenu("Assign Materials To All GridCells")]
    public void Assign()
    {
        var cells = GetComponentsInChildren<GridCell>(true);

        foreach (var cell in cells)
        {
            cell.isWalkable1Material = isWalkable1Material;
            cell.playerMaterial      = playerMaterial;
            cell.enemyMaterial       = enemyMaterial;
            cell.isWalkable0Material = isWalkable0Material;
            cell.Refresh();
        }
        
        Debug.Log($"Assigned materials to {cells.Length} GridCells");
    }
}