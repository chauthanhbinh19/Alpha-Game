using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance { get; private set; }
    [Header("Red")]
    public Material UI_Red_Gradient_Mat;
    public Material UI_Red_Radius_Mat;
    public Material UI_Red_Gradient_Radius_Mat_MaskPercent_10;
    public Material UI_Red_Gradient_Radius_Mat_MaskPercent_20;
    public Material UI_Red_Gradient_Radius_Mat_MaskPercent_30;
    public Material UI_Red_Gradient_Radius_Mat_MaskPercent_40;
    public Material UI_Red_Gradient_Radius_Mat_MaskPercent_45;
    public Material UI_Red_Gradient_Radius_Mat_MaskPercent_50;
    public Material UI_Red_Gradient_Radius_Mat_MaskPercent_60;
    public Material UI_Red_Gradient_Radius_Mat_MaskPercent_70;
    public Material UI_Red_Gradient_Radius_Mat_MaskPercent_80;
    public Material UI_Red_Gradient_Radius_Mat_MaskPercent_90;
    public Material UI_Red_Gradient_Radius_Mat_MaskPercent_100;
    [Header("Green")]
    public Material UI_Green_Gradient_Mat;
    public Material UI_Green_Radius_Mat;
    public Material UI_Green_Gradient_Radius_Mat_MaskPercent_10;
    public Material UI_Green_Gradient_Radius_Mat_MaskPercent_20;
    public Material UI_Green_Gradient_Radius_Mat_MaskPercent_30;
    public Material UI_Green_Gradient_Radius_Mat_MaskPercent_40;
    public Material UI_Green_Gradient_Radius_Mat_MaskPercent_45;
    public Material UI_Green_Gradient_Radius_Mat_MaskPercent_50;
    public Material UI_Green_Gradient_Radius_Mat_MaskPercent_60;
    public Material UI_Green_Gradient_Radius_Mat_MaskPercent_70;
    public Material UI_Green_Gradient_Radius_Mat_MaskPercent_80;
    public Material UI_Green_Gradient_Radius_Mat_MaskPercent_90;
    public Material UI_Green_Gradient_Radius_Mat_MaskPercent_100;
    [Header("Blue")]
    public Material UI_Blue_Gradient_Mat;
    public Material UI_Blue_Radius_Mat;
    public Material UI_Blue_Gradient_Radius_Mat_MaskPercent_10;
    public Material UI_Blue_Gradient_Radius_Mat_MaskPercent_20;
    public Material UI_Blue_Gradient_Radius_Mat_MaskPercent_30;
    public Material UI_Blue_Gradient_Radius_Mat_MaskPercent_40;
    public Material UI_Blue_Gradient_Radius_Mat_MaskPercent_45;
    public Material UI_Blue_Gradient_Radius_Mat_MaskPercent_50;
    public Material UI_Blue_Gradient_Radius_Mat_MaskPercent_60;
    public Material UI_Blue_Gradient_Radius_Mat_MaskPercent_70;
    public Material UI_Blue_Gradient_Radius_Mat_MaskPercent_80;
    public Material UI_Blue_Gradient_Radius_Mat_MaskPercent_90;
    public Material UI_Blue_Gradient_Radius_Mat_MaskPercent_100;
    [Header("Orange")]
    public Material UI_Orange_Gradient_Mat;
    public Material UI_Orange_Radius_Mat;
    public Material UI_Orange_Gradient_Radius_Mat_MaskPercent_10;
    public Material UI_Orange_Gradient_Radius_Mat_MaskPercent_20;
    public Material UI_Orange_Gradient_Radius_Mat_MaskPercent_30;
    public Material UI_Orange_Gradient_Radius_Mat_MaskPercent_40;
    public Material UI_Orange_Gradient_Radius_Mat_MaskPercent_45;
    public Material UI_Orange_Gradient_Radius_Mat_MaskPercent_50;
    public Material UI_Orange_Gradient_Radius_Mat_MaskPercent_60;
    public Material UI_Orange_Gradient_Radius_Mat_MaskPercent_70;
    public Material UI_Orange_Gradient_Radius_Mat_MaskPercent_80;
    public Material UI_Orange_Gradient_Radius_Mat_MaskPercent_90;
    public Material UI_Orange_Gradient_Radius_Mat_MaskPercent_100;
    [Header("Purple")]
    public Material UI_Purple_Gradient_Mat;
    public Material UI_Purple_Radius_Mat;
    public Material UI_Purple_Gradient_Radius_Mat_MaskPercent_10;
    public Material UI_Purple_Gradient_Radius_Mat_MaskPercent_20;
    public Material UI_Purple_Gradient_Radius_Mat_MaskPercent_30;
    public Material UI_Purple_Gradient_Radius_Mat_MaskPercent_40;
    public Material UI_Purple_Gradient_Radius_Mat_MaskPercent_45;
    public Material UI_Purple_Gradient_Radius_Mat_MaskPercent_50;
    public Material UI_Purple_Gradient_Radius_Mat_MaskPercent_60;
    public Material UI_Purple_Gradient_Radius_Mat_MaskPercent_70;
    public Material UI_Purple_Gradient_Radius_Mat_MaskPercent_80;
    public Material UI_Purple_Gradient_Radius_Mat_MaskPercent_90;
    public Material UI_Purple_Gradient_Radius_Mat_MaskPercent_100;
    [Header("Yellow")]
    public Material UI_Yellow_Gradient_Mat;
    public Material UI_Yellow_Radius_Mat;
    public Material UI_Yellow_Gradient_Radius_Mat_MaskPercent_10;
    public Material UI_Yellow_Gradient_Radius_Mat_MaskPercent_20;
    public Material UI_Yellow_Gradient_Radius_Mat_MaskPercent_30;
    public Material UI_Yellow_Gradient_Radius_Mat_MaskPercent_40;
    public Material UI_Yellow_Gradient_Radius_Mat_MaskPercent_45;
    public Material UI_Yellow_Gradient_Radius_Mat_MaskPercent_50;
    public Material UI_Yellow_Gradient_Radius_Mat_MaskPercent_60;
    public Material UI_Yellow_Gradient_Radius_Mat_MaskPercent_70;
    public Material UI_Yellow_Gradient_Radius_Mat_MaskPercent_80;
    public Material UI_Yellow_Gradient_Radius_Mat_MaskPercent_90;
    public Material UI_Yellow_Gradient_Radius_Mat_MaskPercent_100;
    [Header("Pink")]
    public Material UI_Pink_Gradient_Mat;
    public Material UI_Pink_Radius_Mat;
    public Material UI_Pink_Gradient_Radius_Mat_MaskPercent_10;
    public Material UI_Pink_Gradient_Radius_Mat_MaskPercent_20;
    public Material UI_Pink_Gradient_Radius_Mat_MaskPercent_30;
    public Material UI_Pink_Gradient_Radius_Mat_MaskPercent_40;
    public Material UI_Pink_Gradient_Radius_Mat_MaskPercent_45;
    public Material UI_Pink_Gradient_Radius_Mat_MaskPercent_50;
    public Material UI_Pink_Gradient_Radius_Mat_MaskPercent_60;
    public Material UI_Pink_Gradient_Radius_Mat_MaskPercent_70;
    public Material UI_Pink_Gradient_Radius_Mat_MaskPercent_80;
    public Material UI_Pink_Gradient_Radius_Mat_MaskPercent_90;
    public Material UI_Pink_Gradient_Radius_Mat_MaskPercent_100;
    [Header("Gray")]
    public Material UI_Gray_Gradient_Mat;
    public Material UI_Gray_Radius_Mat;
    public Material UI_Gray_Gradient_Radius_Mat_MaskPercent_10;
    public Material UI_Gray_Gradient_Radius_Mat_MaskPercent_20;
    public Material UI_Gray_Gradient_Radius_Mat_MaskPercent_30;
    public Material UI_Gray_Gradient_Radius_Mat_MaskPercent_40;
    public Material UI_Gray_Gradient_Radius_Mat_MaskPercent_45;
    public Material UI_Gray_Gradient_Radius_Mat_MaskPercent_50;
    public Material UI_Gray_Gradient_Radius_Mat_MaskPercent_60;
    public Material UI_Gray_Gradient_Radius_Mat_MaskPercent_70;
    public Material UI_Gray_Gradient_Radius_Mat_MaskPercent_80;
    public Material UI_Gray_Gradient_Radius_Mat_MaskPercent_90;
    public Material UI_Gray_Gradient_Radius_Mat_MaskPercent_100;
    [Header("Black")]
    public Material UI_Black_Gradient_Mat;
    public Material UI_Black_Radius_Mat;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Top_10;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Top_20;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Top_30;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Top_40;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Top_45;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Top_50;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Top_60;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Top_70;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Top_80;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Top_90;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Top_100;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_10;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_20;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_30;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_40;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_45;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_50;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_60;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_70;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_80;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_90;
    public Material UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_100;
    [Header("White")]
    public Material UI_White_Gradient_Mat;
    public Material UI_White_Radius_Mat;
    public Material UI_White_Gradient_Radius_Mat_MaskPercent_10;
    public Material UI_White_Gradient_Radius_Mat_MaskPercent_20;
    public Material UI_White_Gradient_Radius_Mat_MaskPercent_30;
    public Material UI_White_Gradient_Radius_Mat_MaskPercent_40;
    public Material UI_White_Gradient_Radius_Mat_MaskPercent_45;
    public Material UI_White_Gradient_Radius_Mat_MaskPercent_50;
    public Material UI_White_Gradient_Radius_Mat_MaskPercent_60;
    public Material UI_White_Gradient_Radius_Mat_MaskPercent_70;
    public Material UI_White_Gradient_Radius_Mat_MaskPercent_80;
    public Material UI_White_Gradient_Radius_Mat_MaskPercent_90;
    public Material UI_White_Gradient_Radius_Mat_MaskPercent_100;
    
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
    public Material GetRedMaterial(string materialName)
    {
        switch (materialName)
        {
            case "UI_Red_Gradient_Mat":
                return UI_Red_Gradient_Mat;
            case "UI_Red_Radius_Mat":
                return UI_Red_Radius_Mat;
            case "UI_Red_Gradient_Radius_Mat_MaskPercent_10":
                return UI_Red_Gradient_Radius_Mat_MaskPercent_10;
            case "UI_Red_Gradient_Radius_Mat_MaskPercent_20":
                return UI_Red_Gradient_Radius_Mat_MaskPercent_20;
            case "UI_Red_Gradient_Radius_Mat_MaskPercent_30":
                return UI_Red_Gradient_Radius_Mat_MaskPercent_30;
            case "UI_Red_Gradient_Radius_Mat_MaskPercent_40":
                return UI_Red_Gradient_Radius_Mat_MaskPercent_40;
            case "UI_Red_Gradient_Radius_Mat_MaskPercent_45":
                return UI_Red_Gradient_Radius_Mat_MaskPercent_45;
            case "UI_Red_Gradient_Radius_Mat_MaskPercent_50":
                return UI_Red_Gradient_Radius_Mat_MaskPercent_50;
            case "UI_Red_Gradient_Radius_Mat_MaskPercent_60":
                return UI_Red_Gradient_Radius_Mat_MaskPercent_60;
            case "UI_Red_Gradient_Radius_Mat_MaskPercent_70":
                return UI_Red_Gradient_Radius_Mat_MaskPercent_70;
            case "UI_Red_Gradient_Radius_Mat_MaskPercent_80":
                return UI_Red_Gradient_Radius_Mat_MaskPercent_80;
            case "UI_Red_Gradient_Radius_Mat_MaskPercent_90":
                return UI_Red_Gradient_Radius_Mat_MaskPercent_90;
            case "UI_Red_Gradient_Radius_Mat_MaskPercent_100":
                return UI_Red_Gradient_Radius_Mat_MaskPercent_100;
            default:
                return UI_Red_Gradient_Mat;
        }
    }
    public Material GetGreenMaterial(string materialName)
    {
        switch (materialName)
        {
            case "UI_Green_Gradient_Mat":
                return UI_Green_Gradient_Mat;
            case "UI_Green_Radius_Mat":
                return UI_Green_Radius_Mat;
            case "UI_Green_Gradient_Radius_Mat_MaskPercent_10":
                return UI_Green_Gradient_Radius_Mat_MaskPercent_10;
            case "UI_Green_Gradient_Radius_Mat_MaskPercent_20":
                return UI_Green_Gradient_Radius_Mat_MaskPercent_20;
            case "UI_Green_Gradient_Radius_Mat_MaskPercent_30":
                return UI_Green_Gradient_Radius_Mat_MaskPercent_30;
            case "UI_Green_Gradient_Radius_Mat_MaskPercent_40":
                return UI_Green_Gradient_Radius_Mat_MaskPercent_40;
            case "UI_Green_Gradient_Radius_Mat_MaskPercent_45":
                return UI_Green_Gradient_Radius_Mat_MaskPercent_45;
            case "UI_Green_Gradient_Radius_Mat_MaskPercent_50":
                return UI_Green_Gradient_Radius_Mat_MaskPercent_50;
            case "UI_Green_Gradient_Radius_Mat_MaskPercent_60":
                return UI_Green_Gradient_Radius_Mat_MaskPercent_60;
            case "UI_Green_Gradient_Radius_Mat_MaskPercent_70":
                return UI_Green_Gradient_Radius_Mat_MaskPercent_70;
            case "UI_Green_Gradient_Radius_Mat_MaskPercent_80":
                return UI_Green_Gradient_Radius_Mat_MaskPercent_80;
            case "UI_Green_Gradient_Radius_Mat_MaskPercent_90":
                return UI_Green_Gradient_Radius_Mat_MaskPercent_90;
            case "UI_Green_Gradient_Radius_Mat_MaskPercent_100":
                return UI_Green_Gradient_Radius_Mat_MaskPercent_100;
            default:
                return UI_Green_Gradient_Mat;
        }
    }
    public Material GetBlueMaterial(string materialName)
    {
        switch (materialName)
        {
            case "UI_Blue_Gradient_Mat":
                return UI_Blue_Gradient_Mat;
            case "UI_Blue_Radius_Mat":
                return UI_Blue_Radius_Mat;
            case "UI_Blue_Gradient_Radius_Mat_MaskPercent_10":
                return UI_Blue_Gradient_Radius_Mat_MaskPercent_10;
            case "UI_Blue_Gradient_Radius_Mat_MaskPercent_20":
                return UI_Blue_Gradient_Radius_Mat_MaskPercent_20;
            case "UI_Blue_Gradient_Radius_Mat_MaskPercent_30":
                return UI_Blue_Gradient_Radius_Mat_MaskPercent_30;
            case "UI_Blue_Gradient_Radius_Mat_MaskPercent_40":
                return UI_Blue_Gradient_Radius_Mat_MaskPercent_40;
            case "UI_Blue_Gradient_Radius_Mat_MaskPercent_45":
                return UI_Blue_Gradient_Radius_Mat_MaskPercent_45;
            case "UI_Blue_Gradient_Radius_Mat_MaskPercent_50":
                return UI_Blue_Gradient_Radius_Mat_MaskPercent_50;
            case "UI_Blue_Gradient_Radius_Mat_MaskPercent_60":
                return UI_Blue_Gradient_Radius_Mat_MaskPercent_60;
            case "UI_Blue_Gradient_Radius_Mat_MaskPercent_70":
                return UI_Blue_Gradient_Radius_Mat_MaskPercent_70;
            case "UI_Blue_Gradient_Radius_Mat_MaskPercent_80":
                return UI_Blue_Gradient_Radius_Mat_MaskPercent_80;
            case "UI_Blue_Gradient_Radius_Mat_MaskPercent_90":
                return UI_Blue_Gradient_Radius_Mat_MaskPercent_90;
            case "UI_Blue_Gradient_Radius_Mat_MaskPercent_100":
                return UI_Blue_Gradient_Radius_Mat_MaskPercent_100;
            default:
                return UI_Blue_Gradient_Mat;
        }
    }
    public Material GetOrangeMaterial(string materialName)
    {
        switch (materialName)
        {
            case "UI_Orange_Gradient_Mat":
                return UI_Orange_Gradient_Mat;
            case "UI_Orange_Radius_Mat":
                return UI_Orange_Radius_Mat;
            case "UI_Orange_Gradient_Radius_Mat_MaskPercent_10":
                return UI_Orange_Gradient_Radius_Mat_MaskPercent_10;
            case "UI_Orange_Gradient_Radius_Mat_MaskPercent_20":
                return UI_Orange_Gradient_Radius_Mat_MaskPercent_20;
            case "UI_Orange_Gradient_Radius_Mat_MaskPercent_30":
                return UI_Orange_Gradient_Radius_Mat_MaskPercent_30;
            case "UI_Orange_Gradient_Radius_Mat_MaskPercent_40":
                return UI_Orange_Gradient_Radius_Mat_MaskPercent_40;
            case "UI_Orange_Gradient_Radius_Mat_MaskPercent_45":
                return UI_Orange_Gradient_Radius_Mat_MaskPercent_45;
            case "UI_Orange_Gradient_Radius_Mat_MaskPercent_50":
                return UI_Orange_Gradient_Radius_Mat_MaskPercent_50;
            case "UI_Orange_Gradient_Radius_Mat_MaskPercent_60":
                return UI_Orange_Gradient_Radius_Mat_MaskPercent_60;
            case "UI_Orange_Gradient_Radius_Mat_MaskPercent_70":
                return UI_Orange_Gradient_Radius_Mat_MaskPercent_70;
            case "UI_Orange_Gradient_Radius_Mat_MaskPercent_80":
                return UI_Orange_Gradient_Radius_Mat_MaskPercent_80;
            case "UI_Orange_Gradient_Radius_Mat_MaskPercent_90":
                return UI_Orange_Gradient_Radius_Mat_MaskPercent_90;
            case "UI_Orange_Gradient_Radius_Mat_MaskPercent_100":
                return UI_Orange_Gradient_Radius_Mat_MaskPercent_100;
            default:
                return UI_Orange_Gradient_Mat;
        }
    }
    public Material GetPurpleMaterial(string materialName)
    {
        switch (materialName)
        {
            case "UI_Purple_Gradient_Mat":
                return UI_Purple_Gradient_Mat;
            case "UI_Purple_Radius_Mat":
                return UI_Purple_Radius_Mat;
            case "UI_Purple_Gradient_Radius_Mat_MaskPercent_10":
                return UI_Purple_Gradient_Radius_Mat_MaskPercent_10;
            case "UI_Purple_Gradient_Radius_Mat_MaskPercent_20":
                return UI_Purple_Gradient_Radius_Mat_MaskPercent_20;
            case "UI_Purple_Gradient_Radius_Mat_MaskPercent_30":
                return UI_Purple_Gradient_Radius_Mat_MaskPercent_30;
            case "UI_Purple_Gradient_Radius_Mat_MaskPercent_40":
                return UI_Purple_Gradient_Radius_Mat_MaskPercent_40;
            case "UI_Purple_Gradient_Radius_Mat_MaskPercent_45":
                return UI_Purple_Gradient_Radius_Mat_MaskPercent_45;
            case "UI_Purple_Gradient_Radius_Mat_MaskPercent_50":
                return UI_Purple_Gradient_Radius_Mat_MaskPercent_50;
            case "UI_Purple_Gradient_Radius_Mat_MaskPercent_60":
                return UI_Purple_Gradient_Radius_Mat_MaskPercent_60;
            case "UI_Purple_Gradient_Radius_Mat_MaskPercent_70":
                return UI_Purple_Gradient_Radius_Mat_MaskPercent_70;
            case "UI_Purple_Gradient_Radius_Mat_MaskPercent_80":
                return UI_Purple_Gradient_Radius_Mat_MaskPercent_80;
            case "UI_Purple_Gradient_Radius_Mat_MaskPercent_90":
                return UI_Purple_Gradient_Radius_Mat_MaskPercent_90;
            case "UI_Purple_Gradient_Radius_Mat_MaskPercent_100":
                return UI_Purple_Gradient_Radius_Mat_MaskPercent_100;
            default:
                return UI_Purple_Gradient_Mat;
        }
    }
    public Material GetPinkMaterial(string materialName)
    {
        switch (materialName)
        {
            case "UI_Pink_Gradient_Mat":
                return UI_Pink_Gradient_Mat;
            case "UI_Pink_Radius_Mat":
                return UI_Pink_Radius_Mat;
            case "UI_Pink_Gradient_Radius_Mat_MaskPercent_10":
                return UI_Pink_Gradient_Radius_Mat_MaskPercent_10;
            case "UI_Pink_Gradient_Radius_Mat_MaskPercent_20":
                return UI_Pink_Gradient_Radius_Mat_MaskPercent_20;
            case "UI_Pink_Gradient_Radius_Mat_MaskPercent_30":
                return UI_Pink_Gradient_Radius_Mat_MaskPercent_30;
            case "UI_Pink_Gradient_Radius_Mat_MaskPercent_40":
                return UI_Pink_Gradient_Radius_Mat_MaskPercent_40;
            case "UI_Pink_Gradient_Radius_Mat_MaskPercent_45":
                return UI_Pink_Gradient_Radius_Mat_MaskPercent_45;
            case "UI_Pink_Gradient_Radius_Mat_MaskPercent_50":
                return UI_Pink_Gradient_Radius_Mat_MaskPercent_50;
            case "UI_Pink_Gradient_Radius_Mat_MaskPercent_60":
                return UI_Pink_Gradient_Radius_Mat_MaskPercent_60;
            case "UI_Pink_Gradient_Radius_Mat_MaskPercent_70":
                return UI_Pink_Gradient_Radius_Mat_MaskPercent_70;
            case "UI_Pink_Gradient_Radius_Mat_MaskPercent_80":
                return UI_Pink_Gradient_Radius_Mat_MaskPercent_80;
            case "UI_Pink_Gradient_Radius_Mat_MaskPercent_90":
                return UI_Pink_Gradient_Radius_Mat_MaskPercent_90;
            case "UI_Pink_Gradient_Radius_Mat_MaskPercent_100":
                return UI_Pink_Gradient_Radius_Mat_MaskPercent_100;
            default:
                return UI_Pink_Gradient_Mat;
        }
    }
    public Material GetYellowMaterial(string materialName)
    {
        switch (materialName)
        {
            case "UI_Yellow_Gradient_Mat":
                return UI_Yellow_Gradient_Mat;
            case "UI_Yellow_Radius_Mat":
                return UI_Yellow_Radius_Mat;
            case "UI_Yellow_Gradient_Radius_Mat_MaskPercent_10":
                return UI_Yellow_Gradient_Radius_Mat_MaskPercent_10;
            case "UI_Yellow_Gradient_Radius_Mat_MaskPercent_20":
                return UI_Yellow_Gradient_Radius_Mat_MaskPercent_20;
            case "UI_Yellow_Gradient_Radius_Mat_MaskPercent_30":
                return UI_Yellow_Gradient_Radius_Mat_MaskPercent_30;
            case "UI_Yellow_Gradient_Radius_Mat_MaskPercent_40":
                return UI_Yellow_Gradient_Radius_Mat_MaskPercent_40;
            case "UI_Yellow_Gradient_Radius_Mat_MaskPercent_45":
                return UI_Yellow_Gradient_Radius_Mat_MaskPercent_45;
            case "UI_Yellow_Gradient_Radius_Mat_MaskPercent_50":
                return UI_Yellow_Gradient_Radius_Mat_MaskPercent_50;
            case "UI_Yellow_Gradient_Radius_Mat_MaskPercent_60":
                return UI_Yellow_Gradient_Radius_Mat_MaskPercent_60;
            case "UI_Yellow_Gradient_Radius_Mat_MaskPercent_70":
                return UI_Yellow_Gradient_Radius_Mat_MaskPercent_70;
            case "UI_Yellow_Gradient_Radius_Mat_MaskPercent_80":
                return UI_Yellow_Gradient_Radius_Mat_MaskPercent_80;
            case "UI_Yellow_Gradient_Radius_Mat_MaskPercent_90":
                return UI_Yellow_Gradient_Radius_Mat_MaskPercent_90;
            case "UI_Yellow_Gradient_Radius_Mat_MaskPercent_100":
                return UI_Yellow_Gradient_Radius_Mat_MaskPercent_100;
            default:
                return UI_Yellow_Gradient_Mat;
        }
    }
    public Material GetGrayMaterial(string materialName)
    {
        switch (materialName)
        {
            case "UI_Gray_Gradient_Mat":
                return UI_Gray_Gradient_Mat;
            case "UI_Gray_Radius_Mat":
                return UI_Gray_Radius_Mat;
            case "UI_Gray_Gradient_Radius_Mat_MaskPercent_10":
                return UI_Gray_Gradient_Radius_Mat_MaskPercent_10;
            case "UI_Gray_Gradient_Radius_Mat_MaskPercent_20":
                return UI_Gray_Gradient_Radius_Mat_MaskPercent_20;
            case "UI_Gray_Gradient_Radius_Mat_MaskPercent_30":
                return UI_Gray_Gradient_Radius_Mat_MaskPercent_30;
            case "UI_Gray_Gradient_Radius_Mat_MaskPercent_40":
                return UI_Gray_Gradient_Radius_Mat_MaskPercent_40;
            case "UI_Gray_Gradient_Radius_Mat_MaskPercent_45":
                return UI_Gray_Gradient_Radius_Mat_MaskPercent_45;
            case "UI_Gray_Gradient_Radius_Mat_MaskPercent_50":
                return UI_Gray_Gradient_Radius_Mat_MaskPercent_50;
            case "UI_Gray_Gradient_Radius_Mat_MaskPercent_60":
                return UI_Gray_Gradient_Radius_Mat_MaskPercent_60;
            case "UI_Gray_Gradient_Radius_Mat_MaskPercent_70":
                return UI_Gray_Gradient_Radius_Mat_MaskPercent_70;
            case "UI_Gray_Gradient_Radius_Mat_MaskPercent_80":
                return UI_Gray_Gradient_Radius_Mat_MaskPercent_80;
            case "UI_Gray_Gradient_Radius_Mat_MaskPercent_90":
                return UI_Gray_Gradient_Radius_Mat_MaskPercent_90;
            case "UI_Gray_Gradient_Radius_Mat_MaskPercent_100":
                return UI_Gray_Gradient_Radius_Mat_MaskPercent_100;
            default:
                return UI_Gray_Gradient_Mat;
        }
    }
    public Material GetBlackMaterial(string materialName)
    {
        switch (materialName)
        {
            case "UI_Black_Gradient_Mat":
                return UI_Black_Gradient_Mat;
            case "UI_Black_Radius_Mat":
                return UI_Black_Radius_Mat;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Top_10":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Top_10;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Top_20":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Top_20;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Top_30":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Top_30;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Top_40":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Top_40;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Top_45":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Top_45;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Top_50":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Top_50;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Top_60":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Top_60;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Top_70":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Top_70;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Top_80":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Top_80;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Top_90":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Top_90;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Top_100":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Top_100;

            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_10":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_10;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_20":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_20;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_30":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_30;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_40":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_40;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_45":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_45;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_50":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_50;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_60":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_60;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_70":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_70;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_80":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_80;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_90":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_90;
            case "UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_100":
                return UI_Black_Gradient_Radius_Mat_MaskPercent_Bottom_100;
            default:
                return UI_Black_Gradient_Mat;
        }
    }
    public Material GetWhiteMaterial(string materialName)
    {
        switch (materialName)
        {
            case "UI_White_Gradient_Mat":
                return UI_White_Gradient_Mat;
            case "UI_White_Radius_Mat":
                return UI_White_Radius_Mat;
            case "UI_White_Gradient_Radius_Mat_MaskPercent_10":
                return UI_White_Gradient_Radius_Mat_MaskPercent_10;
            case "UI_White_Gradient_Radius_Mat_MaskPercent_20":
                return UI_White_Gradient_Radius_Mat_MaskPercent_20;
            case "UI_White_Gradient_Radius_Mat_MaskPercent_30":
                return UI_White_Gradient_Radius_Mat_MaskPercent_30;
            case "UI_White_Gradient_Radius_Mat_MaskPercent_40":
                return UI_White_Gradient_Radius_Mat_MaskPercent_40;
            case "UI_White_Gradient_Radius_Mat_MaskPercent_45":
                return UI_White_Gradient_Radius_Mat_MaskPercent_45;
            case "UI_White_Gradient_Radius_Mat_MaskPercent_50":
                return UI_White_Gradient_Radius_Mat_MaskPercent_50;
            case "UI_White_Gradient_Radius_Mat_MaskPercent_60":
                return UI_White_Gradient_Radius_Mat_MaskPercent_60;
            case "UI_White_Gradient_Radius_Mat_MaskPercent_70":
                return UI_White_Gradient_Radius_Mat_MaskPercent_70;
            case "UI_White_Gradient_Radius_Mat_MaskPercent_80":
                return UI_White_Gradient_Radius_Mat_MaskPercent_80;
            case "UI_White_Gradient_Radius_Mat_MaskPercent_90":
                return UI_White_Gradient_Radius_Mat_MaskPercent_90;
            case "UI_White_Gradient_Radius_Mat_MaskPercent_100":
                return UI_White_Gradient_Radius_Mat_MaskPercent_100;
            default:
                return UI_White_Gradient_Mat;
        }
    }
}