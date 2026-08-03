using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModuleManager : MonoBehaviour
{
    public static ModuleManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ModulePanelPrefab;
    private GameObject ModuleButtonPrefab;
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
        ModulePanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Module.MODULE_PANEL_PREFAB);
        ModuleButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Module.MODULE_BUTTON_PREFAB);
    }
    public void CreateModule(IStats stat)
    {
        GameObject currentObject = Instantiate(ModulePanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("ModuleContent");
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

        CreateModuleButtonUI(1, AppDisplayConstants.Module.MODULE_BREAKTHROUGH, TextureHelper.LoadTexture2DCached(ImageConstants.Module.MODULE_BREAKTHROUGH_URL), contentPanel);
        CreateModuleButtonUI(2, AppDisplayConstants.Module.MODULE_AWAKENING, TextureHelper.LoadTexture2DCached(ImageConstants.Module.MODULE_AWAKENING_URL), contentPanel);
        CreateModuleButtonUI(3, AppDisplayConstants.Module.MODULE_ASCENSION, TextureHelper.LoadTexture2DCached(ImageConstants.Module.MODULE_ASCENSION_URL), contentPanel);
        CreateModuleButtonUI(4, AppDisplayConstants.Module.MODULE_RESONANCE, TextureHelper.LoadTexture2DCached(ImageConstants.Module.MODULE_RESONANCE_URL), contentPanel);
        CreateModuleButtonUI(5, AppDisplayConstants.Module.MODULE_ENHANCEMENT, TextureHelper.LoadTexture2DCached(ImageConstants.Module.MODULE_ENHANCEMENT_URL), contentPanel);
        CreateModuleButtonUI(6, AppDisplayConstants.Module.MODULE_REFINEMENT, TextureHelper.LoadTexture2DCached(ImageConstants.Module.MODULE_REFINEMENT_URL), contentPanel);
        // CreateModuleButtonUI(7, AppDisplayConstants.Module.Module_VII, TextureHelper.LoadTexture2DCached(ImageConstants.Module.Module_VII_URL), contentPanel);
        // CreateModuleButtonUI(8, AppDisplayConstants.Module.Module_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.Module.Module_VIII_URL), contentPanel);
        // CreateModuleButtonUI(9, AppDisplayConstants.Module.Module_IX, TextureHelper.LoadTexture2DCached(ImageConstants.Module.Module_IX_URL), contentPanel);
        // CreateModuleButtonUI(10, AppDisplayConstants.Module.Module_X, TextureHelper.LoadTexture2DCached(ImageConstants.Module.Module_X_URL), contentPanel);

        CreateModuleButtonEvent(stat, contentPanel);
    }
    private void CreateModuleButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ModuleButtonPrefab, panel);
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
    public void CreateModuleButtonEvent(IStats stat, Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await ModuleBreakthroughManager.Instance.CreateModuleBreakthroughManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await ModuleAwakeningManager.Instance.CreateModuleAwakeningManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await ModuleAscensionManager.Instance.CreateModuleAscensionManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await ModuleResonanceManager.Instance.CreateModuleResonanceManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await ModuleEnhancementManager.Instance.CreateModuleEnhancementManagerAsync(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await ModuleRefinementManager.Instance.CreateModuleRefinementManagerAsync(stat));
        // ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await ModuleVIIManager.Instance.CreateModuleVIIManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await ModuleVIIIManager.Instance.CreateModuleVIIIManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await ModuleIXManager.Instance.CreateModuleIXManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await ModuleXManager.Instance.CreateModuleXManagerAsync());
    }
}
