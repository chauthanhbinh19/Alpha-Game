using UnityEngine;

public class SlideTopToBottomAnimation : MonoBehaviour
{
    public float distance = 200f;   // khoảng cách xuất phát từ trên xuống (px)
    public float duration = 0.5f;   // thời gian chạy (s)

    private RectTransform rectTransform;
    private Vector2 endPos;
    private Vector2 startPos;
    private float elapsed;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null) rectTransform = gameObject.AddComponent<RectTransform>();

        endPos = rectTransform.anchoredPosition;
        startPos = endPos + new Vector2(0, distance); // xuất phát từ trên
        rectTransform.anchoredPosition = startPos;
    }

    void OnEnable()
    {
        elapsed = 0f;
    }

    void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
        }
    }
}
