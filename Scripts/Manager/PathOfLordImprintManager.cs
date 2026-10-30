using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PathOfLordImprintManager : MonoBehaviour
{
    public static PathOfLordImprintManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject PathOfLordImprintPanelPrefab;
    private GameObject PathOfLordImprintButtonPrefab;
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
        PathOfLordImprintPanelPrefab = UIManager.Instance.Get(PrefabConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_PANEL_PREFAB);
        PathOfLordImprintButtonPrefab = UIManager.Instance.Get(PrefabConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_BUTTON_PREFAB);
    }
    public void CreatePathOfLordImprint()
    {
        GameObject currentObject = Instantiate(PathOfLordImprintPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("PathOfLordImprintContent/Scroll View/Viewport/Content");
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
        titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.PATH_OF_LORD_IMPRINT);
        TextMeshProUGUI titleText2 = transform.Find("PathOfLordImprintContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.Title.PATH_OF_LORD_IMPRINT);

        CreatePathOfLordImprintButtonUI(1, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_I, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_I_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(2, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_II, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_II_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(3, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_III, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_III_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(4, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_IV, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_IV_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(5, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_V, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_V_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(6, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_VI, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_VI_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(7, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_VII, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_VII_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(8, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_VIII_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(9, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_IX, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_IX_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(10, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_X, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_X_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(11, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XI, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XI_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(12, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XII, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XII_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(13, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XIII, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XIII_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(14, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XIV, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XIV_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(15, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XV, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XV_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(16, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XVI, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XVI_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(17, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XVII, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XVII_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(18, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XVIII, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XVIII_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(19, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XIX, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XIX_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(20, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XX, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XX_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(21, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXI, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXI_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(22, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXII, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXII_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(23, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXIII, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXIII_URL), contentPanel);
        CreatePathOfLordImprintButtonUI(24, AppDisplayConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXIV, TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXIV_URL), contentPanel);

        CreatePathOfLordImprintButtonEvent(contentPanel);
    }
    private void CreatePathOfLordImprintButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(PathOfLordImprintButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        // Gán hình ảnh cho itemImage
        RawImage image = transform.Find("Image").GetComponent<RawImage>();
        if (image != null && _itemImage != null)
        {
            image.texture = _itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }

        RawImage borderImage = transform.Find("BorderCircleImage").GetComponent<RawImage>();
        if (borderImage != null)
        {
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_BORDER_URL);
        }

        RawImage iconImage = transform.Find("IconImage").GetComponent<RawImage>();
        if (iconImage != null)
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_ICON_URL);
        }
    }
    public void CreatePathOfLordImprintButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_I));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_II));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_III));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_IV));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_V));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_VI));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_VII));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_VIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_IX));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_X));
        ButtonEvent.Instance.AssignButtonEvent("Button_11", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XI));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XII));
        ButtonEvent.Instance.AssignButtonEvent("Button_13", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_14", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XIV));
        ButtonEvent.Instance.AssignButtonEvent("Button_15", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XV));
        ButtonEvent.Instance.AssignButtonEvent("Button_16", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XVI));
        ButtonEvent.Instance.AssignButtonEvent("Button_17", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XVII));
        ButtonEvent.Instance.AssignButtonEvent("Button_18", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XVIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_19", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XIX));
        ButtonEvent.Instance.AssignButtonEvent("Button_20", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XX));
        ButtonEvent.Instance.AssignButtonEvent("Button_21", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXI));
        ButtonEvent.Instance.AssignButtonEvent("Button_22", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXII));
        ButtonEvent.Instance.AssignButtonEvent("Button_23", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_24", panel, async () => await PathOfLordImprintController.Instance.GetPathOfLordImprintAsync(AppConstants.PathOfLordImprint.PATH_OF_LORD_IMPRINT_XXIV));
    }
}
