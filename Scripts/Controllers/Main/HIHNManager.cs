using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HIHNManager : MonoBehaviour
{
    public static HIHNManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject HIHNPanelPrefab;
    private GameObject HIHNButtonPrefab;
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
        HIHNPanelPrefab = UIManager.Instance.Get("HIHNPanelPrefab");
        HIHNButtonPrefab = UIManager.Instance.Get("HIHNButtonPrefab");
    }
    public void CreateHIHN()
    {
        GameObject currentObject = Instantiate(HIHNPanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("HIHNContent/Content");

        CreateHIHNButtonUI(1, AppDisplayConstants.HIHN.HIHN_I, TextureHelper.LoadTexture2DCached(ImageConstants.HIHN.HIHN_I_URL), contentPanel);
        CreateHIHNButtonUI(2, AppDisplayConstants.HIHN.HIHN_II, TextureHelper.LoadTexture2DCached(ImageConstants.HIHN.HIHN_II_URL), contentPanel);
        CreateHIHNButtonUI(3, AppDisplayConstants.HIHN.HIHN_III, TextureHelper.LoadTexture2DCached(ImageConstants.HIHN.HIHN_III_URL), contentPanel);
        CreateHIHNButtonUI(4, AppDisplayConstants.HIHN.HIHN_IV, TextureHelper.LoadTexture2DCached(ImageConstants.HIHN.HIHN_IV_URL), contentPanel);
        CreateHIHNButtonUI(5, AppDisplayConstants.HIHN.HIHN_V, TextureHelper.LoadTexture2DCached(ImageConstants.HIHN.HIHN_V_URL), contentPanel);
        CreateHIHNButtonUI(6, AppDisplayConstants.HIHN.HIHN_VI, TextureHelper.LoadTexture2DCached(ImageConstants.HIHN.HIHN_VI_URL), contentPanel);
        CreateHIHNButtonUI(7, AppDisplayConstants.HIHN.HIHN_VII, TextureHelper.LoadTexture2DCached(ImageConstants.HIHN.HIHN_VII_URL), contentPanel);
        CreateHIHNButtonUI(8, AppDisplayConstants.HIHN.HIHN_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.HIHN.HIHN_VIII_URL), contentPanel);
        CreateHIHNButtonUI(9, AppDisplayConstants.HIHN.HIHN_IX, TextureHelper.LoadTexture2DCached(ImageConstants.HIHN.HIHN_IX_URL), contentPanel);
        CreateHIHNButtonUI(10, AppDisplayConstants.HIHN.HIHN_X, TextureHelper.LoadTexture2DCached(ImageConstants.HIHN.HIHN_X_URL), contentPanel);

        CreateHIHNButtonEvent(contentPanel);
    }
    private void CreateHIHNButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(HIHNButtonPrefab, panel);
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
    public void CreateHIHNButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await HIHNIManager.Instance.CreateHIHNIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await HIHNIIManager.Instance.CreateHIHNIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await HIHNIIIManager.Instance.CreateHIHNIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await HIHNIVManager.Instance.CreateHIHNIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await HIHNVManager.Instance.CreateHIHNVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await HIHNVIManager.Instance.CreateHIHNVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await HIHNVIIManager.Instance.CreateHIHNVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await HIHNVIIIManager.Instance.CreateHIHNVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await HIHNIXManager.Instance.CreateHIHNIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await HIHNXManager.Instance.CreateHIHNXManagerAsync());
    }
}
