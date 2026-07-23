using System.Collections;
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

    // Bộ đếm thời gian
    private Coroutine TimerCoroutine;
    private float ElapsedTime;

    // --- LOGIC ANIMATION SLIDER ---
    private Coroutine SliderAnimationCoroutine;
    private float TargetProgress = 0f; // Giá trị logic hướng tới
    private float CurrentDisplayProgress = 0f; // Giá trị UI đang hiển thị thực tế
    
    [Header("Cấu hình tốc độ")]
    [Tooltip("Tốc độ đuổi theo giá trị mới khi SetProgress được gọi (Càng cao càng nhanh)")]
    public float CatchUpSpeed = 5f; 
    [Tooltip("Tốc độ tự động nhích từ từ khi chờ đợi để đạt tối đa 99% (Càng cao càng nhanh)")]
    public float IdleCreepSpeed = 0.02f; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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

        StopTimer();
        StopSliderAnimation();

        // Xóa UI cũ
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
        TimeText = CurrentLoadingObject.transform.Find("TimeText")?.GetComponent<TextMeshProUGUI>();

        // Reset các giá trị tiến trình về 0
        TargetProgress = 0f;
        CurrentDisplayProgress = 0f;
        if (LoadingSlider != null)
        {
            LoadingSlider.value = 0f;
        }

        StartTimer();
        
        // Kích hoạt luồng chạy Animation cho Slider
        SliderAnimationCoroutine = StartCoroutine(AnimateSliderCoroutine());
    }

    /// <summary>
    /// Cập nhật tiến trình. Animation sẽ lập tức nhảy tới giá trị này và tiếp tục tự chạy.
    /// </summary>
    /// <param name="value">Giá trị từ 0.0 đến 1.0</param>
    public void SetProgress(float value, string percentText = "", string loadingContent = "")
    {
        // Giới hạn giá trị truyền vào luôn nằm trong khoảng [0, 1]
        value = Mathf.Clamp01(value);

        // Bẻ bánh lái: Đặt giá trị hiển thị hiện tại và đích đến bằng chính giá trị mới nhận
        CurrentDisplayProgress = value;
        TargetProgress = value;

        if (LoadingSlider != null)
        {
            LoadingSlider.value = CurrentDisplayProgress;
        }

        // Cập nhật text nội dung ngay lập tức
        if (ContentText != null)
        {
            ContentText.text = $"Loading {percentText} {loadingContent}...";
        }
        
        // Cập nhật text phần trăm dựa trên giá trị thực tế đang hiển thị trên UI
        UpdatePercentText(CurrentDisplayProgress);
    }

    public void HideLoading()
    {
        StopTimer();
        StopSliderAnimation();

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

    #region Slider Animation Logic

    private void StopSliderAnimation()
    {
        if (SliderAnimationCoroutine != null)
        {
            StopCoroutine(SliderAnimationCoroutine);
            SliderAnimationCoroutine = null;
        }
    }

    private IEnumerator AnimateSliderCoroutine()
    {
        while (true)
        {
            yield return null;

            // Nếu logic đã báo hoàn thành tuyệt đối (100% tức là 1.0)
            if (TargetProgress >= 1f)
            {
                // Đuổi nhanh về đích 100%
                CurrentDisplayProgress = Mathf.MoveTowards(CurrentDisplayProgress, 1f, Time.deltaTime * CatchUpSpeed);
            }
            else
            {
                // Nếu chưa đạt 100%, tự động nhích từ từ (Creep) nhưng GIỚI HẠN tối đa là 99% (0.99f)
                CurrentDisplayProgress += Time.deltaTime * IdleCreepSpeed;
                if (CurrentDisplayProgress > 0.99f)
                {
                    CurrentDisplayProgress = 0.99f;
                }
            }

            // Cập nhật thanh Slider và Text hiển thị tương ứng
            if (LoadingSlider != null)
            {
                LoadingSlider.value = CurrentDisplayProgress;
            }
            UpdatePercentText(CurrentDisplayProgress);
        }
    }

    private void UpdatePercentText(float progressValue)
    {
        if (LoadingText != null)
        {
            // Làm tròn xuống để tránh việc 99.6% bị làm tròn lên thành 100% khi chưa được phép
            int percent = Mathf.FloorToInt(progressValue * 100f);
            
            // Khóa bảo hiểm: Nếu chưa chạm target 1.0 thì chữ không bao giờ được hiển thị 100%
            if (TargetProgress < 1f && percent >= 100)
            {
                percent = 99;
            }
            
            LoadingText.text = $"{percent}%";
        }
    }

    #endregion

    #region Timer Logic

    private void StartTimer()
    {
        ElapsedTime = 0f;
        if (TimeText != null)
        {
            TimeText.text = "00:00";
        }
        TimerCoroutine = StartCoroutine(UpdateTimerCoroutine());
    }

    private void StopTimer()
    {
        if (TimerCoroutine != null)
        {
            StopCoroutine(TimerCoroutine);
            TimerCoroutine = null;
        }
    }

    private IEnumerator UpdateTimerCoroutine()
    {
        while (true)
        {
            yield return null;
            ElapsedTime += Time.deltaTime;

            if (TimeText != null)
            {
                int minutes = Mathf.FloorToInt(ElapsedTime / 60f);
                int seconds = Mathf.FloorToInt(ElapsedTime % 60f);
                TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }

    #endregion
}