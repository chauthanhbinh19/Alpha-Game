using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollManager : MonoBehaviour, IScrollHandler
{
    private ScrollRect scrollRect;
    private RectTransform content;
    private float contentHeight; // Chiều cao tổng của Content
    private float viewportHeight; // Chiều cao của Viewport (ScrollView hiển thị)
    private float scrollAmount = 750f; // Khoảng cách cuộn (điều chỉnh theo yêu cầu)
    private RawImage arrowUp; // Hình ảnh mũi tên chỉ lên
    private RawImage arrowDown; // Hình ảnh mũi tên chỉ xuống

    public void Initialize(ScrollRect scrollRect, RawImage arrowUp, RawImage arrowDown, float scrollAmount = 750f)
    {
        this.scrollRect = scrollRect;
        this.content = scrollRect.content;
        this.scrollAmount = scrollAmount;
        this.arrowUp = arrowUp;
        this.arrowDown = arrowDown;

        // Lấy chiều cao của Content và Viewport
        contentHeight = content.rect.height;
        viewportHeight = scrollRect.viewport.rect.height;

        UpdateArrows();
    }

    public void ScrollNext()
    {
        if (scrollRect == null || content == null) return;

        // Tính toán vị trí mới khi cuộn xuống
        Vector2 newPosition = content.anchoredPosition;
        newPosition.y += scrollAmount;

        // Đảm bảo không vượt quá chiều cao tối đa
        float maxScrollY = contentHeight - viewportHeight;
        newPosition.y = Mathf.Clamp(newPosition.y, 0, maxScrollY);

        content.anchoredPosition = newPosition;

        // Cập nhật mũi tên sau khi cuộn
        UpdateArrows();
    }

    public void ScrollPrevious()
    {
        if (scrollRect == null || content == null) return;

        // Tính toán vị trí mới khi cuộn lên
        Vector2 newPosition = content.anchoredPosition;
        newPosition.y -= scrollAmount;

        // Đảm bảo không vượt quá giới hạn dưới (0)
        newPosition.y = Mathf.Clamp(newPosition.y, 0, contentHeight);

        content.anchoredPosition = newPosition;

        // Cập nhật mũi tên sau khi cuộn
        UpdateArrows();
    }

    public void UpdateArrows()
    {
        if (content == null || scrollRect == null) return;

        // Kiểm tra trạng thái mũi tên
        float currentY = content.anchoredPosition.y;
        float maxScrollY = contentHeight - viewportHeight;

        // Ẩn/Hiện mũi tên
        if (arrowUp != null) arrowUp.gameObject.SetActive(currentY > 0);
        if (arrowDown != null) arrowDown.gameObject.SetActive(currentY < maxScrollY);
    }

    // Hàm xử lý cuộn chuột
    public void OnScroll(PointerEventData eventData)
    {
        if (eventData.scrollDelta.y > 0) // Cuộn lên
        {
            ScrollPrevious();
        }
        else if (eventData.scrollDelta.y < 0) // Cuộn xuống
        {
            ScrollNext();
        }
    }
}
