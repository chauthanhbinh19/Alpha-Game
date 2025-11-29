using System.Collections;
using System.Collections.Generic;
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
        TrainButtonPrefab = UIManager.Instance.GetGeneralButton("TrainButtonPrefab");
        TrainPanelPrefab = UIManager.Instance.GetGeneralPanel("TrainPanelPrefab");
    }
    public void CreateTrainPanel()
    {
        GameObject currentObject = Instantiate(TrainPanelPrefab, MainPanel);
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

        var Trains = TrainsService.Create().GetTrains(User.CurrentUserId, 1000, 0);
        CreateTrains(Trains, contentPanel);
    }
    public void CreateTrains(List<Trains> Trains, Transform contentPanel)
    {
        foreach (var Train in Trains)
        {
            GameObject TrainObject = Instantiate(TrainButtonPrefab, contentPanel);

            TextMeshProUGUI Title = TrainObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = Train.Name.Replace("_", " ");

            RawImage Image = TrainObject.transform.Find("MainImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Train.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = TrainObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(Train, MainPanel);
            });

            RawImage rareImage = TrainObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Train.Rare}");
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
