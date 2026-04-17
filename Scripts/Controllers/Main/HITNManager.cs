using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HITNManager : MonoBehaviour
{
    public static HITNManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject HITNPanelPrefab;
    private GameObject HITNButtonPrefab;
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
        HITNPanelPrefab = UIManager.Instance.Get("HITNPanelPrefab");
        HITNButtonPrefab = UIManager.Instance.Get("HITNButtonPrefab");
    }
    public void CreateHITN()
    {
        GameObject currentObject = Instantiate(HITNPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("HITNContent/Content");

        CreateHITNButtonUI(1, AppDisplayConstants.HITN.HITN_I, TextureHelper.LoadTexture2DCached(ImageConstants.HITN.HITN_I_URL), contentPanel);
        CreateHITNButtonUI(2, AppDisplayConstants.HITN.HITN_II, TextureHelper.LoadTexture2DCached(ImageConstants.HITN.HITN_II_URL), contentPanel);
        CreateHITNButtonUI(3, AppDisplayConstants.HITN.HITN_III, TextureHelper.LoadTexture2DCached(ImageConstants.HITN.HITN_III_URL), contentPanel);
        CreateHITNButtonUI(4, AppDisplayConstants.HITN.HITN_IV, TextureHelper.LoadTexture2DCached(ImageConstants.HITN.HITN_IV_URL), contentPanel);
        CreateHITNButtonUI(5, AppDisplayConstants.HITN.HITN_V, TextureHelper.LoadTexture2DCached(ImageConstants.HITN.HITN_V_URL), contentPanel);
        CreateHITNButtonUI(6, AppDisplayConstants.HITN.HITN_VI, TextureHelper.LoadTexture2DCached(ImageConstants.HITN.HITN_VI_URL), contentPanel);
        CreateHITNButtonUI(7, AppDisplayConstants.HITN.HITN_VII, TextureHelper.LoadTexture2DCached(ImageConstants.HITN.HITN_VII_URL), contentPanel);
        CreateHITNButtonUI(8, AppDisplayConstants.HITN.HITN_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.HITN.HITN_VIII_URL), contentPanel);
        CreateHITNButtonUI(9, AppDisplayConstants.HITN.HITN_IX, TextureHelper.LoadTexture2DCached(ImageConstants.HITN.HITN_IX_URL), contentPanel);
        CreateHITNButtonUI(10, AppDisplayConstants.HITN.HITN_X, TextureHelper.LoadTexture2DCached(ImageConstants.HITN.HITN_X_URL), contentPanel);

        CreateHITNButtonEvent(contentPanel);
    }
    private void CreateHITNButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(HITNButtonPrefab, panel);
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
    public void CreateHITNButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await HITNIManager.Instance.CreateHITNIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await HITNIIManager.Instance.CreateHITNIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await HITNIIIManager.Instance.CreateHITNIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await HITNIVManager.Instance.CreateHITNIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await HITNVManager.Instance.CreateHITNVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await HITNVIManager.Instance.CreateHITNVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await HITNVIIManager.Instance.CreateHITNVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await HITNVIIIManager.Instance.CreateHITNVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await HITNIXManager.Instance.CreateHITNIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await HITNXManager.Instance.CreateHITNXManagerAsync());
    }
}
