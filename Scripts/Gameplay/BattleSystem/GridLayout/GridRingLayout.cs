using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // 🔥 chạy cả trong Editor
public class GridRingLayout : MonoBehaviour
{
    [Header("Layout Settings")]
    public float Spacing = 3f;

    private List<Transform> Children = new List<Transform>();

    void Update()
    {
        Rebuild();
    }

    void OnTransformChildrenChanged()
    {
        Rebuild();
    }

    void OnValidate()
    {
        Rebuild();
    }

    // =========================
    // CORE: rebuild layout
    // =========================
    void Rebuild()
    {
        Children.Clear();

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                Children.Add(child);
        }

        for (int i = 0; i < Children.Count; i++)
        {
            Vector2Int pos = GetRingPosition(i);
            Children[i].localPosition = new Vector3(pos.x * Spacing, 0, pos.y * Spacing);
        }
    }

    // =========================
    // Tính vị trí theo ring
    // =========================
    Vector2Int GetRingPosition(int index)
    {
        if (index == 0)
            return Vector2Int.zero;

        int layer = 1;
        int count = 1;

        while (true)
        {
            int ringCount = layer * 8;

            if (index < count + ringCount)
            {
                int offset = index - count;

                int side = offset / (layer * 2);
                int pos = offset % (layer * 2);

                switch (side)
                {
                    case 0: return new Vector2Int(-layer + pos, -layer);
                    case 1: return new Vector2Int(layer, -layer + pos);
                    case 2: return new Vector2Int(layer - pos, layer);
                    case 3: return new Vector2Int(-layer, layer - pos);
                }
            }

            count += ringCount;
            layer++;
        }
    }
}