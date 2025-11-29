using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuSet4Manager : MonoBehaviour
{
    public static UIMainMenuSet4Manager Instance { get; private set; }
    [Header("Main menu set 4")]
    public GameObject MainMenuZarxPanelPrefab;
    public GameObject MainMenuRaikPanelPrefab;
    public GameObject MainMenuDraxPanelPrefab;
    public GameObject MainMenuKronPanelPrefab;
    public GameObject MainMenuZoltPanelPrefab;
    public GameObject MainMenuGorrPanelPrefab;
    public GameObject MainMenuRyzePanelPrefab;
    public GameObject MainMenuJaxxPanelPrefab;
    public GameObject MainMenuTharPanelPrefab;
    public GameObject MainMenuVornPanelPrefab;
    public GameObject MainMenuNyxPanelPrefab;
    public GameObject MainMenuArosPanelPrefab;
    public GameObject MainMenuHexPanelPrefab;
    public GameObject MainMenuLornPanelPrefab;
    public GameObject MainMenuBaxxPanelPrefab;
    public GameObject MainMenuZephPanelPrefab;
    public GameObject MainMenuKaelPanelPrefab;
    public GameObject MainMenuDravPanelPrefab;
    public GameObject MainMenuTornPanelPrefab;
    public GameObject MainMenuMyrrPanelPrefab;
    public GameObject MainMenuVaskPanelPrefab;
    public GameObject MainMenuJorrPanelPrefab;
    public GameObject MainMenuQuenPanelPrefab;
    [Header("Main menu slot set 4")]
    public GameObject ZarxSlotPrefab;
    public GameObject RaikSlotPrefab;
    public GameObject DraxSlotPrefab;
    public GameObject KronSlotPrefab;
    public GameObject ZoltSlotPrefab;
    public GameObject GorrSlotPrefab;
    public GameObject RyzeSlotPrefab;
    public GameObject JaxxSlotPrefab;
    public GameObject TharSlotPrefab;
    public GameObject VornSlotPrefab;
    public GameObject NyxSlotPrefab;
    public GameObject ArosSlotPrefab;
    public GameObject HexSlotPrefab;
    public GameObject LornSlotPrefab;
    public GameObject BaxxSlotPrefab;
    public GameObject ZephSlotPrefab;
    public GameObject KaelSlotPrefab;
    public GameObject DravSlotPrefab;
    public GameObject TornSlotPrefab;
    public GameObject MyrrSlotPrefab;
    public GameObject VaskSlotPrefab;
    public GameObject JorrSlotPrefab;
    public GameObject QuenSlotPrefab;
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
    public GameObject GetGameObjectMainMenu4(string prefabName)
    {
        switch (prefabName)
        {
            // MainMenu Set 4
            case "MainMenuZarxPanelPrefab":
                return MainMenuZarxPanelPrefab;
            case "MainMenuRaikPanelPrefab":
                return MainMenuRaikPanelPrefab;
            case "MainMenuDraxPanelPrefab":
                return MainMenuDraxPanelPrefab;
            case "MainMenuKronPanelPrefab":
                return MainMenuKronPanelPrefab;
            case "MainMenuZoltPanelPrefab":
                return MainMenuZoltPanelPrefab;
            case "MainMenuGorrPanelPrefab":
                return MainMenuGorrPanelPrefab;
            case "MainMenuRyzePanelPrefab":
                return MainMenuRyzePanelPrefab;
            case "MainMenuJaxxPanelPrefab":
                return MainMenuJaxxPanelPrefab;
            case "MainMenuTharPanelPrefab":
                return MainMenuTharPanelPrefab;
            case "MainMenuVornPanelPrefab":
                return MainMenuVornPanelPrefab;
            case "MainMenuNyxPanelPrefab":
                return MainMenuNyxPanelPrefab;
            case "MainMenuArosPanelPrefab":
                return MainMenuArosPanelPrefab;
            case "MainMenuHexPanelPrefab":
                return MainMenuHexPanelPrefab;
            case "MainMenuLornPanelPrefab":
                return MainMenuLornPanelPrefab;
            case "MainMenuBaxxPanelPrefab":
                return MainMenuBaxxPanelPrefab;
            case "MainMenuZephPanelPrefab":
                return MainMenuZephPanelPrefab;
            case "MainMenuKaelPanelPrefab":
                return MainMenuKaelPanelPrefab;
            case "MainMenuDravPanelPrefab":
                return MainMenuDravPanelPrefab;
            case "MainMenuTornPanelPrefab":
                return MainMenuTornPanelPrefab;
            case "MainMenuMyrrPanelPrefab":
                return MainMenuMyrrPanelPrefab;
            case "MainMenuVaskPanelPrefab":
                return MainMenuVaskPanelPrefab;
            case "MainMenuJorrPanelPrefab":
                return MainMenuJorrPanelPrefab;
            case "MainMenuQuenPanelPrefab":
                return MainMenuQuenPanelPrefab;
            // MainMenu Slot Set 4
            case "ZarxSlotPrefab":
                return ZarxSlotPrefab;
            case "RaikSlotPrefab":
                return RaikSlotPrefab;
            case "DraxSlotPrefab":
                return DraxSlotPrefab;
            case "KronSlotPrefab":
                return KronSlotPrefab;
            case "ZoltSlotPrefab":
                return ZoltSlotPrefab;
            case "GorrSlotPrefab":
                return GorrSlotPrefab;
            case "RyzeSlotPrefab":
                return RyzeSlotPrefab;
            case "JaxxSlotPrefab":
                return JaxxSlotPrefab;
            case "TharSlotPrefab":
                return TharSlotPrefab;
            case "VornSlotPrefab":
                return VornSlotPrefab;
            case "NyxSlotPrefab":
                return NyxSlotPrefab;
            case "ArosSlotPrefab":
                return ArosSlotPrefab;
            case "HexSlotPrefab":
                return HexSlotPrefab;
            case "LornSlotPrefab":
                return LornSlotPrefab;
            case "BaxxSlotPrefab":
                return BaxxSlotPrefab;
            case "ZephSlotPrefab":
                return ZephSlotPrefab;
            case "KaelSlotPrefab":
                return KaelSlotPrefab;
            case "DravSlotPrefab":
                return DravSlotPrefab;
            case "TornSlotPrefab":
                return TornSlotPrefab;
            case "MyrrSlotPrefab":
                return MyrrSlotPrefab;
            case "VaskSlotPrefab":
                return VaskSlotPrefab;
            case "JorrSlotPrefab":
                return JorrSlotPrefab;
            case "QuenSlotPrefab":
                return QuenSlotPrefab;
            default:
                Debug.LogWarning($"Prefab {prefabName} not found.");
                return null;
        }
    }
}