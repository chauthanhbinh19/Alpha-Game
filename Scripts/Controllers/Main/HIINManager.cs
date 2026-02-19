using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HIINManager : MonoBehaviour
{
    public static HIINManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject HIINPanelPrefab;
    private GameObject HIINButtonPrefab;
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
        HIINPanelPrefab = UIManager.Instance.Get("HIINPanelPrefab");
        HIINButtonPrefab = UIManager.Instance.Get("HIINButtonPrefab");
    }
    public void CreateHIIN()
    {
        GameObject currentObject = Instantiate(HIINPanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("HIINContent/Content");

        CreateHIINButtonUI(1, AppDisplayConstants.HIIN.HIIN_I, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_I_URL), contentPanel);
        CreateHIINButtonUI(2, AppDisplayConstants.HIIN.HIIN_II, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_II_URL), contentPanel);
        CreateHIINButtonUI(3, AppDisplayConstants.HIIN.HIIN_III, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_III_URL), contentPanel);
        CreateHIINButtonUI(4, AppDisplayConstants.HIIN.HIIN_IV, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_IV_URL), contentPanel);
        CreateHIINButtonUI(5, AppDisplayConstants.HIIN.HIIN_V, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_V_URL), contentPanel);
        CreateHIINButtonUI(6, AppDisplayConstants.HIIN.HIIN_VI, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_VI_URL), contentPanel);
        CreateHIINButtonUI(7, AppDisplayConstants.HIIN.HIIN_VII, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_VII_URL), contentPanel);
        CreateHIINButtonUI(8, AppDisplayConstants.HIIN.HIIN_VIII, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_VIII_URL), contentPanel);
        CreateHIINButtonUI(9, AppDisplayConstants.HIIN.HIIN_IX, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_IX_URL), contentPanel);
        CreateHIINButtonUI(10, AppDisplayConstants.HIIN.HIIN_X, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_X_URL), contentPanel);
        CreateHIINButtonUI(11, AppDisplayConstants.HIIN.HIIN_XI, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_XI_URL), contentPanel);
        CreateHIINButtonUI(12, AppDisplayConstants.HIIN.HIIN_XII, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_XII_URL), contentPanel);
        CreateHIINButtonUI(13, AppDisplayConstants.HIIN.HIIN_XIII, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_XIII_URL), contentPanel);
        CreateHIINButtonUI(14, AppDisplayConstants.HIIN.HIIN_XIV, Resources.Load<Texture2D>(ImageConstants.HIIN.HIIN_XIV_URL), contentPanel);

        CreateHIINButtonEvent(contentPanel);
    }
    private void CreateHIINButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(HIINButtonPrefab, panel);
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
    public void CreateHIINButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await HIINIManager.Instance.CreateHIINIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await HIINIIManager.Instance.CreateHIINIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await HIINIIIManager.Instance.CreateHIINIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await HIINIVManager.Instance.CreateHIINIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await HIINVManager.Instance.CreateHIINVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await HIINVIManager.Instance.CreateHIINVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await HIINVIIManager.Instance.CreateHIINVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await HIINVIIIManager.Instance.CreateHIINVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await HIINIXManager.Instance.CreateHIINIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await HIINXManager.Instance.CreateHIINXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_11", panel, async () => await HIINXIManager.Instance.CreateHIINXIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_12", panel, async () => await HIINXIIManager.Instance.CreateHIINXIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_13", panel, async () => await HIINXIIIManager.Instance.CreateHIINXIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_14", panel, async () => await HIINXIVManager.Instance.CreateHIINXIVManagerAsync());
    }
}
