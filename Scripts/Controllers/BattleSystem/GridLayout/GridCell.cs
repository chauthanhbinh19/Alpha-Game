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

    [Header("Object")]
    public Renderer platform;
    public Renderer basePlatform;

    [Header("Material")]
    public Material isWalkable1Material; // walkable
    public Material playerMaterial;
    public Material enemyMaterial;   // enemy
    public Material isWalkable0Material;  // default / blocked

    // Thêm vào phần khai báo biến
    [Header("Team Display (Auto Found)")]
    private TextMeshProUGUI teamText;
    private GameObject teamCanvas;

    // ================= INIT =================
    private void Awake()
    {
        // LoadMaterials();
        // UpdateMaterial();
    }

    private void OnValidate()
    {
        // LoadMaterials();
        UpdateMaterial();
        UpdateTeamDisplay();
    }

    public void Refresh()
    {
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

    // ================= CẬP NHẬT HIỂN THỊ =================
    public void SetTeam(int _teamNumber)
    {
        teamNumber = _teamNumber;
        UpdateTeamDisplay();
    }

    private void UpdateTeamDisplay()
    {
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

    private void ApplyMaterial(Material mat)
    {
        platform.material = mat;
    }

    private void UpdateMaterial()
    {
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