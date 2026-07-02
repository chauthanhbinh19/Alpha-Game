using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackConfirmUI : MonoBehaviour
{
    // Instance toàn cục trỏ đến UI xác nhận tấn công đang được mở
    public static AttackConfirmUI Instance { get; private set; }

    [Header("UI Components")]
    public GameObject ActionPanel; // Khung chứa 2 nút bấm (Đồng ý / Hủy)
    public Button AgreeButton;
    public Button DisagreeButton;

    private GridCell AttackerCell; // Ô của quân ta (Người tấn công)
    private GridCell TargetCell;   // Ô của quân địch (Mục tiêu bị tấn công)

    void Start()
    {
        // Ẩn panel khi mới vào game
        if (ActionPanel != null) ActionPanel.SetActive(false);

        // Gán sự kiện cho nút bấm
        AgreeButton.onClick.RemoveAllListeners();
        DisagreeButton.onClick.RemoveAllListeners();
        AgreeButton.onClick.AddListener(OnAgreePressed);
        DisagreeButton.onClick.AddListener(OnDisagreePressed);
    }

    /// <summary>
    /// Kích hoạt hiển thị UI xác nhận tấn công
    /// </summary>
    public void ShowUI(bool state, GridCell attacker = null, GridCell target = null)
    {
        if (state)
        {
            // Nếu có một UI tấn công khác đang mở, ẩn nó đi trước
            if (Instance != null && Instance != this)
            {
                Instance.ShowUI(false);
            }
            Instance = this;

            // Lưu lại thông tin 2 ô cờ tham gia trận đánh
            this.AttackerCell = attacker;
            this.TargetCell = target;
        }

        if (ActionPanel != null) ActionPanel.SetActive(state);
    }

    void OnAgreePressed()
    {
        ShowUI(false);

        if (AttackerCell != null && TargetCell != null)
        {
            // Kích hoạt tiến trình xử lý trận đánh
            StartCoroutine(ExecuteAttackRoutine(AttackerCell, TargetCell));
        }
    }

    void OnDisagreePressed()
    {
        ShowUI(false);
        // Hủy chọn: Xóa các vùng đỏ/xanh hiển thị nhưng giữ lại trạng thái ô cờ
        GridManager.Instance.ClearAllMovementRanges();
    }

    IEnumerator ExecuteAttackRoutine(GridCell attacker, GridCell target)
    {
        CardBase attackerCard = attacker.OccupiedCard;
        CardBase targetCard = target.OccupiedCard;

        if (attackerCard == null || targetCard == null) yield break;

        Debug.Log($"[Battle] {attackerCard.Name} bắt đầu tấn công {targetCard.Name}!");

        // 1. Chạy âm thanh tấn công (SFX)
        // AudioManager.Instance.PlaySFX(AudioConstants.SFX.ATTACK_SOUND);

        // 2. Chờ một chút giả lập thời gian diễn ra hoạt ảnh (Animation) đòn đánh
        yield return new WaitForSeconds(0.5f);

        // 3. XỬ LÝ LOGIC TRẬN ĐẤU (Ví dụ cơ bản: Trừ máu)
        // Giả sử CardBase của bạn có các thuộc tính như Atk (Tấn công) và Hp (Máu hiện tại)
        // targetCard.CurrentHp -= attackerCard.Atk;
        
        Debug.Log($"[Battle] {attackerCard.Name} gây sát thương lên {targetCard.Name}!");

        // Kiểm tra nếu mục tiêu tử trận (Hp <= 0)
        /*
        if (targetCard.CurrentHp <= 0)
        {
            Debug.Log($"[Battle] {targetCard.Name} đã bị tiêu diệt!");
            
            // Xóa quân cờ khỏi ô đích
            target.occupiedCard = null;
            
            // Tìm và hủy GameObject CardVisual của quân cờ đó
            CardVisual enemyVisual = target.displayCardPanel.GetComponentInChildren<CardVisual>();
            if (enemyVisual != null) Destroy(enemyVisual.gameObject);
        }
        */

        // 4. DỌN DẸP CHIẾN TRƯỜNG: Xóa sạch các vùng loang màu đỏ tấn công trên lưới
        GridManager.Instance.ClearAllMovementRanges();
    }
}