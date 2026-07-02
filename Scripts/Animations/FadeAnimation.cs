using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    public float Speed = 2f;

    private CanvasGroup CanvasGroup;

    void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        if (CanvasGroup == null) CanvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    void Update()
    {
        CanvasGroup.alpha = (Mathf.Sin(Time.time * Speed) + 1f) / 2f; 
        // dao động alpha 0 ↔ 1
    }
}
