using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureManager : MonoBehaviour
{
    public static StructureManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject PopupStructureButtonPanelPrefab;
    private GameObject StructureButtonPrefab;
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
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        PopupStructureButtonPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.General.POPUP_STRUCTURE_BUTTON_PANEL_PREFAB);
        StructureButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.STRUCTURE_BUTTON_PREFAB);
    }
    public void CreateStructure()
    {
        GameObject currentObject = Instantiate(PopupStructureButtonPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("Scroll View/Viewport/Content");

        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await HomeManager.Instance.CreateHomePanelAsync();
        });

        CreateStructureButtonUI(1, AppDisplayConstants.Title.HIIN, AppDisplayConstants.Title.HIIN_FULLNAME, contentPanel);
        CreateStructureButtonUI(2, AppDisplayConstants.Title.SSWN, AppDisplayConstants.Title.SSWN_FULLNAME, contentPanel);
        CreateStructureButtonUI(3, AppDisplayConstants.Title.HITN, AppDisplayConstants.Title.HITN_FULLNAME, contentPanel);
        CreateStructureButtonUI(4, AppDisplayConstants.Title.HIHN, AppDisplayConstants.Title.HIHN_FULLNAME, contentPanel);
        CreateStructureButtonUI(5, AppDisplayConstants.Title.HIEN, AppDisplayConstants.Title.HIEN_FULLNAME, contentPanel);
        CreateStructureButtonUI(6, AppDisplayConstants.Title.HICA, AppDisplayConstants.Title.HICA_FULLNAME, contentPanel);
        CreateStructureButtonUI(7, AppDisplayConstants.Title.HIRN, AppDisplayConstants.Title.HIRN_FULLNAME, contentPanel);
        CreateStructureButtonUI(8, AppDisplayConstants.Title.HIDC, AppDisplayConstants.Title.HIDC_FULLNAME, contentPanel);
        CreateStructureButtonUI(9, AppDisplayConstants.Title.HICB, AppDisplayConstants.Title.HICB_FULLNAME, contentPanel);
        CreateStructureButtonUI(10, AppDisplayConstants.Title.HISN, AppDisplayConstants.Title.HISN_FULLNAME, contentPanel);

        CreateStructureButtonEvent(contentPanel);
    }
    private void CreateStructureButtonUI(int index, string itemName, string itemDescription, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(StructureButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        // Gán hình ảnh cho itemImage
        // RawImage image = newButton.transform.Find("Image").GetComponent<RawImage>();
        // if (image != null && _itemImage != null)
        // {
        //     image.texture = _itemImage;
        // }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }

        TextMeshProUGUI descriptionText = transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
        if (descriptionText != null)
        {
            descriptionText.text = LocalizationManager.Get(itemDescription);
        }
    }
    public void CreateStructureButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, () => HIINManager.Instance.CreateHIIN());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, () => SSWNManager.Instance.CreateSSWN());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, () => HITNManager.Instance.CreateHITN());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, () => HIHNManager.Instance.CreateHIHN());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, () => HIENManager.Instance.CreateHIEN());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, () => HICAManager.Instance.CreateHICA());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, () => HIRNManager.Instance.CreateHIRN());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, () => HIDCManager.Instance.CreateHIDC());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, () => HICBManager.Instance.CreateHICB());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, () => HISNManager.Instance.CreateHISN());
    }
}
