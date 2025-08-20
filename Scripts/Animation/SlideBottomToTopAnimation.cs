using UnityEngine;

public class SlideBottomToTopAnimation : MonoBehaviour
{
    public float distance = 200f;   // khoảng cách xuất phát từ dưới lên (px)
    public float duration = 0.5f;   // thời gian chạy (s)

    private RectTransform rectTransform;
    private Vector2 endPos;
    private Vector2 startPos;
    private float elapsed;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
            rectTransform = gameObject.AddComponent<RectTransform>();
    }

    void OnEnable()
    {
        // Vị trí gốc (trong Editor) là điểm kết thúc
        endPos = rectTransform.anchoredPosition;

        // Xuất phát từ dưới
        startPos = endPos + new Vector2(0, -distance);

        // Đặt object ở vị trí bắt đầu
        rectTransform.anchoredPosition = startPos;

        // Reset thời gian
        elapsed = 0f;
    }

    void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // SmoothStep cho mượt hơn
            t = Mathf.SmoothStep(0f, 1f, t);

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
        }
    }
}
