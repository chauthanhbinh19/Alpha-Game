using System.Collections;
using UnityEngine;

public class MailManager : MonoBehaviour
{
    public static MailManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject MailPanelPrefab;
    private GameObject MailTabButtonPrefab;
    private GameObject MailButtonPrefab;
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
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        MailPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.General.MAIL_PANEL_PREFAB);
        MailTabButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.MAIL_TAB_BUTTON_PREFAB);
        MailButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.MAIL_BUTTON_PREFAB);
    }
}