using UnityEngine;

public class SlideTopToBottomAnimation : MonoBehaviour
{
    public float Distance = 200f;   // khoảng cách xuất phát từ trên xuống (px)
    public float Duration = 0.5f;   // thời gian chạy (s)

    private RectTransform RectTransform;
    private Vector2 EndPosition;
    private Vector2 StartPosition;
    private float Elapsed;

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        if (RectTransform == null) RectTransform = gameObject.AddComponent<RectTransform>();

        EndPosition = RectTransform.anchoredPosition;
        StartPosition = EndPosition + new Vector2(0, Distance); // xuất phát từ trên
        RectTransform.anchoredPosition = StartPosition;
    }

    void OnEnable()
    {
        Elapsed = 0f;
    }

    void Update()
    {
        if (Elapsed < Duration)
        {
            Elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(Elapsed / Duration);
            RectTransform.anchoredPosition = Vector2.Lerp(StartPosition, EndPosition, t);
        }
    }
}
