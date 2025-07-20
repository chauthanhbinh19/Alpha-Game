using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ImageManager : MonoBehaviour
{
    public static ImageManager Instance { get; private set; }
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    public void ChangeSizeImage(RawImage Image, Texture texture, float width = 250f)
    {
        // Lấy tỷ lệ khung hình của ảnh gốc
        float aspectRatio = (float)texture.height / texture.width;

        // Chiều rộng cố định là 250
        float newWidth = width;
        float newHeight = newWidth * aspectRatio; // Tính chiều cao dựa theo tỷ lệ

        // Cập nhật kích thước ảnh
        Image.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}