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
    private Transform mainMenuSubButtonGroupPanel;
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
    public GameObject itemSecondPrefab;
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
    public GameObject EquipmentFourthPrefab;
    public GameObject ArtworkFirstPrefab;
    public GameObject ArtworkSecondPrefab;
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
    public GameObject TabButton6;
    public GameObject AdvancedButtonFirst;
    public GameObject PopupTeamsPrefab;
    public GameObject TeamsPanelPrefab;
    public GameObject TeamsPositionPrefab;
    public GameObject TeamsTypePrefab;
    public GameObject CardsThirdPrefab;
    public GameObject TypePrefab;
    public GameObject StarPrefab;
    public GameObject PowerPrefab;
    public GameObject LoadingPanelPrefab;
    public GameObject SkillPanelPrefab;
    public GameObject SkillGroupPrefab;
    public GameObject Skill1Prefab;
    public GameObject Skill2Prefab;
    public GameObject PopupSkillDetailPrefab;
    [Header("Science Fiction")]
    public GameObject ReactorPanelPrefab;
    public GameObject ReactorButtonPrefab;
    public GameObject ReactorPanelNumberPrefab;
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
    [Header("Master")]
    public GameObject MasterOfBeastPanelPrefab;
    public GameObject MasterOfBeastSlotPrefab;
    public GameObject MasterOfDragonPanelPrefab;
    public GameObject MasterOfDragonSlotPrefab;
    public GameObject MasterOfMagicPanelPrefab;
    public GameObject MasterOfMagicSlotPrefab;
    public GameObject MasterOfMusicPanelPrefab;
    public GameObject MasterOfMusicSlotPrefab;
    public GameObject MasterOfSciencePanelPrefab;
    public GameObject MasterOfScienceSlotPrefab;
    public GameObject MasterOfSpiritPanelPrefab;
    public GameObject MasterOfSpiritSlotPrefab;
    public GameObject MasterOfWeaponPanelPrefab;
    public GameObject MasterOfWeaponSlotPrefab;
    public GameObject MasterOfChemicalPanelPrefab;
    public GameObject MasterOfChemicalSlotPrefab;
    public GameObject MasterOfPhysicalPanelPrefab;
    public GameObject MasterOfPhysicalSlotPrefab;
    public GameObject MasterOfAtomicPanelPrefab;
    public GameObject MasterOfAtomicSlotPrefab;
    public GameObject MasterOfMentalPanelPrefab;
    public GameObject MasterOfMentalSlotPrefab;
    [Header("Other")]
    public GameObject MainMenuAnimePanelPrefab;
    public GameObject AnimePanelPrefab;
    public GameObject MasterBoardPanelPrefab;
    public GameObject DailyCheckinPanelPrefab;
    public GameObject ArenaPanelPrefab;
    public GameObject ArenaDetailsPanelPrefab;
    public GameObject TowerDetailsPanelPrefab;
    public GameObject PopupEquipmentsPanelPrefab;
    public GameObject PopupSpiritBeastPanelPrefab;
    public GameObject PopupSkillsPanelPrefab;
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
    [Header("Font")]
    public TMP_FontAsset EuroStyleNormalFont;

    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
            mainMenuButtonPanel = MainScencePanel.transform.Find("MainMenu/MenuBackground/MainMenuButton");
            mainMenuCampaignPanel = MainScencePanel.transform.Find("MainMenu/MenuCampaignBackground/MenuCampaign");
            mainMenuSubButtonGroupPanel = MainScencePanel.transform.Find("MainMenu/MainMenuSubButtonGroup");
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
            case "mainMenuSubButtonGroupPanel":
                return mainMenuSubButtonGroupPanel;
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
            case "itemSecondPrefab":
                return itemSecondPrefab;
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
            case "EquipmentFourthPrefab":
                return EquipmentFourthPrefab;
            case "ArtworkSecondPrefab":
                return ArtworkSecondPrefab;
            case "ArtworkFirstPrefab":
                return ArtworkFirstPrefab;
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
            case "TeamsTypePrefab":
                return TeamsTypePrefab;
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
            case "SkillGroupPrefab":
                return SkillGroupPrefab;
            case "SkillPanelPrefab":
                return SkillPanelPrefab;
            case "Skill1Prefab":
                return Skill1Prefab;
            case "Skill2Prefab":
                return Skill2Prefab;
            case "PopupSkillDetailPrefab":
                return PopupSkillDetailPrefab;
            //Market
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
            case "PopupSpiritBeastPanelPrefab":
                return PopupSpiritBeastPanelPrefab;
            case "PopupMenuPanelPrefab":
                return PopupMenuPanelPrefab;
            case "PopupSkillsPanelPrefab":
                return PopupSkillsPanelPrefab;
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
            case "TabButton6":
                return TabButton6;
            case "AdvancedButtonFirst":
                return AdvancedButtonFirst;
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
    public GameObject GetGameObjectMaster(string prefabName)
    {
        switch (prefabName)
        {
            case "MasterOfBeastPanelPrefab":
                return MasterOfBeastPanelPrefab;
            case "MasterOfBeastSlotPrefab":
                return MasterOfBeastSlotPrefab;

            case "MasterOfDragonPanelPrefab":
                return MasterOfDragonPanelPrefab;
            case "MasterOfDragonSlotPrefab":
                return MasterOfDragonSlotPrefab;

            case "MasterOfMagicPanelPrefab":
                return MasterOfMagicPanelPrefab;
            case "MasterOfMagicSlotPrefab":
                return MasterOfMagicSlotPrefab;

            case "MasterOfMusicPanelPrefab":
                return MasterOfMusicPanelPrefab;
            case "MasterOfMusicSlotPrefab":
                return MasterOfMusicSlotPrefab;

            case "MasterOfSciencePanelPrefab":
                return MasterOfSciencePanelPrefab;
            case "MasterOfScienceSlotPrefab":
                return MasterOfScienceSlotPrefab;

            case "MasterOfSpiritPanelPrefab":
                return MasterOfSpiritPanelPrefab;
            case "MasterOfSpiritSlotPrefab":
                return MasterOfSpiritSlotPrefab;

            case "MasterOfWeaponPanelPrefab":
                return MasterOfWeaponPanelPrefab;
            case "MasterOfWeaponSlotPrefab":
                return MasterOfWeaponSlotPrefab;

            case "MasterOfChemicalPanelPrefab":
                return MasterOfChemicalPanelPrefab;
            case "MasterOfChemicalSlotPrefab":
                return MasterOfChemicalSlotPrefab;

            case "MasterOfPhysicalPanelPrefab":
                return MasterOfPhysicalPanelPrefab;
            case "MasterOfPhysicalSlotPrefab":
                return MasterOfPhysicalSlotPrefab;

            case "MasterOfAtomicPanelPrefab":
                return MasterOfAtomicPanelPrefab;
            case "MasterOfAtomicSlotPrefab":
                return MasterOfAtomicSlotPrefab;

            case "MasterOfMentalPanelPrefab":
                return MasterOfMentalPanelPrefab;
            case "MasterOfMentalSlotPrefab":
                return MasterOfMentalSlotPrefab;

            default:
                Debug.LogWarning($"Prefab name '{prefabName}' not found!");
                return null;
        }
    }
    public GameObject GetGameObjectScienceFiction(string prefabName)
    {
        switch (prefabName)
        {
            case "ReactorPanelPrefab":
                return ReactorPanelPrefab;
            case "ReactorButtonPrefab":
                return ReactorButtonPrefab;

            case "ReactorPanelNumberPrefab":
                return ReactorPanelNumberPrefab;

            default:
                Debug.LogWarning($"Prefab name '{prefabName}' not found!");
                return null;
        }
    }
    public TMP_FontAsset GetTMPFontAsset(string fontName)
    {
        switch (fontName)
        {
            case "EuroStyleNormalFont":
                return EuroStyleNormalFont;
            default:
                return EuroStyleNormalFont;
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
        if (mainType.Equals(AppConstants.MainMenuSet1.Affinity) || mainType.Equals(AppConstants.MainMenuSet1.Blessing))
        {
            return;
        }
        Transform BackgroundImageTransform = gameObject.transform.Find("Background");
        if (BackgroundImageTransform != null)
        {
            RawImage BackgroundImage = gameObject.transform.Find("Background").GetComponent<RawImage>();
            Texture backgroundTexture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            BackgroundImage.texture = backgroundTexture;
            BackgroundImage.rectTransform.sizeDelta = new Vector2(350, 350);
        }

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
    public void SetMaterialUI(GameObject gameobject, string itemImage, int itemQuantity, int currencyQuantity, int rankLevel, int maxLevel)
    {
        Transform currencyPanel = gameobject.transform.Find("DictionaryCards/Currency");
        List<Currency> currencies = UserCurrencyService.Create().GetUserCurrency();
        ButtonEvent.Instance.Close(currencyPanel);
        CurrencyManager.Instance.GetMainCurrency(currencies, currencyPanel);

        var oneResult = EvaluateItem.CalculateLevelUp(itemQuantity, currencyQuantity, 1, 10, rankLevel, false, maxLevel);
        var maxResult = EvaluateItem.CalculateLevelUp(itemQuantity, currencyQuantity, 1, 10, rankLevel, true, maxLevel);
        RawImage OneLevelCurrencyImage = gameobject.transform.Find("DictionaryCards/OneLevelCurrency/CurrencyImage").GetComponent<RawImage>();
        RawImage MaxLevelCurrencyImage = gameobject.transform.Find("DictionaryCards/MaxLevelCurrency/CurrencyImage").GetComponent<RawImage>();
        Texture OneLevelCurrencyTexture = Resources.Load<Texture>($"{ImageConstants.Currency.Silver}");
        Texture MaxLevelCurrencyTexture = Resources.Load<Texture>($"{ImageConstants.Currency.Silver}");
        OneLevelCurrencyImage.texture = OneLevelCurrencyTexture;
        MaxLevelCurrencyImage.texture = MaxLevelCurrencyTexture;

        TextMeshProUGUI OneLevelCurrencyText = gameobject.transform.Find("DictionaryCards/OneLevelCurrency/QuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI MaxLevelCurrencyText = gameobject.transform.Find("DictionaryCards/MaxLevelCurrency/QuantityText").GetComponent<TextMeshProUGUI>();
        OneLevelCurrencyText.text = oneResult.totalCurrencyUsed.ToString();
        MaxLevelCurrencyText.text = maxResult.totalCurrencyUsed.ToString();

        Transform OneLevelMaterial = gameobject.transform.Find("DictionaryCards/OneLevelMaterial");
        Transform MaxLevelMaterial = gameobject.transform.Find("DictionaryCards/MaxLevelMaterial");
        ButtonEvent.Instance.Close(OneLevelMaterial);
        ButtonEvent.Instance.Close(MaxLevelMaterial);
        GameObject oneLevelMaterialObject = Instantiate(ElementDetails2Prefab, OneLevelMaterial);
        GameObject maxLevelMaterialObject = Instantiate(ElementDetails2Prefab, MaxLevelMaterial);

        RawImage oneLevelImage = oneLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture oneLevelTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(itemImage)}");
        oneLevelImage.texture = oneLevelTexture;

        // RectTransform oneLevelRectTransform = oneLevelImage.GetComponent<RectTransform>();
        // oneLevelRectTransform.sizeDelta = new Vector2(40, 40);

        TextMeshProUGUI oneLevelQuantity = oneLevelMaterialObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        oneLevelQuantity.text = itemQuantity + "/" + oneResult.totalMaterialUsed;

        RawImage maxLevelImage = maxLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture maxLevelTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(itemImage)}");
        maxLevelImage.texture = maxLevelTexture;

        TextMeshProUGUI maxLevelQuantity = maxLevelMaterialObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        maxLevelQuantity.text = itemQuantity + "/" + maxResult.totalMaterialUsed;

        // RectTransform maxLevelRectTransform = maxLevelImage.GetComponent<RectTransform>();
        // maxLevelRectTransform.sizeDelta = new Vector2(40, 40);
    }
    public void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = Resources.Load<Texture>($"{image}");
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
            CreateSinglePropertyUI(property, value,
                firstPopupPanel, elementPopupPanel, element2PopupPanel, element3PopupPanel, element4PopupPanel, descriptionPopupPanel);
        }
    }
    public void CreateSinglePropertyUI(PropertyInfo property, object value,
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
                if (ColorUtility.TryParseHtmlString(ColorConstants.HexColor.descriptionColor, out color)) // Chuyển mã hex thành Color
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
            else if (property.Name.Equals(AppConstants.StatFields.Health)
            || property.Name.Equals(AppConstants.StatFields.PhysicalAttack)
            || property.Name.Equals(AppConstants.StatFields.PhysicalDefense)
            || property.Name.Equals(AppConstants.StatFields.MagicalAttack)
            || property.Name.Equals(AppConstants.StatFields.MagicalDefense)
            || property.Name.Equals(AppConstants.StatFields.ChemicalAttack)
            || property.Name.Equals(AppConstants.StatFields.ChemicalDefense)
            || property.Name.Equals(AppConstants.StatFields.AtomicAttack)
            || property.Name.Equals(AppConstants.StatFields.AtomicDefense)
            || property.Name.Equals(AppConstants.StatFields.MentalAttack)
            || property.Name.Equals(AppConstants.StatFields.MentalDefense))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
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
                            elementContentText.text = NumberFormatter.FormatNumber(intValue, false);

                        RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                        CreatePropertyRuneUI(property.Name, runeImage);
                    }
                }
            }
            else if (property.Name.Equals(AppConstants.StatFields.Speed)
            || property.Name.Equals(AppConstants.StatFields.CriticalDamageRate)
            || property.Name.Equals(AppConstants.StatFields.CriticalRate)
            || property.Name.Equals(AppConstants.StatFields.CriticalResistanceRate)
            || property.Name.Equals(AppConstants.StatFields.IgnoreCriticalRate)
            || property.Name.Equals(AppConstants.StatFields.PenetrationRate)
            || property.Name.Equals(AppConstants.StatFields.PenetrationResistanceRate)
            || property.Name.Equals(AppConstants.StatFields.EvasionRate)
            || property.Name.Equals(AppConstants.StatFields.DamageAbsorptionRate)
            || property.Name.Equals(AppConstants.StatFields.IgnoreDamageAbsorptionRate)
            || property.Name.Equals(AppConstants.StatFields.AbsorbedDamageRate)
            || property.Name.Equals(AppConstants.StatFields.VitalityRegenerationRate)
            || property.Name.Equals(AppConstants.StatFields.VitalityRegenerationResistanceRate))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
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
            }
            else if (property.Name.Equals(AppConstants.StatFields.AccuracyRate)
            || property.Name.Equals(AppConstants.StatFields.LifestealRate)
            || property.Name.Equals(AppConstants.StatFields.ShieldStrength)
            || property.Name.Equals(AppConstants.StatFields.Tenacity)
            || property.Name.Equals(AppConstants.StatFields.ResistanceRate)
            || property.Name.Equals(AppConstants.StatFields.ComboRate)
            || property.Name.Equals(AppConstants.StatFields.IgnoreComboRate)
            || property.Name.Equals(AppConstants.StatFields.ComboDamageRate)
            || property.Name.Equals(AppConstants.StatFields.ComboResistanceRate)
            || property.Name.Equals(AppConstants.StatFields.StunRate)
            || property.Name.Equals(AppConstants.StatFields.IgnoreStunRate)
            || property.Name.Equals(AppConstants.StatFields.Mana)
            || property.Name.Equals(AppConstants.StatFields.ManaRegenerationRate)
            || property.Name.Equals(AppConstants.StatFields.ReflectionRate)
            || property.Name.Equals(AppConstants.StatFields.IgnoreReflectionRate)
            || property.Name.Equals(AppConstants.StatFields.ReflectionDamageRate)
            || property.Name.Equals(AppConstants.StatFields.ReflectionResistanceRate))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
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
            }
            else if (property.Name.Equals(AppConstants.StatFields.DamageToDifferentFactionRate)
            || property.Name.Equals(AppConstants.StatFields.ResistanceToDifferentFactionRate)
            || property.Name.Equals(AppConstants.StatFields.DamageToSameFactionRate)
            || property.Name.Equals(AppConstants.StatFields.ResistanceToSameFactionRate)
            || property.Name.Equals(AppConstants.StatFields.NormalDamageRate)
            || property.Name.Equals(AppConstants.StatFields.NormalResistanceRate)
            || property.Name.Equals(AppConstants.StatFields.SkillDamageRate)
            || property.Name.Equals(AppConstants.StatFields.SkillResistanceRate))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
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
            }
        }
    }
    public void CreatePropertyRuneUI(string title, RawImage runeImage)
    {
        Texture runeTexture;
        if (title.Equals(AppConstants.StatFields.PhysicalAttack))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.PhysicalAttackUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.PhysicalDefense))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.PhysicalDefenseUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MagicalAttack))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MagicalAttackUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MagicalDefense))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MagicalDefenseUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ChemicalAttack))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.ChemicalAttackUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ChemicalDefense))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.ChemicalDefenseUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.AtomicAttack))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.AtomicAttackUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.AtomicDefense))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.AtomicDefenseUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MentalAttack))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MentalAttackUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MentalDefense))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MentalDefenseUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.Health))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.Mental1Url);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.Speed)
        || title.Equals(AppConstants.StatFields.CriticalDamageRate)
        || title.Equals(AppConstants.StatFields.CriticalRate)
        || title.Equals(AppConstants.StatFields.CriticalResistanceRate)
        || title.Equals(AppConstants.StatFields.IgnoreCriticalRate)
        || title.Equals(AppConstants.StatFields.PenetrationRate)
        || title.Equals(AppConstants.StatFields.PenetrationResistanceRate)
        || title.Equals(AppConstants.StatFields.EvasionRate)
        || title.Equals(AppConstants.StatFields.DamageAbsorptionRate)
        || title.Equals(AppConstants.StatFields.IgnoreDamageAbsorptionRate)
        || title.Equals(AppConstants.StatFields.AbsorbedDamageRate)
        || title.Equals(AppConstants.StatFields.VitalityRegenerationRate)
        || title.Equals(AppConstants.StatFields.VitalityRegenerationResistanceRate))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.Atomic1Url}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.AccuracyRate)
        || title.Equals(AppConstants.StatFields.LifestealRate)
        || title.Equals(AppConstants.StatFields.ShieldStrength)
        || title.Equals(AppConstants.StatFields.Tenacity)
        || title.Equals(AppConstants.StatFields.ResistanceRate)
        || title.Equals(AppConstants.StatFields.ComboRate)
        || title.Equals(AppConstants.StatFields.IgnoreComboRate)
        || title.Equals(AppConstants.StatFields.ComboDamageRate)
        || title.Equals(AppConstants.StatFields.ComboResistanceRate)
        || title.Equals(AppConstants.StatFields.StunRate)
        || title.Equals(AppConstants.StatFields.IgnoreStunRate)
        || title.Equals(AppConstants.StatFields.Mana)
        || title.Equals(AppConstants.StatFields.ManaRegenerationRate)
        || title.Equals(AppConstants.StatFields.ReflectionRate)
        || title.Equals(AppConstants.StatFields.IgnoreReflectionRate)
        || title.Equals(AppConstants.StatFields.ReflectionDamageRate)
        || title.Equals(AppConstants.StatFields.ReflectionResistanceRate))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.Chemical1Url}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.DamageToDifferentFactionRate) || title.Equals(AppConstants.StatFields.ResistanceToDifferentFactionRate) ||
         title.Equals(AppConstants.StatFields.DamageToSameFactionRate) || title.Equals(AppConstants.StatFields.ResistanceToSameFactionRate) ||
         title.Equals(AppConstants.StatFields.NormalDamageRate) || title.Equals(AppConstants.StatFields.NormalResistanceRate) ||
         title.Equals(AppConstants.StatFields.SkillDamageRate) || title.Equals(AppConstants.StatFields.SkillResistanceRate))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.Magic1Url}");
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
                            elementContentText.text = "+" + NumberFormatter.FormatNumber(newintValue, false);
                            Color greenColor;
                            if (ColorUtility.TryParseHtmlString(ColorConstants.Green, out greenColor)) // Màu xanh lá LimeGreen
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
                            elementContentText.text = "+" + NumberFormatter.FormatNumber(newintValue, false);
                            Color greenColor;
                            if (ColorUtility.TryParseHtmlString(ColorConstants.Green, out greenColor)) // Màu xanh lá LimeGreen
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
        foreach (Items item in items)
        {
            GameObject itemObject = Instantiate(ItemThird, LevelMaterialContent);

            RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
            Texture equipmentTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(item.image)}");
            eImage.texture = equipmentTexture;

            TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            eQuantity.text = NumberFormatter.FormatNumber(item.quantity, false);
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
        Texture starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star1Url}");
        switch (starIndex)
        {
            case 0:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star1Url}");
                starImage.texture = starTexture;
                break;
            case 1:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star2Url}");
                starImage.texture = starTexture;
                break;
            case 2:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star3Url}");
                starImage.texture = starTexture;
                break;
            case 3:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star4Url}");
                starImage.texture = starTexture;
                break;
            case 4:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star5Url}");
                starImage.texture = starTexture;
                break;
            case 5:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star6Url}");
                starImage.texture = starTexture;
                break;
            case 6:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star7Url}");
                starImage.texture = starTexture;
                break;
            case 7:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star8Url}");
                starImage.texture = starTexture;
                break;
            case 8:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star9Url}");
                starImage.texture = starTexture;
                break;
            case 9:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star10Url}");
                starImage.texture = starTexture;
                break;
            default:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.Star1Url}");
                starImage.texture = starTexture;
                break;
        }
    }
}
