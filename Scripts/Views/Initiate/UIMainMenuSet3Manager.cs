using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuSet3Manager : MonoBehaviour
{
    public static UIMainMenuSet3Manager Instance { get; private set; }
    [Header("Main menu set 3")]
    public GameObject MainMenuAzathothPanelPrefab;
    public GameObject MainMenuYogSothothPanelPrefab;
    public GameObject MainMenuNyarlathotepPanelPrefab;
    public GameObject MainMenuShubNiggurathPanelPrefab;
    public GameObject MainMenuNihorathPanelPrefab;
    public GameObject MainMenuAeonaxPanelPrefab;
    public GameObject MainMenuSeraphirosPanelPrefab;
    public GameObject MainMenuThorindarPanelPrefab;
    public GameObject MainMenuZilthrosPanelPrefab;
    public GameObject MainMenuKhorazalPanelPrefab;
    public GameObject MainMenuIxithraPanelPrefab;
    public GameObject MainMenuOmnitheusPanelPrefab;
    public GameObject MainMenuPhyrixaPanelPrefab;
    public GameObject MainMenuAtherionPanelPrefab;
    public GameObject MainMenuVorathosPanelPrefab;
    public GameObject MainMenuTenebrisPanelPrefab;
    public GameObject MainMenuXylkorPanelPrefab;
    public GameObject MainMenuVeltharionPanelPrefab;
    public GameObject MainMenuArcanosPanelPrefab;
    public GameObject MainMenuDolomathPanelPrefab;
    public GameObject MainMenuArathorPanelPrefab;
    public GameObject MainMenuXyphosPanelPrefab;
    public GameObject MainMenuVaelithPanelPrefab;
    [Header("Main menu slot set 3")]
    public GameObject AzathothSlotPrefab;
    public GameObject YogSothothSlotPrefab;
    public GameObject NyarlathotepSlotPrefab;
    public GameObject ShubNiggurathSlotPrefab;
    public GameObject NihorathSlotPrefab;
    public GameObject AeonaxSlotPrefab;
    public GameObject SeraphirosSlotPrefab;
    public GameObject ThorindarSlotPrefab;
    public GameObject ZilthrosSlotPrefab;
    public GameObject KhorazalSlotPrefab;
    public GameObject IxithraSlotPrefab;
    public GameObject OmnitheusSlotPrefab;
    public GameObject PhyrixaSlotPrefab;
    public GameObject AtherionSlotPrefab;
    public GameObject VorathosSlotPrefab;
    public GameObject TenebrisSlotPrefab;
    public GameObject XylkorSlotPrefab;
    public GameObject VeltharionSlotPrefab;
    public GameObject ArcanosSlotPrefab;
    public GameObject DolomathSlotPrefab;
    public GameObject ArathorSlotPrefab;
    public GameObject XyphosSlotPrefab;
    public GameObject VaelithSlotPrefab;
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
    public GameObject GetGameObjectMainMenu3(string prefabName)
    {
        switch (prefabName)
        {
            // MainMenu Set 3
            case "MainMenuAzathothPanelPrefab":
                return MainMenuAzathothPanelPrefab;
            case "MainMenuYogSothothPanelPrefab":
                return MainMenuYogSothothPanelPrefab;
            case "MainMenuNyarlathotepPanelPrefab":
                return MainMenuNyarlathotepPanelPrefab;
            case "MainMenuShubNiggurathPanelPrefab":
                return MainMenuShubNiggurathPanelPrefab;
            case "MainMenuNihorathPanelPrefab":
                return MainMenuNihorathPanelPrefab;
            case "MainMenuAeonaxPanelPrefab":
                return MainMenuAeonaxPanelPrefab;
            case "MainMenuSeraphirosPanelPrefab":
                return MainMenuSeraphirosPanelPrefab;
            case "MainMenuThorindarPanelPrefab":
                return MainMenuThorindarPanelPrefab;
            case "MainMenuZilthrosPanelPrefab":
                return MainMenuZilthrosPanelPrefab;
            case "MainMenuKhorazalPanelPrefab":
                return MainMenuKhorazalPanelPrefab;
            case "MainMenuIxithraPanelPrefab":
                return MainMenuIxithraPanelPrefab;
            case "MainMenuOmnitheusPanelPrefab":
                return MainMenuOmnitheusPanelPrefab;
            case "MainMenuPhyrixaPanelPrefab":
                return MainMenuPhyrixaPanelPrefab;
            case "MainMenuAtherionPanelPrefab":
                return MainMenuAtherionPanelPrefab;
            case "MainMenuVorathosPanelPrefab":
                return MainMenuVorathosPanelPrefab;
            case "MainMenuTenebrisPanelPrefab":
                return MainMenuTenebrisPanelPrefab;
            case "MainMenuXylkorPanelPrefab":
                return MainMenuXylkorPanelPrefab;
            case "MainMenuVeltharionPanelPrefab":
                return MainMenuVeltharionPanelPrefab;
            case "MainMenuArcanosPanelPrefab":
                return MainMenuArcanosPanelPrefab;
            case "MainMenuDolomathPanelPrefab":
                return MainMenuDolomathPanelPrefab;
            case "MainMenuArathorPanelPrefab":
                return MainMenuArathorPanelPrefab;
            case "MainMenuXyphosPanelPrefab":
                return MainMenuXyphosPanelPrefab;
            case "MainMenuVaelithPanelPrefab":
                return MainMenuVaelithPanelPrefab;

            // MainMenu Slot Set 3
            case "AzathothSlotPrefab":
                return AzathothSlotPrefab;
            case "YogSothothSlotPrefab":
                return YogSothothSlotPrefab;
            case "NyarlathotepSlotPrefab":
                return NyarlathotepSlotPrefab;
            case "ShubNiggurathSlotPrefab":
                return ShubNiggurathSlotPrefab;
            case "NihorathSlotPrefab":
                return NihorathSlotPrefab;
            case "AeonaxSlotPrefab":
                return AeonaxSlotPrefab;
            case "SeraphirosSlotPrefab":
                return SeraphirosSlotPrefab;
            case "ThorindarSlotPrefab":
                return ThorindarSlotPrefab;
            case "ZilthrosSlotPrefab":
                return ZilthrosSlotPrefab;
            case "KhorazalSlotPrefab":
                return KhorazalSlotPrefab;
            case "IxithraSlotPrefab":
                return IxithraSlotPrefab;
            case "OmnitheusSlotPrefab":
                return OmnitheusSlotPrefab;
            case "PhyrixaSlotPrefab":
                return PhyrixaSlotPrefab;
            case "AtherionSlotPrefab":
                return AtherionSlotPrefab;
            case "VorathosSlotPrefab":
                return VorathosSlotPrefab;
            case "TenebrisSlotPrefab":
                return TenebrisSlotPrefab;
            case "XylkorSlotPrefab":
                return XylkorSlotPrefab;
            case "VeltharionSlotPrefab":
                return VeltharionSlotPrefab;
            case "ArcanosSlotPrefab":
                return ArcanosSlotPrefab;
            case "DolomathSlotPrefab":
                return DolomathSlotPrefab;
            case "ArathorSlotPrefab":
                return ArathorSlotPrefab;
            case "XyphosSlotPrefab":
                return XyphosSlotPrefab;
            case "VaelithSlotPrefab":
                return VaelithSlotPrefab;
            default:
                Debug.LogWarning($"Prefab {prefabName} not found.");
                return null;
        }
    }
}