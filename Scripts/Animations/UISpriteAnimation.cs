using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    public Sprite[] frames; // Nếu muốn tự động, không cần kéo từng sprite
    public Sprite spriteSheet; // Kéo sprite sheet (Multiple) vào đây
    public float frameRate = 12f;
    public float lastFrameHoldTime = 0.2f;

    private Image image;
    private int currentFrame;
    private float timer;

    void Awake()
    {
        image = GetComponent<Image>();

        // Nếu frames chưa có, tự động lấy từ spriteSheet
        if ((frames == null || frames.Length == 0) && spriteSheet != null)
        {
            string path = UnityEditor.AssetDatabase.GetAssetPath(spriteSheet);
            Object[] sprites = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(path);
            List<Sprite> spriteList = new List<Sprite>();
            foreach (var s in sprites)
            {
                if (s is Sprite && s != spriteSheet)
                    spriteList.Add((Sprite)s);
            }
            frames = spriteList.ToArray();
        }
    }

    void Update()
    {
        if (frames == null || frames.Length == 0) return;

        timer += Time.deltaTime;
        float frameTime = 1f / frameRate;
        // Nếu đang ở frame cuối, giữ lâu hơn
        if (currentFrame == frames.Length - 1)
            frameTime = lastFrameHoldTime;

        if (timer >= frameTime)
        {
            timer -= frameTime;
            currentFrame++;
            if (currentFrame >= frames.Length)
                currentFrame = 0;
            image.sprite = frames[currentFrame];
        }
    }
}
