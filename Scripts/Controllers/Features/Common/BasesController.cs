using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasesController : MonoBehaviour
{
    public static BasesController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject BaseButtonPrefab;
    private GameObject BasePanelPrefab;
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
        BaseButtonPrefab = UIManager.Instance.Get("BaseButtonPrefab");
        BasePanelPrefab = UIManager.Instance.Get("BasePanelPrefab");
    }
    public async Task CreateBasePanelAsync()
    {
        GameObject currentObject = Instantiate(BasePanelPrefab, MainPanel);
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

        var Bases = await BasesService.Create().GetBasesAsync(User.CurrentUserId, 1000, 0);
        CreateBases(Bases, contentPanel);
    }
    public void CreateBases(List<Bases> Bases, Transform contentPanel)
    {
        foreach (var Base in Bases)
        {
            GameObject BaseObject = Instantiate(BaseButtonPrefab, contentPanel);

            TextMeshProUGUI Title = BaseObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = Base.Name.Replace("_", " ");

            RawImage Image = BaseObject.transform.Find("MainImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Base.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = BaseObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(Base, MainPanel);
            });

            RawImage rareImage = BaseObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Base.Rare}");
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
