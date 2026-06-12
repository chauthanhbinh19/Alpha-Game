using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CellType
{
    Empty,
    Player,
    Enemy
}

// === THÊM THUỘC TÍNH MÔI TRƯỜNG ===
public enum CellEnvironment
{
    Normal,     // Ô bình thường
    Structure,  // Công trình (Nhà, Tháp, rào chắn...)
    Hazard,      // Đầm lầy (Giảm tốc độ, không cho đi qua tùy bạn)
    Obstacle    // Vật cản không thể đi qua
}

public class GridCell : MonoBehaviour
{
    public int ID;
    public Vector2Int GridPos; // Tọa độ Grid (Ví dụ: x: -6, z: 5)
    public int teamNumber;

    public CellType CellType;
    public CellEnvironment EnvironmentType = CellEnvironment.Normal; // Địa hình ô
    
    public bool IsWalkable = true;
    public CardController CurrentCard;
    public bool IsOccupied => CurrentCard != null;

    // Kiểm tra xem ô này có thể đặt công trình hoặc vật thể khác lên không
    public bool CanPlaceStructure => IsWalkable && !IsOccupied && EnvironmentType == CellEnvironment.Normal;

    // ================= RENDERERS =================
    [Header("Renderers (Toggle by SetActive)")]
    public GameObject walkableRenderer;
    public GameObject blockedRenderer;
    public GameObject playerRenderer;
    public GameObject enemyRenderer;
    
    [Header("Environment Renderers")]
    public GameObject structureRenderer; // Render cho công trình
    public GameObject swampRenderer;     // Render cho đầm lầy

    private List<GameObject> allRenderers;

    // ================= TEAM DISPLAY =================
    [Header("Team Display (Auto Found)")]
    private TextMeshProUGUI teamText;
    private GameObject teamCanvas;

    private void Awake()
    {
        CacheRenderers();
        FindTeamDisplay();
        UpdateVisual();
        UpdateTeamDisplay();
    }

    private void OnValidate()
    {
        CacheRenderers();
        UpdateVisual();
        UpdateTeamDisplay();
    }

    private void CacheRenderers()
    {
        allRenderers = new List<GameObject>();

        if (walkableRenderer != null) allRenderers.Add(walkableRenderer);
        if (blockedRenderer != null) allRenderers.Add(blockedRenderer);
        if (playerRenderer != null) allRenderers.Add(playerRenderer);
        if (enemyRenderer != null) allRenderers.Add(enemyRenderer);
        if (structureRenderer != null) allRenderers.Add(structureRenderer);
        if (swampRenderer != null) allRenderers.Add(swampRenderer);
    }

    private void FindTeamDisplay()
    {
        if (teamCanvas == null) teamCanvas = transform.Find("TeamCanvas")?.gameObject;
        if (teamText == null && teamCanvas != null) teamText = teamCanvas.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setup(int id, Vector2Int pos)
    {
        ID = id;
        GridPos = pos;
        
        // Tự động gán Team dựa trên tọa độ Z (Z âm là Team 1, Z dương là Team 2)
        if (pos.y < 0) SetTeam(1);      // Team Mình (Z âm)
        else if (pos.y > 0) SetTeam(2); // Team Địch (Z dương)
        else SetTeam(0);                // Đường giữa (Z = 0)
    }

    public void SetEnvironment(CellEnvironment envType)
    {
        EnvironmentType = envType;
        
        // Nếu là vật cản hoặc công trình lớn, có thể tự động khóa không cho đi qua
        if (envType == CellEnvironment.Obstacle || envType == CellEnvironment.Structure)
        {
            IsWalkable = false;
        }
        else
        {
            IsWalkable = true;
        }
        
        UpdateVisual();
    }

    public void SetType(CellType type) { CellType = type; UpdateVisual(); }
    public void SetWalkable(bool isWalkable) { IsWalkable = isWalkable; UpdateVisual(); }
    public void SetTeam(int _teamNumber) { teamNumber = _teamNumber; UpdateTeamDisplay(); }

    private void DisableAllRenderers()
    {
        if (allRenderers == null) return;
        foreach (var obj in allRenderers) { if (obj != null) obj.SetActive(false); }
    }

    public void UpdateVisual()
    {
        DisableAllRenderers();

        // 1. Kiểm tra môi trường trước
        if (EnvironmentType == CellEnvironment.Structure && structureRenderer != null)
        {
            structureRenderer.SetActive(true);
            return;
        }
        if (EnvironmentType == CellEnvironment.Hazard && swampRenderer != null)
        {
            swampRenderer.SetActive(true);
            return;
        }

        // 2. Kiểm tra Walkable thông thường
        if (!IsWalkable)
        {
            if (blockedRenderer != null) blockedRenderer.SetActive(true);
            return;
        }

        // 3. Xét loại Card đang đứng chiếm ô
        switch (CellType)
        {
            case CellType.Player:
                if (playerRenderer != null) playerRenderer.SetActive(true);
                break;
            case CellType.Enemy:
                if (enemyRenderer != null) enemyRenderer.SetActive(true);
                break;
            case CellType.Empty:
            default:
                if (walkableRenderer != null) walkableRenderer.SetActive(true);
                break;
        }
    }

    private void UpdateTeamDisplay()
    {
        bool shouldShow = (teamNumber != 0);
        if (teamCanvas != null) teamCanvas.SetActive(shouldShow);
        if (teamText != null && shouldShow) teamText.text = "T" + teamNumber;
    }

    public void OccupyCell(CardController card)
    {
        CurrentCard = card;
        // Giả sử card.CardData.MainPosition == 1 là phe mình
        SetType(card.CardData.MainPosition == 1 ? CellType.Player : CellType.Enemy);
    }

    public void ClearCell()
    {
        CurrentCard = null;
        SetType(CellType.Empty);
    }
}