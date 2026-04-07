using System.Collections.Generic;
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
    public int teamNumber;

    public CellType CellType;
    public bool IsWalkable = true;

    [Header("Visual")]
    public Material isWalkable1Material; // walkable
    public Material playerMaterial;
    public Material enemyMaterial;   // enemy
    public Material isWalkable0Material;  // default / blocked

    // cache renderer của platform
    private List<Renderer> platformRenderers = new List<Renderer>();

    // ================= INIT =================
    private void Awake()
    {
        // LoadMaterials();
        FindPlatform();
        UpdateMaterial();
    }

    private void OnValidate()
    {
        // LoadMaterials();
        FindPlatform();
        UpdateMaterial();
    }

    public void Refresh()
    {
        FindPlatform();
        UpdateMaterial();
    }

    // ================= SET DATA =================
    public void Setup(int id, Vector2Int pos)
    {
        ID = id;
        GridPos = pos;
    }

    public void SetType(CellType type)
    {
        CellType = type;
        UpdateMaterial();
    }

    public void SetWalkable(bool isWalkable)
    {
        IsWalkable = isWalkable;
        UpdateMaterial();
    }

    // ================= FIND PLATFORM =================
    private void FindPlatform()
    {
        platformRenderers.Clear();

        var renderers = GetComponentsInChildren<Renderer>(true);

        foreach (var r in renderers)
        {
            // 🔥 Cách 1: dùng name (đơn giản)
            if (r.gameObject.name.Contains("Platform"))
            {
                platformRenderers.Add(r);
            }

            // 🔥 Nếu bạn dùng Tag thì dùng cái này (tốt hơn)
            // if (r.CompareTag("Platform"))
            // {
            //     platformRenderers.Add(r);
            // }
        }
    }

    // ================= MATERIAL =================
    // private void LoadMaterials()
    // {
    //     if (MaterialManager.Instance == null) return;

    //     // isWalkable1Material = MaterialManager.Instance.Get("Is_Walkable_1_Mat");
    //     // playerMaterial = MaterialManager.Instance.Get("Player_Mat");
    //     // enemyMaterial = MaterialManager.Instance.Get("Enemy_Mat");
    //     // isWalkable0Material = MaterialManager.Instance.Get("Is_Walkable_0_Mat");
    //     isWalkable1Material = Resources.Load<Material>("Main Feature/Materials/Platform/Is_Walkable_1_Mat");
    //     playerMaterial = Resources.Load<Material>("Main Feature/Materials/Platform/Player_Mat");
    //     enemyMaterial = Resources.Load<Material>("Main Feature/Materials/Platform/Enemy_Mat");
    //     isWalkable0Material = Resources.Load<Material>("Main Feature/Materials/Platform/Is_Walkable_0_Mat");
    // }

    private void ApplyMaterial(Material mat)
    {
        foreach (var r in platformRenderers)
        {
            if (r != null)
                r.sharedMaterial = mat;
        }
    }

    private void UpdateMaterial()
    {
        if (platformRenderers == null || platformRenderers.Count == 0)
            return;

        // ưu tiên IsWalkable
        if (!IsWalkable)
        {
            ApplyMaterial(isWalkable0Material);
            return;
        }

        Material mat = !IsWalkable
        ? isWalkable0Material
        : CellType switch
        {
            CellType.Enemy => enemyMaterial,
            CellType.Player => playerMaterial,
            CellType.Empty => isWalkable1Material,
            _ => isWalkable1Material
        };

        ApplyMaterial(mat);
    }
}