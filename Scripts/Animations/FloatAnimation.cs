using UnityEngine;

public class FloatAnimation : MonoBehaviour
{
    public float Speed = 2f;          // tốc độ
    public float AmplitudePercent = 0.1f; // biên độ % theo height

    private RectTransform RectTransform;
    private Vector2 StartPosition;
    private float Amplitude;

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        if (RectTransform == null) RectTransform = gameObject.AddComponent<RectTransform>();
        StartPosition = RectTransform.anchoredPosition;
        Amplitude = RectTransform.rect.height * AmplitudePercent;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * Speed) * Amplitude;
        RectTransform.anchoredPosition = StartPosition + new Vector2(0, y);
    }
}
