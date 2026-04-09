using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CitiesController : MonoBehaviour
{
    public static CitiesController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CityButtonPrefab;
    private GameObject CityPanelPrefab;
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
        CityButtonPrefab = UIManager.Instance.Get("CityButtonPrefab");
        CityPanelPrefab = UIManager.Instance.Get("CityPanelPrefab");
    }
    public async Task CreateCityPanelAsync()
    {
        GameObject currentObject = Instantiate(CityPanelPrefab, MainPanel);
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

        var Cities = await CitiesService.Create().GetCitiesAsync(User.CurrentUserId, 1000, 0);
        CreateCity(Cities, contentPanel);
    }
    public void CreateCity(List<Cities> cities, Transform contentPanel)
    {
        foreach (var city in cities)
        {
            GameObject cityObject = Instantiate(CityButtonPrefab, contentPanel);

            TextMeshProUGUI titleText = cityObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = city.Name.Replace("_", " ");

            RawImage image = cityObject.transform.Find("MainImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(city.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            Button button = cityObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(city, MainPanel);
            });

            RawImage rareImage = cityObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{city.Rare}");
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
