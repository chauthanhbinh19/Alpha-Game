using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UniverseManager : MonoBehaviour
{
    public static UniverseManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject UniversePanelPrefab;
    private GameObject UniverseButtonPrefab;
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
        UniversePanelPrefab = UIManager.Instance.Get("UniversePanelPrefab");
        UniverseButtonPrefab = UIManager.Instance.Get("UniverseButtonPrefab");
    }
    public void CreateUniverse()
    {
        GameObject currentObject = Instantiate(UniversePanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("UniverseContent/Content");

        CreateUniverseButtonUI(1, AppDisplayConstants.Universe.UNIVERSE_I, TextureHelper.LoadTexture2DCached(ImageConstants.Universe.UNIVERSE_I_URL), contentPanel);
        CreateUniverseButtonUI(2, AppDisplayConstants.Universe.UNIVERSE_II, TextureHelper.LoadTexture2DCached(ImageConstants.Universe.UNIVERSE_II_URL), contentPanel);
        CreateUniverseButtonUI(3, AppDisplayConstants.Universe.UNIVERSE_III, TextureHelper.LoadTexture2DCached(ImageConstants.Universe.UNIVERSE_III_URL), contentPanel);
        CreateUniverseButtonUI(4, AppDisplayConstants.Universe.UNIVERSE_IV, TextureHelper.LoadTexture2DCached(ImageConstants.Universe.UNIVERSE_IV_URL), contentPanel);
        CreateUniverseButtonUI(5, AppDisplayConstants.Universe.UNIVERSE_V, TextureHelper.LoadTexture2DCached(ImageConstants.Universe.UNIVERSE_V_URL), contentPanel);
        CreateUniverseButtonUI(6, AppDisplayConstants.Universe.UNIVERSE_VI, TextureHelper.LoadTexture2DCached(ImageConstants.Universe.UNIVERSE_VI_URL), contentPanel);
        CreateUniverseButtonUI(7, AppDisplayConstants.Universe.UNIVERSE_VII, TextureHelper.LoadTexture2DCached(ImageConstants.Universe.UNIVERSE_VII_URL), contentPanel);
        CreateUniverseButtonUI(8, AppDisplayConstants.Universe.UNIVERSE_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.Universe.UNIVERSE_VIII_URL), contentPanel);
        CreateUniverseButtonUI(9, AppDisplayConstants.Universe.UNIVERSE_IX, TextureHelper.LoadTexture2DCached(ImageConstants.Universe.UNIVERSE_IX_URL), contentPanel);
        CreateUniverseButtonUI(10, AppDisplayConstants.Universe.UNIVERSE_X, TextureHelper.LoadTexture2DCached(ImageConstants.Universe.UNIVERSE_X_URL), contentPanel);

        CreateUniverseButtonEvent(contentPanel);
    }
    private void CreateUniverseButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(UniverseButtonPrefab, panel);
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
    public void CreateUniverseButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await UniverseIManager.Instance.CreateUniverseIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await UniverseIIManager.Instance.CreateUniverseIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await UniverseIIIManager.Instance.CreateUniverseIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await UniverseIVManager.Instance.CreateUniverseIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await UniverseVManager.Instance.CreateUniverseVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await UniverseVIManager.Instance.CreateUniverseVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await UniverseVIIManager.Instance.CreateUniverseVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await UniverseVIIIManager.Instance.CreateUniverseVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await UniverseIXManager.Instance.CreateUniverseIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await UniverseXManager.Instance.CreateUniverseXManagerAsync());
    }
}
