using System.Collections;
using System.Collections.Generic;
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
            case "MainMenuAnimePanelPrefab":
                return MainMenuAnimePanelPrefab;
            case "ArenaPanelPrefab":
                return ArenaPanelPrefab;
            case "AnimePanelPrefab":
                return AnimePanelPrefab;
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
            // MainMenu Slot Set 4
            case "ZarxSlotPrefab": return ZarxSlotPrefab;
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
}
