using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;
public class ScienceFictionManager : MonoBehaviour
{
    public static ScienceFictionManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ScienceFictionPanelPrefab;
    private GameObject ScienceFictionButtonPrefab;
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
        ScienceFictionPanelPrefab = UIManager.Instance.Get("ScienceFictionPanelPrefab");
        ScienceFictionButtonPrefab = UIManager.Instance.Get("ScienceFictionButtonPrefab");
    }
    public void CreateScienceFiction()
    {
        GameObject currentObject = Instantiate(ScienceFictionPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("ScienceFictionContent/Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SCIENCE_FICTION);
        TextMeshProUGUI titleText2 = transform.Find("ScienceFictionContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.MainType.SCIENCE_FICTION);

        CreateScienceFictionButtonUI(1, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_1, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_1_URL), contentPanel);
        CreateScienceFictionButtonUI(2, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_2, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_2_URL), contentPanel);
        CreateScienceFictionButtonUI(3, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_3, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_3_URL), contentPanel);
        CreateScienceFictionButtonUI(4, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_4, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_4_URL), contentPanel);
        CreateScienceFictionButtonUI(5, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_5, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_5_URL), contentPanel);
        CreateScienceFictionButtonUI(6, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_6, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_6_URL), contentPanel);
        CreateScienceFictionButtonUI(7, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_7, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_7_URL), contentPanel);
        CreateScienceFictionButtonUI(8, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_8, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_8_URL), contentPanel);
        CreateScienceFictionButtonUI(9, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_9, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_9_URL), contentPanel);
        CreateScienceFictionButtonUI(10, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_10, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_10_URL), contentPanel);
        CreateScienceFictionButtonUI(11, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_11, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_11_URL), contentPanel);
        CreateScienceFictionButtonUI(12, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_12, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_12_URL), contentPanel);
        CreateScienceFictionButtonUI(13, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_13, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_13_URL), contentPanel);
        CreateScienceFictionButtonUI(14, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_14, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_14_URL), contentPanel);
        CreateScienceFictionButtonUI(15, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_15, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_15_URL), contentPanel);
        CreateScienceFictionButtonUI(16, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_16, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_16_URL), contentPanel);
        CreateScienceFictionButtonUI(17, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_17, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_17_URL), contentPanel);
        CreateScienceFictionButtonUI(18, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_18, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_18_URL), contentPanel);
        CreateScienceFictionButtonUI(19, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_19, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_19_URL), contentPanel);
        CreateScienceFictionButtonUI(20, AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_20, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_NUMBER_20_URL), contentPanel);

        GetScienceFictionButton(contentPanel);
    }
    private void CreateScienceFictionButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ScienceFictionButtonPrefab, panel);
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

        RawImage borderImage = transform.Find("BorderCircleImage").GetComponent<RawImage>();
        if (borderImage != null)
        {
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_BORDER_URL);
        }

        RawImage iconImage = transform.Find("IconImage").GetComponent<RawImage>();
        if (iconImage != null)
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_ICON_URL);
        }
    }
    public void GetScienceFictionButton(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await ScienceFictionIManager.Instance.CreateScienceFictionIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await ScienceFictionIIManager.Instance.CreateScienceFictionIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await ScienceFictionIIIManager.Instance.CreateScienceFictionIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await ScienceFictionIVManager.Instance.CreateScienceFictionIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await ScienceFictionVManager.Instance.CreateScienceFictionVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await ScienceFictionVIManager.Instance.CreateScienceFictionVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await ScienceFictionVIIManager.Instance.CreateScienceFictionVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await ScienceFictionVIIIManager.Instance.CreateScienceFictionVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await ScienceFictionIXManager.Instance.CreateScienceFictionIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await ScienceFictionXManager.Instance.CreateScienceFictionXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_11", panel, async () => await ScienceFictionXIManager.Instance.CreateScienceFictionXIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_12", panel, async () => await ScienceFictionXIIManager.Instance.CreateScienceFictionXIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_13", panel, async () => await ScienceFictionXIIIManager.Instance.CreateScienceFictionXIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_14", panel, async () => await ScienceFictionXIVManager.Instance.CreateScienceFictionXIVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_15", panel, async () => await ScienceFictionXVManager.Instance.CreateScienceFictionXVManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_16", panel, async () => await ScienceFictionXVIManager.Instance.CreateScienceFictionXVIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_17", panel, async () => await ScienceFictionXVIIManager.Instance.CreateScienceFictionXVIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_18", panel, async () => await ScienceFictionXVIIIManager.Instance.CreateScienceFictionXVIIIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_19", panel, async () => await ScienceFictionXIXManager.Instance.CreateScienceFictionXIXManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_20", panel, async () => await ScienceFictionXXManager.Instance.CreateScienceFictionXXManagerAsync());
    }
}