using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance { get; private set; }
    public Transform LoadingPanel;
    public GameObject LoadingProcessPanelPrefab;
    private GameObject CurrentLoadingObject;

    private Slider LoadingSlider;
    private TextMeshProUGUI LoadingText;
    private TextMeshProUGUI ContentText;
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
        LoadingPanel = UIManager.Instance.GetTransform("LoadingPanel");
        LoadingProcessPanelPrefab = UIManager.Instance.Get("LoadingProcessPanelPrefab");
    }
    public void ShowLoading()
    {
        CurrentLoadingObject = Instantiate(LoadingProcessPanelPrefab, LoadingPanel);
        LoadingSlider = CurrentLoadingObject.transform.Find("Slider").GetComponent<Slider>();
        LoadingText = CurrentLoadingObject.transform.Find("LoadingText").GetComponent<TextMeshProUGUI>();
        ContentText = CurrentLoadingObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();

        LoadingSlider.value = 0;
    }
    public void SetProgress(float value, string percentText = "", string loadingContent = "")
    {
        // Slider
        if (LoadingSlider != null)
        {
            LoadingSlider.value = value;
        }

        // Hiển thị %
        if (LoadingText != null)
        {
            int percent = Mathf.RoundToInt(value * 100f);
            LoadingText.text = $"{percent}%";
        }

        // Nội dung đang load
        if (ContentText != null)
        {
            ContentText.text = $"Loading {percentText} {loadingContent}...";
        }
    }
    public void HideLoading()
    {
        Destroy(CurrentLoadingObject);
    }
}