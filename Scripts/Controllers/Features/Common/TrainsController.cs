using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrainsController : MonoBehaviour
{
    public static TrainsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject TrainButtonPrefab;
    private GameObject TrainPanelPrefab;
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
        TrainButtonPrefab = UIManager.Instance.Get("TrainButtonPrefab");
        TrainPanelPrefab = UIManager.Instance.Get("TrainPanelPrefab");
    }
    public async Task CreateTrainPanelAsync()
    {
        GameObject currentObject = Instantiate(TrainPanelPrefab, MainPanel);
        Transform contentPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        Button closeButton = currentObject.transform.Find("CloseButton").GetComponent<Button>();
        Button homeButton = currentObject.transform.Find("HomeButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        var Trains = await TrainsService.Create().GetTrainsAsync(User.CurrentUserId, 1000, 0);
        CreateTrains(Trains, contentPanel);
    }
    public void CreateTrains(List<Trains> trains, Transform contentPanel)
    {
        foreach (var train in trains)
        {
            GameObject trainObject = Instantiate(TrainButtonPrefab, contentPanel);

            TextMeshProUGUI titleText = trainObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = train.Name.Replace("_", " ");

            RawImage image = trainObject.transform.Find("MainImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(train.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            Button button = trainObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(train, MainPanel);
            });

            RawImage rareImage = trainObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{train.Rare}");
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
