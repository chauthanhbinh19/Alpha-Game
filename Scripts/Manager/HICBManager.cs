using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HICBManager : MonoBehaviour
{
    public static HICBManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject HICBPanelPrefab;
    private GameObject HICBButtonPrefab;
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
        HICBPanelPrefab = UIManager.Instance.Get("HICBPanelPrefab");
        HICBButtonPrefab = UIManager.Instance.Get("HICBButtonPrefab");
    }
    public void CreateHICB()
    {
        GameObject currentObject = Instantiate(HICBPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("HICBContent/Content");

        CreateHICBButtonUI(1, AppDisplayConstants.HICB.HICB_I, TextureHelper.LoadTexture2DCached(ImageConstants.HICB.HICB_I_URL), contentPanel);
        CreateHICBButtonUI(2, AppDisplayConstants.HICB.HICB_II, TextureHelper.LoadTexture2DCached(ImageConstants.HICB.HICB_II_URL), contentPanel);
        CreateHICBButtonUI(3, AppDisplayConstants.HICB.HICB_III, TextureHelper.LoadTexture2DCached(ImageConstants.HICB.HICB_III_URL), contentPanel);
        CreateHICBButtonUI(4, AppDisplayConstants.HICB.HICB_IV, TextureHelper.LoadTexture2DCached(ImageConstants.HICB.HICB_IV_URL), contentPanel);
        CreateHICBButtonUI(5, AppDisplayConstants.HICB.HICB_V, TextureHelper.LoadTexture2DCached(ImageConstants.HICB.HICB_V_URL), contentPanel);
        CreateHICBButtonUI(6, AppDisplayConstants.HICB.HICB_VI, TextureHelper.LoadTexture2DCached(ImageConstants.HICB.HICB_VI_URL), contentPanel);
        CreateHICBButtonUI(7, AppDisplayConstants.HICB.HICB_VII, TextureHelper.LoadTexture2DCached(ImageConstants.HICB.HICB_VII_URL), contentPanel);
        CreateHICBButtonUI(8, AppDisplayConstants.HICB.HICB_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.HICB.HICB_VIII_URL), contentPanel);
        CreateHICBButtonUI(9, AppDisplayConstants.HICB.HICB_IX, TextureHelper.LoadTexture2DCached(ImageConstants.HICB.HICB_IX_URL), contentPanel);
        CreateHICBButtonUI(10, AppDisplayConstants.HICB.HICB_X, TextureHelper.LoadTexture2DCached(ImageConstants.HICB.HICB_X_URL), contentPanel);

        CreateHICBButtonEvent(contentPanel);
    }
    private void CreateHICBButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(HICBButtonPrefab, panel);
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
    public void CreateHICBButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await HICBIManager.Instance.CreateHICBIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await HICBIIManager.Instance.CreateHICBIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await HICBIIIManager.Instance.CreateHICBIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await HICBIVManager.Instance.CreateHICBIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await HICBVManager.Instance.CreateHICBVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await HICBVIManager.Instance.CreateHICBVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await HICBVIIManager.Instance.CreateHICBVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await HICBVIIIManager.Instance.CreateHICBVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await HICBIXManager.Instance.CreateHICBIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await HICBXManager.Instance.CreateHICBXManagerAsync());
    }
}
