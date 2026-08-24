using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject MasterPanelPrefab;
    private GameObject MasterButtonPrefab;
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
        MasterPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Master.MASTER_PANEL_PREFAB);
        MasterButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Master.MASTER_BUTTON_PREFAB);
    }
    public void CreateMaster(IStats stat)
    {
        GameObject currentObject = Instantiate(MasterPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("MasterContent/Scroll View/Viewport/Content");
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
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UNIVERSE);
        TextMeshProUGUI titleText2 = transform.Find("MasterContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.MainType.UNIVERSE);

        CreateButtonWithBackgroundUI(1, AppDisplayConstants.Master.MASTER_OF_BEAST, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Zarx"), contentPanel);
        CreateButtonWithBackgroundUI(2, AppDisplayConstants.Master.MASTER_OF_DRAGON, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Raik"), contentPanel);
        CreateButtonWithBackgroundUI(3, AppDisplayConstants.Master.MASTER_OF_MAGIC, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Drax"), contentPanel);
        CreateButtonWithBackgroundUI(4, AppDisplayConstants.Master.MASTER_OF_MUSIC, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Kron"), contentPanel);
        CreateButtonWithBackgroundUI(5, AppDisplayConstants.Master.MASTER_OF_SCIENCE, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Zolt"), contentPanel);
        CreateButtonWithBackgroundUI(6, AppDisplayConstants.Master.MASTER_OF_SPIRIT, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Gorr"), contentPanel);
        CreateButtonWithBackgroundUI(7, AppDisplayConstants.Master.MASTER_OF_WEAPON, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Ryze"), contentPanel);
        CreateButtonWithBackgroundUI(8, AppDisplayConstants.Master.MASTER_OF_CHEMICAL, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Jaxx"), contentPanel);
        CreateButtonWithBackgroundUI(9, AppDisplayConstants.Master.MASTER_OF_PHYSICAL, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Thar"), contentPanel);
        CreateButtonWithBackgroundUI(10, AppDisplayConstants.Master.MASTER_OF_ATOMIC, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Vorn"), contentPanel);
        CreateButtonWithBackgroundUI(11, AppDisplayConstants.Master.MASTER_OF_MENTAL, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Nyx"), contentPanel);

        CreateMasterButtonEvent(stat, contentPanel);
    }
    private void CreateButtonWithBackgroundUI(int index, string itemName, string itemBackground, Texture2D itemImage, Transform panel)
    {
        if (panel == null)
        {
            Debug.Log("Panel is null for index: " + index);
            return;
        }
        // Tạo button từ prefab
        GameObject newButton = Instantiate(MasterButtonPrefab, panel);
        newButton.name = "Button_" + index;
        Transform transform = newButton.transform;

        // Gán màu cho itemBackground
        RawImage background = transform.Find("Background").GetComponent<RawImage>();
        Texture texture = TextureHelper.LoadTextureCached($"{itemBackground}");
        if (background != null && itemBackground != null)
        {
            background.texture = texture;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = transform.Find("MainImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void CreateMasterButtonEvent(IStats stat, Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await MasterOfBeastManager.Instance.CreateMasterOfBeastManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await MasterOfDragonManager.Instance.CreateMasterOfDragonManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await MasterOfMagicManager.Instance.CreateMasterOfMagicManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await MasterOfMusicManager.Instance.CreateMasterOfMusicManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await MasterOfScienceManager.Instance.CreateMasterOfScienceManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await MasterOfSpiritManager.Instance.CreateMasterOfSpiritManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await MasterOfWeaponManager.Instance.CreateMasterOfWeaponManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await MasterOfChemicalManager.Instance.CreateMasterOfChemicalManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await MasterOfPhysicalManager.Instance.CreateMasterOfPhysicalManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await MasterOfAtomicManager.Instance.CreateMasterOfAtomicManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_11", panel, async () => await MasterOfMentalManager.Instance.CreateMasterOfMentalManagerAsync(stat));
    }
}
