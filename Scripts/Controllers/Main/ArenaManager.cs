using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArenaManager : MonoBehaviour
{
    private Transform MainPanel;
    private GameObject ArenaButtonPrefab;
    private GameObject ArenaDetailsPanelPrefab;
    private GameObject ArenaSlotPrefab;
    private GameObject currentObject;
    private Texture2D itemBackground;
    public static ArenaManager Instance { get; private set; }
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
        ArenaButtonPrefab = UIManager.Instance.Get("ArenaButtonPrefab");
        ArenaDetailsPanelPrefab = UIManager.Instance.Get("ArenaDetailPanelPrefab");
        ArenaSlotPrefab = UIManager.Instance.Get("ArenaSlotPrefab");
    }
    public async Task CreateArenaButtonAsync(Transform arenaMenuPanel)
    {

        var uniqueMode = await ArenaService.Create().GetUniqueTypesAsync();
        foreach (var type in uniqueMode)
        {
            CreateArenaButtonUI(type, itemBackground, TextureHelper.LoadTexture2DCached($"UI/Button/Arena/{type}"), arenaMenuPanel);
        }
        CreateArenaButton(arenaMenuPanel);
        arenaMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateArenaButtonUI(string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ArenaButtonPrefab, panel);
        newButton.name = itemName;

        // Gán màu cho itemBackground
        // RawImage  background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        // if (background != null && itemBackground != null)
        // {
        //     background.texture = itemBackground;
        // }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("Image").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = itemName;
        }
    }
    public void CreateArenaButton(Transform arenaMenuPanel)
    {
        Button[] buttons = arenaMenuPanel.gameObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            string buttonName = button.name; // Lưu lại giá trị cục bộ để tránh lỗi closure
            button.onClick.AddListener(async() => { 
                await CreateArenaDetailsAsync(buttonName); 
            });
        }
    }
    public async Task CreateArenaDetailsAsync(string type)
    {
        currentObject = Instantiate(ArenaDetailsPanelPrefab, MainPanel);
        RawImage avatarImage = currentObject.transform.Find("DictionaryCards/AvatarImage").GetComponent<RawImage>();
        RawImage borderImage = currentObject.transform.Find("DictionaryCards/BorderImage").GetComponent<RawImage>();
        Transform arenaSlotGroup = currentObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        TextMeshProUGUI rankText = currentObject.transform.Find("DictionaryCards/RankText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI rankPointText = currentObject.transform.Find("DictionaryCards/RankPointText").GetComponent<TextMeshProUGUI>();
        Texture avatarTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(User.CurrentUserAvatar)}");
        Texture borderTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(User.CurrentUserBorder)}");
        avatarImage.texture = avatarTexture;
        borderImage.texture = borderTexture;
        Button closeButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button homeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Close(MainPanel);
            await HomeManager.Instance.CreateHomePanelAsync();
        });
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });

        // Arena arena = new Arena();
        IArenaRepository arenaRepository = new ArenaRepository();
        ArenaService arenaService = new ArenaService(arenaRepository);
        string arena_id = await arenaService.GetArenaModeIdAsync(type);
        int rank_point = await arenaService.GetArenaParticipantPointAsync(User.CurrentUserId, arena_id);
        rankPointText.text = rank_point.ToString();

        //Lấy xếp hạng của người chơi
        var rankings = await arenaService.GetArenaParticipantByRankingAsync(arena_id);
        foreach (var pair in rankings)
        {
            if (pair.Key.Equals(User.CurrentUserId))
            {
                // Nếu là người chơi hiện tại, hiển thị thông tin cá nhân
                avatarImage.texture = avatarTexture;
                borderImage.texture = borderTexture;
                rankText.text = pair.Value.ToString();
            }
            else if (pair.Key.Equals("0")) // Trường hợp không có người chơi nào
            {
                rankText.text = pair.Value.ToString();
            }
            else
            {
                GameObject arenaSlotObject = Instantiate(ArenaSlotPrefab, arenaSlotGroup);

                IUserRepository userRepository = new UserRepository();
                UserService userService = new UserService(userRepository);
                User user = await userService.GetUserByIdAsync(pair.Key.ToString());
                
                RawImage arenaAvatarImage = arenaSlotObject.transform.Find("AvatarImage").GetComponent<RawImage>();
                RawImage arenaBorderImage = arenaSlotObject.transform.Find("BorderImage").GetComponent<RawImage>();
                Texture arenaAvatarTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(User.CurrentUserAvatar)}");
                Texture arenaBorderTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(User.CurrentUserBorder)}");
                arenaAvatarImage.texture = arenaAvatarTexture;
                arenaBorderImage.texture = arenaBorderTexture;
                TextMeshProUGUI arenaRankText = arenaSlotObject.transform.Find("RankText").GetComponent<TextMeshProUGUI>();
                arenaRankText.text = pair.Value.ToString();
                TextMeshProUGUI arenaTitleText = arenaSlotObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                arenaTitleText.text = user.Name;
                TextMeshProUGUI arenaLevelText = arenaSlotObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
                arenaLevelText.text = user.Level.ToString();
                TextMeshProUGUI arenaPowerText = arenaSlotObject.transform.Find("PowerText").GetComponent<TextMeshProUGUI>();
                arenaPowerText.text = user.Power.ToString();
            }
        }
    }
    public void Close(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
