using System.Collections.Generic;
using TMPro;
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
    public CardController CurrentCard; // Card đang đứng tại ô này
    public bool IsOccupied => CurrentCard != null; // Kiểm tra ô có người chưa

    [Header("Visual")]
    public Material isWalkable1Material; // walkable
    public Material playerMaterial;
    public Material enemyMaterial;   // enemy
    public Material isWalkable0Material;  // default / blocked

    // Thêm vào phần khai báo biến
    [Header("Team Display (Auto Found)")]
    private TextMeshProUGUI teamText;
    private GameObject teamCanvas;

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
        UpdateTeamDisplay();
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

        // 🔥 TỰ ĐỘNG TÌM THEO CẤU TRÚC: Canvas/PositionText
        // transform.Find sẽ tìm chính xác theo đường dẫn từ Object cha
        Transform canvasTransform = transform.Find("Canvas");
        if (canvasTransform != null)
        {
            teamCanvas = canvasTransform.gameObject;

            Transform textTransform = canvasTransform.Find("PositionText");
            if (textTransform != null)
            {
                teamText = textTransform.GetComponent<TextMeshProUGUI>();
            }
        }
    }

    // ================= CẬP NHẬT HIỂN THỊ =================
    public void SetTeam(int _teamNumber)
    {
        teamNumber = _teamNumber;
        UpdateTeamDisplay();
    }

    private void UpdateTeamDisplay()
    {
        // Tìm lại nếu chưa có (phòng trường hợp Refresh/OnValidate)
        if (teamCanvas == null) FindPlatform();

        bool shouldShow = (teamNumber != 0);

        if (teamCanvas != null)
        {
            teamCanvas.SetActive(shouldShow);
        }

        if (teamText != null && shouldShow)
        {
            teamText.text = teamNumber.ToString();
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

        // 1. Nếu không thể đi (Blocked) -> Ưu tiên hiện màu đỏ/đen ngay lập tức
        if (!IsWalkable)
        {
            ApplyMaterial(isWalkable0Material);
            return;
        }

        // 2. Nếu có thể đi (IsWalkable == true) -> Xét tiếp loại Cell
        Material mat;
        switch (CellType)
        {
            case CellType.Enemy:
                mat = enemyMaterial;
                break;
            case CellType.Player:
                mat = playerMaterial;
                break;
            case CellType.Empty:
            default:
                mat = isWalkable1Material;
                break;
        }

        ApplyMaterial(mat);
    }

    // Hàm để Card "nhập hộ khẩu" vào ô
    public void OccupyCell(CardController card)
    {
        CurrentCard = card;
        // Cập nhật CellType dựa trên team của Card nếu cần
        SetType(card.CardData.MainPosition == 1 ? CellType.Player : CellType.Enemy);
    }

    public void ClearCell()
    {
        CurrentCard = null;
        SetType(CellType.Empty);
    }
}