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
    public Renderer AttackPlatform;
    public TextMeshProUGUI GridNumberText;
    public Transform DisplayCardPanel;
    public CardBase OccupiedCard; // Dữ liệu quân cờ đang đứng tại đây

    // Biến phụ trợ cho Pathfinding
    [HideInInspector] public GridCell ParentNode;

    private Material DefaultMaterial;
    // Lưu trạng thái hiện tại của ô hiển thị tầm
    private Material CurrentRangeMaterial;
    public bool IsWalkableRange = false;
    public bool IsAttackRange =false;

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
            DefaultMaterial = defaultMat; // Ghi nhớ lại màu ô trống
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
            DefaultMaterial = spawnMat;
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
        if (Platform != null && DefaultMaterial != null)
        {
            Platform.material = DefaultMaterial;
        }
    }

    public void Setup(int id, Vector2Int position)
    {
        CellId = id;
        GridPosition = position;

        // Mặc định lúc vào game phải ẩn ô xanh đi
        HideMovementRange();
        HideAttackRange();
    }

    public void SetEnvironment(CellEnvironment type)
    {
        EnvironmentType = type;
        if (type == CellEnvironment.Structure)
        {
            IsWalkable = false; // Ô có công trình thì không đi vào được
        }
    }

    public void ShowMovementRange(Material rangeMat, bool isWalkableRange)
    {
        if (MovementPlatform != null)
        {
            MovementPlatform.gameObject.SetActive(true);
            MovementPlatform.material = rangeMat; // Đổi màu xanh hoặc màu cảnh báo thiếu điểm
            CurrentRangeMaterial = rangeMat;
            IsWalkableRange = isWalkableRange;
        }
    }

    public void HideMovementRange()
    {
        if (MovementPlatform != null)
        {
            MovementPlatform.gameObject.SetActive(false);
            CurrentRangeMaterial = null;
        }

        MovementWarningUI cellUI = GetComponent<MovementWarningUI>();
        if (cellUI != null)
        {
            cellUI.HideUI(); // Hoặc hàm tương tự để disable khung Panel warning của ô đó
        }
        
        IsWalkableRange = false;
    }

    // Đổi màu riêng cho ô ĐÍCH ĐẾN (Destination)
    public void SetAsDestination(Material destMat)
    {
        if (MovementPlatform != null)
        {
            MovementPlatform.gameObject.SetActive(true);
            MovementPlatform.material = destMat;
        }
    }

    // Đổi màu riêng cho các ô TRUNG GIAN trên đường đi
    public void SetAsPathNode(Material pathMat)
    {
        if (MovementPlatform != null)
        {
            MovementPlatform.gameObject.SetActive(true);
            MovementPlatform.material = pathMat;
        }
    }

    // Khôi phục lại màu loang ban đầu (dùng khi hủy lộ trình hoặc vẽ lại đường mới)
    public void ResetToRangeMaterial()
    {
        if (MovementPlatform != null && CurrentRangeMaterial != null)
        {
            MovementPlatform.material = CurrentRangeMaterial;
        }
    }
    /// <summary>
    /// Thay đổi material cho tấm nền chính dưới chân ô cờ
    /// </summary>
    public void SetPlatformMaterial(Material newMat)
    {
        if (Platform != null && newMat != null)
        {
            Platform.material = newMat;
        }
    }

    public void ShowAttackRange(Material attackMat)
    {
        if (AttackPlatform != null)
        {
            AttackPlatform.gameObject.SetActive(true);
            AttackPlatform.material = attackMat;
            IsAttackRange = true;
        }
    }

    public void HideAttackRange()
    {
        if (AttackPlatform != null)
        {
            AttackPlatform.gameObject.SetActive(false);
        }
        IsAttackRange = false;
    }
}