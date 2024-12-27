using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton instance
    public static UIManager Instance { get; private set; }

    [Header("Panels")]
    public Transform mainMenuButtonPanel;
    public Transform galleryMenuPanel;
    public Transform collectionMenuPanel;
    public Transform equipmentMenuPanel;
    public Transform notificationPanel; 
    public Transform MainPanel;
    public Transform popupPanel;

    // Prefab references
    [Header("GameObject")]
    public GameObject buttonPrefab;
    public GameObject notificationPrefab;
    public GameObject GalleryPanel;
    public GameObject CollectionPanel;
    public GameObject EquipmentsPanel;
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

    [Header("Button")]
    public Button GalleryButton;
    public Button CollectionButton;
    public Button EquipmentsButton;
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
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
            default:
                Debug.LogWarning($"Panel {panelName} not found.");
                return null;
        }
    }
    public GameObject GetGameObject(string prefabName)
    {
        switch (prefabName)
        {
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
