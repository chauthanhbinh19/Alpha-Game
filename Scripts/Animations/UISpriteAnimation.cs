using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    public Sprite[] Frames; // Nếu muốn tự động, không cần kéo từng sprite
    public Sprite SpriteSheet; // Kéo sprite sheet (Multiple) vào đây
    public float FrameRate = 12f;
    public float LastFrameHoldTime = 0.2f;

    private Image Image;
    private int CurrentFrame;
    private float Timer;

    void Awake()
    {
        Image = GetComponent<Image>();

        // Nếu frames chưa có, tự động lấy từ spriteSheet
        if ((Frames == null || Frames.Length == 0) && SpriteSheet != null)
        {
            string path = UnityEditor.AssetDatabase.GetAssetPath(SpriteSheet);
            Object[] sprites = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(path);
            List<Sprite> spriteList = new List<Sprite>();
            foreach (var s in sprites)
            {
                if (s is Sprite && s != SpriteSheet)
                    spriteList.Add((Sprite)s);
            }
            Frames = spriteList.ToArray();
        }
    }

    void Update()
    {
        if (Frames == null || Frames.Length == 0) return;

        Timer += Time.deltaTime;
        float frameTime = 1f / FrameRate;
        // Nếu đang ở frame cuối, giữ lâu hơn
        if (CurrentFrame == Frames.Length - 1)
            frameTime = LastFrameHoldTime;

        if (Timer >= frameTime)
        {
            Timer -= frameTime;
            CurrentFrame++;
            if (CurrentFrame >= Frames.Length)
                CurrentFrame = 0;
            Image.sprite = Frames[CurrentFrame];
        }
    }
}
