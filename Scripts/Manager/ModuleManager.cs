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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ModulePanelPrefab = UIManager.Instance.Get("ModulePanelPrefab");
        ModuleButtonPrefab = UIManager.Instance.Get("ModuleButtonPrefab");
    }
    public void CreateModule()
    {
        GameObject currentObject = Instantiate(ModulePanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("ModuleContent");

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

        CreateModuleButtonEvent(contentPanel);
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
    public void CreateModuleButtonEvent(Transform panel)
    {
        // ButtonEvent.Instance.AssignButtonEvent("ModuleButtonPrefab", panel, async () => await ModuleIManager.Instance.CreateModuleIManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("ModuleElementButtonPrefab", panel, async () => await ModuleIIManager.Instance.CreateModuleIIManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("ModuleEvolutionButtonPrefab", panel, async () => await ModuleIIIManager.Instance.CreateModuleIIIManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("ModuleQualityButtonPrefab", panel, async () => await ModuleIVManager.Instance.CreateModuleIVManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("ModuleAscensionButtonPrefab", panel, async () => await ModuleVManager.Instance.CreateModuleVManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("ModuleRefinementButtonPrefab", panel, async () => await ModuleVIManager.Instance.CreateModuleVIManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await ModuleVIIManager.Instance.CreateModuleVIIManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await ModuleVIIIManager.Instance.CreateModuleVIIIManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await ModuleIXManager.Instance.CreateModuleIXManagerAsync());
        // ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await ModuleXManager.Instance.CreateModuleXManagerAsync());
    }
}
