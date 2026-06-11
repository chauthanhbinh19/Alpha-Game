using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private int direction = -1;

    private RectTransform rectTransform;
    private bool initialized;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Nếu gắn sẵn trên Prefab hoặc Inspector
        if (rotationSpeed != 0)
        {
            initialized = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(float speed, int dir)
    {
        rotationSpeed = speed;
        direction = dir >= 0 ? 1 : -1;
        initialized = true;
    }

    public void SetSpeed(float speed)
    {
        rotationSpeed = speed;
    }

    public void SetDirection(int dir)
    {
        direction = dir >= 0 ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized || rectTransform == null)
            return;
            
        rectTransform.Rotate(0f, 0f, direction * rotationSpeed * Time.deltaTime);
    }
}
