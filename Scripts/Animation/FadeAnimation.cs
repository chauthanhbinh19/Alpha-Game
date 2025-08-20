using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    public float speed = 2f;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    void Update()
    {
        canvasGroup.alpha = (Mathf.Sin(Time.time * speed) + 1f) / 2f; 
        // dao động alpha 0 ↔ 1
    }
}
