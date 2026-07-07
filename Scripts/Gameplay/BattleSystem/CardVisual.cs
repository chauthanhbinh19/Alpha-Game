using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardVisual : MonoBehaviour, IPointerClickHandler
{
    private MeshRenderer MeshRenderer;
    private CardBase CardData;

    [Header("Status Sliders")]
    public Slider HealthSlider;
    public Slider ManaSlider;
    public Slider ShieldSlider;
    public TextMeshProUGUI HealthRatioText;

    // Đoạn code này đặt trong script Quản lý tương tác chuột (ví dụ: BattleManager hoặc InputHandler)
    public enum ActionState { None, MoveMode, AttackMode }
    public ActionState CurrentState = ActionState.None;

    private CardBase SelectedCardData;
    private Vector2Int SelectedCardPos;

    void Awake()
    {
        // Lấy MeshRenderer gắn trên chính tấm Plane này
        MeshRenderer = GetComponent<MeshRenderer>();

        if (MeshRenderer == null)
        {
            Debug.LogError($"GameObject {gameObject.name} không có MeshRenderer! Hãy chắc chắn đây là một Plane.");
        }
    }

    /// <summary>
    /// Hàm nạp dữ liệu và đổi hình ảnh hiển thị cho thẻ bài
    /// </summary>
    public void SetupVisual(CardBase cardData)
    {
        if (cardData == null || MeshRenderer == null) return;

        this.CardData = cardData;

        Texture2D cardTexture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(cardData.Image));

        if (cardTexture != null)
        {
            MeshRenderer.material.SetTexture("_MainTex", cardTexture);

            // Debug.Log($"[Visual] Đã đổi ảnh thành công cho {cardData.Name} từ đường dẫn: {cardData.Image}");
        }
        else
        {
            Debug.LogError($"[Visual Error] Không tìm thấy ảnh tại đường dẫn: Assets/Resources/{cardData.Image}");
        }

        UpdateStatusSliders();
    }

    // Khi người chơi bấm vào nút "ATTACK" trên ActionMenu (như trong ảnh image_3cecc3.jpg của bạn)
    public void OnAttackButtonClicked()
    {
        if (SelectedCardData != null)
        {
            CurrentState = ActionState.AttackMode;
            // Bật hiển thị tầm đánh màu đỏ xung quanh quân cờ
            // int rangeDanh = selectedCardData.AttackRange; // Giả sử CardBase của bạn có thuộc tính AttackRange
            // GridManager.ShowAttackRangeAt(selectedCardPos, rangeDanh, selectedCardData);
        }
    }

    // Khi người chơi click chuột vào một ô GridCell bất kỳ trên bàn cờ trong chế độ AttackMode
    public void HandleGridCellClick(GridCell clickedCell)
    {
        if (CurrentState == ActionState.AttackMode)
        {
            // Nếu click trúng ô có kẻ địch đang đứng
            if (clickedCell.OccupiedCard != null)
            {
                // Thực hiện tấn công
                // gridManager.ExecuteAttack(selectedCardData, clickedCell);
                CurrentState = ActionState.None;
            }
        }
    }

    private void UpdateStatusSliders()
    {
        if (CardData == null)
        {
            return;
        }

        float healthRatio = GetRatio(CardData.CurrentHealth, CardData.Health);
        float manaRatio = GetRatio(CardData.CurrentMana, CardData.Mana);
        float shieldRatio = GetRatio(CardData.CurrentShieldStrength, CardData.ShieldStrength);

        if (HealthSlider != null)
        {
            HealthSlider.maxValue = 1f;
            HealthSlider.value = healthRatio;
        }

        if (ManaSlider != null)
        {
            ManaSlider.maxValue = 1f;
            ManaSlider.value = manaRatio;
        }

        if (ShieldSlider != null)
        {
            ShieldSlider.maxValue = 1f;
            ShieldSlider.value = shieldRatio;
        }

        if (HealthRatioText != null)
        {
            string multiplierText = CardData.Health > 0 ? $"x{(CardData.CurrentHealth / CardData.Health):0.##}" : "x0";
            HealthRatioText.text = $"x{multiplierText}";
        }
    }

    private float GetRatio(double current, double maximum)
    {
        if (maximum <= 0.0)
        {
            return 0f;
        }

        double ratio = current / maximum;
        if (double.IsFinite(ratio))
        {
            return Mathf.Clamp01((float)ratio);
        }

        return 0f;
    }

    // Hàm tự động kích hoạt khi người chơi CLICK vào Thẻ bài
    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardData == null || CardData.Class == null) return;

        // Lấy dữ liệu di chuyển và tầm đánh từ class data
        int movementRange = CardData.Class.MovementRange;
        int movementPoint = CardData.CurrentMovementPoint;
        int attackRange = CardData.Class.AttackRange; // Thuộc tính tầm đánh mới của bạn

        AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);

        // Tìm ô cờ chứa thẻ bài này
        GridCell currentCell = GetComponentInParent<GridCell>();

        if (currentCell != null)
        {
            Debug.Log($"Click vào {CardData.Name}. Mở Action Menu tại ô: {currentCell.GridPosition}");

            // Lấy vị trí click chuột hoặc vị trí của thẻ bài trên màn hình để đặt UI Menu
            Vector3 clickScreenPos = eventData.position;

            // Gọi ActionMenu hiển thị lên thay vì hiển thị Grid range ngay lập tức
            if (ActionMenuUI.Instance != null)
            {
                GridManager.Instance.ClearAllMovementRanges();
                GridManager.Instance.SelectCell(currentCell);
                ActionMenuUI.Instance.ShowMenu(currentCell, movementRange, movementPoint, attackRange, clickScreenPos, CardData);
            }
            else
            {
                Debug.LogError("Chưa đặt ActionMenuUI vào Scene hoặc chưa bật Instance!");
            }
        }
    }
}