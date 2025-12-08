using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance { get; private set; }
    private Dictionary<string, Material> materialDict;
    
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
            LoadAllPrefabs();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    private void LoadAllPrefabs()
    {
        materialDict = new Dictionary<string, Material>();

        // Load toàn bộ prefab trong thư mục Resources/UI/MainMenu/
        List<Material> materials = new List<Material>();

        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Black/Basic"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Black/Radius"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Blue/Basic"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Blue/Radius"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Gray/Basic"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Gray/Radius"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Green/Basic"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Green/Radius"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Orange/Basic"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Orange/Radius"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Pink/Basic"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Pink/Radius"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Purple/Basic"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Purple/Radius"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Red/Basic"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Red/Radius"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/White/Basic"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/White/Radius"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Yellow/Basic"));
        materials.AddRange(Resources.LoadAll<Material>("Main Feature/Materials/Gradient/Yellow/Radius"));

        foreach (var material in materials)
        {
            // Key = tên prefab
            materialDict[material.name] = material;
            // Debug.Log($"Loaded: {material.name}");
        }
    }

    public Material Get(string prefabName)
    {
        if (materialDict.TryGetValue(prefabName, out var material))
        {
            // Debug.Log("Material OK");
            return material;
        }

        Debug.LogWarning($"Prefab '{prefabName}' not found in dictionary!");
        return null;
    }
}