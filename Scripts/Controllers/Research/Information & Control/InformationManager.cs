using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationManager : MonoBehaviour
{
    public static InformationManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ResearchPanelPrefab;
    private GameObject ResearchButtonPrefab;
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ResearchPanelPrefab = UIManager.Instance.Get("ResearchPanelPrefab");
        ResearchButtonPrefab = UIManager.Instance.Get("ResearchButtonPrefab");
    }
}