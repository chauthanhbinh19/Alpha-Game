using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HIENManager : MonoBehaviour
{
    public static HIENManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject HIENPanelPrefab;
    private GameObject HIENButtonPrefab;
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
        HIENPanelPrefab = UIManager.Instance.Get("HIENPanelPrefab");
        HIENButtonPrefab = UIManager.Instance.Get("HIENButtonPrefab");
    }
    public void CreateHIEN()
    {
        GameObject currentObject = Instantiate(HIENPanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("HIENContent/Content");

        CreateHIENButtonUI(1, AppDisplayConstants.HIEN.HIEN_I, TextureHelper.LoadTexture2DCached(ImageConstants.HIEN.HIEN_I_URL), contentPanel);
        CreateHIENButtonUI(2, AppDisplayConstants.HIEN.HIEN_II, TextureHelper.LoadTexture2DCached(ImageConstants.HIEN.HIEN_II_URL), contentPanel);
        CreateHIENButtonUI(3, AppDisplayConstants.HIEN.HIEN_III, TextureHelper.LoadTexture2DCached(ImageConstants.HIEN.HIEN_III_URL), contentPanel);
        CreateHIENButtonUI(4, AppDisplayConstants.HIEN.HIEN_IV, TextureHelper.LoadTexture2DCached(ImageConstants.HIEN.HIEN_IV_URL), contentPanel);
        CreateHIENButtonUI(5, AppDisplayConstants.HIEN.HIEN_V, TextureHelper.LoadTexture2DCached(ImageConstants.HIEN.HIEN_V_URL), contentPanel);
        CreateHIENButtonUI(6, AppDisplayConstants.HIEN.HIEN_VI, TextureHelper.LoadTexture2DCached(ImageConstants.HIEN.HIEN_VI_URL), contentPanel);
        CreateHIENButtonUI(7, AppDisplayConstants.HIEN.HIEN_VII, TextureHelper.LoadTexture2DCached(ImageConstants.HIEN.HIEN_VII_URL), contentPanel);
        CreateHIENButtonUI(8, AppDisplayConstants.HIEN.HIEN_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.HIEN.HIEN_VIII_URL), contentPanel);
        CreateHIENButtonUI(9, AppDisplayConstants.HIEN.HIEN_IX, TextureHelper.LoadTexture2DCached(ImageConstants.HIEN.HIEN_IX_URL), contentPanel);
        CreateHIENButtonUI(10, AppDisplayConstants.HIEN.HIEN_X, TextureHelper.LoadTexture2DCached(ImageConstants.HIEN.HIEN_X_URL), contentPanel);

        CreateHIENButtonEvent(contentPanel);
    }
    private void CreateHIENButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(HIENButtonPrefab, panel);
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
    public void CreateHIENButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await HIENIManager.Instance.CreateHIENIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await HIENIIManager.Instance.CreateHIENIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await HIENIIIManager.Instance.CreateHIENIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await HIENIVManager.Instance.CreateHIENIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await HIENVManager.Instance.CreateHIENVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await HIENVIManager.Instance.CreateHIENVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await HIENVIIManager.Instance.CreateHIENVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await HIENVIIIManager.Instance.CreateHIENVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await HIENIXManager.Instance.CreateHIENIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await HIENXManager.Instance.CreateHIENXManagerAsync());
    }
}
