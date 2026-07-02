using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // chạy cả trong Editor
public class Grid3DLayout : MonoBehaviour
{
    [Header("Grid Settings")]
    public int Columns = 12;        // số cột
    public float SpacingX = 1f;  // khoảng cách ngang
    public float SpacingZ = 1f;  // khoảng cách dọc

    [Header("Alignment")]
    public bool Center = true;     // căn giữa grid

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
    // CORE
    // =========================
    void Rebuild()
    {
        Children.Clear();

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                Children.Add(child);
        }

        int count = Children.Count;
        if (count == 0 || Columns <= 0) return;

        int rows = Mathf.CeilToInt((float)count / Columns);

        float offsetX = 0;
        float offsetZ = 0;

        if (Center)
        {
            offsetX = (Columns - 1) * SpacingX / 2f;
            offsetZ = (rows - 1) * SpacingZ / 2f;
        }

        for (int i = 0; i < count; i++)
        {
            int row = i / Columns;
            int col = i % Columns;

            float x = col * SpacingX - offsetX;
            float z = -row * SpacingZ + offsetZ;

            Children[i].localPosition = new Vector3(x, 0, z);
        }
    }
}