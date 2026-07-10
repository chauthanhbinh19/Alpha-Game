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
        LoadingPanel = UIManager.Instance.GetTransform(AppConstants.Transform.LOADING_PANEL);
        LoadingProcessPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.General.LOADING_PROCESS_PANEL_PREFAB);
    }
    public void ShowLoading()
    {
        if (LoadingProcessPanelPrefab == null || LoadingPanel == null)
        {
            Debug.LogWarning("[LoadingManager] LoadingProcessPanelPrefab hoặc LoadingPanel chưa được gán.");
            return;
        }

        // Clear any old loading UI first
        if (CurrentLoadingObject != null)
        {
            Destroy(CurrentLoadingObject);
            CurrentLoadingObject = null;
        }

        foreach (Transform child in LoadingPanel)
        {
            Destroy(child.gameObject);
        }

        CurrentLoadingObject = Instantiate(LoadingProcessPanelPrefab, LoadingPanel);
        LoadingSlider = CurrentLoadingObject.transform.Find("Slider")?.GetComponent<Slider>();
        LoadingText = CurrentLoadingObject.transform.Find("LoadingText")?.GetComponent<TextMeshProUGUI>();
        ContentText = CurrentLoadingObject.transform.Find("ContentText")?.GetComponent<TextMeshProUGUI>();

        if (LoadingSlider != null)
        {
            LoadingSlider.value = 0;
        }
    }

    public void SetProgress(float value, string percentText = "", string loadingContent = "")
    {
        if (LoadingSlider != null)
        {
            LoadingSlider.value = value;
        }

        if (LoadingText != null)
        {
            int percent = Mathf.RoundToInt(value * 100f);
            LoadingText.text = $"{percent}%";
        }

        if (ContentText != null)
        {
            ContentText.text = $"Loading {percentText} {loadingContent}...";
        }
    }

    public void HideLoading()
    {
        if (CurrentLoadingObject != null)
        {
            Destroy(CurrentLoadingObject);
            CurrentLoadingObject = null;
        }

        if (LoadingSlider != null)
        {
            LoadingSlider.value = 0;
            LoadingSlider = null;
        }

        LoadingText = null;
        ContentText = null;
    }
}