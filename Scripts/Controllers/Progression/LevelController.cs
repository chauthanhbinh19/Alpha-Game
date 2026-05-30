using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    private Transform MainPanel;
    public GameObject LevelPanelPrefab;
    private GameObject currentPanel;
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
        LevelPanelPrefab = UIManager.Instance.Get("LevelPanelPrefab");
    }
    public void Open(int currentLevel, int maxLevel, double currentExp, long itemQuantity, Func<int, double> expRule)
    {
        currentPanel = Instantiate(LevelPanelPrefab, MainPanel);

        InitializeUI(currentPanel, currentLevel, maxLevel, currentExp, itemQuantity, expRule);
    }
    private void InitializeUI(GameObject panel, int currentLevel, int maxLevel, double currentExp, long itemQuantity, Func<int, double> expRule)
    {
        TextMeshProUGUI currentLevelText = panel.transform.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nextLevelText = panel.transform.Find("NextLevel").GetComponent<TextMeshProUGUI>();
        Slider progressionSlider = panel.transform.Find("ProgressionSlider").GetComponent<Slider>();
        Slider quantitySlider = panel.transform.Find("QuantitySlider").GetComponent<Slider>();
        TextMeshProUGUI experienceText = panel.transform.Find("ExperienceText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI userItemQuantityText = panel.transform.Find("UserItemQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemUsedQuantityText = panel.transform.Find("ItemUsedQuantityText").GetComponent<TextMeshProUGUI>();
        RawImage userItemImage = panel.transform.Find("UserItemImage").GetComponent<RawImage>();
        RawImage itemUsedImage = panel.transform.Find("ItemUsedImage").GetComponent<RawImage>();
        Button increaseOne = panel.transform.Find("IncreaseOneLevelButton").GetComponent<Button>();
        Button increaseTen = panel.transform.Find("IncreaseTenLevelButton").GetComponent<Button>();
        Button increaseMax = panel.transform.Find("IncreaseMaxLevelButton").GetComponent<Button>();
        Button decreaseOne = panel.transform.Find("DecreaseOneLevelButton").GetComponent<Button>();
        Button decreaseTen = panel.transform.Find("DecreaseTenLevelButton").GetComponent<Button>();
        Button decreaseMax = panel.transform.Find("DecreaseMaxLevelButton").GetComponent<Button>();
        Button confirm = panel.transform.Find("ConfirmButton").GetComponent<Button>();
        Button close = panel.transform.Find("CloseButton").GetComponent<Button>();

        int targetLevel = currentLevel;

        Refresh();

        void Refresh()
        {
            currentLevelText.text = currentLevel.ToString();

            if (targetLevel >= maxLevel)
            {
                nextLevelText.text = "MAX";
            }
            else
            {
                nextLevelText.text = targetLevel.ToString();
            }

            double requiredExp =
                expRule(currentLevel);

            progressionSlider.value =
                (float)(currentExp / requiredExp);

            experienceText.text =
                $"{currentExp:N0}/{requiredExp:N0}";

            // quantityText.text =
            //     itemQuantity.ToString("N0");
        }

        increaseOne.onClick.AddListener(() =>
        {
            targetLevel = Mathf.Min(
                targetLevel + 1,
                maxLevel);

            Refresh();
        });

        increaseTen.onClick.AddListener(() =>
        {
            targetLevel = Mathf.Min(
                targetLevel + 10,
                maxLevel);

            Refresh();
        });

        increaseMax.onClick.AddListener(() =>
        {
            targetLevel = maxLevel;
            Refresh();
        });

        decreaseOne.onClick.AddListener(() =>
        {
            targetLevel = Mathf.Max(
                targetLevel - 1,
                currentLevel);

            Refresh();
        });

        decreaseTen.onClick.AddListener(() =>
        {
            targetLevel = Mathf.Max(
                targetLevel - 10,
                currentLevel);

            Refresh();
        });

        decreaseMax.onClick.AddListener(() =>
        {
            targetLevel = currentLevel;
            Refresh();
        });

        close.onClick.AddListener(() =>
        {
            Destroy(panel);
        });

        confirm.onClick.AddListener(() =>
        {
            Debug.Log(
                $"Level {currentLevel} -> {targetLevel}");

            Destroy(panel);
        });
    }
}