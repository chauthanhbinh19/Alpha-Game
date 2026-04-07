using UnityEngine;

public class GridCellAutoAdder : MonoBehaviour
{
    [ContextMenu("Add GridCell To All Children")]
    public void AddGridCellToAllChildren()
    {
        int count = 0;

        // lấy tất cả Transform con (kể cả inactive)
        var transforms = GetComponentsInChildren<Transform>(true);

        foreach (var t in transforms)
        {
            // bỏ qua chính object cha (Map)
            if (t == transform) continue;

            // nếu chưa có thì add
            if (t.GetComponent<GridCell>() == null)
            {
                t.gameObject.AddComponent<GridCell>();
                count++;
            }
        }

        Debug.Log($"Added GridCell to {count} objects");
    }
    [ContextMenu("Remove GridCell From All Children")]
    public void RemoveGridCellFromAllChildren()
    {
        int count = 0;

        var transforms = GetComponentsInChildren<Transform>(true);

        foreach (var t in transforms)
        {
            if (t == transform) continue;

            var cell = t.GetComponent<GridCell>();
            if (cell != null)
            {
#if UNITY_EDITOR
                DestroyImmediate(cell); // dùng trong Editor
#else
                    Destroy(cell); // dùng khi play
#endif
                count++;
            }
        }

        Debug.Log($"Removed GridCell from {count} objects");
    }
}