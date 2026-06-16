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

            // Debug.Log($"[Visual] Đã đổi ảnh thành công cho {cardData.Name} từ đường dẫn: {cardData.Image}");
        }
        else
        {
            Debug.LogError($"[Visual Error] Không tìm thấy ảnh tại đường dẫn: Assets/Resources/{cardData.Image}");
        }
    }

    // Hàm tự động kích hoạt khi người chơi CLICK vào Thẻ bài
    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardData == null || cardData.Class == null) return;

        // Lấy dữ liệu di chuyển và tầm đánh từ class data
        int movementRange = cardData.Class.MovementRange;
        int movementPoint = cardData.CurrentMovementPoint;
        int attackRange = cardData.Class.AttackRange; // Thuộc tính tầm đánh mới của bạn

        AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);

        // Tìm ô cờ chứa thẻ bài này
        GridCell currentCell = GetComponentInParent<GridCell>();

        if (currentCell != null)
        {
            Debug.Log($"Click vào {cardData.Name}. Mở Action Menu tại ô: {currentCell.GridPosition}");

            // Lấy vị trí click chuột hoặc vị trí của thẻ bài trên màn hình để đặt UI Menu
            Vector3 clickScreenPos = eventData.position; 

            // Gọi ActionMenu hiển thị lên thay vì hiển thị Grid range ngay lập tức
            if (ActionMenuUI.Instance != null)
            {
                GridManager.Instance.SelectCell(currentCell);
                ActionMenuUI.Instance.ShowMenu(currentCell, movementRange, movementPoint, attackRange, clickScreenPos, cardData);
            }
            else
            {
                Debug.LogError("Chưa đặt ActionMenuUI vào Scene hoặc chưa bật Instance!");
            }
        }
    }
}