using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MovementConfirmUI : MonoBehaviour
{
    // Instance toàn cục bây giờ sẽ trỏ đến cái UI đang ĐƯỢC MỞ
    public static MovementConfirmUI Instance { get; private set; }

    [Header("UI Components")]
    public GameObject ActionPanel; // Khung chứa 2 nút bấm
    public Button AgreeButton;
    public Button DisagreeButton;

    [Header("Settings")]
    public float MoveSpeed = 5f; // Tốc độ di chuyển của CardVisual

    void Start()
    {
        // Khi mới sinh ra game, ẩn hết panel đi
        if (ActionPanel != null) ActionPanel.SetActive(false);

        // Gán sự kiện cho nút bấm
        AgreeButton.onClick.RemoveAllListeners();
        DisagreeButton.onClick.RemoveAllListeners();
        AgreeButton.onClick.AddListener(OnAgreePressed);
        DisagreeButton.onClick.AddListener(OnDisagreePressed);
    }

    // --- HÀM THÊM MỚI QUAN TRỌNG ---
    // Khi ô này được kích hoạt hiển thị, nó tự cướp quyền làm Instance đại diện
    public void ShowUI(bool state)
    {
        if (state)
        {
            // Nếu đang có một UI của ô khác mở, ẩn nó đi trước
            if (Instance != null && Instance != this)
            {
                Instance.ShowUI(false);
            }

            // Gán chính mình làm Instance đang hoạt động
            Instance = this;
        }

        if (ActionPanel != null) ActionPanel.SetActive(state);
    }

    void OnAgreePressed()
    {
        ShowUI(false);
        List<GridCell> path = GridManager.Instance.GetCurrentPath();

        if (path != null && path.Count > 0)
        {
            StartCoroutine(MoveCardAlongPath(path));
        }
    }

    void OnDisagreePressed()
    {
        ShowUI(false);
        GridManager.Instance.HighlightPath(false);
    }

    IEnumerator MoveCardAlongPath(List<GridCell> path)
    {
        // 1. Lấy ô xuất phát và đích
        GridCell startCell = path[0];
        GridCell destinationCell = path[path.Count - 1];

        CardBase cardData = startCell.OccupiedCard;
        if (cardData == null) yield break;

        CardVisual cardVisual = startCell.DisplayCardPanel.GetComponentInChildren<CardVisual>();
        if (cardVisual == null) yield break;

        Transform cardTransform = cardVisual.transform;

        // Đưa tạm ra ngoài cha GridManager để tịnh tiến không bị ảnh hưởng bởi local scale/position của ô cũ
        cardTransform.SetParent(GridManager.Instance.transform, true);

        // Cập nhật logic: Ô cũ không còn tướng đứng nữa
        startCell.OccupiedCard = null;

        int moveCost = path.Count - 1;

        if (cardData.CurrentMovementPoint < moveCost)
        {
            Debug.LogWarning(
                $"Không đủ điểm di chuyển. Cần {moveCost}, hiện có {cardData.CurrentMovementPoint}");

            yield break;
        }

        // 2. Di chuyển tịnh tiến qua từng ô trong danh sách đường đi
        for (int i = 1; i < path.Count; i++)
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.MOVING_SOUND);

            GridCell nextCell = path[i];
            Vector3 targetPos = nextCell.transform.position;
            targetPos.y = cardTransform.position.y; // Giữ nguyên độ cao Y

            while (Vector3.Distance(cardTransform.position, targetPos) > 0.02f)
            {
                cardTransform.position = Vector3.MoveTowards(cardTransform.position, targetPos, MoveSpeed * Time.deltaTime);
                yield return null;
            }
            cardTransform.position = targetPos;

            if (cardData != null)
            {
                cardData.CurrentMovementPoint = Mathf.Max(0, cardData.CurrentMovementPoint - 1);

                if (ActionMenuUI.Instance != null)
                {
                    ActionMenuUI.Instance.UpdateMovementPoint(
                        cardData.CurrentMovementPoint);
                }

                // Log ra màn hình console để bạn dễ dàng debug kiểm tra điểm
                Debug.Log($"[Movement] {cardData.Name} di chuyển vào ô {nextCell.GridPosition}. Điểm di chuyển còn lại: {cardData.CurrentMovementPoint}");
            }
        }

        // 3. --- SAU KHI ĐI ĐẾN ĐÍCH CUỐI CÙNG THÀNH CÔNG ---

        // Gán dữ liệu quân cờ vào ô mới
        destinationCell.OccupiedCard = cardData;

        // Đổi cha của CardVisual sang panel hiển thị của ô đích mới
        cardTransform.SetParent(destinationCell.DisplayCardPanel);
        cardTransform.localPosition = Vector3.zero;
        cardTransform.localRotation = Quaternion.identity;

        // Xóa sạch toàn bộ các vùng loang đỏ/xanh lộ trình cũ đang hiển thị
        GridManager.Instance.ClearAllMovementRanges();
        GridManager.Instance.ClearCurrentPathData();

        // --- BƯỚC THÊM MỚI THEO YÊU CẦU CỦA BẠN ---
        // Đổi màu nền tấm nền chính (platform) của các ô
        startCell.SetPlatformMaterial(GridManager.Instance.EmptyPositionMaterial);       // Ô cũ thành trống
        destinationCell.SetPlatformMaterial(GridManager.Instance.SelectedPositionMaterial); // Ô mới thành được chọn

        // Cập nhật lại ô đang chọn hiện tại trong GridManager là ô đích này
        GridManager.Instance.SetOriginCell(destinationCell);

        // Tự động gọi lại hàm tính toán tầm đi mới từ vị trí vừa đáp xuống
        int movementRange = cardData.Class.MovementRange; // Lấy tầm đi của thẻ bài
        int movementPoint = cardData.CurrentMovementPoint;
        GridManager.Instance.ShowMovementRangeAt(destinationCell.GridPosition, movementRange, movementPoint);
    }
}