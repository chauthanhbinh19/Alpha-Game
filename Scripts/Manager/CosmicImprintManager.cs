using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CosmicImprintManager : MonoBehaviour
{
    public static CosmicImprintManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CosmicImprintPanelPrefab;
    private GameObject CosmicImprintButtonPrefab;
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
        CosmicImprintPanelPrefab = UIManager.Instance.Get(PrefabConstants.CosmicImprint.COSMIC_IMPRINT_PANEL_PREFAB);
        CosmicImprintButtonPrefab = UIManager.Instance.Get(PrefabConstants.CosmicImprint.COSMIC_IMPRINT_BUTTON_PREFAB);
    }
    public void CreateCosmicImprint()
    {
        GameObject currentObject = Instantiate(CosmicImprintPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("CosmicImprintContent/Scroll View/Viewport/Content");
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
        titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.COSMIC_IMPRINT);
        TextMeshProUGUI titleText2 = transform.Find("CosmicImprintContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.Title.COSMIC_IMPRINT);

        CreateCosmicImprintButtonUI(1, AppDisplayConstants.CosmicImprint.COSMIC_IMPRINT_I, TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_I_URL), contentPanel);
        CreateCosmicImprintButtonUI(2, AppDisplayConstants.CosmicImprint.COSMIC_IMPRINT_II, TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_II_URL), contentPanel);
        CreateCosmicImprintButtonUI(3, AppDisplayConstants.CosmicImprint.COSMIC_IMPRINT_III, TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_III_URL), contentPanel);
        CreateCosmicImprintButtonUI(4, AppDisplayConstants.CosmicImprint.COSMIC_IMPRINT_IV, TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_IV_URL), contentPanel);
        CreateCosmicImprintButtonUI(5, AppDisplayConstants.CosmicImprint.COSMIC_IMPRINT_V, TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_V_URL), contentPanel);
        CreateCosmicImprintButtonUI(6, AppDisplayConstants.CosmicImprint.COSMIC_IMPRINT_VI, TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_VI_URL), contentPanel);
        CreateCosmicImprintButtonUI(7, AppDisplayConstants.CosmicImprint.COSMIC_IMPRINT_VII, TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_VII_URL), contentPanel);
        CreateCosmicImprintButtonUI(8, AppDisplayConstants.CosmicImprint.COSMIC_IMPRINT_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_VIII_URL), contentPanel);
        CreateCosmicImprintButtonUI(9, AppDisplayConstants.CosmicImprint.COSMIC_IMPRINT_IX, TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_IX_URL), contentPanel);
        CreateCosmicImprintButtonUI(10, AppDisplayConstants.CosmicImprint.COSMIC_IMPRINT_X, TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_X_URL), contentPanel);

        CreateCosmicImprintButtonEvent(contentPanel);
    }
    private void CreateCosmicImprintButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(CosmicImprintButtonPrefab, panel);
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
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_BORDER_URL);
        }

        RawImage iconImage = transform.Find("IconImage").GetComponent<RawImage>();
        if (iconImage != null)
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.CosmicImprint.COSMIC_IMPRINT_ICON_URL);
        }
    }
    public void CreateCosmicImprintButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await CosmicImprintController.Instance.GetCosmicImprintAsync(AppConstants.CosmicImprint.COSMIC_IMPRINT_I));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await CosmicImprintController.Instance.GetCosmicImprintAsync(AppConstants.CosmicImprint.COSMIC_IMPRINT_II));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await CosmicImprintController.Instance.GetCosmicImprintAsync(AppConstants.CosmicImprint.COSMIC_IMPRINT_III));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await CosmicImprintController.Instance.GetCosmicImprintAsync(AppConstants.CosmicImprint.COSMIC_IMPRINT_IV));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await CosmicImprintController.Instance.GetCosmicImprintAsync(AppConstants.CosmicImprint.COSMIC_IMPRINT_V));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await CosmicImprintController.Instance.GetCosmicImprintAsync(AppConstants.CosmicImprint.COSMIC_IMPRINT_VI));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await CosmicImprintController.Instance.GetCosmicImprintAsync(AppConstants.CosmicImprint.COSMIC_IMPRINT_VII));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await CosmicImprintController.Instance.GetCosmicImprintAsync(AppConstants.CosmicImprint.COSMIC_IMPRINT_VIII));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await CosmicImprintController.Instance.GetCosmicImprintAsync(AppConstants.CosmicImprint.COSMIC_IMPRINT_IX));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await CosmicImprintController.Instance.GetCosmicImprintAsync(AppConstants.CosmicImprint.COSMIC_IMPRINT_X));
    }
}
