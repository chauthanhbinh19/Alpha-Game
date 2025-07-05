using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyCheckinManager : MonoBehaviour
{
    public static DailyCheckinManager Instance { get; private set; }
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private GameObject buttonPrefab;
    private GameObject DailyCheckinPanelPrefab;
    private GameObject DailyCheckinComponentPrefab;
    private Transform DailyCheckinPanel;
    private string mainType;
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        DailyCheckinPanelPrefab = UIManager.Instance.GetGameObject("DailyCheckinPanelPrefab");
        DailyCheckinComponentPrefab = UIManager.Instance.GetGameObject("DailyCheckinComponentPrefab");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateDailyCheckinGroup()
    {
        GameObject popupObject = Instantiate(DailyCheckinPanelPrefab, MainPanel);
        TextMeshProUGUI titleTMPText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        Button CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => ButtonEvent.Instance.Close(MainPanel));
        Button HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => ButtonEvent.Instance.Close(MainPanel));
        titleTMPText.text = LocalizationManager.Get("daily_checkin");
        DailyCheckinPanel = popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        // Dictionary<string, int> uniqueTypes = new Dictionary<string, int>();
        // Features features = new Features();
        List<string> uniqueTypes = new List<string> { "daily_checkin" };
        if (uniqueTypes.Count > 0)
        {
            int index = 0;
            foreach (var uniqueType in uniqueTypes)
            {
                string subType = uniqueType;
                GameObject button = Instantiate(buttonPrefab, TabButtonPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = uniqueType.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnButtonClick(button, subType));

                if (index == 0)
                {
                    mainType = uniqueType;
                    UIManager.Instance.ChangeButtonBackground(button, "Background_V4_166");
                    if (mainType.Equals("daily_checkin"))
                    {
                        CreateDailyCheckinUI();
                    }
                }
                else
                {
                    UIManager.Instance.ChangeButtonBackground(button, "Background_V4_167");
                }
                index = index + 1;
            }
        }
    }
    void OnButtonClick(GameObject clickedButton, string type)
    {
        foreach (Transform child in TabButtonPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                UIManager.Instance.ChangeButtonBackground(button.gameObject, "Background_V4_167"); // Giả sử bạn có texture trắng
            }
        }

        mainType = type;
        UIManager.Instance.ChangeButtonBackground(clickedButton, "Background_V4_166");
        if (mainType.Equals("daily_checkin"))
        {
            CreateDailyCheckinUI();
        }
    }
    public void CreateDailyCheckinUI()
    {

    }
}
