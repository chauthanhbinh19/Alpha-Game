using UnityEngine;

public class SlideBottomToTopAnimation : MonoBehaviour
{
    public float Distance = 200f;   // khoảng cách xuất phát từ dưới lên (px)
    public float Duration = 0.5f;   // thời gian chạy (s)

    private RectTransform RectTransform;
    private Vector2 EndPosition;
    private Vector2 StartPosition;
    private float Elapsed;

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        if (RectTransform == null)
            RectTransform = gameObject.AddComponent<RectTransform>();
    }

    void OnEnable()
    {
        // Vị trí gốc (trong Editor) là điểm kết thúc
        EndPosition = RectTransform.anchoredPosition;

        // Xuất phát từ dưới
        StartPosition = EndPosition + new Vector2(0, -Distance);

        // Đặt object ở vị trí bắt đầu
        RectTransform.anchoredPosition = StartPosition;

        // Reset thời gian
        Elapsed = 0f;
    }

    void Update()
    {
        if (Elapsed < Duration)
        {
            Elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(Elapsed / Duration);

            // SmoothStep cho mượt hơn
            t = Mathf.SmoothStep(0f, 1f, t);

            RectTransform.anchoredPosition = Vector2.Lerp(StartPosition, EndPosition, t);
        }
    }
}
