using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArchiveManager : MonoBehaviour
{
    public static ArchiveManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ArchivePanelPrefab;
    private GameObject ArchiveButtonPrefab;
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
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ArchivePanelPrefab = UIManager.Instance.Get("ArchivePanelPrefab");
        ArchiveButtonPrefab = UIManager.Instance.Get("ArchiveButtonPrefab");
    }
    public void CreateArchive()
    {
        GameObject currentObject = Instantiate(ArchivePanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("ArchiveContent/Content");

        CreateArchiveButtonUI(1, AppDisplayConstants.Archive.ARCHIVE_I, Resources.Load<Texture2D>(ImageConstants.Research.HOUSING_URL), contentPanel);
        CreateArchiveButtonUI(2, AppDisplayConstants.Archive.ARCHIVE_II, Resources.Load<Texture2D>(ImageConstants.Research.INFRASTRUCTURE_URL), contentPanel);
        CreateArchiveButtonUI(3, AppDisplayConstants.Archive.ARCHIVE_III, Resources.Load<Texture2D>(ImageConstants.Research.LOGISTICS_URL), contentPanel);
        CreateArchiveButtonUI(4, AppDisplayConstants.Archive.ARCHIVE_IV, Resources.Load<Texture2D>(ImageConstants.Research.SANITATION_URL), contentPanel);
        CreateArchiveButtonUI(5, AppDisplayConstants.Archive.ARCHIVE_V, Resources.Load<Texture2D>(ImageConstants.Research.TRANSPORTATION_URL), contentPanel);
        CreateArchiveButtonUI(6, AppDisplayConstants.Archive.ARCHIVE_VI, Resources.Load<Texture2D>(ImageConstants.Research.URBANIZATION_URL), contentPanel);
        CreateArchiveButtonUI(7, AppDisplayConstants.Archive.ARCHIVE_VII, Resources.Load<Texture2D>(ImageConstants.Research.UTILITIES_URL), contentPanel);
        CreateArchiveButtonUI(8, AppDisplayConstants.Archive.ARCHIVE_VIII, Resources.Load<Texture2D>(ImageConstants.Research.WASTE_URL), contentPanel);
        CreateArchiveButtonUI(9, AppDisplayConstants.Archive.ARCHIVE_IX, Resources.Load<Texture2D>(ImageConstants.Research.WATER_URL), contentPanel);
        CreateArchiveButtonUI(10, AppDisplayConstants.Archive.ARCHIVE_X, Resources.Load<Texture2D>(ImageConstants.Research.CONSTRUCTION_URL), contentPanel);
        CreateArchiveButtonUI(11, AppDisplayConstants.Archive.ARCHIVE_XI, Resources.Load<Texture2D>(ImageConstants.Research.ENERGY_URL), contentPanel);
        CreateArchiveButtonUI(12, AppDisplayConstants.Archive.ARCHIVE_XII, Resources.Load<Texture2D>(ImageConstants.Research.ENGINEERING_URL), contentPanel);
        CreateArchiveButtonUI(13, AppDisplayConstants.Archive.ARCHIVE_XIII, Resources.Load<Texture2D>(ImageConstants.Research.INDUSTRY_URL), contentPanel);
        CreateArchiveButtonUI(14, AppDisplayConstants.Archive.ARCHIVE_XIV, Resources.Load<Texture2D>(ImageConstants.Research.MANUFACTURING_URL), contentPanel);

        CreateArchiveButtonEvent(contentPanel);
    }
    private void CreateArchiveButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ArchiveButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("Image").GetComponent<RawImage>();
        if (image != null && _itemImage != null)
        {
            image.texture = _itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void CreateArchiveButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await ArchiveIManager.Instance.CreateArchiveIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await ArchiveIIManager.Instance.CreateArchiveIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await ArchiveIIIManager.Instance.CreateArchiveIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await ArchiveIVManager.Instance.CreateArchiveIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await ArchiveVManager.Instance.CreateArchiveVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await ArchiveVIManager.Instance.CreateArchiveVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await ArchiveVIIManager.Instance.CreateArchiveVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await ArchiveVIIIManager.Instance.CreateArchiveVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await ArchiveIXManager.Instance.CreateArchiveIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await ArchiveXManager.Instance.CreateArchiveXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_11", panel, async () => await ArchiveXIManager.Instance.CreateArchiveXIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_12", panel, async () => await ArchiveXIIManager.Instance.CreateArchiveXIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_13", panel, async () => await ArchiveXIIIManager.Instance.CreateArchiveXIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_14", panel, async () => await ArchiveXIVManager.Instance.CreateArchiveXIVManagerAsync());
    }
}
