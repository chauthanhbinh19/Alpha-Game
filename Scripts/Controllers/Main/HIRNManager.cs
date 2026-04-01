using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HIRNManager : MonoBehaviour
{
    public static HIRNManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject HIRNPanelPrefab;
    private GameObject HIRNButtonPrefab;
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
        HIRNPanelPrefab = UIManager.Instance.Get("HIRNPanelPrefab");
        HIRNButtonPrefab = UIManager.Instance.Get("HIRNButtonPrefab");
    }
    public void CreateHIRN()
    {
        GameObject currentObject = Instantiate(HIRNPanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("HIRNContent/Content");

        CreateHIRNButtonUI(1, AppDisplayConstants.HIRN.HIRN_I, TextureHelper.LoadTexture2DCached(ImageConstants.HIRN.HIRN_I_URL), contentPanel);
        CreateHIRNButtonUI(2, AppDisplayConstants.HIRN.HIRN_II, TextureHelper.LoadTexture2DCached(ImageConstants.HIRN.HIRN_II_URL), contentPanel);
        CreateHIRNButtonUI(3, AppDisplayConstants.HIRN.HIRN_III, TextureHelper.LoadTexture2DCached(ImageConstants.HIRN.HIRN_III_URL), contentPanel);
        CreateHIRNButtonUI(4, AppDisplayConstants.HIRN.HIRN_IV, TextureHelper.LoadTexture2DCached(ImageConstants.HIRN.HIRN_IV_URL), contentPanel);
        CreateHIRNButtonUI(5, AppDisplayConstants.HIRN.HIRN_V, TextureHelper.LoadTexture2DCached(ImageConstants.HIRN.HIRN_V_URL), contentPanel);
        CreateHIRNButtonUI(6, AppDisplayConstants.HIRN.HIRN_VI, TextureHelper.LoadTexture2DCached(ImageConstants.HIRN.HIRN_VI_URL), contentPanel);
        CreateHIRNButtonUI(7, AppDisplayConstants.HIRN.HIRN_VII, TextureHelper.LoadTexture2DCached(ImageConstants.HIRN.HIRN_VII_URL), contentPanel);
        CreateHIRNButtonUI(8, AppDisplayConstants.HIRN.HIRN_VIII, TextureHelper.LoadTexture2DCached(ImageConstants.HIRN.HIRN_VIII_URL), contentPanel);
        CreateHIRNButtonUI(9, AppDisplayConstants.HIRN.HIRN_IX, TextureHelper.LoadTexture2DCached(ImageConstants.HIRN.HIRN_IX_URL), contentPanel);
        CreateHIRNButtonUI(10, AppDisplayConstants.HIRN.HIRN_X, TextureHelper.LoadTexture2DCached(ImageConstants.HIRN.HIRN_X_URL), contentPanel);

        CreateHIRNButtonEvent(contentPanel);
    }
    private void CreateHIRNButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(HIRNButtonPrefab, panel);
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
    public void CreateHIRNButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await HIRNIManager.Instance.CreateHIRNIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await HIRNIIManager.Instance.CreateHIRNIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await HIRNIIIManager.Instance.CreateHIRNIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await HIRNIVManager.Instance.CreateHIRNIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await HIRNVManager.Instance.CreateHIRNVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await HIRNVIManager.Instance.CreateHIRNVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await HIRNVIIManager.Instance.CreateHIRNVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await HIRNVIIIManager.Instance.CreateHIRNVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await HIRNIXManager.Instance.CreateHIRNIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await HIRNXManager.Instance.CreateHIRNXManagerAsync());
    }
}
