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
    private Transform galleryMenuPanel;
    private Transform collectionMenuPanel;
    private Transform equipmentMenuPanel;
    public Transform notificationPanel; 
    public Transform WaitingPanel;
    public Transform MainScencePanel;
    public Transform MainPanel;
    public Transform popupPanel;
    public Transform summonPanel;
    public Transform currencyPanel;
    public Transform userPanel;

    // Prefab references
    [Header("GameObject")]
    public GameObject SignInPanel;
    public GameObject SignUpPanel;
    public GameObject CreateNamePanel;
    public GameObject buttonPrefab;
    public GameObject notificationPrefab;
    private GameObject GalleryPanel;
    private GameObject CollectionPanel;
    private GameObject EquipmentsPanel;
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
    public GameObject MainMenuDetailPanelPrefab;
    public GameObject SummonPanelPrefab;
    public GameObject PositionPrefab;
    public GameObject CampaignPrefab;
    public GameObject CampaignDetailPrefab;
    public GameObject ShopManagerPrefab;
    public GameObject ShopButtonPrefab;
    public GameObject ShopPrefab;

    [Header("Button")]
    private Button GalleryButton;
    private Button CollectionButton;
    private Button EquipmentsButton;
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
            GalleryPanel = MainScencePanel.transform.Find("GalleryMenu")?.gameObject;
            CollectionPanel = MainScencePanel.transform.Find("CollectionMenu")?.gameObject;
            EquipmentsPanel = MainScencePanel.transform.Find("EquipmentMenu")?.gameObject;
            GalleryButton = MainScencePanel.transform.Find("GalleryButton").GetComponent<Button>();
            CollectionButton = MainScencePanel.transform.Find("CollectionButton").GetComponent<Button>();
            EquipmentsButton = MainScencePanel.transform.Find("EquipmentButton").GetComponent<Button>();
            galleryMenuPanel = MainScencePanel.transform.Find("GalleryMenu/GalleryMenuPanel");
            collectionMenuPanel = MainScencePanel.transform.Find("CollectionMenu/CollectionMenuPanel");
            equipmentMenuPanel = MainScencePanel.transform.Find("EquipmentMenu/Scroll View/Viewport/EquipmentMenuPanel");
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
            case "galleryMenuPanel":
                return galleryMenuPanel;
            case "collectionMenuPanel":
                return collectionMenuPanel;
            case "equipmentMenuPanel":
                return equipmentMenuPanel;
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
            case "GalleryPanel":
                return GalleryPanel;
            case "CollectionPanel":
                return CollectionPanel;
            case "EquipmentsPanel":
                return EquipmentsPanel;
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
            case "EquipmentSecondPrefab":
                return EquipmentSecondPrefab;
            case "EquipmentFirstPrefab":
                return EquipmentFirstPrefab;
            case "ElementDetailsPrefab":
                return ElementDetailsPrefab;
            case "MainMenuDetailPanelPrefab":
                return MainMenuDetailPanelPrefab;
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
            default:
                Debug.LogWarning($"Prefab {prefabName} not found.");
                return null;
        }
    }
    public Button GetButton(string prefabName)
    {
        switch (prefabName)
        {
            case "GalleryButton":
                return GalleryButton;
            case "CollectionButton":
                return CollectionButton;
            case "EquipmentsButton":
                return EquipmentsButton;
            default:
                Debug.LogWarning($"Button {prefabName} not found.");
                return null;
        }
    }
}
