using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }
    private GameObject notificationPrefab; // Prefab chứa thông báo
    private Transform notificationPanel; 
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
    void Start(){
        notificationPanel=UIManager.Instance.GetTransform("notificationPanel");
        notificationPrefab=UIManager.Instance.GetGameObject("notificationPrefab");
    }
    public void ShowNotification(string message)
    {
        // Tạo thông báo từ prefab
        GameObject notification = Instantiate(notificationPrefab, notificationPanel);
        
        // Đặt thông báo ở giữa màn hình
        RectTransform rectTransform = notification.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero; // Vị trí giữa màn hình (0, 0)

        // Đặt nội dung thông báo
        TextMeshProUGUI notificationText = notification.GetComponent<TextMeshProUGUI>();
        notificationText.text = message;

        // Bắt đầu hiệu ứng bay lên và mờ dần
        StartCoroutine(FadeAndDestroy(notification));
    }

    private IEnumerator FadeAndDestroy(GameObject notification)
    {
        float duration = 1.5f; // Thời gian hiệu ứng
        float elapsed = 0f;

        // Lấy vị trí ban đầu
        RectTransform rectTransform = notification.GetComponent<RectTransform>();
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 endPosition = startPosition + new Vector2(0, 50); // Bay lên 50px

        // Lấy thành phần TextMeshProUGUI để làm mờ
        TextMeshProUGUI notificationText = notification.GetComponent<TextMeshProUGUI>();
        Color startColor = notificationText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Di chuyển lên
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);

            // Làm mờ
            notificationText.color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }

        // Hủy thông báo sau khi hoàn tất
        Destroy(notification);
    }
}
