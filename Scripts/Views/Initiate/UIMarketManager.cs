using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMarketManager : MonoBehaviour
{
    public static UIMarketManager Instance { get; private set; }
    [Header("Market")]
    public GameObject RareMarketManagerPrefab;
    public GameObject RareMarketPrefab;
    public GameObject RareMarketButtonPrefab;
    public GameObject UltraRareMarketManagerPrefab;
    public GameObject UltraRareMarketPrefab;
    public GameObject UltraRareMarketButtonPrefab;
    public GameObject LegendaryMarketManagerPrefab;
    public GameObject LegendaryMarketPrefab;
    public GameObject LegendaryMarketButtonPrefab;
    public GameObject MysticMarketManagerPrefab;
    public GameObject MysticMarketPrefab;
    public GameObject MysticMarketButtonPrefab;
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
    public GameObject GetMarketPanel(string prefabName)
    {
        switch (prefabName)
        {
            case "RareMarketManagerPrefab":
                return RareMarketManagerPrefab;
            case "RareMarketButtonPrefab":
                return RareMarketButtonPrefab;
            case "RareMarketPrefab":
                return RareMarketPrefab;
            case "UltraRareMarketManagerPrefab":
                return UltraRareMarketManagerPrefab;
            case "UltraRareMarketButtonPrefab":
                return UltraRareMarketButtonPrefab;
            case "UltraRareMarketPrefab":
                return UltraRareMarketPrefab;
            case "LegendaryMarketManagerPrefab":
                return LegendaryMarketManagerPrefab;
            case "LegendaryMarketButtonPrefab":
                return LegendaryMarketButtonPrefab;
            case "LegendaryMarketPrefab":
                return LegendaryMarketPrefab;
            case "MysticMarketManagerPrefab":
                return MysticMarketManagerPrefab;
            case "MysticMarketButtonPrefab":
                return MysticMarketButtonPrefab;
            case "MysticMarketPrefab":
                return MysticMarketPrefab;
            default:
                return RareMarketManagerPrefab;
        }
    }
}