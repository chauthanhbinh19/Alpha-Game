using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private Camera cam;

    void Awake()
    {
        // Tự động tìm Slider con
        slider = GetComponentInChildren<Slider>();
        cam = Camera.main;
        if (slider == null)
        {
            Debug.LogError("HealthBar: Không tìm thấy Slider trong prefab!");
        }
    }

    void Update()
    {
        // Quay mặt về camera
        if (cam != null)
        {
            transform.LookAt(cam.transform);
            transform.Rotate(0, 180, 0); // tránh bị ngược
        }
    }

    public void SetMaxHealth(double max)
    {
        slider.maxValue = (float)max;
        slider.value = (float)max;
    }

    public void SetHealth(double value)
    {
        slider.value = (float)value;
    }
}
