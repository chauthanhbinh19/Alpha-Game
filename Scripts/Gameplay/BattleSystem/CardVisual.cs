using UnityEngine;
using UnityEngine.EventSystems;

public class CardVisual : MonoBehaviour, IPointerClickHandler
{
    private MeshRenderer meshRenderer;
    private CardBase cardData;

    void Awake()
    {
        // Lấy MeshRenderer gắn trên chính tấm Plane này
        meshRenderer = GetComponent<MeshRenderer>();
        
        if (meshRenderer == null)
        {
            Debug.LogError($"GameObject {gameObject.name} không có MeshRenderer! Hãy chắc chắn đây là một Plane.");
        }
    }

    /// <summary>
    /// Hàm nạp dữ liệu và đổi hình ảnh hiển thị cho thẻ bài
    /// </summary>
    public void SetupVisual(CardBase cardData)
    {
        if (cardData == null || meshRenderer == null) return;

        this.cardData = cardData;

        Texture2D cardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(cardData.Image));

        if (cardTexture != null)
        {
            meshRenderer.material.SetTexture("_MainTex", cardTexture);
            
            Debug.Log($"[Visual] Đã đổi ảnh thành công cho {cardData.Name} từ đường dẫn: {cardData.Image}");
        }
        else
        {
            Debug.LogError($"[Visual Error] Không tìm thấy ảnh tại đường dẫn: Assets/Resources/{cardData.Image}");
        }
    }

    // Hàm tự động kích hoạt khi người chơi CLICK vào Thẻ bài này
    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardData == null || cardData.Class == null) return;

        // Lấy số ô có thể đi từ Data
        int moveRange = cardData.Class.MovementRange;

        // Tìm xem thẻ bài này đang nằm trên ô cờ (GridCell) nào
        // Vì ở file trước bạn Instantiate cardVisual làm CON của targetCell.displayCardPanel
        // Nên cha của cardVisual là displayCardPanel, và cha của displayCardPanel chính là GridCell
        GridCell currentCell = GetComponentInParent<GridCell>();

        if (currentCell != null)
        {
            Debug.Log($"Click vào {cardData.Name}. Vị trí ô: {currentCell.GridPosition}, Tầm đi: {moveRange}");

            // --- BƯỚC THÊM MỚI: Đổi màu nền flatform của ô cờ này thành màu Selected ---
            GridManager.Instance.SelectCell(currentCell);
            
            // Gọi GridManager xóa hết range cũ và vẽ range mới xung quanh ô này
            GridManager.Instance.ShowMovementRangeAt(currentCell.GridPosition, moveRange);
        }
    }
}