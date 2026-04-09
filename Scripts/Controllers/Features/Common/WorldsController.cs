using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldsController : MonoBehaviour
{
    public static WorldsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject WorldButtonPrefab;
    private GameObject WorldPanelPrefab;
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
        WorldButtonPrefab = UIManager.Instance.Get("WorldButtonPrefab");
        WorldPanelPrefab = UIManager.Instance.Get("WorldPanelPrefab");
    }
    public async Task CreateWorldPanelAsync()
    {
        GameObject currentObject = Instantiate(WorldPanelPrefab, MainPanel);
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

        var worlds = await WorldsService.Create().GetWorldsAsync(User.CurrentUserId, 1000, 0);
        CreateWorlds(worlds, contentPanel);
    }
    public void CreateWorlds(List<Worlds> worlds, Transform contentPanel)
    {
        foreach (var world in worlds)
        {
            GameObject worldObject = Instantiate(WorldButtonPrefab, contentPanel);

            TextMeshProUGUI titleText = worldObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = world.Name.Replace("_", " ");

            RawImage image = worldObject.transform.Find("MainImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(world.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            if (world.Status.Equals("block"))
            {
                image.color = Color.gray;
            }
            else
            {
                image.color = Color.white;
            }

            Button button = worldObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(world, MainPanel);
            });

            RawImage rareImage = worldObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{world.Rare}");
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
