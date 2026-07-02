using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    public float Speed = 2f;
    public float ScaleAmount = 0.1f; // tỉ lệ phóng to thêm

    private Vector3 StartScale;

    void Start()
    {
        StartScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * Speed) * ScaleAmount;
        transform.localScale = StartScale * scale;
    }
}
