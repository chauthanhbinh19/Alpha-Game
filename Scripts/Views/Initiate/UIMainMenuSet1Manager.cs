using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuSet1Manager : MonoBehaviour
{
    public static UIMainMenuSet1Manager Instance { get; private set; }
    [Header("Main menu set 1")]
    public GameObject MainMenuEquipmentPanelPrefab;
    public GameObject MainMenuAptitudePanelPrefab;
    public GameObject MainMenuUpgradePanelPrefab;
    public GameObject MainMenuAffinityPanelPrefab;
    public GameObject MainMenuBlessingPanelPrefab;
    public GameObject MainMenuCorePanelPrefab;
    public GameObject MainMenuRealmPanelPrefab;
    public GameObject MainMenuPhysiquePanelPrefab;
    public GameObject MainMenuBloodlinePanelPrefab;
    public GameObject MainMenuOmnivisionPanelPrefab;
    public GameObject MainMenuOmnipotencePanelPrefab;
    public GameObject MainMenuOmnipresencePanelPrefab;
    public GameObject MainMenuOmnivoryPanelPrefab;
    public GameObject MainMenuOmnisciencePanelPrefab;
    public GameObject MainMenuAngelPanelPrefab;
    public GameObject MainMenuDemonPanelPrefab;
    public GameObject MainMenuSwordPanelPrefab;
    public GameObject MainMenuSpearPanelPrefab;
    public GameObject MainMenuShieldPanelPrefab;
    public GameObject MainMenuBowPanelPrefab;
    public GameObject MainMenuGunPanelPrefab;
    public GameObject MainMenuCyberPanelPrefab;
    public GameObject MainMenuFairyPanelPrefab;
    [Header("Main menu slot set 1")]
    public GameObject AptitudeSlotPrefab;
    public GameObject UpgradeSlotPrefab;
    public GameObject BlessingSlotPrefab;
    public GameObject CoreSlotPrefab;
    public GameObject RealmSlotPrefab;
    public GameObject PhysiqueSlotPrefab;
    public GameObject BloodlineSlotPrefab;
    public GameObject OmnivisionSlotPrefab;
    public GameObject OmnipotenceSlotPrefab;
    public GameObject OmnipresenceSlotPrefab;
    public GameObject OmnivorySlotPrefab;
    public GameObject OmniscienceSlotPrefab;
    public GameObject AngelSlotPrefab;
    public GameObject DemonSlotPrefab;
    public GameObject SwordSlotPrefab;
    public GameObject SpearSlotPrefab;
    public GameObject ShieldSlotPrefab;
    public GameObject BowSlotPrefab;
    public GameObject GunSlotPrefab;
    public GameObject CyberSlotPrefab;
    public GameObject FairySlotPrefab;
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
    public GameObject GetGameObjectMainMenu1(string prefabName)
    {
        switch (prefabName)
        {
            // MainMenu Set 1
            case "MainMenuAptitudePanelPrefab":
                return MainMenuAptitudePanelPrefab;
            case "MainMenuEquipmentPanelPrefab":
                return MainMenuEquipmentPanelPrefab;
            case "MainMenuUpgradePanelPrefab":
                return MainMenuUpgradePanelPrefab;
            case "MainMenuAffinityPanelPrefab":
                return MainMenuAffinityPanelPrefab;
            case "MainMenuBlessingPanelPrefab":
                return MainMenuBlessingPanelPrefab;
            case "MainMenuCorePanelPrefab":
                return MainMenuCorePanelPrefab;
            case "MainMenuRealmPanelPrefab":
                return MainMenuRealmPanelPrefab;
            case "MainMenuPhysiquePanelPrefab":
                return MainMenuPhysiquePanelPrefab;
            case "MainMenuBloodlinePanelPrefab":
                return MainMenuBloodlinePanelPrefab;
            case "MainMenuOmnivisionPanelPrefab":
                return MainMenuOmnivisionPanelPrefab;
            case "MainMenuOmnipotencePanelPrefab":
                return MainMenuOmnipotencePanelPrefab;
            case "MainMenuOmnipresencePanelPrefab":
                return MainMenuOmnipresencePanelPrefab;
            case "MainMenuOmnisciencePanelPrefab":
                return MainMenuOmnisciencePanelPrefab;
            case "MainMenuOmnivoryPanelPrefab":
                return MainMenuOmnivoryPanelPrefab;
            case "MainMenuAngelPanelPrefab":
                return MainMenuAngelPanelPrefab;
            case "MainMenuDemonPanelPrefab":
                return MainMenuDemonPanelPrefab;
            case "MainMenuSwordPanelPrefab":
                return MainMenuSwordPanelPrefab;
            case "MainMenuSpearPanelPrefab":
                return MainMenuSpearPanelPrefab;
            case "MainMenuShieldPanelPrefab":
                return MainMenuShieldPanelPrefab;
            case "MainMenuBowPanelPrefab":
                return MainMenuBowPanelPrefab;
            case "MainMenuCyberPanelPrefab":
                return MainMenuCyberPanelPrefab;
            case "MainMenuFairyPanelPrefab":
                return MainMenuFairyPanelPrefab;
            case "MainMenuGunPanelPrefab":
                return MainMenuGunPanelPrefab;

            // MainMenu Slot Set 1
            case "AptitudeSlotPrefab":
                return AptitudeSlotPrefab;
            case "UpgradeSlotPrefab":
                return UpgradeSlotPrefab;
            case "BlessingSlotPrefab":
                return BlessingSlotPrefab;
            case "CoreSlotPrefab":
                return CoreSlotPrefab;
            case "RealmSlotPrefab":
                return RealmSlotPrefab;
            case "PhysiqueSlotPrefab":
                return PhysiqueSlotPrefab;
            case "BloodlineSlotPrefab":
                return BloodlineSlotPrefab;
            case "OmnivisionSlotPrefab":
                return OmnivisionSlotPrefab;
            case "OmnipotenceSlotPrefab":
                return OmnipotenceSlotPrefab;
            case "OmnipresenceSlotPrefab":
                return OmnipresenceSlotPrefab;
            case "OmnivorySlotPrefab":
                return OmnivorySlotPrefab;
            case "OmniscienceSlotPrefab":
                return OmniscienceSlotPrefab;
            case "AngelSlotPrefab":
                return AngelSlotPrefab;
            case "DemonSlotPrefab":
                return DemonSlotPrefab;
            case "SwordSlotPrefab":
                return SwordSlotPrefab;
            case "SpearSlotPrefab":
                return SpearSlotPrefab;
            case "ShieldSlotPrefab":
                return ShieldSlotPrefab;
            case "BowSlotPrefab":
                return BowSlotPrefab;
            case "CyberSlotPrefab":
                return CyberSlotPrefab;
            case "FairySlotPrefab":
                return FairySlotPrefab;
            case "GunSlotPrefab":
                return GunSlotPrefab;

            default:
                Debug.LogWarning($"Prefab {prefabName} not found.");
                return null;
        }
    }
}