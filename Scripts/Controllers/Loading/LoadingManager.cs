using System.Collections; // Cần dùng cho Coroutine
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
    private TextMeshProUGUI TimeText;

    // Các biến phục vụ cho bộ đếm thời gian
    private Coroutine timerCoroutine;
    private float elapsedTime;

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
            UnityEngine.Debug.LogWarning("[LoadingManager] LoadingProcessPanelPrefab hoặc LoadingPanel chưa được gán.");
            return;
        }

        // Dừng coroutine đếm giờ cũ nếu có để tránh chạy đè luồng
        StopTimer();

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
        
        // Tối ưu hóa: thay thế transform.Find thành GetComponentInChildren hoặc tìm trực tiếp
        LoadingSlider = CurrentLoadingObject.transform.Find("Slider")?.GetComponent<Slider>();
        LoadingText = CurrentLoadingObject.transform.Find("LoadingText")?.GetComponent<TextMeshProUGUI>();
        ContentText = CurrentLoadingObject.transform.Find("ContentText")?.GetComponent<TextMeshProUGUI>();
        TimeText = CurrentLoadingObject.transform.Find("TimeText")?.GetComponent<TextMeshProUGUI>();

        if (LoadingSlider != null)
        {
            LoadingSlider.value = 0;
        }

        // Bắt đầu đếm thời gian từ 0
        StartTimer();
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
        // Dừng đếm thời gian ngay khi kết thúc loading
        StopTimer();

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
        TimeText = null;
    }

    #region Timer Logic

    private void StartTimer()
    {
        elapsedTime = 0f;
        if (TimeText != null)
        {
            TimeText.text = "00:00";
        }
        timerCoroutine = StartCoroutine(UpdateTimerCoroutine());
    }

    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    private IEnumerator UpdateTimerCoroutine()
    {
        // Vòng lặp chạy vô hạn cho đến khi Coroutine bị dừng chủ động bởi HideLoading()
        while (true)
        {
            yield return null; // Đợi sang frame tiếp theo
            elapsedTime += Time.deltaTime;

            if (TimeText != null)
            {
                int minutes = Mathf.FloorToInt(elapsedTime / 60f);
                int seconds = Mathf.FloorToInt(elapsedTime % 60f);
                
                // Định dạng hiển thị luôn đủ 2 chữ số (ví dụ: 01:05 thay vì 1:5)
                TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }

    #endregion
}