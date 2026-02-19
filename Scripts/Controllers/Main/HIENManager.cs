using System.Collections;
using System.Collections.Generic;
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

        CreateHIENButtonUI(1, AppDisplayConstants.HIEN.HIEN_I, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_I_URL), contentPanel);
        CreateHIENButtonUI(2, AppDisplayConstants.HIEN.HIEN_II, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_II_URL), contentPanel);
        CreateHIENButtonUI(3, AppDisplayConstants.HIEN.HIEN_III, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_III_URL), contentPanel);
        CreateHIENButtonUI(4, AppDisplayConstants.HIEN.HIEN_IV, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_IV_URL), contentPanel);
        CreateHIENButtonUI(5, AppDisplayConstants.HIEN.HIEN_V, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_V_URL), contentPanel);
        CreateHIENButtonUI(6, AppDisplayConstants.HIEN.HIEN_VI, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_VI_URL), contentPanel);
        CreateHIENButtonUI(7, AppDisplayConstants.HIEN.HIEN_VII, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_VII_URL), contentPanel);
        CreateHIENButtonUI(8, AppDisplayConstants.HIEN.HIEN_VIII, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_VIII_URL), contentPanel);
        CreateHIENButtonUI(9, AppDisplayConstants.HIEN.HIEN_IX, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_IX_URL), contentPanel);
        CreateHIENButtonUI(10, AppDisplayConstants.HIEN.HIEN_X, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_X_URL), contentPanel);
        CreateHIENButtonUI(11, AppDisplayConstants.HIEN.HIEN_XI, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_XI_URL), contentPanel);
        CreateHIENButtonUI(12, AppDisplayConstants.HIEN.HIEN_XII, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_XII_URL), contentPanel);
        CreateHIENButtonUI(13, AppDisplayConstants.HIEN.HIEN_XIII, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_XIII_URL), contentPanel);
        CreateHIENButtonUI(14, AppDisplayConstants.HIEN.HIEN_XIV, Resources.Load<Texture2D>(ImageConstants.HIEN.HIEN_XIV_URL), contentPanel);

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
        ButtonEvent.Instance.AssignButtonEvent("Button_11", panel, async () => await HIENXIManager.Instance.CreateHIENXIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_12", panel, async () => await HIENXIIManager.Instance.CreateHIENXIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_13", panel, async () => await HIENXIIIManager.Instance.CreateHIENXIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_14", panel, async () => await HIENXIVManager.Instance.CreateHIENXIVManagerAsync());
    }
}
