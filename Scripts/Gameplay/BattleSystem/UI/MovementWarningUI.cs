using UnityEngine;
using UnityEngine.UI;

public class MovementWarningUI : MonoBehaviour
{
    public static MovementWarningUI Instance { get; private set; }

    [Header("UI Components")]
    public GameObject actionPanel; // Khung chứa UI cảnh báo
    // public Button closeButton;     // Nút bấm đóng/bỏ qua

    void Awake()
    {
        Instance = this;
        if (actionPanel != null) actionPanel.SetActive(false);
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
        if (actionPanel != null)
        {
            actionPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void HideUI()
    {
        if (actionPanel != null) actionPanel.SetActive(false);
    }
}