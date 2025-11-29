using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuSet2Manager : MonoBehaviour
{
    public static UIMainMenuSet2Manager Instance { get; private set; }
    [Header("Main menu set 2")]
    public GameObject MainMenuDarkPanelPrefab;
    public GameObject MainMenuLightPanelPrefab;
    public GameObject MainMenuFirePanelPrefab;
    public GameObject MainMenuIcePanelPrefab;
    public GameObject MainMenuEarthPanelPrefab;
    public GameObject MainMenuThunderPanelPrefab;
    public GameObject MainMenuLifePanelPrefab;
    public GameObject MainMenuSpacePanelPrefab;
    public GameObject MainMenuTimePanelPrefab;
    public GameObject MainMenuNanotechPanelPrefab;
    public GameObject MainMenuQuantumPanelPrefab;
    public GameObject MainMenuHolographyPanelPrefab;
    public GameObject MainMenuPlasmaPanelPrefab;
    public GameObject MainMenuBiomechPanelPrefab;
    public GameObject MainMenuCryotechPanelPrefab;
    public GameObject MainMenuPsionicsPanelPrefab;
    public GameObject MainMenuNeurotechPanelPrefab;
    public GameObject MainMenuAntimatterPanelPrefab;
    public GameObject MainMenuPhantomwarePanelPrefab;
    public GameObject MainMenuGravitechPanelPrefab;
    public GameObject MainMenuAethernetPanelPrefab;
    public GameObject MainMenuStarforgePanelPrefab;
    public GameObject MainMenuOrbitalisPanelPrefab;
    [Header("Main menu slot set 2")]
    public GameObject DarkSlotPrefab;
    public GameObject LightSlotPrefab;
    public GameObject FireSlotPrefab;
    public GameObject IceSlotPrefab;
    public GameObject EarthSlotPrefab;
    public GameObject ThunderSlotPrefab;
    public GameObject LifeSlotPrefab;
    public GameObject SpaceSlotPrefab;
    public GameObject TimeSlotPrefab;
    public GameObject NanotechSlotPrefab;
    public GameObject QuantumSlotPrefab;
    public GameObject HolographySlotPrefab;
    public GameObject PlasmaSlotPrefab;
    public GameObject BiomechSlotPrefab;
    public GameObject CryotechSlotPrefab;
    public GameObject PsionicsSlotPrefab;
    public GameObject NeurotechSlotPrefab;
    public GameObject AntimatterSlotPrefab;
    public GameObject PhantomwareSlotPrefab;
    public GameObject GravitechSlotPrefab;
    public GameObject AethernetSlotPrefab;
    public GameObject StarforgeSlotPrefab;
    public GameObject OrbitalisSlotPrefab;
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
    public GameObject GetGameObjectMainMenu2(string prefabName)
    {
        switch (prefabName)
        {
            // MainMenu Set 2
            case "MainMenuDarkPanelPrefab":
                return MainMenuDarkPanelPrefab;
            case "MainMenuLightPanelPrefab":
                return MainMenuLightPanelPrefab;
            case "MainMenuFirePanelPrefab":
                return MainMenuFirePanelPrefab;
            case "MainMenuIcePanelPrefab":
                return MainMenuIcePanelPrefab;
            case "MainMenuEarthPanelPrefab":
                return MainMenuEarthPanelPrefab;
            case "MainMenuThunderPanelPrefab":
                return MainMenuThunderPanelPrefab;
            case "MainMenuLifePanelPrefab":
                return MainMenuLifePanelPrefab;
            case "MainMenuSpacePanelPrefab":
                return MainMenuSpacePanelPrefab;
            case "MainMenuTimePanelPrefab":
                return MainMenuTimePanelPrefab;
            case "MainMenuNanotechPanelPrefab":
                return MainMenuNanotechPanelPrefab;
            case "MainMenuQuantumPanelPrefab":
                return MainMenuQuantumPanelPrefab;
            case "MainMenuHolographyPanelPrefab":
                return MainMenuHolographyPanelPrefab;
            case "MainMenuPlasmaPanelPrefab":
                return MainMenuPlasmaPanelPrefab;
            case "MainMenuBiomechPanelPrefab":
                return MainMenuBiomechPanelPrefab;
            case "MainMenuCryotechPanelPrefab":
                return MainMenuCryotechPanelPrefab;
            case "MainMenuPsionicsPanelPrefab":
                return MainMenuPsionicsPanelPrefab;
            case "MainMenuNeurotechPanelPrefab":
                return MainMenuNeurotechPanelPrefab;
            case "MainMenuAntimatterPanelPrefab":
                return MainMenuAntimatterPanelPrefab;
            case "MainMenuPhantomwarePanelPrefab":
                return MainMenuPhantomwarePanelPrefab;
            case "MainMenuGravitechPanelPrefab":
                return MainMenuGravitechPanelPrefab;
            case "MainMenuAethernetPanelPrefab":
                return MainMenuAethernetPanelPrefab;
            case "MainMenuStarforgePanelPrefab":
                return MainMenuStarforgePanelPrefab;
            case "MainMenuOrbitalisPanelPrefab":
                return MainMenuOrbitalisPanelPrefab;

            // MainMenu Slot Set 2
            case "DarkSlotPrefab":
                return DarkSlotPrefab;
            case "LightSlotPrefab":
                return LightSlotPrefab;
            case "FireSlotPrefab":
                return FireSlotPrefab;
            case "IceSlotPrefab":
                return IceSlotPrefab;
            case "EarthSlotPrefab":
                return EarthSlotPrefab;
            case "ThunderSlotPrefab":
                return ThunderSlotPrefab;
            case "LifeSlotPrefab":
                return LifeSlotPrefab;
            case "SpaceSlotPrefab":
                return SpaceSlotPrefab;
            case "TimeSlotPrefab":
                return TimeSlotPrefab;
            case "NanotechSlotPrefab":
                return NanotechSlotPrefab;
            case "QuantumSlotPrefab":
                return QuantumSlotPrefab;
            case "HolographySlotPrefab":
                return HolographySlotPrefab;
            case "PlasmaSlotPrefab":
                return PlasmaSlotPrefab;
            case "BiomechSlotPrefab":
                return BiomechSlotPrefab;
            case "CryotechSlotPrefab":
                return CryotechSlotPrefab;
            case "PsionicsSlotPrefab":
                return PsionicsSlotPrefab;
            case "NeurotechSlotPrefab":
                return NeurotechSlotPrefab;
            case "AntimatterSlotPrefab":
                return AntimatterSlotPrefab;
            case "PhantomwareSlotPrefab":
                return PhantomwareSlotPrefab;
            case "GravitechSlotPrefab":
                return GravitechSlotPrefab;
            case "AethernetSlotPrefab":
                return AethernetSlotPrefab;
            case "StarforgeSlotPrefab":
                return StarforgeSlotPrefab;
            case "OrbitalisSlotPrefab":
                return OrbitalisSlotPrefab;
            default:
                Debug.LogWarning($"Prefab {prefabName} not found.");
                return null;
        }
    }
}