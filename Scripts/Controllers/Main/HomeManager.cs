using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using System.Reflection;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
public class HomeManager : MonoBehaviour
{
    public static HomeManager Instance { get; private set; }
    private GameObject HomePanelPrefab;
    private Transform MainPanel;
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
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        HomePanelPrefab = UIManager.Instance.Get("HomePanelPrefab");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
    }
    public void CreateHomePanel()
    {
        GameObject currentObject = Instantiate(HomePanelPrefab, MainPanel);

        Transform achivementTransform = currentObject.transform.Find("Scroll View/Viewport/Content/StatisticsPart/AchievementPanel");
        TextMeshProUGUI achievementTitleText = achivementTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI achievementQuantityText = achivementTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        // UserAchievementsService.Create().GetUserAchievementsCountAsync()
        
    }
}