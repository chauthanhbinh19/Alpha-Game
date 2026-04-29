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
    public CardController CurrentCard;
    public bool IsOccupied => CurrentCard != null;

    // ================= RENDERERS =================
    [Header("Renderers (Toggle by SetActive)")]
    public GameObject walkableRenderer;
    public GameObject blockedRenderer;
    public GameObject playerRenderer;
    public GameObject enemyRenderer;

    // (Optional) list để tắt nhanh
    private List<GameObject> allRenderers;

    // ================= TEAM DISPLAY =================
    [Header("Team Display (Auto Found)")]
    private TextMeshProUGUI teamText;
    private GameObject teamCanvas;

    // ================= INIT =================
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
    }

    private void FindTeamDisplay()
    {
        // Tự tìm nếu chưa gán
        if (teamCanvas == null)
        {
            teamCanvas = transform.Find("TeamCanvas")?.gameObject;
        }

        if (teamText == null && teamCanvas != null)
        {
            teamText = teamCanvas.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void Refresh()
    {
        UpdateVisual();
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
        UpdateVisual();
    }

    public void SetWalkable(bool isWalkable)
    {
        IsWalkable = isWalkable;
        UpdateVisual();
    }

    public void SetTeam(int _teamNumber)
    {
        teamNumber = _teamNumber;
        UpdateTeamDisplay();
    }

    // ================= VISUAL =================
    private void DisableAllRenderers()
    {
        if (allRenderers == null) return;

        foreach (var obj in allRenderers)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    private void UpdateVisual()
    {
        DisableAllRenderers();

        // 1. Blocked ưu tiên cao nhất
        if (!IsWalkable)
        {
            if (blockedRenderer != null)
                blockedRenderer.SetActive(true);
            return;
        }

        // 2. Xét loại cell
        switch (CellType)
        {
            case CellType.Player:
                if (playerRenderer != null)
                    playerRenderer.SetActive(true);
                break;

            case CellType.Enemy:
                if (enemyRenderer != null)
                    enemyRenderer.SetActive(true);
                break;

            case CellType.Empty:
            default:
                if (walkableRenderer != null)
                    walkableRenderer.SetActive(true);
                break;
        }
    }

    // ================= TEAM UI =================
    private void UpdateTeamDisplay()
    {
        bool shouldShow = (teamNumber != 0);

        if (teamCanvas != null)
            teamCanvas.SetActive(shouldShow);

        if (teamText != null && shouldShow)
            teamText.text = teamNumber.ToString();
    }

    // ================= OCCUPY =================
    public void OccupyCell(CardController card)
    {
        CurrentCard = card;

        // Set type theo team
        SetType(card.CardData.MainPosition == 1 
            ? CellType.Player 
            : CellType.Enemy);
    }

    public void ClearCell()
    {
        CurrentCard = null;
        SetType(CellType.Empty);
    }
}