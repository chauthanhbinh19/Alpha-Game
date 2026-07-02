using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] private float RotationSpeed = 50f;
    [SerializeField] private int Direction = -1;

    private RectTransform RectTransform;
    private bool Initialized;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();

        // Nếu gắn sẵn trên Prefab hoặc Inspector
        if (RotationSpeed != 0)
        {
            Initialized = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(float speed, int dir)
    {
        RotationSpeed = speed;
        Direction = dir >= 0 ? 1 : -1;
        Initialized = true;
    }

    public void SetSpeed(float speed)
    {
        RotationSpeed = speed;
    }

    public void SetDirection(int dir)
    {
        Direction = dir >= 0 ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Initialized || RectTransform == null)
            return;
            
        RectTransform.Rotate(0f, 0f, Direction * RotationSpeed * Time.deltaTime);
    }
}
