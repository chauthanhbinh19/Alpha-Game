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
    private GameObject ReactorButtonPrefab;
    public static ScienceFictionManager Instance { get; private set; }
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
        ReactorButtonPrefab = UIManager.Instance.Get("ReactorButtonPrefab");
    }
    public void CreateScienceFictionButton(Transform reactorMenuPanel)
    {
        Texture2D itemBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Badge.BADGE_INVENTORY_URL);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_1, 1, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_1_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_2, 2, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_2_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_3, 3, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_3_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_4, 4, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_4_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_5, 5, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_5_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_6, 6, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_6_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_7, 7, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_7_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_8, 8, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_8_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_9, 9, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_9_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_10, 10, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_10_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_11, 11, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_11_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_12, 12, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_12_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_13, 13, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_13_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_14, 14, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_14_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_15, 15, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_15_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_16, 16, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_16_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_17, 17, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_17_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_18, 18, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_18_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_19, 19, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_19_URL), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_20, 20, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.REACTOR_NUMBER_20_URL), reactorMenuPanel);

        FindAnyObjectByType<MainMenuAnimeStatsManager>().CreateAnimeButton(reactorMenuPanel);
        reactorMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateScienceFictionButtonUI(string itemName, int number, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ReactorButtonPrefab, panel);
        newButton.name = itemName;

        // Gán màu cho itemBackground
        // RawImage  background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        // if (background != null && itemBackground != null)
        // {
        //     background.texture = itemBackground;
        // }

        RawImage image = newButton.transform.Find("Image").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // TextMeshProUGUI nameText = newButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        // if (nameText != null)
        // {
        //     nameText.text = LocalizationManager.Get(itemName);
        // }

        TextMeshProUGUI numberText = newButton.transform.Find("NumberText").GetComponent<TextMeshProUGUI>();
        if (numberText != null)
        {
            numberText.text = number.ToString("D2");
        }

        // RawImage borderImage = newButton.transform.Find("BorderImage").GetComponent<RawImage>();
    }
    public void GetScienceFictionButton(Transform scienceFictionPanel)
    {
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_1, scienceFictionPanel, async () => await ReactorNumber1Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_2, scienceFictionPanel, async () => await ReactorNumber2Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_3, scienceFictionPanel, async () => await ReactorNumber3Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_4, scienceFictionPanel, async () => await ReactorNumber4Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_5, scienceFictionPanel, async () => await ReactorNumber5Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_6, scienceFictionPanel, async () => await ReactorNumber6Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_7, scienceFictionPanel, async () => await ReactorNumber7Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_8, scienceFictionPanel, async () => await ReactorNumber8Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_9, scienceFictionPanel, async () => await ReactorNumber9Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_10, scienceFictionPanel, async () => await ReactorNumber10Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_11, scienceFictionPanel, async () => await ReactorNumber11Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_12, scienceFictionPanel, async () => await ReactorNumber12Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_13, scienceFictionPanel, async () => await ReactorNumber13Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_14, scienceFictionPanel, async () => await ReactorNumber14Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_15, scienceFictionPanel, async () => await ReactorNumber15Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_16, scienceFictionPanel, async () => await ReactorNumber16Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_17, scienceFictionPanel, async () => await ReactorNumber17Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_18, scienceFictionPanel, async () => await ReactorNumber18Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_19, scienceFictionPanel, async () => await ReactorNumber19Manager.Instance.CreateReactorPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_20, scienceFictionPanel, async () => await ReactorNumber20Manager.Instance.CreateReactorPanelAsync());
    }
}