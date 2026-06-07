using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimeManager : MonoBehaviour
{
    public static AnimeManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject AnimePanelPrefab;
    private GameObject AnimeButtonPrefab;
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
        AnimePanelPrefab = UIManager.Instance.Get("AnimePanelPrefab");
        AnimeButtonPrefab = UIManager.Instance.Get("AnimeButtonPrefab");
    }
    public void CreateAnime()
    {
        GameObject currentObject = Instantiate(AnimePanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("AnimeContent/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UNIVERSE);
        TextMeshProUGUI titleText2 = transform.Find("AnimeContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.MainType.UNIVERSE);

        CreateAnimeButtonUI(1, AppDisplayConstants.Anime.BLACK_CLOVER, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.BLACK_CLOVER_URL), contentPanel);
        CreateAnimeButtonUI(2, AppDisplayConstants.Anime.BLEACH, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.BLEACH_URL), contentPanel);
        CreateAnimeButtonUI(3, AppDisplayConstants.Anime.DEMON_SLAYER, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.DEMON_SLAYER_URL), contentPanel);
        CreateAnimeButtonUI(4, AppDisplayConstants.Anime.DRAGON_BALL, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.DRAGON_BALL_URL), contentPanel);
        CreateAnimeButtonUI(5, AppDisplayConstants.Anime.FAIRY_TAIL, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.FAIRY_TAIL_URL), contentPanel);
        CreateAnimeButtonUI(6, AppDisplayConstants.Anime.HUNTER_X_HUNTER, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.HUNTER_X_HUNTER_URL), contentPanel);
        CreateAnimeButtonUI(7, AppDisplayConstants.Anime.JUJUTSU_KAISEN, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.JUJUTSU_KAISEN_URL), contentPanel);
        CreateAnimeButtonUI(8, AppDisplayConstants.Anime.NARUTO, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.NARUTO_URL), contentPanel);
        CreateAnimeButtonUI(9, AppDisplayConstants.Anime.ONE_PIECE, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.ONE_PIECE_URL), contentPanel);
        CreateAnimeButtonUI(10, AppDisplayConstants.Anime.ONE_PUNCH_MAN, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.ONE_PUNCH_MAN_URL), contentPanel);
        CreateAnimeButtonUI(11, AppDisplayConstants.Anime.SWORD_ART_ONLINE, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.SWORD_ART_ONLINE_URL), contentPanel);

        CreateAnimeButtonEvent(contentPanel);
    }
    private void CreateAnimeButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(AnimeButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        // Gán hình ảnh cho itemImage
        RawImage image = transform.Find("Image").GetComponent<RawImage>();
        if (image != null && _itemImage != null)
        {
            image.texture = _itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void CreateAnimeButtonEvent(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await BlackCloverManager.Instance.CreateAnimeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await BleachManager.Instance.CreateAnimeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await DemonSlayerManager.Instance.CreateAnimeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await DragonBallManager.Instance.CreateAnimeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await FairyTailManager.Instance.CreateAnimeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await HunterXHunterManager.Instance.CreateAnimeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await JujutsuKaisenManager.Instance.CreateAnimeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await NarutoManager.Instance.CreateAnimeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await OnePieceManager.Instance.CreateAnimeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await OnePunchManManager.Instance.CreateAnimeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_11", panel, async () => await SwordArtOnlineManager.Instance.CreateAnimeManagerAsync());
    }
}
