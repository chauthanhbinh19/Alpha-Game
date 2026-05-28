using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    public float speed = 2f;
    public float scaleAmount = 0.1f; // tỉ lệ phóng to thêm

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * speed) * scaleAmount;
        transform.localScale = startScale * scale;
    }
}
