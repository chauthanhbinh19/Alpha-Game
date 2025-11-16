using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CircleScrollView : MonoBehaviour
{
    [Header("Setup")]
    public RectTransform content;    // GameObject chứa các button có sẵn
    private float radius = 270f;      // Bán kính vòng tròn
    private float scrollSpeed = 50f; // Tốc độ xoay (scroll hoặc drag)
    private bool startFromTop = true; // Bắt đầu từ hướng trên
    private bool isClockwise = false;    // TRUE = xoay theo chiều kim đồng hồ
    private bool isScrolling = false;
    private float scrollStopDelay = 0.02f; // thời gian chờ nhỏ trước khi dừng sound
    private float scrollStopTimer = 0f;

    private float currentAngle = 0f;
    private List<RectTransform> items = new List<RectTransform>();

    void Start()
    {
        // Lấy toàn bộ con của content (ví dụ: Button, Image, v.v.)
        items.Clear();
        foreach (Transform child in content)
        {
            RectTransform rt = child.GetComponent<RectTransform>();
            if (rt != null)
                items.Add(rt);
        }
        items.Reverse();

        UpdateItemPositions();
    }

    void Update()
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(content, Input.mousePosition))
            return;
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        bool isDragging = Input.GetMouseButton(0);
        float delta = Input.GetAxis("Mouse X");

        bool isScrollActive = scroll != 0f || (isDragging && Mathf.Abs(delta) > 0.02f);

        if (isScrollActive)
        {
            // Xử lý góc xoay
            currentAngle += (scroll != 0 ? scroll : -delta) * scrollSpeed;
            UpdateItemPositions();

            // Reset timer mỗi khi có thao tác
            scrollStopTimer = scrollStopDelay;

            // Nếu chưa phát âm thanh thì phát ngay
            if (!isScrolling)
            {
                isScrolling = true;
                // Debug.Log("🔊 Start scroll sound");
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.GEAR_SCROLL_SOUND);
            }
        }
        else
        {
            // Khi không còn thao tác, bắt đầu đếm ngược thời gian dừng
            if (isScrolling)
            {
                if (scrollStopTimer > 0)
                {
                    scrollStopTimer -= Time.deltaTime;
                }
                else
                {
                    // Debug.Log("🔇 Stop scroll sound");
                    isScrolling = false;
                    AudioManager.Instance.StopSFX(AudioConstants.SFX.GEAR_SCROLL_SOUND);
                }
            }
        }
    }

    void UpdateItemPositions()
    {
        int count = items.Count;
        if (count == 0) return;

        // Nếu startFromTop = true => góc bắt đầu từ 90° (tức là trên)
        float startOffset = startFromTop ? 90f : 0f;

        // Chiều quay: clockwise = +1, counterclockwise = -1
        float direction = isClockwise ? 1f : -1f;

        for (int i = 0; i < count; i++)
        {
            // Nếu clockwise thì đảo index
            int index = isClockwise ? (count - 1 - i) : i;

            float angle = (360f / count) * index * direction + currentAngle + startOffset;
            float rad = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(rad) * radius;
            float y = Mathf.Sin(rad) * radius;

            RectTransform item = items[i];
            item.anchoredPosition = new Vector2(x, y);

            // // Hiệu ứng phóng to item phía trước
            // float scale = Mathf.Lerp(0.7f, 1f, (y + radius) / (2 * radius));
            // item.localScale = Vector3.one * scale;
        }
    }
}
