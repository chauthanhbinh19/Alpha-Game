using UnityEngine;
using UnityEngine.UI;

public class TargetUI : MonoBehaviour
{
    public static TargetUI Instance { get; private set; }

    [Header("UI Components")]
    public GameObject DamageTargetPanel;
    public GameObject HealTargetPanel;
    public GameObject BuffTargetPanel;
    public GameObject DebuffTargetPanel;
    public GameObject CleanseTargetPanel;
    public GameObject SummonTagretPanel;

    void Awake()
    {
        Instance = this;
        if (DamageTargetPanel != null) DamageTargetPanel.SetActive(false);
        if (HealTargetPanel != null) HealTargetPanel.SetActive(false);
        if (BuffTargetPanel != null) BuffTargetPanel.SetActive(false);
        if (DebuffTargetPanel != null) DebuffTargetPanel.SetActive(false);
        if (CleanseTargetPanel != null) CleanseTargetPanel.SetActive(false);
        if (SummonTagretPanel != null) SummonTagretPanel.SetActive(false);
    }

    void Start()
    {
        // if (closeButton != null)
        // {
        //     closeButton.onClick.RemoveAllListeners();
        //     closeButton.onClick.AddListener(HideUI);
        // }
    }

    public void ShowDamageTargetPanelUI(string message = "")
    {
        if (DamageTargetPanel != null)
        {
            DamageTargetPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void ShowHealTargetPanelUI(string message = "")
    {
        if (HealTargetPanel != null)
        {
            HealTargetPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void ShowBuffTargetPanelUI(string message = "")
    {
        if (BuffTargetPanel != null)
        {
            BuffTargetPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void ShowDebuffTargetPanelUI(string message = "")
    {
        if (DebuffTargetPanel != null)
        {
            DebuffTargetPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void ShowCleanseTargetPanelUI(string message = "")
    {
        if (CleanseTargetPanel != null)
        {
            CleanseTargetPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void ShowSummonTargetPanelUI(string message = "")
    {
        if (SummonTagretPanel != null)
        {
            SummonTagretPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void HideDamageTargetPanelUI()
    {
        if (DamageTargetPanel != null) DamageTargetPanel.SetActive(false);
    }

    public void HideHealTargetPanelUI()
    {
        if (HealTargetPanel != null) HealTargetPanel.SetActive(false);
    }

    public void HideBuffTargetPanelUI()
    {
        if (BuffTargetPanel != null) BuffTargetPanel.SetActive(false);
    }

    public void HideDebuffTargetPanelUI()
    {
        if (DebuffTargetPanel != null) DebuffTargetPanel.SetActive(false);
    }

    public void HideCleanseTargetPanelUI()
    {
        if (CleanseTargetPanel != null) CleanseTargetPanel.SetActive(false);
    }

    public void HideSummonTargetPanelUI()
    {
        if (SummonTagretPanel != null) SummonTagretPanel.SetActive(false);
    }
}