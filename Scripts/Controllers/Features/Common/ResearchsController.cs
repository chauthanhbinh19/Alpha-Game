using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResearchsController : MonoBehaviour
{
    public static ResearchsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ResearchButtonPrefab;
    private GameObject ResearchPanelPrefab;
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
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ResearchButtonPrefab = UIManager.Instance.Get("ResearchButtonPrefab");
        ResearchPanelPrefab = UIManager.Instance.GetGeneralPanel("ResearchPanelPrefab");
    }
    public async Task CreateResearchPanelAsync()
    {
        GameObject currentObject = Instantiate(ResearchPanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        Button CloseButton = currentObject.transform.Find("CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("HomeButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        var Researchs = await ResearchsService.Create().GetResearchsAsync(User.CurrentUserId, 1000, 0);
        CreateResearchs(Researchs, contentPanel);
    }
    public void CreateResearchs(List<Researchs> Researchs, Transform contentPanel)
    {
        foreach (var Research in Researchs)
        {
            GameObject ResearchObject = Instantiate(ResearchButtonPrefab, contentPanel);

            TextMeshProUGUI Title = ResearchObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = Research.Name.Replace("_", " ");

            RawImage Image = ResearchObject.transform.Find("MainImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Research.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = ResearchObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(Research, MainPanel);
            });

            RawImage rareImage = ResearchObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Research.Rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(400, 520);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
