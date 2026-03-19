using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HICAManager : MonoBehaviour
{
    public static HICAManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject HICAPanelPrefab;
    private GameObject HICAButtonPrefab;
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
        HICAPanelPrefab = UIManager.Instance.Get("HICAPanelPrefab");
        HICAButtonPrefab = UIManager.Instance.Get("HICAButtonPrefab");
    }
    public void CreateHICA()
    {
        GameObject currentObject = Instantiate(HICAPanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("HICAContent/Content");

        CreateHICAButtonUI(1, AppDisplayConstants.HICA.HICA_I, Resources.Load<Texture2D>(ImageConstants.HICA.HICA_I_URL), contentPanel);
        CreateHICAButtonUI(2, AppDisplayConstants.HICA.HICA_II, Resources.Load<Texture2D>(ImageConstants.HICA.HICA_II_URL), contentPanel);
        CreateHICAButtonUI(3, AppDisplayConstants.HICA.HICA_III, Resources.Load<Texture2D>(ImageConstants.HICA.HICA_III_URL), contentPanel);
        CreateHICAButtonUI(4, AppDisplayConstants.HICA.HICA_IV, Resources.Load<Texture2D>(ImageConstants.HICA.HICA_IV_URL), contentPanel);
        CreateHICAButtonUI(5, AppDisplayConstants.HICA.HICA_V, Resources.Load<Texture2D>(ImageConstants.HICA.HICA_V_URL), contentPanel);
        CreateHICAButtonUI(6, AppDisplayConstants.HICA.HICA_VI, Resources.Load<Texture2D>(ImageConstants.HICA.HICA_VI_URL), contentPanel);
        CreateHICAButtonUI(7, AppDisplayConstants.HICA.HICA_VII, Resources.Load<Texture2D>(ImageConstants.HICA.HICA_VII_URL), contentPanel);
        CreateHICAButtonUI(8, AppDisplayConstants.HICA.HICA_VIII, Resources.Load<Texture2D>(ImageConstants.HICA.HICA_VIII_URL), contentPanel);
        CreateHICAButtonUI(9, AppDisplayConstants.HICA.HICA_IX, Resources.Load<Texture2D>(ImageConstants.HICA.HICA_IX_URL), contentPanel);
        CreateHICAButtonUI(10, AppDisplayConstants.HICA.HICA_X, Resources.Load<Texture2D>(ImageConstants.HICA.HICA_X_URL), contentPanel);

        CreateHICAButtonEvent(contentPanel);
    }
    private void CreateHICAButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(HICAButtonPrefab, panel);
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
    public void CreateHICAButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await HICAIManager.Instance.CreateHICAIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await HICAIIManager.Instance.CreateHICAIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await HICAIIIManager.Instance.CreateHICAIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await HICAIVManager.Instance.CreateHICAIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await HICAVManager.Instance.CreateHICAVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await HICAVIManager.Instance.CreateHICAVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await HICAVIIManager.Instance.CreateHICAVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await HICAVIIIManager.Instance.CreateHICAVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await HICAIXManager.Instance.CreateHICAIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await HICAXManager.Instance.CreateHICAXManagerAsync());
    }
}
