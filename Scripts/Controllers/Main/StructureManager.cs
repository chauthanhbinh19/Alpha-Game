using System.Collections;
using System.Collections.Generic;
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        PopupStructureButtonPanelPrefab = UIManager.Instance.Get("PopupStructureButtonPanelPrefab");
        StructureButtonPrefab = UIManager.Instance.Get("StructureButtonPrefab");
    }
    public void CreateStructure()
    {
        GameObject currentObject = Instantiate(PopupStructureButtonPanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("Scroll View/Viewport/Content");

        Button CloseButton = currentObject.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button HomeButton = currentObject.transform.Find("HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await HomeManager.Instance.CreateHomePanelAsync();
        });

        CreateStructureButtonUI(1, AppDisplayConstants.MainType.HIIN, AppDisplayConstants.MainType.HIIN_FULLNAME, contentPanel);
        CreateStructureButtonUI(2, AppDisplayConstants.MainType.SSWN, AppDisplayConstants.MainType.SSWN_FULLNAME, contentPanel);
        CreateStructureButtonUI(3, AppDisplayConstants.MainType.HITN, AppDisplayConstants.MainType.HITN_FULLNAME, contentPanel);
        CreateStructureButtonUI(4, AppDisplayConstants.MainType.HIHN, AppDisplayConstants.MainType.HIHN_FULLNAME, contentPanel);
        CreateStructureButtonUI(5, AppDisplayConstants.MainType.HIEN, AppDisplayConstants.MainType.HIEN_FULLNAME, contentPanel);
        CreateStructureButtonUI(6, AppDisplayConstants.MainType.HICA, AppDisplayConstants.MainType.HICA_FULLNAME, contentPanel);
        CreateStructureButtonUI(7, AppDisplayConstants.MainType.HIRN, AppDisplayConstants.MainType.HIRN_FULLNAME, contentPanel);

        CreateStructureButtonEvent(contentPanel);
    }
    private void CreateStructureButtonUI(int index, string itemName, string itemDescription, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(StructureButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán hình ảnh cho itemImage
        // RawImage image = newButton.transform.Find("Image").GetComponent<RawImage>();
        // if (image != null && _itemImage != null)
        // {
        //     image.texture = _itemImage;
        // }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }

        TextMeshProUGUI descriptionText = newButton.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
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
    }
}
