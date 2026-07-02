using UnityEngine;

public class SwingAnimation : MonoBehaviour
{
    public float Speed = 2f;
    public float Distance = 50f; // px

    private RectTransform RectTransform;
    private Vector2 StartPosition;

    void Start()
    {
        RectTransform = GetComponent<RectTransform>();
        StartPosition = RectTransform.anchoredPosition;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * Speed) * Distance;
        RectTransform.anchoredPosition = StartPosition + new Vector2(x, 0);
    }
}
