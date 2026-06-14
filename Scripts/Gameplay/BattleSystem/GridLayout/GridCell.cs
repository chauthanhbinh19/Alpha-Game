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
    public CardBase OccupiedCard; // Dữ liệu quân cờ đang đứng tại đây

    private Material defaultMaterial;

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
            defaultMaterial = defaultMat; // Ghi nhớ lại màu ô trống
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
            defaultMaterial = spawnMat;
        }

        // Hiển thị số số thứ tự 1-10
        if (GridNumberText != null)
        {
            GridNumberText.gameObject.SetActive(true);
            GridNumberText.text = positionIndex.ToString();
        }
    }

    // --- CÁC HÀM THÊM MỚI ĐỂ PHỤC VỤ CHO LOGIC CLICK ĐỔI MÀU ---

    /// <summary>
    /// Hàm đổi màu tạm thời (Ví dụ đổi sang SelectedPositionMaterial khi click)
    /// </summary>
    public void ChangeRuntimeMaterial(Material targetMat)
    {
        if (Platform != null && targetMat != null)
        {
            Platform.material = targetMat;
        }
    }

    /// <summary>
    /// Hàm khôi phục lại màu sắc ban đầu (Dù là ô trống hay ô Spawn đều trả về đúng màu gốc)
    /// </summary>
    public void ResetToDefaultMaterial()
    {
        if (Platform != null && defaultMaterial != null)
        {
            Platform.material = defaultMaterial;
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