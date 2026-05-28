using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HISNManager : MonoBehaviour
{
    public static HISNManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject HISNPanelPrefab;
    private GameObject HISNButtonPrefab;
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
        HISNPanelPrefab = UIManager.Instance.Get("HISNPanelPrefab");
        HISNButtonPrefab = UIManager.Instance.Get("HISNButtonPrefab");
    }
    public void CreateHISN()
    {
        GameObject currentObject = Instantiate(HISNPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("HISNContent/Content");

        CreateHISNButtonUI(1, AppDisplayConstants.HISN.HISN_I, TextureHelper.LoadTexture2DCached(ImageConstants.HISN.HISN_I_URL), contentPanel);
        CreateHISNButtonUI(2, AppDisplayConstants.HISN.HISN_II, TextureHelper.LoadTexture2DCached(ImageConstants.HISN.HISN_II_URL), contentPanel);
        CreateHISNButtonUI(3, AppDisplayConstants.HISN.HISN_III, TextureHelper.LoadTexture2DCached(ImageConstants.HISN.HISN_III_URL), contentPanel);
        CreateHISNButtonUI(4, AppDisplayConstants.HISN.HISN_IV, TextureHelper.LoadTexture2DCached(ImageConstants.HISN.HISN_IV_URL), contentPanel);
        CreateHISNButtonUI(5, AppDisplayConstants.HISN.HISN_V, TextureHelper.LoadTexture2DCached(ImageConstants.HISN.HISN_V_URL), contentPanel);
        CreateHISNButtonUI(6, AppDisplayConstants.HISN.HISN_VI, TextureHelper.LoadTexture2DCached(ImageConstants.HISN.HISN_VI_URL), contentPanel);
        CreateHISNButtonUI(7, AppDisplayConstants.HISN.HISN_VII, TextureHelper.LoadTexture2DCached(ImageConstants.HISN.HISN_VII_URL), contentPanel);
        CreateHISNButtonUI(8, AppDisplayConstants.HISN.HISN_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.HISN.HISN_VIII_URL), contentPanel);
        CreateHISNButtonUI(9, AppDisplayConstants.HISN.HISN_IX, TextureHelper.LoadTexture2DCached(ImageConstants.HISN.HISN_IX_URL), contentPanel);
        CreateHISNButtonUI(10, AppDisplayConstants.HISN.HISN_X, TextureHelper.LoadTexture2DCached(ImageConstants.HISN.HISN_X_URL), contentPanel);

        CreateHISNButtonEvent(contentPanel);
    }
    private void CreateHISNButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(HISNButtonPrefab, panel);
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
    public void CreateHISNButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await HISNIManager.Instance.CreateHISNIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await HISNIIManager.Instance.CreateHISNIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await HISNIIIManager.Instance.CreateHISNIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await HISNIVManager.Instance.CreateHISNIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await HISNVManager.Instance.CreateHISNVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await HISNVIManager.Instance.CreateHISNVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await HISNVIIManager.Instance.CreateHISNVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await HISNVIIIManager.Instance.CreateHISNVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await HISNIXManager.Instance.CreateHISNIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await HISNXManager.Instance.CreateHISNXManagerAsync());
    }
}
