using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton instance
    public static UIManager Instance { get; private set; }

    [Header("Panels")]
    private Transform mainMenuButtonPanel;
    private Transform mainMenuCampaignPanel;
    public Transform notificationPanel;
    public Transform WaitingPanel;
    public Transform MainScencePanel;
    public Transform MainPanel;
    public Transform popupPanel;
    public Transform summonPanel;
    public Transform currencyPanel;
    public Transform userPanel;
    public Transform LoadingPanel;


    // Prefab references
    [Header("GameObject")]
    public GameObject SignInPanel;
    public GameObject SignUpPanel;
    public GameObject CreateNamePanel;
    public GameObject buttonPrefab;
    public GameObject notificationPrefab;
    public GameObject DictionaryPanel;
    public GameObject TabButton;
    public GameObject ItemPrefab;
    public GameObject MainMenuPanel;
    public GameObject MainMenuShopPanel;
    public GameObject MainMenuEnhancementPanel;
    public GameObject MainMenuCampaignPanel;
    public GameObject quantityPopupPrefab;
    public GameObject equipmentsPrefab;
    public GameObject equipmentsShopPrefab;
    public GameObject currencyPrefab;
    public GameObject EquipmentsPanelPrefab;
    public GameObject CardsPrefab;
    public GameObject CardsSecondPrefab;
    public GameObject EquipmentSecondPrefab;
    public GameObject EquipmentFirstPrefab;
    public GameObject ElementDetailsPrefab;
    public GameObject ElementDetails2Prefab;
    public GameObject MainMenuDetailPanelPrefab;
    public GameObject MainMenuDetailPanel2Prefab;
    public GameObject SummonPanelPrefab;
    public GameObject PositionPrefab;
    public GameObject CampaignPrefab;
    public GameObject CampaignDetailPrefab;
    public GameObject ShopManagerPrefab;
    public GameObject ShopButtonPrefab;
    public GameObject ShopPrefab;
    public GameObject NumberDetailPrefab;
    public GameObject NumberDetail2Prefab;
    public GameObject NumberDetail3Prefab;
    public GameObject TabButton2;
    public GameObject TabButton3;
    public GameObject TabButton4;
    public GameObject ReceivedNotification;
    public GameObject ItemThird;
    public GameObject TabButton5;
    public GameObject PopupTeamsPrefab;
    public GameObject TeamsPanelPrefab;
    public GameObject TeamsPositionPrefab;
    public GameObject CardsThirdPrefab;
    public GameObject TypePrefab;
    public GameObject StarPrefab;
    public GameObject PowerPrefab;
    public GameObject LoadingPanelPrefab;
    // MainMenu Set 1
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
    // MainMenu Set 2
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
    // MainMenu Set 3
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
    [Header("Other")]
    public GameObject MainMenuAnimePanelPrefab;
    public GameObject AnimePanelPrefab;
    public GameObject MasterBoardPanelPrefab;
    public GameObject DailyCheckinPanelPrefab;
    public GameObject ArenaPanelPrefab;
    public GameObject ArenaDetailsPanelPrefab;
    public GameObject TowerDetailsPanelPrefab;
    public GameObject PopupEquipmentsPanelPrefab;
    public GameObject PopupMenuPanelPrefab;
    public GameObject EquipmentsWearingPrefab;
    public GameObject Slot1Prefab;
    public GameObject Slot4Prefab;
    public GameObject Slot6Prefab;
    public GameObject Slot8Prefab;
    public GameObject Slot10Prefab;
    public GameObject Slot12Prefab;
    public GameObject Slot14Prefab;
    public GameObject Slot16Prefab;
    public GameObject ArenaButtonPrefab;
    public GameObject AnimeButtonPrefab;
    public GameObject DailyCheckinComponentPrefab;
    // MainMenu Slot Set 1
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
    // MainMenu Slot Set 2
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
    // MainMenu Slot Set 3
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
    [Header("Main menu slot set 3")]
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
    [Header("Other")]
    public GameObject AnimeSlotPrefab;
    public GameObject MasterBoardNodePrefab;
    public GameObject MasterBoardPopupPrefab;
    public GameObject ArenaSlotPrefab;

    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
            mainMenuButtonPanel = MainScencePanel.transform.Find("MainMenu/MenuBackground/MainMenuButton");
            mainMenuCampaignPanel = MainScencePanel.transform.Find("MainMenu/MenuCampaignBackground/MenuCampaign");
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    public Transform GetTransform(string panelName)
    {
        switch (panelName)
        {
            case "MainPanel":
                return MainPanel;
            case "WaitingPanel":
                return WaitingPanel;
            case "MainScencePanel":
                return MainScencePanel;
            case "mainMenuButtonPanel":
                return mainMenuButtonPanel;
            case "mainMenuCampaignPanel":
                return mainMenuCampaignPanel;
            case "notificationPanel":
                return notificationPanel;
            case "popupPanel":
                return popupPanel;
            case "summonPanel":
                return summonPanel;
            case "currencyPanel":
                return currencyPanel;
            case "userPanel":
                return userPanel;
            case "LoadingPanel":
                return LoadingPanel;
            default:
                Debug.LogWarning($"Panel {panelName} not found.");
                return null;
        }
    }
    public GameObject GetGameObject(string prefabName)
    {
        switch (prefabName)
        {
            case "SignInPanel":
                return SignInPanel;
            case "SignUpPanel":
                return SignUpPanel;
            case "CreateNamePanel":
                return CreateNamePanel;
            case "buttonPrefab":
                return buttonPrefab;
            case "notificationPrefab":
                return notificationPrefab;
            case "DictionaryPanel":
                return DictionaryPanel;
            case "TabButton":
                return TabButton;
            case "ItemPrefab":
                return ItemPrefab;
            case "MainMenuPanel":
                return MainMenuPanel;
            case "MainMenuShopPanel":
                return MainMenuShopPanel;
            case "MainMenuEnhancementPanel":
                return MainMenuEnhancementPanel;
            case "MainMenuCampaignPanel":
                return MainMenuCampaignPanel;
            case "quantityPopupPrefab":
                return quantityPopupPrefab;
            case "equipmentsPrefab":
                return equipmentsPrefab;
            case "equipmentsShopPrefab":
                return equipmentsShopPrefab;
            case "currencyPrefab":
                return currencyPrefab;
            case "EquipmentsPanelPrefab":
                return EquipmentsPanelPrefab;
            case "CardsPrefab":
                return CardsPrefab;
            case "CardsSecondPrefab":
                return CardsSecondPrefab;
            case "CardsThirdPrefab":
                return CardsThirdPrefab;
            case "EquipmentSecondPrefab":
                return EquipmentSecondPrefab;
            case "EquipmentFirstPrefab":
                return EquipmentFirstPrefab;
            case "ElementDetailsPrefab":
                return ElementDetailsPrefab;
            case "MainMenuDetailPanelPrefab":
                return MainMenuDetailPanelPrefab;
            case "MainMenuDetailPanel2Prefab":
                return MainMenuDetailPanel2Prefab;
            case "SummonPanelPrefab":
                return SummonPanelPrefab;
            case "PositionPrefab":
                return PositionPrefab;
            case "CampaignPrefab":
                return CampaignPrefab;
            case "CampaignDetailPrefab":
                return CampaignDetailPrefab;
            case "ShopButtonPrefab":
                return ShopButtonPrefab;
            case "ShopManagerPrefab":
                return ShopManagerPrefab;
            case "ShopPrefab":
                return ShopPrefab;
            case "NumberDetailPrefab":
                return NumberDetailPrefab;
            case "NumberDetail2Prefab":
                return NumberDetail2Prefab;
            case "NumberDetail3Prefab":
                return NumberDetail3Prefab;
            case "TabButton2":
                return TabButton2;
            case "TabButton3":
                return TabButton3;
            case "TabButton4":
                return TabButton4;
            case "ReceivedNotification":
                return ReceivedNotification;
            case "ItemThird":
                return ItemThird;
            case "PopupTeamsPrefab":
                return PopupTeamsPrefab;
            case "TeamsPanelPrefab":
                return TeamsPanelPrefab;
            case "TeamsPositionPrefab":
                return TeamsPositionPrefab;
            case "TypePrefab":
                return TypePrefab;
            case "StarPrefab":
                return StarPrefab;
            case "ElementDetails2Prefab":
                return ElementDetails2Prefab;
            case "PowerPrefab":
                return PowerPrefab;
            case "LoadingPanelPrefab":
                return LoadingPanelPrefab;

            case "MainMenuAnimePanelPrefab":
                return MainMenuAnimePanelPrefab;
            case "ArenaPanelPrefab":
                return ArenaPanelPrefab;
            case "AnimePanelPrefab":
                return AnimePanelPrefab;
            case "MasterBoardPanelPrefab":
                return MasterBoardPanelPrefab;
            case "DailyCheckinPanelPrefab":
                return DailyCheckinPanelPrefab;
            case "ArenaDetailsPanelPrefab":
                return ArenaDetailsPanelPrefab;
            case "TowerDetailsPanelPrefab":
                return TowerDetailsPanelPrefab;
            case "PopupEquipmentsPanelPrefab":
                return PopupEquipmentsPanelPrefab;
            case "PopupMenuPanelPrefab":
                return PopupMenuPanelPrefab;
            case "EquipmentsWearingPrefab":
                return EquipmentsWearingPrefab;
            case "Slot1Prefab":
                return Slot1Prefab;
            case "Slot4Prefab":
                return Slot4Prefab;
            case "Slot6Prefab":
                return Slot6Prefab;
            case "Slot8Prefab":
                return Slot8Prefab;
            case "Slot10Prefab":
                return Slot10Prefab;
            case "Slot12Prefab":
                return Slot12Prefab;
            case "Slot14Prefab":
                return Slot14Prefab;
            case "Slot16Prefab":
                return Slot16Prefab;
            case "ArenaButtonPrefab":
                return ArenaButtonPrefab;
            case "AnimeButtonPrefab":
                return AnimeButtonPrefab;
            case "DailyCheckinComponentPrefab":
                return DailyCheckinComponentPrefab;
            case "AnimeSlotPrefab":
                return AnimeSlotPrefab;
            case "MasterBoardNodePrefab":
                return MasterBoardNodePrefab;
            case "MasterBoardPopupPrefab":
                return MasterBoardPopupPrefab;
            case "ArenaSlotPrefab":
                return ArenaSlotPrefab;
            case "TabButton5":
                return TabButton5;
            default:
                Debug.LogWarning($"Prefab {prefabName} not found.");
                return null;
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

            case "AnimeSlotPrefab":
                return AnimeSlotPrefab;
            case "ArenaSlotPrefab":
                return ArenaSlotPrefab;
            case "TabButton5":
                return TabButton5;
            default:
                Debug.LogWarning($"Prefab {prefabName} not found.");
                return null;
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
    // public Button GetButton(string prefabName)
    // {
    //     switch (prefabName)
    //     {
    //         case "GalleryButton":
    //             return GalleryButton;
    //         case "CollectionButton":
    //             return CollectionButton;
    //         case "EquipmentsButton":
    //             return EquipmentsButton;
    //         default:
    //             Debug.LogWarning($"Button {prefabName} not found.");
    //             return null;
    //     }
    // }
    public void SetUI(GameObject gameObject, string type, int level = 0, string mainType = "")
    {
        if (mainType.Equals("Aptitude") || mainType.Equals("Blessing"))
        {
            return;
        }
        RawImage BackgroundImage = gameObject.transform.Find("Background").GetComponent<RawImage>();
        Texture backgroundTexture = Resources.Load<Texture>($"UI/Background3/{mainType}");
        BackgroundImage.texture = backgroundTexture;

        Transform backgroundTransform = gameObject.transform.Find("BackgroundCircle");
        if (backgroundTransform != null)
        {
            RawImage backgroundImageCircle = backgroundTransform.GetComponent<RawImage>();
            if (backgroundImageCircle != null)
            {
                backgroundImageCircle.gameObject.AddComponent<RotateAnimation>();
            }
        }

        int totalSkills = 10;
        int levelsPerSkill = 1000;

        // Đặt tất cả kỹ năng về trạng thái mặc định (đen + text "0/1000")
        for (int i = 1; i <= totalSkills; i++)
        {
            Transform aptitudeSkill = gameObject.transform.Find($"UpgradeSkill{i}");
            if (aptitudeSkill == null) continue;

            RawImage aptitudeImage = aptitudeSkill.Find("AptitudeImage").GetComponent<RawImage>();
            TextMeshProUGUI levelText = aptitudeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

            Texture texture = Resources.Load<Texture>($"UI/Rank/{type}");
            aptitudeImage.texture = texture;

            if (aptitudeImage != null) aptitudeImage.color = Color.black;
            if (levelText != null) levelText.text = $"0/{levelsPerSkill}";
        }

        // Xác định số kỹ năng được kích hoạt
        int activeSkillsCount = Mathf.Clamp((level / levelsPerSkill), 1, totalSkills);
        for (int i = 1; i <= activeSkillsCount; i++)
        {
            Transform activeSkill = gameObject.transform.Find($"UpgradeSkill{i}");
            if (activeSkill != null)
            {
                RawImage activeImage = activeSkill.Find("AptitudeImage").GetComponent<RawImage>();
                TextMeshProUGUI activeLevelText = activeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

                if (activeImage != null && level != 0) activeImage.color = Color.white;

                if (activeLevelText != null)
                {
                    // Kiểm tra nếu level là bội số của levelsPerSkill (1000, 2000, ..., 10000)
                    int displayedLevel = (level % levelsPerSkill == 0) ? levelsPerSkill : level % levelsPerSkill;
                    activeLevelText.text = $"{displayedLevel}/{levelsPerSkill}";
                }
            }
        }
        TextMeshProUGUI LevelText = gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        LevelText.text = level.ToString();
    }
    public void SetMaterialUI(GameObject gameobject, string type, int level = 0, int userMaterialQuantity = 0)
    {
        int levelsPerSkill = 1000;
        int materialQuantity = (level == 0) ? 1 : (level % levelsPerSkill == 0 ? levelsPerSkill : level % levelsPerSkill);
        Transform OneLevelMaterial = gameobject.transform.Find("DictionaryCards/OneLevelMaterial");
        Transform MaxLevelMaterial = gameobject.transform.Find("DictionaryCards/MaxLevelMaterial");
        ButtonEvent.Instance.Close(OneLevelMaterial);
        ButtonEvent.Instance.Close(MaxLevelMaterial);
        GameObject oneLevelMaterialObject = Instantiate(ElementDetails2Prefab, OneLevelMaterial);
        GameObject maxLevelMaterialObject = Instantiate(ElementDetails2Prefab, MaxLevelMaterial);

        RawImage oneLevelImage = oneLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture oneLevelTexture = Resources.Load<Texture>($"Item/Material/{type}");
        oneLevelImage.texture = oneLevelTexture;

        RectTransform oneLevelRectTransform = oneLevelImage.GetComponent<RectTransform>();
        oneLevelRectTransform.sizeDelta = new Vector2(50, 50);

        TextMeshProUGUI oneLevelQuantity = oneLevelMaterialObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        oneLevelQuantity.text = userMaterialQuantity + "/" + materialQuantity;

        RawImage maxLevelImage = maxLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture maxLevelTexture = Resources.Load<Texture>($"Item/Material/{type}");
        maxLevelImage.texture = maxLevelTexture;

        TextMeshProUGUI maxLevelQuantity = maxLevelMaterialObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        maxLevelQuantity.text = userMaterialQuantity + "/" + EvaluateItem.CalculateMaxMaterialQuantity(userMaterialQuantity, level);

        RectTransform maxLevelRectTransform = maxLevelImage.GetComponent<RectTransform>();
        maxLevelRectTransform.sizeDelta = new Vector2(50, 50);
    }
    public void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = Resources.Load<Texture>($"UI/Background4/{image}");
            if (texture != null)
            {
                buttonImage.texture = texture;
            }
            else
            {
                Debug.LogError($"Texture '{image}' not found in Resources.");
            }
        }
        else
        {
            Debug.LogError("Button does not have a RawImage component.");
        }
    }
    public void CreateLevelUI(int level, GameObject currentObject)
    {
        TextMeshProUGUI currentLevelText = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/Level/CurrentLevelText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nextLevelText = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/Level/NextLevelText").GetComponent<TextMeshProUGUI>();
        currentLevelText.text = level.ToString();
        int nextLevel = level + 1;
        if ((int)level == 100000)
        {
            nextLevelText.text = "Max";
        }
        else
        {
            nextLevelText.text = nextLevel.ToString();
        }
    }
    public void CreatePropertyUI(int status, PropertyInfo[] properties, object targetObject, GameObject currentObject)
    {
        Transform detailsContent = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel/Scroll View/Viewport/Content");

        GameObject firstDetailsObject = Instantiate(NumberDetail2Prefab, detailsContent);
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, detailsContent);
        GameObject elementDetails2Object = Instantiate(NumberDetail3Prefab, detailsContent);
        GameObject elementDetails3Object = Instantiate(NumberDetail3Prefab, detailsContent);
        GameObject elementDetails4Object = Instantiate(NumberDetail3Prefab, detailsContent);
        GameObject descriptionDetailsObject = Instantiate(NumberDetail3Prefab, detailsContent);

        Transform firstPopupPanel = firstDetailsObject.transform.Find("ElementDetails");
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform element2PopupPanel = elementDetails2Object.transform.Find("ElementDetails");
        Transform element3PopupPanel = elementDetails3Object.transform.Find("ElementDetails");
        Transform element4PopupPanel = elementDetails4Object.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        foreach (var property in properties)
        {
            object value = property.GetValue(targetObject, null);

            // Gọi hàm xử lý riêng cho từng property
            CreateSinglePropertyUI(status, property, value,
                firstPopupPanel, elementPopupPanel, element2PopupPanel, element3PopupPanel, element4PopupPanel, descriptionPopupPanel);
        }
    }
    public void CreateSinglePropertyUI(int status, PropertyInfo property, object value,
    Transform firstPopupPanel, Transform elementPopupPanel, Transform element2PopupPanel, Transform element3PopupPanel, Transform element4PopupPanel, Transform descriptionPopupPanel)
    {
        // Transform DetailsContent = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel/Scroll View/Viewport/Content");
        // GameObject firstDetailsObject = Instantiate(NumberDetail2Prefab, DetailsContent);
        // GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, DetailsContent);
        // GameObject elementDetails2Object = Instantiate(NumberDetail3Prefab, DetailsContent);
        // GameObject descriptionDetailsObject = Instantiate(NumberDetail3Prefab, DetailsContent);
        // Transform firstPopupPanel = firstDetailsObject.transform.Find("ElementDetails");
        // Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        // Transform element2PopupPanel = elementDetails2Object.transform.Find("ElementDetails");
        // Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");
        if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
                && !property.Name.Equals("status") && !property.Name.Equals("name")
                && !property.Name.Equals("image"))
        {
            if (property.Name.Equals("description"))
            {
                // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                GameObject descriptionTextObject = new GameObject("DescriptionText");
                descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                // Thêm component TextMeshProUGUI vào đối tượng mới
                TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                // Đổi màu chữ bằng mã hex #844000
                Color color;
                if (ColorUtility.TryParseHtmlString(ColorConstanst.HexColor.descriptionColor, out color)) // Chuyển mã hex thành Color
                {
                    descriptionText.color = color; // Gán màu cho text
                }

                // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(600, 100);
                rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                GridLayoutGroup gridLayout = descriptionPopupPanel.GetComponent<GridLayoutGroup>();
                if (gridLayout != null)
                {
                    gridLayout.cellSize = new Vector2(670, 800);
                }
            }
            else if (property.Name.Equals("power") || property.Name.Equals("rare") || property.Name.Equals("type")
            || property.Name.Equals("star") || property.Name.Equals("level") || property.Name.Equals("all_power"))
            {
                if (value != null)
                {
                    bool shouldDisplay = false;

                    if (value is int intValue)
                    {
                        shouldDisplay = intValue != -1;
                    }
                    else if (value is double doubleValue)
                    {
                        shouldDisplay = doubleValue != -1;
                    }
                    else if (value is string)
                    {
                        shouldDisplay = true;
                    }

                    if (shouldDisplay)
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, firstPopupPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = value.ToString();
                    }
                }

            }
            else if (property.Name.Equals("health") || property.Name.Equals("physical_attack") || property.Name.Equals("physical_defense") ||
                property.Name.Equals("magical_attack") || property.Name.Equals("magical_defense") ||
                property.Name.Equals("chemical_attack") || property.Name.Equals("chemical_defense") ||
                property.Name.Equals("atomic_attack") || property.Name.Equals("atomic_defense") ||
                property.Name.Equals("mental_attack") || property.Name.Equals("mental_defense") ||
                property.Name.Equals("all_health") || property.Name.Equals("all_physical_attack") || property.Name.Equals("all_physical_defense") ||
                property.Name.Equals("all_magical_attack") || property.Name.Equals("all_magical_defense") ||
                property.Name.Equals("all_chemical_attack") || property.Name.Equals("all_chemical_defense") ||
                property.Name.Equals("all_atomic_attack") || property.Name.Equals("all_atomic_defense") ||
                property.Name.Equals("all_mental_attack") || property.Name.Equals("all_mental_defense"))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
                    {
                        if (status == 1)
                        {
                            if (property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();

                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                        else if (status == 0)
                        {
                            if (!property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();

                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                    }
                }
            }
            else if (property.Name.Equals("speed") || property.Name.Equals("critical_damage_rate") || property.Name.Equals("critical_rate")
                || property.Name.Equals("critical_resistance_rate") || property.Name.Equals("ignore_critical_rate")
                || property.Name.Equals("penetration_rate") || property.Name.Equals("penetration_resistance_rate") || property.Name.Equals("evasion_rate")
                || property.Name.Equals("damage_absorption_rate") || property.Name.Equals("ignore_damage_absorption_rate") || property.Name.Equals("absorbed_damage_rate")
                || property.Name.Equals("vitality_regeneration_rate") || property.Name.Equals("vitality_regeneration_resistance_rate")
                || property.Name.Equals("all_speed") || property.Name.Equals("all_critical_damage_rate") || property.Name.Equals("all_critical_rate")
                || property.Name.Equals("all_critical_resistance_rate") || property.Name.Equals("all_ignore_critical_rate")
                || property.Name.Equals("all_penetration_rate") || property.Name.Equals("all_penetration_resistance_rate") || property.Name.Equals("all_evasion_rate")
                || property.Name.Equals("all_damage_absorption_rate") || property.Name.Equals("all_ignore_damage_absorption_rate") || property.Name.Equals("all_absorbed_damage_rate")
                || property.Name.Equals("all_vitality_regeneration_rate") || property.Name.Equals("all_vitality_regeneration_resistance_rate"))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
                    {
                        if (status == 1)
                        {
                            if (property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, element2PopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();
                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                        else if (status == 0)
                        {
                            if (!property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();

                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                    }
                }
            }
            else if (property.Name.Equals("accuracy_rate") || property.Name.Equals("lifesteal_rate")
                || property.Name.Equals("shield_strength") || property.Name.Equals("tenacity")
                || property.Name.Equals("resistance_rate") || property.Name.Equals("combo_rate") || property.Name.Equals("ignore_combo_rate")
                || property.Name.Equals("combo_damage_rate") || property.Name.Equals("combo_resistance_rate") || property.Name.Equals("stun_rate") || property.Name.Equals("ignore_stun_rate")
                || property.Name.Equals("mana") || property.Name.Equals("mana_regeneration_rate")
                || property.Name.Equals("reflection_rate") || property.Name.Equals("ignore_reflection_rate") || property.Name.Equals("reflection_damage_rate") || property.Name.Equals("reflection_resistance_rate")
                || property.Name.Equals("all_accuracy_rate") || property.Name.Equals("all_lifesteal_rate")
                || property.Name.Equals("all_shield_strength") || property.Name.Equals("all_tenacity")
                || property.Name.Equals("all_resistance_rate") || property.Name.Equals("all_combo_rate") || property.Name.Equals("all_ignore_combo_rate")
                || property.Name.Equals("all_combo_damage_rate") || property.Name.Equals("all_combo_resistance_rate") || property.Name.Equals("all_stun_rate") || property.Name.Equals("all_ignore_stun_rate")
                || property.Name.Equals("all_mana") || property.Name.Equals("all_mana_regeneration_rate")
                || property.Name.Equals("all_reflection_rate") || property.Name.Equals("all_ignore_reflection_rate") || property.Name.Equals("all_reflection_damage_rate") || property.Name.Equals("all_reflection_resistance_rate"))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
                    {
                        if (status == 1)
                        {
                            if (property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, element3PopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();
                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                        else if (status == 0)
                        {
                            if (!property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();

                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                    }
                }
            }
            else if (property.Name.Equals("damage_to_different_faction_rate") || property.Name.Equals("resistance_to_different_faction_rate")
                || property.Name.Equals("damage_to_same_faction_rate") || property.Name.Equals("resistance_to_same_faction_rate")
                || property.Name.Equals("normal_damage_rate") || property.Name.Equals("normal_resistance_rate")
                || property.Name.Equals("skill_damage_rate") || property.Name.Equals("skill_resistance_rate")
                || property.Name.Equals("all_damage_to_different_faction_rate") || property.Name.Equals("all_resistance_to_different_faction_rate")
                || property.Name.Equals("all_damage_to_same_faction_rate") || property.Name.Equals("all_resistance_to_same_faction_rate")
                || property.Name.Equals("all_normal_damage_rate") || property.Name.Equals("all_normal_resistance_rate")
                || property.Name.Equals("all_skill_damage_rate") || property.Name.Equals("all_skill_resistance_rate"))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
                    {
                        if (status == 1)
                        {
                            if (property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, element4PopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();
                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                        else if (status == 0)
                        {
                            if (!property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();

                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                    }
                }
            }
        }
    }
    public void CreatePropertyRuneUI(string title, RawImage runeImage)
    {
        Texture runeTexture;
        if (title.Equals("physical_attack") || title.Equals("all_physical_attack"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Physical_Attack");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("physical_defense") || title.Equals("all_physical_defense"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Physical_Defense");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("magical_attack") || title.Equals("all_magical_attack"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Magic_Attack");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("magical_defense") || title.Equals("all_magical_defense"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Magic_Defense");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("chemical_attack") || title.Equals("all_chemical_attack"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Chemical_Attack");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("chemical_defense") || title.Equals("all_chemical_defense"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Chemical_Defense");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("atomic_attack") || title.Equals("all_atomic_attack"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Atomic_Attack");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("atomic_defense") || title.Equals("all_atomic_defense"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Atomic_Defense");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("mental_attack") || title.Equals("all_mental_attack"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Mental_Attack");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("mental_defense") || title.Equals("all_mental_defense"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Mental_Defense");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("health") || title.Equals("all_health"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Mental_1");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("speed") || title.Equals("critical_damage_rate") || title.Equals("critical_rate") ||
                title.Equals("critical_resistance_rate") || title.Equals("ignore_critical_rate") ||
                title.Equals("penetration_rate") || title.Equals("penetration_resistance_rate") || title.Equals("evasion_rate") ||
                title.Equals("damage_absorption_rate") || title.Equals("ignore_damage_absorption_rate") || title.Equals("absorbed_damage_rate") ||
                title.Equals("vitality_regeneration_rate") || title.Equals("vitality_regeneration_resistance_rate") ||
                title.Equals("all_speed") || title.Equals("all_critical_damage_rate") || title.Equals("all_critical_rate") ||
                title.Equals("all_critical_resistance_rate") || title.Equals("all_ignore_critical_rate") ||
                title.Equals("all_penetration_rate") || title.Equals("all_penetration_resistance_rate") || title.Equals("all_evasion_rate") ||
                title.Equals("all_damage_absorption_rate") || title.Equals("all_ignore_damage_absorption_rate") || title.Equals("all_absorbed_damage_rate") ||
                title.Equals("all_vitality_regeneration_rate") || title.Equals("all_vitality_regeneration_resistance_rate"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Atomic_1");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("accuracy_rate") || title.Equals("lifesteal_rate") ||
                title.Equals("shield_strength") || title.Equals("tenacity") ||
                title.Equals("resistance_rate") || title.Equals("combo_rate") || title.Equals("ignore_combo_rate") ||
                title.Equals("combo_damage_rate") || title.Equals("combo_resistance_rate") || title.Equals("stun_rate") || title.Equals("ignore_stun_rate") ||
                title.Equals("mana") || title.Equals("mana_regeneration_rate") ||
                title.Equals("reflection_rate") || title.Equals("ignore_reflection_rate") || title.Equals("reflection_damage_rate") || title.Equals("reflection_resistance_rate") ||
                title.Equals("all_accuracy_rate") || title.Equals("all_lifesteal_rate") ||
                title.Equals("all_shield_strength") || title.Equals("all_tenacity") ||
                title.Equals("all_resistance_rate") || title.Equals("all_combo_rate") || title.Equals("all_ignore_combo_rate") ||
                title.Equals("all_combo_damage_rate") || title.Equals("all_combo_resistance_rate") || title.Equals("all_stun_rate") || title.Equals("all_ignore_stun_rate") ||
                title.Equals("all_mana") || title.Equals("all_mana_regeneration_rate") ||
                title.Equals("all_reflection_rate") || title.Equals("all_ignore_reflection_rate") || title.Equals("all_reflection_damage_rate") || title.Equals("all_reflection_resistance_rate"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Chemical_1");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("damage_to_different_faction_rate") || title.Equals("resistance_to_different_faction_rate") ||
                title.Equals("damage_to_same_faction_rate") || title.Equals("resistance_to_same_faction_rate") ||
                title.Equals("normal_damage_rate") || title.Equals("normal_resistance_rate") ||
                title.Equals("skill_damage_rate") || title.Equals("skill_resistance_rate") ||
                title.Equals("all_damage_to_different_faction_rate") || title.Equals("all_resistance_to_different_faction_rate") ||
                title.Equals("all_damage_to_same_faction_rate") || title.Equals("all_resistance_to_same_faction_rate") ||
                title.Equals("all_normal_damage_rate") || title.Equals("all_normal_resistance_rate") ||
                title.Equals("all_skill_damage_rate") || title.Equals("all_skill_resistance_rate"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Magic_1");
            runeImage.texture = runeTexture;
        }
        runeImage.gameObject.SetActive(true);
    }
    public void CreatePropertyLevelUI(PropertyInfo[] properties, object targetObject, double increasePerLevel, GameObject currentObject)
    {
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(targetObject, null);
            CreateSinglePropertyLevelUI(property, value, increasePerLevel, LevelElementContent, currentObject);
        }
    }
    public void CreateSinglePropertyLevelUI(PropertyInfo property, object value, double increasePerLevel,
    Transform LevelElementContent, GameObject currentObject)
    {

        if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
                && !property.Name.Equals("status") && !property.Name.Equals("name")
                && !property.Name.Equals("image") && !property.Name.Equals("description") && !property.Name.Equals("power")
                && !property.Name.Equals("rare") && !property.Name.Equals("type")
                && !property.Name.Equals("star") && !property.Name.Equals("level"))
        {
            // Kiểm tra nếu value không phải null
            if (value != null)
            {
                if (value is double intValue && intValue != -1)
                {
                    if (!property.Name.Contains("all"))
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, LevelElementContent);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                        {
                            double newintValue = increasePerLevel * intValue;
                            elementContentText.text = "+" + newintValue.ToString();
                            Color greenColor;
                            if (ColorUtility.TryParseHtmlString("#32CD32", out greenColor)) // Màu xanh lá LimeGreen
                            {
                                elementContentText.color = greenColor;
                                elementContentText.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, Color.green); // Màu phát sáng
                                elementContentText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.5f); // Độ mạnh phát sáng (giảm giá trị
                            }
                        }
                    }
                }
            }
        }
        else if (property.Name.Equals("level"))
        {
            CreateLevelUI((int)value, currentObject);
        }
    }
    public void CreatePropertyUpgradeUI(PropertyInfo property, object value, double increasePerUpgrade, GameObject currentObject)
    {
        Transform UpgradeElementContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
                && !property.Name.Equals("status") && !property.Name.Equals("name")
                && !property.Name.Equals("image") && !property.Name.Equals("description") && !property.Name.Equals("power")
                && !property.Name.Equals("rare") && !property.Name.Equals("type")
                && !property.Name.Equals("star") && !property.Name.Equals("level"))
        {
            // Kiểm tra nếu value không phải null
            if (value != null)
            {
                if (value is double intValue && intValue != -1)
                {
                    if (!property.Name.Contains("all"))
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, UpgradeElementContent);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                        {
                            double newintValue = increasePerUpgrade * intValue;
                            elementContentText.text = "+" + newintValue.ToString();
                            Color greenColor;
                            if (ColorUtility.TryParseHtmlString("#32CD32", out greenColor)) // Màu xanh lá LimeGreen
                            {
                                elementContentText.color = greenColor;
                                elementContentText.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, Color.green); // Màu phát sáng
                                elementContentText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.5f); // Độ mạnh phát sáng (giảm giá trị
                            }
                        }
                    }
                }
            }
        }
        else if (property.Name.Equals("star"))
        {
            TextMeshProUGUI currentLevelText = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/CurrentLevelText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nextLevelText = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/NextLevelText").GetComponent<TextMeshProUGUI>();
            currentLevelText.text = value.ToString();
            int nextLevel = (int)value + 1;
            if ((int)value == 100000)
            {
                nextLevelText.text = "Max";
            }
            else
            {
                nextLevelText.text = nextLevel.ToString();
            }
        }
    }
    public void CreateMaterialUI(List<Items> items, GameObject currentObject)
    {
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        foreach (Items items1 in items)
        {
            GameObject itemObject = Instantiate(ItemThird, LevelMaterialContent);

            RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = items1.image.Replace(".png", "");
            Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            eImage.texture = equipmentTexture;

            TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            eQuantity.text = items1.quantity.ToString();
        }
    }
    public void CreateStarUI(int star, GameObject currentObject)
    {
        Transform currentStar = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/CurrentStar");
        Transform nextStar = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/NextStar");
        int currentimageIndex = (star == 0) ? 0 : ((star - 1) % 10) + 1;
        int currentstarIndex = (star == 0) ? 0 : (star - 1) / 10;
        int newStar = (star + 1 > 100000) ? 0 : star + 1;
        int nextimageIndex = (newStar == 0) ? 0 : ((newStar - 1) % 10) + 1;
        int nextstarIndex = (newStar == 0) ? 0 : (newStar - 1) / 10;
        for (int i = 0; i < currentimageIndex; i++)
        {
            GameObject starObject = Instantiate(StarPrefab, currentStar);

            RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
            GetStarImage(starImage, currentstarIndex);
        }
        for (int i = 0; i < nextimageIndex; i++)
        {
            GameObject starObject = Instantiate(StarPrefab, nextStar);

            RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
            GetStarImage(starImage, nextstarIndex);
        }
        GridLayoutGroup currentGridLayout = currentStar.GetComponent<GridLayoutGroup>();
        if (currentGridLayout != null)
        {
            currentGridLayout.cellSize = new Vector2(20, 20);
        }
        GridLayoutGroup nextGridLayout = nextStar.GetComponent<GridLayoutGroup>();
        if (nextGridLayout != null)
        {
            nextGridLayout.cellSize = new Vector2(20, 20);
        }
    }
    public void GetStarImage(RawImage starImage, int starIndex)
    {
        Texture starTexture = Resources.Load<Texture>($"UI/UI/Star1");
        switch (starIndex)
        {
            case 0:
                starTexture = Resources.Load<Texture>($"UI/UI/Star1");
                starImage.texture = starTexture;
                break;
            case 1:
                starTexture = Resources.Load<Texture>($"UI/UI/Star2");
                starImage.texture = starTexture;
                break;
            case 2:
                starTexture = Resources.Load<Texture>($"UI/UI/Star3");
                starImage.texture = starTexture;
                break;
            case 3:
                starTexture = Resources.Load<Texture>($"UI/UI/Star4");
                starImage.texture = starTexture;
                break;
            case 4:
                starTexture = Resources.Load<Texture>($"UI/UI/Star5");
                starImage.texture = starTexture;
                break;
            case 5:
                starTexture = Resources.Load<Texture>($"UI/UI/Star6");
                starImage.texture = starTexture;
                break;
            case 6:
                starTexture = Resources.Load<Texture>($"UI/UI/Star7");
                starImage.texture = starTexture;
                break;
            case 7:
                starTexture = Resources.Load<Texture>($"UI/UI/Star8");
                starImage.texture = starTexture;
                break;
            case 8:
                starTexture = Resources.Load<Texture>($"UI/UI/Star9");
                starImage.texture = starTexture;
                break;
            case 9:
                starTexture = Resources.Load<Texture>($"UI/UI/Star10");
                starImage.texture = starTexture;
                break;
            default:
                starTexture = Resources.Load<Texture>($"UI/UI/Star1");
                starImage.texture = starTexture;
                break;
        }
    }
}
    