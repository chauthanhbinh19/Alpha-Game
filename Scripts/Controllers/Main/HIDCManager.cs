using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HIDCManager : MonoBehaviour
{
    public static HIDCManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject HIDCPanelPrefab;
    private GameObject HIDCButtonPrefab;
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
        HIDCPanelPrefab = UIManager.Instance.Get("HIDCPanelPrefab");
        HIDCButtonPrefab = UIManager.Instance.Get("HIDCButtonPrefab");
    }
    public void CreateHIDC()
    {
        GameObject currentObject = Instantiate(HIDCPanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("HIDCContent/Content");

        CreateHIDCButtonUI(1, AppDisplayConstants.HIDC.HIDC_I, TextureHelper.LoadTexture2DCached(ImageConstants.HIDC.HIDC_I_URL), contentPanel);
        CreateHIDCButtonUI(2, AppDisplayConstants.HIDC.HIDC_II, TextureHelper.LoadTexture2DCached(ImageConstants.HIDC.HIDC_II_URL), contentPanel);
        CreateHIDCButtonUI(3, AppDisplayConstants.HIDC.HIDC_III, TextureHelper.LoadTexture2DCached(ImageConstants.HIDC.HIDC_III_URL), contentPanel);
        CreateHIDCButtonUI(4, AppDisplayConstants.HIDC.HIDC_IV, TextureHelper.LoadTexture2DCached(ImageConstants.HIDC.HIDC_IV_URL), contentPanel);
        CreateHIDCButtonUI(5, AppDisplayConstants.HIDC.HIDC_V, TextureHelper.LoadTexture2DCached(ImageConstants.HIDC.HIDC_V_URL), contentPanel);
        CreateHIDCButtonUI(6, AppDisplayConstants.HIDC.HIDC_VI, TextureHelper.LoadTexture2DCached(ImageConstants.HIDC.HIDC_VI_URL), contentPanel);
        CreateHIDCButtonUI(7, AppDisplayConstants.HIDC.HIDC_VII, TextureHelper.LoadTexture2DCached(ImageConstants.HIDC.HIDC_VII_URL), contentPanel);
        CreateHIDCButtonUI(8, AppDisplayConstants.HIDC.HIDC_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.HIDC.HIDC_VIII_URL), contentPanel);
        CreateHIDCButtonUI(9, AppDisplayConstants.HIDC.HIDC_IX, TextureHelper.LoadTexture2DCached(ImageConstants.HIDC.HIDC_IX_URL), contentPanel);
        CreateHIDCButtonUI(10, AppDisplayConstants.HIDC.HIDC_X, TextureHelper.LoadTexture2DCached(ImageConstants.HIDC.HIDC_X_URL), contentPanel);

        CreateHIDCButtonEvent(contentPanel);
    }
    private void CreateHIDCButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(HIDCButtonPrefab, panel);
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
    public void CreateHIDCButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await HIDCIManager.Instance.CreateHIDCIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await HIDCIIManager.Instance.CreateHIDCIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await HIDCIIIManager.Instance.CreateHIDCIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await HIDCIVManager.Instance.CreateHIDCIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await HIDCVManager.Instance.CreateHIDCVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await HIDCVIManager.Instance.CreateHIDCVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await HIDCVIIManager.Instance.CreateHIDCVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await HIDCVIIIManager.Instance.CreateHIDCVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await HIDCIXManager.Instance.CreateHIDCIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await HIDCXManager.Instance.CreateHIDCXManagerAsync());
    }
}
