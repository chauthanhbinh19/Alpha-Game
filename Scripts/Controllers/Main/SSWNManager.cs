using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SSWNManager : MonoBehaviour
{
    public static SSWNManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject SSWNPanelPrefab;
    private GameObject SSWNButtonPrefab;
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
        SSWNPanelPrefab = UIManager.Instance.Get("SSWNPanelPrefab");
        SSWNButtonPrefab = UIManager.Instance.Get("SSWNButtonPrefab");
    }
    public void CreateSSWN()
    {
        GameObject currentObject = Instantiate(SSWNPanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("SSWNContent/Content");

        CreateSSWNButtonUI(1, AppDisplayConstants.SSWN.SSWN_I, TextureHelper.LoadTexture2DCached(ImageConstants.SSWN.SSWN_I_URL), contentPanel);
        CreateSSWNButtonUI(2, AppDisplayConstants.SSWN.SSWN_II, TextureHelper.LoadTexture2DCached(ImageConstants.SSWN.SSWN_II_URL), contentPanel);
        CreateSSWNButtonUI(3, AppDisplayConstants.SSWN.SSWN_III, TextureHelper.LoadTexture2DCached(ImageConstants.SSWN.SSWN_III_URL), contentPanel);
        CreateSSWNButtonUI(4, AppDisplayConstants.SSWN.SSWN_IV, TextureHelper.LoadTexture2DCached(ImageConstants.SSWN.SSWN_IV_URL), contentPanel);
        CreateSSWNButtonUI(5, AppDisplayConstants.SSWN.SSWN_V, TextureHelper.LoadTexture2DCached(ImageConstants.SSWN.SSWN_V_URL), contentPanel);
        CreateSSWNButtonUI(6, AppDisplayConstants.SSWN.SSWN_VI, TextureHelper.LoadTexture2DCached(ImageConstants.SSWN.SSWN_VI_URL), contentPanel);
        CreateSSWNButtonUI(7, AppDisplayConstants.SSWN.SSWN_VII, TextureHelper.LoadTexture2DCached(ImageConstants.SSWN.SSWN_VII_URL), contentPanel);
        CreateSSWNButtonUI(8, AppDisplayConstants.SSWN.SSWN_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.SSWN.SSWN_VIII_URL), contentPanel);
        CreateSSWNButtonUI(9, AppDisplayConstants.SSWN.SSWN_IX, TextureHelper.LoadTexture2DCached(ImageConstants.SSWN.SSWN_IX_URL), contentPanel);
        CreateSSWNButtonUI(10, AppDisplayConstants.SSWN.SSWN_X, TextureHelper.LoadTexture2DCached(ImageConstants.SSWN.SSWN_X_URL), contentPanel);

        CreateSSWNButtonEvent(contentPanel);
    }
    private void CreateSSWNButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(SSWNButtonPrefab, panel);
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
    public void CreateSSWNButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await SSWNIManager.Instance.CreateSSWNIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await SSWNIIManager.Instance.CreateSSWNIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await SSWNIIIManager.Instance.CreateSSWNIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await SSWNIVManager.Instance.CreateSSWNIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await SSWNVManager.Instance.CreateSSWNVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await SSWNVIManager.Instance.CreateSSWNVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await SSWNVIIManager.Instance.CreateSSWNVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await SSWNVIIIManager.Instance.CreateSSWNVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await SSWNIXManager.Instance.CreateSSWNIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await SSWNXManager.Instance.CreateSSWNXManagerAsync());
    }
}
