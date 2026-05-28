using UnityEngine;

public class FloatAnimation : MonoBehaviour
{
    public float speed = 2f;          // tốc độ
    public float amplitudePercent = 0.1f; // biên độ % theo height

    private RectTransform rectTransform;
    private Vector2 startPos;
    private float amplitude;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null) rectTransform = gameObject.AddComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        amplitude = rectTransform.rect.height * amplitudePercent;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;
        rectTransform.anchoredPosition = startPos + new Vector2(0, y);
    }
}
