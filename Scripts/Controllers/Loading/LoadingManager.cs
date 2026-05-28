using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance { get; private set; }
    public Transform LoadingPanel;
    public GameObject LoadingProcessPanelPrefab;
    private GameObject currentLoadingObject;

    private Slider loadingSlider;
    private TextMeshProUGUI loadingText;
    private TextMeshProUGUI contentText;
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
        currentLoadingObject = Instantiate(LoadingProcessPanelPrefab, LoadingPanel);
        loadingSlider = currentLoadingObject.transform.Find("Slider").GetComponent<Slider>();
        loadingText = currentLoadingObject.transform.Find("LoadingText").GetComponent<TextMeshProUGUI>();
        contentText = currentLoadingObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();

        loadingSlider.value = 0;
    }
    public void SetProgress(float value, string percentText = "", string loadingContent = "")
    {
        // Slider
        if (loadingSlider != null)
        {
            loadingSlider.value = value;
        }

        // Hiển thị %
        if (loadingText != null)
        {
            int percent = Mathf.RoundToInt(value * 100f);
            loadingText.text = $"{percent}%";
        }

        // Nội dung đang load
        if (contentText != null)
        {
            contentText.text = $"Loading {percentText} {loadingContent}...";
        }
    }
    public void HideLoading()
    {
        Destroy(currentLoadingObject);
    }
}