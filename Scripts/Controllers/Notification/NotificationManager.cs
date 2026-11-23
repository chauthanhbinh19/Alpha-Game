using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    [Header("Toast Settings")]
    [SerializeField] private int maxVisibleToasts = 3;       // Số thông báo tối đa hiện cùng lúc trên Horizontal Layout
    [SerializeField] private float baseDuration = 3.5f;      // Thời gian hiển thị chuẩn (khi chỉ có 1 thông báo)
    [SerializeField] private float minDuration = 0.8f;       // Thời gian hiển thị tối thiểu (khi spam quá nhiều)
    [SerializeField] private float fadeDuration = 0.25f;     // Thời gian hiệu ứng mờ dần khi tắt

    private GameObject NotificationPrefab;
    private GameObject SuccessNotificationPrefab;
    private GameObject ErrorNotificationPrefab;
    private GameObject WarningNotificationPrefab;
    private GameObject InformationNotificationPrefab;
    private Transform NotificationPanel;
    private Transform RightLayoutNotificationPanel;
    private Transform LeftLayoutNotificationPanel;

    // Cấu trúc dữ liệu cho hàng đợi
    private enum ToastType { Success, Error, Warning, Information }
    private struct ToastRequest
    {
        public ToastType Type;
        public string Message;
    }

    private readonly Queue<ToastRequest> _toastQueue = new Queue<ToastRequest>();
    private readonly List<GameObject> _activeToasts = new List<GameObject>();

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

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        NotificationPanel = UIManager.Instance.GetTransform(AppConstants.Transform.NOTIFICATION_PANEL);
        NotificationPrefab = UIManager.Instance.Get(PrefabConstants.General.NOTIFICATION_PREFAB);
        SuccessNotificationPrefab = UIManager.Instance.Get(PrefabConstants.Notification.SUCCESS_NOTIFICATION_PREFAB);
        ErrorNotificationPrefab = UIManager.Instance.Get(PrefabConstants.Notification.ERROR_NOTIFICATION_PREFAB);
        WarningNotificationPrefab = UIManager.Instance.Get(PrefabConstants.Notification.WARNING_NOTIFICATION_PREFAB);
        InformationNotificationPrefab = UIManager.Instance.Get(PrefabConstants.Notification.INFORMATION_NOTIFICATION_PREFAB);

        if (NotificationPanel != null)
        {
            RightLayoutNotificationPanel = NotificationPanel.Find("RightLayoutPanel");
            LeftLayoutNotificationPanel = NotificationPanel.Find("LeftLayoutPanel");
        }
    }

    #region Legacy Floating Text (Giữ nguyên cho thông báo giữa màn hình)
    public void ShowNotification(string message)
    {
        GameObject notification = Instantiate(NotificationPrefab, NotificationPanel);
        RectTransform rectTransform = notification.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;

        TextMeshProUGUI notificationText = notification.GetComponent<TextMeshProUGUI>();
        notificationText.text = message;

        StartCoroutine(FloatingFadeAndDestroy(notification));
    }

    private IEnumerator FloatingFadeAndDestroy(GameObject notification)
    {
        float duration = 1.5f;
        float elapsed = 0f;

        RectTransform rectTransform = notification.GetComponent<RectTransform>();
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 endPosition = startPosition + new Vector2(0, 50);

        TextMeshProUGUI notificationText = notification.GetComponent<TextMeshProUGUI>();
        Color startColor = notificationText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
            notificationText.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        Destroy(notification);
    }
    #endregion

    #region Smart Queue Toast System (Success & Error)
    public void ShowSuccessNotification(string message)
    {
        EnqueueToast(ToastType.Success, message);
    }

    public void ShowErrorNotification(string message)
    {
        EnqueueToast(ToastType.Error, message);
    }

    public void ShowWarningNotification(string message)
    {
        EnqueueToast(ToastType.Warning, message);
    }

    public void ShowInformationNotification(string message)
    {
        EnqueueToast(ToastType.Information, message);
    }

    private void EnqueueToast(ToastType type, string message)
    {
        // Đảm bảo panel đã được cache nếu hàm được gọi sớm trước Start()
        if (RightLayoutNotificationPanel == null && NotificationPanel != null)
        {
            RightLayoutNotificationPanel = NotificationPanel.Find("RightLayoutPanel");
        }

        _toastQueue.Enqueue(new ToastRequest { Type = type, Message = message });
        ProcessQueue();
    }

    private void ProcessQueue()
    {
        // Đẩy thông báo từ hàng đợi ra màn hình cho đến khi đạt giới hạn maxVisibleToasts
        while (_activeToasts.Count < maxVisibleToasts && _toastQueue.Count > 0)
        {
            ToastRequest request = _toastQueue.Dequeue();
            SpawnToast(request);
        }
    }

    private void SpawnToast(ToastRequest request)
    {
        GameObject prefab = request.Type == ToastType.Success 
            ? SuccessNotificationPrefab 
            : ErrorNotificationPrefab;

        GameObject toastObj = Instantiate(prefab, RightLayoutNotificationPanel);
        
        // Đưa thông báo mới nhất lên đầu hoặc cuối tùy hướng Horizontal Layout của bạn
        // toastObj.transform.SetAsFirstSibling(); // Bỏ comment dòng này nếu muốn cái mới đẩy cái cũ sang bên cạnh

        _activeToasts.Add(toastObj);

        // Gán nội dung
        TextMeshProUGUI descriptionText = toastObj.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
        descriptionText.text = request.Message;

        // Đảm bảo có CanvasGroup để fade toàn bộ UI (Background + Icon + Text + Button)
        CanvasGroup canvasGroup = toastObj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = toastObj.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1f;

        // Gắn sự kiện CloseButton
        Button closeButton = toastObj.transform.Find("CloseButton").GetComponent<Button>();
        bool isClosing = false;
        Coroutine autoCloseCoroutine = null;

        // Hàm đóng thông báo dùng chung cho cả Auto-Close và Click CloseButton
        void TriggerClose()
        {
            if (isClosing) return;
            isClosing = true;

            if (autoCloseCoroutine != null)
            {
                StopCoroutine(autoCloseCoroutine);
            }
            StartCoroutine(FadeOutAndRemoveToast(toastObj, canvasGroup));
        }

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(TriggerClose);

        // Chạy bộ đếm thời gian thông minh
        autoCloseCoroutine = StartCoroutine(SmartToastTimer(TriggerClose));
    }

    private IEnumerator SmartToastTimer(System.Action onTimeout)
    {
        float elapsed = 0f;

        while (true)
        {
            // Tính tổng áp lực hàng đợi theo thời gian thực (đang hiện + đang chờ)
            int totalBacklog = _activeToasts.Count + _toastQueue.Count;

            // Công thức giảm thời gian theo cấp số nhân nhẹ:
            // 1 cái = 3.5s | 2 cái = 2.3s | 3 cái = 1.75s | 5 cái = 1.16s | >=8 cái = 0.8s (minDuration)
            float dynamicDuration = Mathf.Max(minDuration, baseDuration / (1f + (totalBacklog - 1) * 0.5f));

            elapsed += Time.deltaTime;
            if (elapsed >= dynamicDuration)
            {
                onTimeout?.Invoke();
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator FadeOutAndRemoveToast(GameObject toastObj, CanvasGroup canvasGroup)
    {
        // Ngắt tương tác click khi đang mờ dần
        canvasGroup.blocksRaycasts = false;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        _activeToasts.Remove(toastObj);
        Destroy(toastObj);

        // Ngay khi 1 ô trống xuất hiện, gọi cái tiếp theo trong Queue ra ngay lập tức
        ProcessQueue();
    }
    #endregion
}