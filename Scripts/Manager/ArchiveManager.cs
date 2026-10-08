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
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        ArchivePanelPrefab = UIManager.Instance.Get(PrefabConstants.Archive.ARCHIVE_PANEL_PREFAB);
        ArchiveButtonPrefab = UIManager.Instance.Get(PrefabConstants.Archive.ARCHIVE_BUTTON_PREFAB);
    }
    public void CreateArchive()
    {
        GameObject currentObject = Instantiate(ArchivePanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("ArchiveContent/Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.ARCHIVE);
        TextMeshProUGUI titleText2 = transform.Find("ArchiveContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.Title.ARCHIVE);

        CreateArchiveButtonUI(1, AppDisplayConstants.Archive.ARCHIVE_I, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_I_URL), contentPanel);
        CreateArchiveButtonUI(2, AppDisplayConstants.Archive.ARCHIVE_II, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_II_URL), contentPanel);
        CreateArchiveButtonUI(3, AppDisplayConstants.Archive.ARCHIVE_III, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_III_URL), contentPanel);
        CreateArchiveButtonUI(4, AppDisplayConstants.Archive.ARCHIVE_IV, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_IV_URL), contentPanel);
        CreateArchiveButtonUI(5, AppDisplayConstants.Archive.ARCHIVE_V, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_V_URL), contentPanel);
        CreateArchiveButtonUI(6, AppDisplayConstants.Archive.ARCHIVE_VI, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_VI_URL), contentPanel);
        CreateArchiveButtonUI(7, AppDisplayConstants.Archive.ARCHIVE_VII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_VII_URL), contentPanel);
        CreateArchiveButtonUI(8, AppDisplayConstants.Archive.ARCHIVE_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_VIII_URL), contentPanel);
        CreateArchiveButtonUI(9, AppDisplayConstants.Archive.ARCHIVE_IX, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_IX_URL), contentPanel);
        CreateArchiveButtonUI(10, AppDisplayConstants.Archive.ARCHIVE_X, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_X_URL), contentPanel);
        CreateArchiveButtonUI(11, AppDisplayConstants.Archive.ARCHIVE_XI, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XI_URL), contentPanel);
        CreateArchiveButtonUI(12, AppDisplayConstants.Archive.ARCHIVE_XII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XII_URL), contentPanel);
        CreateArchiveButtonUI(13, AppDisplayConstants.Archive.ARCHIVE_XIII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XIII_URL), contentPanel);
        CreateArchiveButtonUI(14, AppDisplayConstants.Archive.ARCHIVE_XIV, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XIV_URL), contentPanel);
        CreateArchiveButtonUI(15, AppDisplayConstants.Archive.ARCHIVE_XV, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XV_URL), contentPanel);
        CreateArchiveButtonUI(16, AppDisplayConstants.Archive.ARCHIVE_XVI, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XVI_URL), contentPanel);
        CreateArchiveButtonUI(17, AppDisplayConstants.Archive.ARCHIVE_XVII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XVII_URL), contentPanel);
        CreateArchiveButtonUI(18, AppDisplayConstants.Archive.ARCHIVE_XVIII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XVIII_URL), contentPanel);
        CreateArchiveButtonUI(19, AppDisplayConstants.Archive.ARCHIVE_XIX, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XIX_URL), contentPanel);
        CreateArchiveButtonUI(20, AppDisplayConstants.Archive.ARCHIVE_XX, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XX_URL), contentPanel);
        CreateArchiveButtonUI(21, AppDisplayConstants.Archive.ARCHIVE_XXI, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXI_URL), contentPanel);
        CreateArchiveButtonUI(22, AppDisplayConstants.Archive.ARCHIVE_XXII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXII_URL), contentPanel);
        CreateArchiveButtonUI(23, AppDisplayConstants.Archive.ARCHIVE_XXIII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXIII_URL), contentPanel);
        CreateArchiveButtonUI(24, AppDisplayConstants.Archive.ARCHIVE_XXIV, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXIV_URL), contentPanel);
        CreateArchiveButtonUI(25, AppDisplayConstants.Archive.ARCHIVE_XXV, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXV_URL), contentPanel);
        CreateArchiveButtonUI(26, AppDisplayConstants.Archive.ARCHIVE_XXVI, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXVI_URL), contentPanel);
        CreateArchiveButtonUI(27, AppDisplayConstants.Archive.ARCHIVE_XXVII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXVII_URL), contentPanel);
        CreateArchiveButtonUI(28, AppDisplayConstants.Archive.ARCHIVE_XXVIII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXVIII_URL), contentPanel);
        CreateArchiveButtonUI(29, AppDisplayConstants.Archive.ARCHIVE_XXIX, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXIX_URL), contentPanel);
        CreateArchiveButtonUI(30, AppDisplayConstants.Archive.ARCHIVE_XXX, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXX_URL), contentPanel);
        CreateArchiveButtonUI(31, AppDisplayConstants.Archive.ARCHIVE_XXXI, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXXI_URL), contentPanel);
        CreateArchiveButtonUI(32, AppDisplayConstants.Archive.ARCHIVE_XXXII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXXII_URL), contentPanel);
        CreateArchiveButtonUI(33, AppDisplayConstants.Archive.ARCHIVE_XXXIII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXXIII_URL), contentPanel);
        CreateArchiveButtonUI(34, AppDisplayConstants.Archive.ARCHIVE_XXXIV, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXXIV_URL), contentPanel);
        CreateArchiveButtonUI(35, AppDisplayConstants.Archive.ARCHIVE_XXXV, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXXV_URL), contentPanel);
        CreateArchiveButtonUI(36, AppDisplayConstants.Archive.ARCHIVE_XXXVI, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXXVI_URL), contentPanel);
        CreateArchiveButtonUI(37, AppDisplayConstants.Archive.ARCHIVE_XXXVII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXXVII_URL), contentPanel);
        CreateArchiveButtonUI(38, AppDisplayConstants.Archive.ARCHIVE_XXXVIII, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXXVIII_URL), contentPanel);
        CreateArchiveButtonUI(39, AppDisplayConstants.Archive.ARCHIVE_XXXIX, TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_XXXIX_URL), contentPanel);

        CreateArchiveButtonEvent(contentPanel);
    }
    private void CreateArchiveButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        GameObject newButton = Instantiate(ArchiveButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        RawImage image = transform.Find("Image").GetComponent<RawImage>();
        if (image != null && _itemImage != null)
        {
            image.texture = _itemImage;
        }

        TextMeshProUGUI nameText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }

        RawImage borderImage = transform.Find("BorderCircleImage").GetComponent<RawImage>();
        if (borderImage != null)
        {
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_BORDER_URL);
        }

        RawImage iconImage = transform.Find("IconImage").GetComponent<RawImage>();
        if (iconImage != null)
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.Archive.ARCHIVE_ICON_URL);
        }
    }
    public void CreateArchiveButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_I));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_II));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_III));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_IV));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_V));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_VI));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_VII));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_VIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_IX));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_X));
        ButtonEvent.Instance.AssignButtonEvent("Button_11", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XI));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XII));
        ButtonEvent.Instance.AssignButtonEvent("Button_13", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_14", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XIV));
        ButtonEvent.Instance.AssignButtonEvent("Button_15", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XV));
        ButtonEvent.Instance.AssignButtonEvent("Button_16", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XVI));
        ButtonEvent.Instance.AssignButtonEvent("Button_17", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XVII));
        ButtonEvent.Instance.AssignButtonEvent("Button_18", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XVIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_19", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XIX));
        ButtonEvent.Instance.AssignButtonEvent("Button_20", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XX));
        ButtonEvent.Instance.AssignButtonEvent("Button_21", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXI));
        ButtonEvent.Instance.AssignButtonEvent("Button_22", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXII));
        ButtonEvent.Instance.AssignButtonEvent("Button_23", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_24", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXIV));
        ButtonEvent.Instance.AssignButtonEvent("Button_25", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXV));
        ButtonEvent.Instance.AssignButtonEvent("Button_26", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXVI));
        ButtonEvent.Instance.AssignButtonEvent("Button_27", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXVII));
        ButtonEvent.Instance.AssignButtonEvent("Button_28", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXVIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_29", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXIX));
        ButtonEvent.Instance.AssignButtonEvent("Button_30", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXX));
        ButtonEvent.Instance.AssignButtonEvent("Button_31", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXXI));
        ButtonEvent.Instance.AssignButtonEvent("Button_32", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXXII));
        ButtonEvent.Instance.AssignButtonEvent("Button_33", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXXIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_34", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXXIV));
        ButtonEvent.Instance.AssignButtonEvent("Button_35", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXXV));
        ButtonEvent.Instance.AssignButtonEvent("Button_36", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXXVI));
        ButtonEvent.Instance.AssignButtonEvent("Button_37", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXXVII));
        ButtonEvent.Instance.AssignButtonEvent("Button_38", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXXVIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_39", panel, async () => await ArchiveController.Instance.GetArchiveAsync(AppConstants.Archive.ARCHIVE_XXXIX));
    }
}
