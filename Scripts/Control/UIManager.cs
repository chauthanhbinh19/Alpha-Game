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
    public GameObject TeamsPrefab;
    public GameObject CardsThirdPrefab;
    public GameObject TypePrefab;
    public GameObject StarPrefab;
    public GameObject PowerPrefab;
    public GameObject LoadingPanelPrefab;
    public GameObject MainMenuEquipmentPanelPrefab;
    public GameObject MainMenuAptitudePanelPrefab;
    public GameObject MainMenuUpgradePanelPrefab;
    public GameObject MainMenuAffinityPanelPrefab;
    public GameObject MainMenuBlessingPanelPrefab;
    public GameObject MainMenuCorePanelPrefab;
    public GameObject MainMenuRealmPanelPrefab;
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
    public GameObject AptitudeSlotPrefab;
    public GameObject UpgradeSlotPrefab;
    public GameObject BlessingSlotPrefab;
    public GameObject CoreSlotPrefab;
    public GameObject RealmSlotPrefab;

    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
            mainMenuButtonPanel = MainScencePanel.transform.Find("MainMenu/MenuBackground/MainMenuButton");
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
            case "TeamsPrefab":
                return TeamsPrefab;
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
