using UnityEngine;

public class SwingAnimation : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 50f; // px

    private RectTransform rectTransform;
    private Vector2 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * distance;
        rectTransform.anchoredPosition = startPos + new Vector2(x, 0);
    }
}
