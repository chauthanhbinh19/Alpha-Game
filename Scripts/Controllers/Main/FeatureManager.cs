using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FeatureManager : MonoBehaviour
{
    public static FeatureManager Instance { get; private set; }
    private GameObject FeaturePanelPrefab;
    private Transform MainPanel;
    private Transform currentContent;
    private Transform currencyPanel;
    private Transform popupPanel;
    private Button CloseButton;
    private Button HomeButton;
    private TextMeshProUGUI titleText;
    private Currencies currentCurrency;

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
        FeaturePanelPrefab = UIManager.Instance.Get("FeaturePanelPrefab");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
    }
    public void CreateFeature()
    {
        GameObject featureObject = Instantiate(FeaturePanelPrefab, MainPanel);
        Transform featureTransform = featureObject.transform.Find("Scroll View/Viewport/Content");
        titleText = featureObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        // CloseButton = featureObject.transform.Find("CloseButton").GetComponent<Button>();
        // CloseButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     Destroy(featureObject);
        // });
        // HomeButton = featureObject.transform.Find("HomeButton").GetComponent<Button>();
        // HomeButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     ButtonEvent.Instance.Close(MainPanel);
        // });

        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.FEATURE);
        ButtonLoader.Instance.CreateFeatureButton(featureTransform);
        GetButtonEvent(featureTransform);
    }
    public void GetButtonEvent(Transform contentPanel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, async () => await BasesController.Instance.CreateBasePanelAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, async () => await TrainsController.Instance.CreateTrainPanelAsync());
        // ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, () => TrainsController.Instance.CreateTrainPanel());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, async () => await EmployeesController.Instance.CreateEmployeePanelAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, async () => await WorldsController.Instance.CreateWorldPanelAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, async () => await CitiesController.Instance.CreateCityPanelAsync());
    }
}