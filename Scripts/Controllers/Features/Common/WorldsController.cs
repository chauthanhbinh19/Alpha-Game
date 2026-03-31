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

        var worlds = await WorldsService.Create().GetWorldsAsync(User.CurrentUserId, 1000, 0);
        CreateWorlds(worlds, contentPanel);
    }
    public void CreateWorlds(List<Worlds> Worlds, Transform contentPanel)
    {
        foreach (var world in Worlds)
        {
            GameObject WorldObject = Instantiate(WorldButtonPrefab, contentPanel);

            TextMeshProUGUI Title = WorldObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = world.Name.Replace("_", " ");

            RawImage Image = WorldObject.transform.Find("MainImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(world.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            Image.texture = texture;

            if (world.Status.Equals("block"))
            {
                Image.color = Color.gray;
            }
            else
            {
                Image.color = Color.white;
            }

            Button button = WorldObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(world, MainPanel);
            });

            RawImage rareImage = WorldObject.transform.Find("Rare").GetComponent<RawImage>();
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
