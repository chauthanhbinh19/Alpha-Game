using UnityEngine;
using UnityEngine.UI;

public class MovementWarningUI : MonoBehaviour
{
    public static MovementWarningUI Instance { get; private set; }

    [Header("UI Components")]
    public GameObject ActionPanel; // Khung chứa UI cảnh báo
    // public Button closeButton;     // Nút bấm đóng/bỏ qua

    void Awake()
    {
        Instance = this;
        if (ActionPanel != null) ActionPanel.SetActive(false);
    }

    void Start()
    {
        // if (closeButton != null)
        // {
        //     closeButton.onClick.RemoveAllListeners();
        //     closeButton.onClick.AddListener(HideUI);
        // }
    }

    public void ShowUI(string message = "")
    {
        if (ActionPanel != null)
        {
            ActionPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void HideUI()
    {
        if (ActionPanel != null) ActionPanel.SetActive(false);
    }
}