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

        CreateArchiveButtonUI(1, AppDisplayConstants.Archive.ARCHIVE_I, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_I_URL), contentPanel);
        CreateArchiveButtonUI(2, AppDisplayConstants.Archive.ARCHIVE_II, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_II_URL), contentPanel);
        CreateArchiveButtonUI(3, AppDisplayConstants.Archive.ARCHIVE_III, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_III_URL), contentPanel);
        CreateArchiveButtonUI(4, AppDisplayConstants.Archive.ARCHIVE_IV, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_IV_URL), contentPanel);
        CreateArchiveButtonUI(5, AppDisplayConstants.Archive.ARCHIVE_V, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_V_URL), contentPanel);
        CreateArchiveButtonUI(6, AppDisplayConstants.Archive.ARCHIVE_VI, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_VI_URL), contentPanel);
        CreateArchiveButtonUI(7, AppDisplayConstants.Archive.ARCHIVE_VII, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_VII_URL), contentPanel);
        CreateArchiveButtonUI(8, AppDisplayConstants.Archive.ARCHIVE_VIII, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_VIII_URL), contentPanel);
        CreateArchiveButtonUI(9, AppDisplayConstants.Archive.ARCHIVE_IX, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_IX_URL), contentPanel);
        CreateArchiveButtonUI(10, AppDisplayConstants.Archive.ARCHIVE_X, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_X_URL), contentPanel);
        CreateArchiveButtonUI(11, AppDisplayConstants.Archive.ARCHIVE_XI, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XI_URL), contentPanel);
        CreateArchiveButtonUI(12, AppDisplayConstants.Archive.ARCHIVE_XII, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XII_URL), contentPanel);
        CreateArchiveButtonUI(13, AppDisplayConstants.Archive.ARCHIVE_XIII, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XIII_URL), contentPanel);
        CreateArchiveButtonUI(14, AppDisplayConstants.Archive.ARCHIVE_XIV, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XIV_URL), contentPanel);
        CreateArchiveButtonUI(15, AppDisplayConstants.Archive.ARCHIVE_XV, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XV_URL), contentPanel);
        CreateArchiveButtonUI(16, AppDisplayConstants.Archive.ARCHIVE_XVI, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XVI_URL), contentPanel);
        CreateArchiveButtonUI(17, AppDisplayConstants.Archive.ARCHIVE_XVII, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XVII_URL), contentPanel);
        CreateArchiveButtonUI(18, AppDisplayConstants.Archive.ARCHIVE_XVIII, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XVIII_URL), contentPanel);
        CreateArchiveButtonUI(19, AppDisplayConstants.Archive.ARCHIVE_XIX, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XIX_URL), contentPanel);
        CreateArchiveButtonUI(20, AppDisplayConstants.Archive.ARCHIVE_XX, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XX_URL), contentPanel);
        CreateArchiveButtonUI(21, AppDisplayConstants.Archive.ARCHIVE_XXI, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XXI_URL), contentPanel);
        CreateArchiveButtonUI(22, AppDisplayConstants.Archive.ARCHIVE_XXII, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XXII_URL), contentPanel);
        CreateArchiveButtonUI(23, AppDisplayConstants.Archive.ARCHIVE_XXIII, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XXIII_URL), contentPanel);
        CreateArchiveButtonUI(24, AppDisplayConstants.Archive.ARCHIVE_XXIV, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XXIV_URL), contentPanel);
        CreateArchiveButtonUI(25, AppDisplayConstants.Archive.ARCHIVE_XXV, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XXV_URL), contentPanel);
        CreateArchiveButtonUI(26, AppDisplayConstants.Archive.ARCHIVE_XXVI, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XXVI_URL), contentPanel);
        CreateArchiveButtonUI(27, AppDisplayConstants.Archive.ARCHIVE_XXVII, Resources.Load<Texture2D>(ImageConstants.Archive.ARCHIVE_XXVII_URL), contentPanel);

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
        ButtonEvent.Instance.AssignButtonEvent("Button_15", panel, async () => await ArchiveXVManager.Instance.CreateArchiveXVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_16", panel, async () => await ArchiveXVIManager.Instance.CreateArchiveXVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_17", panel, async () => await ArchiveXVIIManager.Instance.CreateArchiveXVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_18", panel, async () => await ArchiveXVIIIManager.Instance.CreateArchiveXVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_19", panel, async () => await ArchiveXIXManager.Instance.CreateArchiveXIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_20", panel, async () => await ArchiveXXManager.Instance.CreateArchiveXXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_21", panel, async () => await ArchiveXXIManager.Instance.CreateArchiveXXIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_22", panel, async () => await ArchiveXXIIManager.Instance.CreateArchiveXXIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_23", panel, async () => await ArchiveXXIIIManager.Instance.CreateArchiveXXIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_24", panel, async () => await ArchiveXXIVManager.Instance.CreateArchiveXXIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_25", panel, async () => await ArchiveXXVManager.Instance.CreateArchiveXXVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_26", panel, async () => await ArchiveXXVIManager.Instance.CreateArchiveXXVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_27", panel, async () => await ArchiveXXVIIManager.Instance.CreateArchiveXXVIIManagerAsync());
    }
}
