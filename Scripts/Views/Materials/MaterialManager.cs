using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance { get; private set; }
    public Material UI_Red_Gradient_Mat;
    public Material UI_Green_Gradient_Mat;
    public Material UI_Blue_Gradient_Mat;
    public Material UI_Orange_Gradient_Mat;
    public Material UI_Purple_Gradient_Mat;
    public Material UI_Yellow_Gradient_Mat;
    public Material UI_Pink_Gradient_Mat;
    public Material UI_Red_Gradient_Radius_Mat;
    public Material UI_Green_Gradient_Radius_Mat;
    public Material UI_Blue_Gradient_Radius_Mat;
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
    public Material GetMaterial(string panelName)
    {
        switch (panelName)
        {
            case "UI_Red_Gradient_Mat":
                return UI_Red_Gradient_Mat;
            case "UI_Green_Gradient_Mat":
                return UI_Green_Gradient_Mat;
            case "UI_Blue_Gradient_Mat":
                return UI_Blue_Gradient_Mat;
            case "UI_Orange_Gradient_Mat":
                return UI_Orange_Gradient_Mat;
            case "UI_Purple_Gradient_Mat":
                return UI_Purple_Gradient_Mat;
            case "UI_Yellow_Gradient_Mat":
                return UI_Yellow_Gradient_Mat;
            case "UI_Pink_Gradient_Mat":
                return UI_Pink_Gradient_Mat;
            case "UI_Red_Gradient_Radius_Mat":
                return UI_Red_Gradient_Radius_Mat;
            case "UI_Green_Gradient_Radius_Mat":
                return UI_Green_Gradient_Radius_Mat;
            case "UI_Blue_Gradient_Radius_Mat":
                return UI_Blue_Gradient_Radius_Mat;
            default:
                Debug.LogWarning($"Panel {panelName} not found.");
                return null;
        }
    }
}