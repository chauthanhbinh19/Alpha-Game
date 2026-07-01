using UnityEngine;
using UnityEngine.UI;

public class TargetUI : MonoBehaviour
{
    public static TargetUI Instance { get; private set; }

    [Header("UI Components")]
    public GameObject damageTargetPanel;
    public GameObject healTargetPanel;
    public GameObject buffTargetPanel;
    public GameObject debuffTargetPanel;
    public GameObject cleanseTargetPanel;
    public GameObject summonTagretPanel;

    void Awake()
    {
        Instance = this;
        if (damageTargetPanel != null) damageTargetPanel.SetActive(false);
        if (healTargetPanel != null) healTargetPanel.SetActive(false);
        if (buffTargetPanel != null) buffTargetPanel.SetActive(false);
        if (debuffTargetPanel != null) debuffTargetPanel.SetActive(false);
        if (cleanseTargetPanel != null) cleanseTargetPanel.SetActive(false);
        if (summonTagretPanel != null) summonTagretPanel.SetActive(false);
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
        if (damageTargetPanel != null)
        {
            damageTargetPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void ShowHealTargetPanelUI(string message = "")
    {
        if (healTargetPanel != null)
        {
            healTargetPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void ShowBuffTargetPanelUI(string message = "")
    {
        if (buffTargetPanel != null)
        {
            buffTargetPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void ShowDebuffTargetPanelUI(string message = "")
    {
        if (debuffTargetPanel != null)
        {
            debuffTargetPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void ShowCleanseTargetPanelUI(string message = "")
    {
        if (cleanseTargetPanel != null)
        {
            cleanseTargetPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void ShowSummonTargetPanelUI(string message = "")
    {
        if (summonTagretPanel != null)
        {
            summonTagretPanel.SetActive(true);
            // Debug.Log($"[Warning UI] Hiển thị cảnh báo di chuyển: {message}");
        }
    }

    public void HideDamageTargetPanelUI()
    {
        if (damageTargetPanel != null) damageTargetPanel.SetActive(false);
    }

    public void HideHealTargetPanelUI()
    {
        if (healTargetPanel != null) healTargetPanel.SetActive(false);
    }

    public void HideBuffTargetPanelUI()
    {
        if (buffTargetPanel != null) buffTargetPanel.SetActive(false);
    }

    public void HideDebuffTargetPanelUI()
    {
        if (debuffTargetPanel != null) debuffTargetPanel.SetActive(false);
    }

    public void HideCleanseTargetPanelUI()
    {
        if (cleanseTargetPanel != null) cleanseTargetPanel.SetActive(false);
    }

    public void HideSummonTargetPanelUI()
    {
        if (summonTagretPanel != null) summonTagretPanel.SetActive(false);
    }
}