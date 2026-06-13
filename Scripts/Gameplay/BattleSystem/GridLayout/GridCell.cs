using TMPro;
using UnityEngine;

public enum CellEnvironment { Normal, Hazard, Structure }

public class GridCell : MonoBehaviour
{
    public int CellId { get; private set; }
    public Vector2Int GridPosition { get; private set; }
    public bool IsWalkable { get; set; } = true;
    public CellEnvironment EnvironmentType { get; private set; } = CellEnvironment.Normal;
    public int MainPosition { get; private set; } = 0; // Số ô từ 1-10 (0 là ô thường)
    public bool IsPlayerSpawnCell { get; private set; } // Phân biệt ô này của Player hay Enemy
    
    [Header("UI Components")]
    public Renderer Platform;
    public Renderer MovementPlatform;
    public TextMeshProUGUI GridNumberText;
    public Transform DisplayCardPanel;
    public CardHero OccupiedCard; // Dữ liệu quân cờ đang đứng tại đây

    private void Awake()
    {
        if (GridNumberText != null) GridNumberText.gameObject.SetActive(false);
    }

    // Hàm thiết lập Material mặc định ban đầu
    public void SetDefaultMaterial(Material defaultMat)
    {
        if (Platform != null && defaultMat != null)
        {
            Platform.material = defaultMat;
        }
    }

    // Hàm thiết lập ô này làm ô Spawn (Đồng thời đổi màu tương ứng)
    public void SetAsSpawnCell(int positionIndex, bool isPlayer, Material spawnMat)
    {
        MainPosition = positionIndex;
        IsPlayerSpawnCell = isPlayer;

        // Đổi màu sắc ô Spawn
        if (Platform != null && spawnMat != null)
        {
            Platform.material = spawnMat;
        }

        // Hiển thị số số thứ tự 1-10
        if (GridNumberText != null)
        {
            GridNumberText.gameObject.SetActive(true);
            GridNumberText.text = positionIndex.ToString();
        }
    }

    public void Setup(int id, Vector2Int position)
    {
        CellId = id;
        GridPosition = position;

        // Mặc định lúc vào game phải ẩn ô xanh đi
        HideMovementRange();
    }

    public void SetEnvironment(CellEnvironment type)
    {
        EnvironmentType = type;
        if (type == CellEnvironment.Structure)
        {
            IsWalkable = false; // Ô có công trình thì không đi vào được
        }
    }

    public void ShowMovementRange()
    {
        if (MovementPlatform != null) MovementPlatform.gameObject.SetActive(true);
    }

    public void HideMovementRange()
    {
        if (MovementPlatform != null) MovementPlatform.gameObject.SetActive(false);
    }
}