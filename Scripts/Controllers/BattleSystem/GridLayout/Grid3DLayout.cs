using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // chạy cả trong Editor
public class Grid3DLayout : MonoBehaviour
{
    [Header("Grid Settings")]
    public int columns = 5;        // số cột
    public float spacingX = 3f;  // khoảng cách ngang
    public float spacingZ = 3f;  // khoảng cách dọc

    [Header("Alignment")]
    public bool center = true;     // căn giữa grid

    private List<Transform> children = new List<Transform>();

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
        children.Clear();

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                children.Add(child);
        }

        int count = children.Count;
        if (count == 0 || columns <= 0) return;

        int rows = Mathf.CeilToInt((float)count / columns);

        float offsetX = 0;
        float offsetZ = 0;

        if (center)
        {
            offsetX = (columns - 1) * spacingX / 2f;
            offsetZ = (rows - 1) * spacingZ / 2f;
        }

        for (int i = 0; i < count; i++)
        {
            int row = i / columns;
            int col = i % columns;

            float x = col * spacingX - offsetX;
            float z = -row * spacingZ + offsetZ;

            children[i].localPosition = new Vector3(x, 0, z);
        }
    }
}