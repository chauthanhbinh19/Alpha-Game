using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HousingManager : MonoBehaviour
{
    public static HousingManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ResearchPanelPrefab;
    private GameObject ResearchButtonPrefab;
    private GameObject PopupResearchPanelPrefab;
    private GameObject PopupResearchButtonPrefab;
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
        PopupResearchPanelPrefab = UIManager.Instance.Get("PopupResearchPanelPrefab");
        PopupResearchButtonPrefab = UIManager.Instance.Get("PopupResearchButtonPrefab");
    }
    public async Task CreateHousingManagerAsync()
    {
        GameObject currentObject = Instantiate(PopupResearchPanelPrefab, MainPanel);
        Transform content = currentObject.transform.Find("Scroll View/Viewport/Content");
        Dictionary<string, Features> uniqueTypes = new Dictionary<string, Features>();
        uniqueTypes = await FeaturesService.Create().GetFeaturesByTypeAsync(AppConstants.Research.HOUSING);
        uniqueTypes = uniqueTypes
            .OrderBy(kvp =>
            {
                var match = Regex.Match(kvp.Value.FeatureName, @"\d+$");
                return match.Success ? int.Parse(match.Value) : 0;
            })
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        if (uniqueTypes.Count > 0)
        {
            int index = 1;
            foreach (var kvp in uniqueTypes)
            {
                // Tạo một nút mới từ prefab
                string subtype = kvp.Key;
                int requiredLevel = kvp.Value.RequiredLevel;
                GameObject button = Instantiate(PopupResearchButtonPrefab, content);

                TextMeshProUGUI buttonText = button.transform.Find("ContentText").GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = subtype.Replace("_", " ");

                RawImage image = button.transform.Find("Image").GetComponent<RawImage>();
                Texture texture = Resources.Load<Texture2D>(ImageConstants.Research.HOUSING_URL);
                if (image != null && texture != null)
                {
                    image.texture = texture;
                }

                TextMeshProUGUI quantityText = button.transform.Find("QuantityText").GetComponentInChildren<TextMeshProUGUI>();
                quantityText.text = index.ToString();

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    // await OnButtonClickAsync(button, data, kvp.Value, requiredLevel);
                });

                index = index + 1;
            }
        }
    }
}