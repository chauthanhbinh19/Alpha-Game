using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject UpgradePanelPrefab;
    private GameObject UpgradeButtonPrefab;
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
        UpgradePanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Upgrade.UPGRADE_PANEL_PREFAB);
        UpgradeButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Upgrade.UPGRADE_BUTTON_PREFAB);
    }
    public void CreateUpgrade(IStats stat)
    {
        GameObject currentObject = Instantiate(UpgradePanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("UpgradeContent");
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

        CreateUpgradeButtonUI(1, AppDisplayConstants.Upgrade.UPGRADE_BREAKTHROUGH, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_BREAKTHROUGH_URL), contentPanel);
        CreateUpgradeButtonUI(2, AppDisplayConstants.Upgrade.UPGRADE_AWAKENING, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_AWAKENING_URL), contentPanel);
        CreateUpgradeButtonUI(3, AppDisplayConstants.Upgrade.UPGRADE_ASCENSION, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_ASCENSION_URL), contentPanel);
        CreateUpgradeButtonUI(4, AppDisplayConstants.Upgrade.UPGRADE_RESONANCE, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_RESONANCE_URL), contentPanel);
        CreateUpgradeButtonUI(5, AppDisplayConstants.Upgrade.UPGRADE_ENHANCEMENT, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_ENHANCEMENT_URL), contentPanel);
        CreateUpgradeButtonUI(6, AppDisplayConstants.Upgrade.UPGRADE_REFINEMENT, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_REFINEMENT_URL), contentPanel);
        CreateUpgradeButtonUI(7, AppDisplayConstants.Upgrade.UPGRADE_APOTHEOSIS, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_APOTHEOSIS_URL), contentPanel);
        CreateUpgradeButtonUI(8, AppDisplayConstants.Upgrade.UPGRADE_ENGRAVING, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_ENGRAVING_URL), contentPanel);
        CreateUpgradeButtonUI(9, AppDisplayConstants.Upgrade.UPGRADE_INTEGRATION, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_INTEGRATION_URL), contentPanel);
        CreateUpgradeButtonUI(10, AppDisplayConstants.Upgrade.UPGRADE_SANCTIFICATION, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_SANCTIFICATION_URL), contentPanel);
        CreateUpgradeButtonUI(11, AppDisplayConstants.Upgrade.UPGRADE_SYNCHRONIZATION, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_SYNCHRONIZATION_URL), contentPanel);
        CreateUpgradeButtonUI(12, AppDisplayConstants.Upgrade.UPGRADE_EVOLUTION, TextureHelper.LoadTexture2DCached(ImageConstants.Upgrade.UPGRADE_EVOLUTION_URL), contentPanel);

        CreateUpgradeButtonEvent(stat, contentPanel);
    }
    private void CreateUpgradeButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(UpgradeButtonPrefab, panel);
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
    }
    public void CreateUpgradeButtonEvent(IStats stat, Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await UpgradeBreakthroughManager.Instance.CreateUpgradeBreakthroughManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await UpgradeAwakeningManager.Instance.CreateUpgradeAwakeningManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await UpgradeAscensionManager.Instance.CreateUpgradeAscensionManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await UpgradeResonanceManager.Instance.CreateUpgradeResonanceManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await UpgradeEnhancementManager.Instance.CreateUpgradeEnhancementManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await UpgradeRefinementManager.Instance.CreateUpgradeRefinementManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await UpgradeApotheosisManager.Instance.CreateUpgradeApotheosisManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await UpgradeEngravingManager.Instance.CreateUpgradeEngravingManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await UpgradeIntegrationManager.Instance.CreateUpgradeIntegrationManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await UpgradeSanctificationManager.Instance.CreateUpgradeSanctificationManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_11", panel, async () => await UpgradeSynchronizationManager.Instance.CreateUpgradeSynchronizationManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_12", panel, async () => await UpgradeEvolutionManager.Instance.CreateUpgradeEvolutionManagerAsync(stat));
    }
}
