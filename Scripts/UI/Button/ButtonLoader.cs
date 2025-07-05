using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonLoader : MonoBehaviour
{
    private GameObject buttonPrefab; // Prefab của button
    private Transform mainMenuButtonPanel; // Nơi chứa các button trong scene
    private Transform mainMenuCampaignPanel;
    private Transform summonPanel;
    private GameObject TabButton4;
    private GameObject ArenaButtonPrefab;
    private GameObject AnimeButtonPrefab;
    public Transform buttonGroupPanel1;
    public Transform buttonGroupPanel2;
    public Transform buttonGroupPanel3;
    private int set;
    Texture2D backgroundImage;
    Texture2D backgroundImage2;
    // Start is called before the first frame update
    public static ButtonLoader Instance { get; private set; }
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
        buttonPrefab = UIManager.Instance.GetGameObject("buttonPrefab");
        mainMenuButtonPanel = UIManager.Instance.GetTransform("mainMenuButtonPanel");
        mainMenuCampaignPanel = UIManager.Instance.GetTransform("mainMenuCampaignPanel");
        summonPanel = UIManager.Instance.GetTransform("summonPanel");
        TabButton4 = UIManager.Instance.GetGameObject("TabButton4");
        ArenaButtonPrefab = UIManager.Instance.GetGameObject("ArenaButtonPrefab");
        AnimeButtonPrefab = UIManager.Instance.GetGameObject("AnimeButtonPrefab");

        backgroundImage = Resources.Load<Texture2D>($"UI/Background4/Background_V4_58");
        backgroundImage2 = Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder");

        CreateButton(1, "campaigns", Resources.Load<Texture2D>($"UI/Background4/Background_V4_110"), Resources.Load<Texture2D>($"UI/UI/Campaign"), mainMenuCampaignPanel);
        //Main menu
        CreateButton(1, "card_heroes", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Cards"), mainMenuButtonPanel);
        CreateButton(2, "books", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Books"), mainMenuButtonPanel);
        CreateButton(3, "pets", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Pets"), mainMenuButtonPanel);
        CreateButton(4, "card_captains", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Captains"), mainMenuButtonPanel);
        CreateButton(5, "card_colonels", backgroundImage, Resources.Load<Texture2D>($"UI/Button/philosophies_of_conflict"), mainMenuButtonPanel);
        CreateButton(6, "card_generals", backgroundImage, Resources.Load<Texture2D>($"UI/Button/philosophies_of_contention"), mainMenuButtonPanel);
        CreateButton(7, "card_admirals", backgroundImage, Resources.Load<Texture2D>($"UI/Button/philosophies_of_diligence"), mainMenuButtonPanel);
        CreateButton(8, "card_military", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Military"), mainMenuButtonPanel);
        CreateButton(9, "card_spell", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Spell"), mainMenuButtonPanel);
        CreateButton(10, "card_monsters", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Monsters"), mainMenuButtonPanel);
        // CreateButton(13, "equipments",backgroundImage,Resources.Load<Texture2D>($"UI/UI/equipments"), mainMenuButtonPanel);
        CreateButton(11, "bag", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Bag"), mainMenuButtonPanel);
        CreateButton(12, "teams", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Teams"), mainMenuButtonPanel);
        CreateButton(13, "more", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Teams"), mainMenuButtonPanel);


        CreateButton(14, "summon_card_heroes", backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonCards"), summonPanel);
        CreateButton(15, "summon_books", backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonBooks"), summonPanel);
        CreateButton(16, "summon_card_captains", backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonCaptains"), summonPanel);
        CreateButton(17, "summon_card_monsters", backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonMonsters"), summonPanel);
        CreateButton(18, "summon_card_military", backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonMilitary"), summonPanel);
        CreateButton(19, "summon_card_spell", backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonSpell"), summonPanel);
        CreateButton(20, "summon_card_colonels", backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonColonels"), summonPanel);
        CreateButton(21, "summon_card_generals", backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonGenerals"), summonPanel);
        CreateButton(22, "summon_card_admirals", backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonAdmirals"), summonPanel);

        CreateButton(23, "shop", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Shop"), summonPanel);

        CreateButton(24, "gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery"), summonPanel);
        CreateButton(25, "collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection"), summonPanel);
        CreateButton(26, "equipments", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Legend_Equipment"), summonPanel);
        CreateButton(27, "anime", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Lobby_Icon-That_Time_I_Got_Reincarnated_as_a_Slime_Collab"), summonPanel);
        CreateButton(28, "arena", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Arena"), summonPanel);
        CreateButton(29, "guild", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Guild"), summonPanel);
        CreateButton(30, "tower", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Tower"), summonPanel);
        CreateButton(31, "event", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Lobby_Icon-Event_002"), summonPanel);
        CreateButton(32, "daily_checkin", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Daily_Checkin"), summonPanel);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateMoreButton(Transform moreMenuPanel)
    {
        CreateButton(1, "collaboration_equipments", backgroundImage, Resources.Load<Texture2D>($"UI/UI/CollaborationEquipments"), moreMenuPanel);
        CreateButton(2, "collaborations", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Collaboration"), moreMenuPanel);
        CreateButton(3, "medals", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Bag"), moreMenuPanel);
        CreateButton(4, "skills", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Skills"), moreMenuPanel);
        CreateButton(5, "symbols", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Symbols"), moreMenuPanel);
        CreateButton(6, "titles", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Titles"), moreMenuPanel);
        CreateButton(7, "magic_formation_circle", backgroundImage, Resources.Load<Texture2D>($"UI/Button/MagicFormationCircle"), moreMenuPanel);
        CreateButton(8, "relics", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Relics"), moreMenuPanel);
        CreateButton(9, "talisman", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Talisman"), moreMenuPanel);
        CreateButton(10, "puppet", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Puppet"), moreMenuPanel);
        CreateButton(11, "alchemy", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Alchemy"), moreMenuPanel);
        CreateButton(12, "forge", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Forge"), moreMenuPanel);
        CreateButton(13, "card_life", backgroundImage, Resources.Load<Texture2D>($"UI/UI/Life"), moreMenuPanel);
        CreateButton(14, "master_board", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Master_Board"), moreMenuPanel);
    }
    private void CreateButton(int index, string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(buttonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && itemBackground != null)
        {
            background.texture = itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    private void CreateButtonWithName(string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(buttonPrefab, panel);
        newButton.name = itemName;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && itemBackground != null)
        {
            background.texture = itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = itemName.Replace("_", " ");
        }
    }
    public List<string> GetUniqueTypes()
    {
        // if (mainType.Equals("Equipments"))
        // {
        //     return Equipments.GetUniqueEquipmentsTypes();
        // }
        var equipment = EquipmentsService.Create();
        return equipment.GetUniqueEquipmentsTypes();
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
    private void CreateAnimeButtonUI(string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(AnimeButtonPrefab, panel);
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

        //Tạo animation cho border image
        RawImage borderImage = newButton.transform.Find("BorderImage").GetComponent<RawImage>();
        // Gán script RotateUI
        borderImage.gameObject.AddComponent<RotateAnimation>();
    }
    public void CreateGalleryButton(Transform galleryMenuPanel)
    {
        //Gallery menu
        CreateButton(1, "card_heroes_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/CardsGallery"), galleryMenuPanel);
        CreateButton(2, "books_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/BooksGallery"), galleryMenuPanel);
        CreateButton(3, "pets_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/PetsGallery"), galleryMenuPanel);
        CreateButton(4, "card_captains_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/CaptainsGallery"), galleryMenuPanel);
        CreateButton(5, "collaboration_equipments_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsGallery"), galleryMenuPanel);
        CreateButton(6, "card_military_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/MilitaryGallery"), galleryMenuPanel);
        CreateButton(7, "card_spell_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/SpellGallery"), galleryMenuPanel);
        CreateButton(8, "collaborations_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/CollaborationsGallery"), galleryMenuPanel);
        CreateButton(9, "card_monsters_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/MonstersGallery"), galleryMenuPanel);
        CreateButton(10, "equipments_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/EquipmentsGallery"), galleryMenuPanel);
        CreateButton(11, "medals_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/MedalsGallery"), galleryMenuPanel);
        CreateButton(12, "skills_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/SkillsGallery"), galleryMenuPanel);
        CreateButton(13, "symbols_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/SymbolsGallery"), galleryMenuPanel);
        CreateButton(14, "titles_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/TitlesGallery"), galleryMenuPanel);
        CreateButton(15, "magic_formation_circle_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/MagicFormationCircleGallery"), galleryMenuPanel);
        CreateButton(16, "relics_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/RelicsGallery"), galleryMenuPanel);
        CreateButton(17, "card_colonels_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/teachings_of_conflict"), galleryMenuPanel);
        CreateButton(18, "card_generals_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/teachings_of_contention"), galleryMenuPanel);
        CreateButton(19, "card_admirals_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/teachings_of_diligence"), galleryMenuPanel);
        CreateButton(20, "borders_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/BorderGallery"), galleryMenuPanel);
        CreateButton(21, "talisman_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/TalismanGallery"), galleryMenuPanel);
        CreateButton(22, "puppet_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/PuppetGallery"), galleryMenuPanel);
        CreateButton(23, "alchemy_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/AlchemyGallery"), galleryMenuPanel);
        CreateButton(24, "forge_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/ForgeGallery"), galleryMenuPanel);
        CreateButton(25, "life_gallery", backgroundImage, Resources.Load<Texture2D>($"UI/Button/LifeGallery"), galleryMenuPanel);

        FindAnyObjectByType<GalleryManager>().CreateGallery(galleryMenuPanel);
    }
    public void CreateCollectionButton(Transform collectionMenuPanel)
    {
        //Collection menu
        CreateButton(1, "card_heroes_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/CardsCollection"), collectionMenuPanel);
        CreateButton(2, "books_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/BooksCollection"), collectionMenuPanel);
        CreateButton(3, "pets_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/PetsCollection"), collectionMenuPanel);
        CreateButton(4, "card_captains_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/CaptainsCollection"), collectionMenuPanel);
        CreateButton(5, "collaboration_equipments_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsCollection"), collectionMenuPanel);
        CreateButton(6, "card_military_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/MilitaryCollection"), collectionMenuPanel);
        CreateButton(7, "card_spell_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/SpellCollection"), collectionMenuPanel);
        CreateButton(8, "collaborations_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/CollaborationsCollection"), collectionMenuPanel);
        CreateButton(9, "card_monsters_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/MonstersCollection"), collectionMenuPanel);
        CreateButton(10, "equipments_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/EquipmentsCollection"), collectionMenuPanel);
        CreateButton(11, "medals_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/MedalsCollection"), collectionMenuPanel);
        CreateButton(12, "skills_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/SkillsCollection"), collectionMenuPanel);
        CreateButton(13, "symbols_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/SymbolsCollection"), collectionMenuPanel);
        CreateButton(14, "titles_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/TitlesCollection"), collectionMenuPanel);
        CreateButton(15, "magic_formation_circle_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/MagicFormationCircleCollection"), collectionMenuPanel);
        CreateButton(16, "relics_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/RelicsCollection"), collectionMenuPanel);
        CreateButton(17, "card_colonels_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/guide_to_conflict"), collectionMenuPanel);
        CreateButton(18, "card_generals_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/guide_to_contention"), collectionMenuPanel);
        CreateButton(19, "card_admirals_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/guide_to_diligence"), collectionMenuPanel);
        CreateButton(20, "borders_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/BordersCollection"), collectionMenuPanel);
        CreateButton(21, "talisman_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/TalismanCollection"), collectionMenuPanel);
        CreateButton(22, "puppet_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/PuppetCollection"), collectionMenuPanel);
        CreateButton(23, "alchemy_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/AlchemyCollection"), collectionMenuPanel);
        CreateButton(24, "forge_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/ForgeCollection"), collectionMenuPanel);
        CreateButton(25, "life_collection", backgroundImage, Resources.Load<Texture2D>($"UI/Button/LifeCollection"), collectionMenuPanel);

        FindAnyObjectByType<CollectionManager>().CreateCollection(collectionMenuPanel);
    }
    public void CreateEquipmentsButton(Transform equipmentMenuPanel)
    {
        //Equipment menu
        List<string> uniqueTypes = GetUniqueTypes();
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                string subtype = uniqueTypes[i];
                CreateButtonWithName(subtype, backgroundImage, Resources.Load<Texture2D>($"UI/Button/{subtype}"), equipmentMenuPanel);
            }
        }
        FindAnyObjectByType<EquipmentManager>().CreateEquipments(equipmentMenuPanel);
    }
    public void CreateAnimeButton(Transform animeMenuPanel)
    {
        CreateAnimeButtonUI("one_piece", backgroundImage, Resources.Load<Texture2D>($"UI/Button/One Piece"), animeMenuPanel);
        CreateAnimeButtonUI("naruto", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Naruto"), animeMenuPanel);
        CreateAnimeButtonUI("dragon_ball", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Dragon Ball"), animeMenuPanel);
        CreateAnimeButtonUI("fairy_tail", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Fairy Tail"), animeMenuPanel);
        CreateAnimeButtonUI("sword_art_online", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Sword Art Online"), animeMenuPanel);
        CreateAnimeButtonUI("demon_slayer", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Demon Slayer"), animeMenuPanel);
        CreateAnimeButtonUI("bleach", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Bleach"), animeMenuPanel);
        CreateAnimeButtonUI("jujutsu_kaisen", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Jujutsu Kaisen"), animeMenuPanel);
        CreateAnimeButtonUI("black_clover", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Black Clover"), animeMenuPanel);
        CreateAnimeButtonUI("hunter_x_hunter", backgroundImage, Resources.Load<Texture2D>($"UI/Button/Hunter x Hunter"), animeMenuPanel);
        CreateAnimeButtonUI("one_punch_man", backgroundImage, Resources.Load<Texture2D>($"UI/Button/One Punch Man"), animeMenuPanel);

        FindAnyObjectByType<MainMenuAnimeStatsManager>().CreateAnimeButton(animeMenuPanel);
    }
    public void CreateArenaButton(Transform arenaMenuPanel)
    {

        var uniqueMode = ArenaService.Create().GetUniqueTypes();
        foreach (var type in uniqueMode)
        {
            CreateArenaButtonUI(type, backgroundImage, Resources.Load<Texture2D>($"UI/Button/{type}"), arenaMenuPanel);
        }
        FindAnyObjectByType<ArenaManager>().CreateArenaButton(arenaMenuPanel);
    }
    public void CreateTowerButton(Transform towerMenuPanel)
    {
        // CreateArenaButton(1, "Tower 1",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_1"), towerMenuPanel);
        // CreateArenaButton(2, "Tower 2",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_2"), towerMenuPanel);
        // CreateArenaButton(3, "Tower 3",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_3"), towerMenuPanel);
        // CreateArenaButton(4, "Tower 4",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_4"), towerMenuPanel);
        // CreateArenaButton(5, "Tower 5",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_5"), towerMenuPanel);
        // CreateArenaButton(6, "Tower 6",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_6"), towerMenuPanel);
        // CreateArenaButton(7, "Tower 7",backgroundImage,Resources.Load<Texture2D>($"UI/Button/Tower_7"), towerMenuPanel);
    }
    public void CreateButton(int index, string itemName, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(TabButton4, panel);
        newButton.name = "Button_" + index;

        // Gán tên cho itemName
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = itemName;
        }
    }
    public void OnButtonClicked(string buttonName, Transform tabPanel)
    {
        // Tìm button hiện tại từ RightButtonContent
        Button button = tabPanel.Find(buttonName)?.GetComponent<Button>();
        if (button == null) return;

        // Đổi background các button
        ChangeBackgroundButtonTab(button, tabPanel);
    }
    public void ChangeBackgroundButtonTab(Button clickedButton, Transform tabPanel)
    {
        foreach (Transform child in tabPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                ChangeButtonBackground(button.gameObject, "Background_V4_216");
            }
        }
        // Đổi background cho button được nhấn
        if (clickedButton != null)
        {
            ChangeButtonBackground(clickedButton.gameObject, "Background_V4_241"); // Background clicked
        }
    }
    public void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = Resources.Load<Texture>($"UI/Background4/{image}");
            if (texture != null)
            {
                buttonImage.texture = texture;
            }
            else
            {
                Debug.LogError($"Texture '{image}' not found in Resources.");
            }
        }
        else
        {
            Debug.LogError("Button does not have a RawImage component.");
        }
    }
    public void CreateSetButtonGroup(object data, GameObject buttonPrefab, Transform buttonPanel)
    {
        if (data is CardHeroes cardHeroes || data is CardCaptains cardCaptains ||
        data is CardColonels cardColonels || data is CardGenerals cardGenerals ||
        data is CardAdmirals cardAdmirals || data is CardMonsters cardMonsters ||
        data is CardMilitary cardMilitary || data is CardSpell cardSpell ||
        data is Books books || data is Pets pets || data is Equipments equipments
        )
        {
            int setButtonNumber = 4;
            for (int i = 0; i < setButtonNumber; i++)
            {
                int index = i; // <- tạo biến tạm
                GameObject button = Instantiate(buttonPrefab, buttonPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = (index + 1).ToString();

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    set = index + 1;
                    foreach (Transform child in buttonPanel)
                    {
                        // Lấy component Button từ con cái
                        Button button = child.GetComponent<Button>();
                        if (button != null)
                        {
                            // Gọi hàm ChangeButtonBackground với màu trắng
                            UIManager.Instance.ChangeButtonBackground(button.gameObject, "Background_V4_259"); // Giả sử bạn có texture trắng
                        }
                    }
                    UIManager.Instance.ChangeButtonBackground(btn.gameObject, "Background_V4_332");
                    CreateButtonGroup(data);
                });

                if (index == 0)
                {
                    set = index + 1;
                    UIManager.Instance.ChangeButtonBackground(button, "Background_V4_332");
                    CreateButtonGroup(data);
                }
                else
                {
                    UIManager.Instance.ChangeButtonBackground(button, "Background_V4_259");
                }
            }
        }
    }
    public void CreateButtonGroup(object data)
    {
        if (data is CardHeroes cardHeroes)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardCaptains cardCaptains)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardColonels cardColonels)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardGenerals cardGenerals)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardMonsters cardMonsters)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardMilitary cardMilitary)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardSpell cardSpell)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is Books books)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is Pets pets)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is Equipments equipments)
        {
            CreateButtonGroupDetails(data);
        }
    }
    private void CreateButtonWithBackground(int index, string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        if (panel == null)
        {
            Debug.Log("Panel is null for index: " + index);
            return;
        }
        // Tạo button từ prefab
        GameObject newButton = Instantiate(buttonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && itemBackground != null)
        {
            background.texture = itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = itemName;
        }
    }
    public void CreateButtonGroupDetails(object data)
    {
        ButtonEvent.Instance.Close(buttonGroupPanel1);
        ButtonEvent.Instance.Close(buttonGroupPanel2);
        ButtonEvent.Instance.Close(buttonGroupPanel3);
        if (data is CardHeroes cardHeroes || data is CardCaptains cardCaptains ||
        data is CardColonels cardColonels || data is CardGenerals cardGenerals ||
        data is CardAdmirals cardAdmirals || data is CardMonsters cardMonsters ||
        data is CardMilitary cardMilitary || data is CardSpell cardSpell ||
        data is Books books || data is Pets pets
        )
        {
            if (set == 1)
            {
                CreateButtonWithBackground(1, "Equipments", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Equipments"), buttonGroupPanel1);
                CreateButtonWithBackground(2, "Realm", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Realm"), buttonGroupPanel1);
                CreateButtonWithBackground(3, "Upgrade", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Upgrade"), buttonGroupPanel1);
                CreateButtonWithBackground(4, "Aptitude", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Aptitude"), buttonGroupPanel1);
                CreateButtonWithBackground(5, "Affinity", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Affinity"), buttonGroupPanel1);
                CreateButtonWithBackground(6, "Blessing", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Blessing"), buttonGroupPanel1);
                CreateButtonWithBackground(7, "Core", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Core"), buttonGroupPanel1);
                CreateButtonWithBackground(8, "Physique", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Physique"), buttonGroupPanel1);
                CreateButtonWithBackground(9, "Bloodline", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Bloodline"), buttonGroupPanel1);
                CreateButtonWithBackground(10, "Omnivision", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnivision"), buttonGroupPanel2);
                CreateButtonWithBackground(11, "Omnipotence", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnipotence"), buttonGroupPanel2);
                CreateButtonWithBackground(12, "Omnipresence", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnipresence"), buttonGroupPanel2);
                CreateButtonWithBackground(13, "Omniscience", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omniscience"), buttonGroupPanel2);
                CreateButtonWithBackground(14, "Omnivory", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnivory"), buttonGroupPanel2);
                CreateButtonWithBackground(15, "Angel", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Angel"), buttonGroupPanel2);
                CreateButtonWithBackground(16, "Demon", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Demon"), buttonGroupPanel2);
                CreateButtonWithBackground(17, "Sword", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Sword"), buttonGroupPanel3);
                CreateButtonWithBackground(18, "Spear", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Spear"), buttonGroupPanel3);
                CreateButtonWithBackground(19, "Shield", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Shield"), buttonGroupPanel3);
                CreateButtonWithBackground(20, "Bow", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Bow"), buttonGroupPanel3);
                CreateButtonWithBackground(21, "Gun", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Gun"), buttonGroupPanel3);
                CreateButtonWithBackground(22, "Cyber", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Cyber"), buttonGroupPanel3);
                CreateButtonWithBackground(23, "Fairy", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Fairy"), buttonGroupPanel3);

                ButtonEvent.Instance.AssignButtonEvent("Button_1", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuEquipmentManager>().CreateMainMenuEquipmentManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_2", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuRealmManager>().CreateMainMenuRealmManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_3", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuUpgradeManager>().CreateMainMenuUpgradeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_4", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuAptitudeManager>().CreateMainMenuAptitudeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_5", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuAffinityManager>().CreateMainMenuAffinityManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_6", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuBlessingManager>().CreateMainMenuBlessingManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_7", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuCoreManager>().CreateMainMenuCoreManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_8", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuPhysiqueManager>().CreateMainMenuPhysiqueManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_9", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuBloodlineManager>().CreateMainMenuBloodlineManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_10", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmnivisionManager>().CreateMainMenuOmnivisionManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_11", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmnipotenceManager>().CreateMainMenuOmnipotenceManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_12", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmnipresenceManager>().CreateMainMenuOmnipresenceManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_13", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmniscienceManager>().CreateMainMenuOmniscienceManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_14", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmnivoryManager>().CreateMainMenuOmnivoryManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_15", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuAngelManager>().CreateMainMenuAngelManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_16", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuDemonManager>().CreateMainMenuDemonManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_17", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuSwordManager>().CreateMainMenuSwordManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_18", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuSpearManager>().CreateMainMenuSpearManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_19", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuShieldManager>().CreateMainMenuShieldManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_20", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuBowManager>().CreateMainMenuBowManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_21", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuGunManager>().CreateMainMenuGunManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_22", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuCyberManager>().CreateMainMenuCyberManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_23", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuFairyManager>().CreateMainMenuFairyManager(data);
                });
            }
            else if (set == 2)
            {
                CreateButtonWithBackground(24, "Dark", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Dark"), buttonGroupPanel1);
                CreateButtonWithBackground(25, "Light", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Light"), buttonGroupPanel1);
                CreateButtonWithBackground(26, "Fire", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Fire"), buttonGroupPanel1);
                CreateButtonWithBackground(27, "Ice", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Ice"), buttonGroupPanel1);
                CreateButtonWithBackground(28, "Earth", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Earth"), buttonGroupPanel1);
                CreateButtonWithBackground(29, "Thunder", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Thunder"), buttonGroupPanel1);
                CreateButtonWithBackground(30, "Life", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Life"), buttonGroupPanel1);
                CreateButtonWithBackground(31, "Space", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Space"), buttonGroupPanel1);
                CreateButtonWithBackground(32, "Time", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Time"), buttonGroupPanel1);
                CreateButtonWithBackground(33, "Nanotech", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Nanotech"), buttonGroupPanel2);
                CreateButtonWithBackground(34, "Quantum", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Quantum"), buttonGroupPanel2);
                CreateButtonWithBackground(35, "Holography", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Holography"), buttonGroupPanel2);
                CreateButtonWithBackground(36, "Plasma", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Plasma"), buttonGroupPanel2);
                CreateButtonWithBackground(37, "Biomech", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Biomech"), buttonGroupPanel2);
                CreateButtonWithBackground(38, "Cryotech", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Cryotech"), buttonGroupPanel2);
                CreateButtonWithBackground(39, "Psionics", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Psionics"), buttonGroupPanel2);
                CreateButtonWithBackground(40, "Neurotech", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Neurotech"), buttonGroupPanel3);
                CreateButtonWithBackground(41, "Antimatter", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Antimatter"), buttonGroupPanel3);
                CreateButtonWithBackground(42, "Phantomware", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Phantomware"), buttonGroupPanel3);
                CreateButtonWithBackground(43, "Gravitech", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Gravitech"), buttonGroupPanel3);
                CreateButtonWithBackground(44, "Aethernet", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Aethernet"), buttonGroupPanel3);
                CreateButtonWithBackground(45, "Starforge", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Starforge"), buttonGroupPanel3);
                CreateButtonWithBackground(46, "Orbitalis", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Orbitalis"), buttonGroupPanel3);
                ButtonEvent.Instance.AssignButtonEvent("Button_24", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuDarkManager>().CreateMainMenuDarkManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_25", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuLightManager>().CreateMainMenuLightManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_26", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuFireManager>().CreateMainMenuFireManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_27", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuIceManager>().CreateMainMenuIceManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_28", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuEarthManager>().CreateMainMenuEarthManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_29", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuThunderManager>().CreateMainMenuThunderManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_30", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuLifeManager>().CreateMainMenuLifeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_31", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuSpaceManager>().CreateMainMenuSpaceManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_32", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuTimeManager>().CreateMainMenuTimeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_33", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuNanotechManager>().CreateMainMenuNanotechManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_34", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuQuantumManager>().CreateMainMenuQuantumManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_35", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuHolographyManager>().CreateMainMenuHolographyManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_36", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuPlasmaManager>().CreateMainMenuPlasmaManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_37", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuBiomechManager>().CreateMainMenuBiomechManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_38", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuCryotechManager>().CreateMainMenuCryotechManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_39", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuPsionicsManager>().CreateMainMenuPsionicsManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_40", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuNeurotechManager>().CreateMainMenuNeurotechManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_41", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuAntimatterManager>().CreateMainMenuAntimatterManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_42", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuPhantomwareManager>().CreateMainMenuPhantomwareManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_43", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuGravitechManager>().CreateMainMenuGravitechManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_44", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuAethernetManager>().CreateMainMenuAethernetManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_45", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuStarforgeManager>().CreateMainMenuStarforgeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_46", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuOrbitalisManager>().CreateMainMenuOrbitalisManager(data);
                });
            }
            else if (set == 3)
            {
                CreateButtonWithBackground(47, "Azathoth", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Azathoth"), buttonGroupPanel1);
                CreateButtonWithBackground(48, "Yog-Sothoth", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Yog-Sothoth"), buttonGroupPanel1);
                CreateButtonWithBackground(49, "Nyarlathotep", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Nyarlathotep"), buttonGroupPanel1);
                CreateButtonWithBackground(50, "Shub-Niggurath", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Shub-Niggurath"), buttonGroupPanel1);
                CreateButtonWithBackground(51, "Nihorath", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Nihorath"), buttonGroupPanel1);
                CreateButtonWithBackground(52, "Aeonax", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Aeonax"), buttonGroupPanel1);
                CreateButtonWithBackground(53, "Seraphiros", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Seraphiros"), buttonGroupPanel1);
                CreateButtonWithBackground(54, "Thorindar", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Thorindar"), buttonGroupPanel1);
                CreateButtonWithBackground(55, "Zilthros", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Zilthros"), buttonGroupPanel1);
                CreateButtonWithBackground(56, "Khorazal", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Khorazal"), buttonGroupPanel2);
                CreateButtonWithBackground(57, "Ixithra", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Ixithra"), buttonGroupPanel2);
                CreateButtonWithBackground(58, "Omnitheus", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnitheus"), buttonGroupPanel2);
                CreateButtonWithBackground(59, "Phyrixa", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Phyrixa"), buttonGroupPanel2);
                CreateButtonWithBackground(60, "Atherion", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Atherion"), buttonGroupPanel2);
                CreateButtonWithBackground(61, "Vorathos", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Vorathos"), buttonGroupPanel2);
                CreateButtonWithBackground(62, "Tenebris", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Tenebris"), buttonGroupPanel2);
                CreateButtonWithBackground(63, "Xylkor", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Xylkor"), buttonGroupPanel3);
                CreateButtonWithBackground(64, "Veltharion", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Veltharion"), buttonGroupPanel3);
                CreateButtonWithBackground(65, "Arcanos", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Arcanos"), buttonGroupPanel3);
                CreateButtonWithBackground(66, "Dolomath", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Dolomath"), buttonGroupPanel3);
                CreateButtonWithBackground(67, "Arathor", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Arathor"), buttonGroupPanel3);
                CreateButtonWithBackground(68, "Xyphos", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Xyphos"), buttonGroupPanel3);
                CreateButtonWithBackground(69, "Vaelith", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Vaelith"), buttonGroupPanel3);
                ButtonEvent.Instance.AssignButtonEvent("Button_47", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuAzathothManager>().CreateMainMenuAzathothManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_48", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuYogSothothManager>().CreateMainMenuYogSothothManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_49", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuNyarlathotepManager>().CreateMainMenuNyarlathotepManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_50", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuShubNiggurathManager>().CreateMainMenuShubNiggurathManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_51", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuNihorathManager>().CreateMainMenuNihorathManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_52", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuAeonaxManager>().CreateMainMenuAeonaxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_53", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuSeraphirosManager>().CreateMainMenuSeraphirosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_54", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuThorindarManager>().CreateMainMenuThorindarManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_55", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuZilthrosManager>().CreateMainMenuZilthrosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_56", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuKhorazalManager>().CreateMainMenuKhorazalManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_57", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuIxithraManager>().CreateMainMenuIxithraManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_58", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmnitheusManager>().CreateMainMenuOmnitheusManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_59", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuPhyrixaManager>().CreateMainMenuPhyrixaManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_60", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuAtherionManager>().CreateMainMenuAtherionManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_61", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuVorathosManager>().CreateMainMenuVorathosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_62", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuTenebrisManager>().CreateMainMenuTenebrisManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_63", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuXylkorManager>().CreateMainMenuXylkorManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_64", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuVeltharionManager>().CreateMainMenuVeltharionManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_65", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuArcanosManager>().CreateMainMenuArcanosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_66", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuDolomathManager>().CreateMainMenuDolomathManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_67", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuArathorManager>().CreateMainMenuArathorManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_68", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuXyphosManager>().CreateMainMenuXyphosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_69", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuVaelithManager>().CreateMainMenuVaelithManager(data);
                });
            }
            else if (set == 4)
            {
                CreateButtonWithBackground(70, "Zarx", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Zarx"), buttonGroupPanel1);
                CreateButtonWithBackground(71, "Raik", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Raik"), buttonGroupPanel1);
                CreateButtonWithBackground(72, "Drax", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Drax"), buttonGroupPanel1);
                CreateButtonWithBackground(73, "Kron", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Kron"), buttonGroupPanel1);
                CreateButtonWithBackground(74, "Zolt", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Zolt"), buttonGroupPanel1);
                CreateButtonWithBackground(75, "Gorr", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Gorr"), buttonGroupPanel1);
                CreateButtonWithBackground(76, "Ryze", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Ryze"), buttonGroupPanel1);
                CreateButtonWithBackground(77, "Jaxx", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Jaxx"), buttonGroupPanel1);
                CreateButtonWithBackground(78, "Thar", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Thar"), buttonGroupPanel1);
                CreateButtonWithBackground(79, "Vorn", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Vorn"), buttonGroupPanel2);
                CreateButtonWithBackground(80, "Nyx", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Nyx"), buttonGroupPanel2);
                CreateButtonWithBackground(81, "Aros", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Aros"), buttonGroupPanel2);
                CreateButtonWithBackground(82, "Hex", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Hex"), buttonGroupPanel2);
                CreateButtonWithBackground(83, "Lorn", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Lorn"), buttonGroupPanel2);
                CreateButtonWithBackground(84, "Baxx", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Baxx"), buttonGroupPanel2);
                CreateButtonWithBackground(85, "Zeph", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Zeph"), buttonGroupPanel2);
                CreateButtonWithBackground(86, "Kael", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Kael"), buttonGroupPanel3);
                CreateButtonWithBackground(87, "Drav", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Drav"), buttonGroupPanel3);
                CreateButtonWithBackground(88, "Torn", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Torn"), buttonGroupPanel3);
                CreateButtonWithBackground(89, "Myrr", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Myrr"), buttonGroupPanel3);
                CreateButtonWithBackground(90, "Vask", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Vask"), buttonGroupPanel3);
                CreateButtonWithBackground(91, "Jorr", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Jorr"), buttonGroupPanel3);
                CreateButtonWithBackground(92, "Quen", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Quen"), buttonGroupPanel3);
                ButtonEvent.Instance.AssignButtonEvent("Button_70", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuZarxManager>().CreateMainMenuZarxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_71", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuRaikManager>().CreateMainMenuRaikManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_72", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuDraxManager>().CreateMainMenuDraxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_73", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuKronManager>().CreateMainMenuKronManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_74", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuZoltManager>().CreateMainMenuZoltManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_75", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuGorrManager>().CreateMainMenuGorrManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_76", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuRyzeManager>().CreateMainMenuRyzeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_77", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuJaxxManager>().CreateMainMenuJaxxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_78", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuTharManager>().CreateMainMenuTharManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_79", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuVornManager>().CreateMainMenuVornManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_80", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuNyxManager>().CreateMainMenuNyxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_81", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuArosManager>().CreateMainMenuArosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_82", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuHexManager>().CreateMainMenuHexManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_83", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuLornManager>().CreateMainMenuLornManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_84", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuBaxxManager>().CreateMainMenuBaxxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_85", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuZephManager>().CreateMainMenuZephManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_86", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuKaelManager>().CreateMainMenuKaelManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_87", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuDravManager>().CreateMainMenuDravManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_88", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuTornManager>().CreateMainMenuTornManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_89", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuMyrrManager>().CreateMainMenuMyrrManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_90", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuVaskManager>().CreateMainMenuVaskManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_91", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuJorrManager>().CreateMainMenuJorrManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_92", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuQuenManager>().CreateMainMenuQuenManager(data);
                });


            }
        }
        else if (data is Equipments equipments)
        {
            // CreateButtonWithBackground(1, "Equipments", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Equipments"), buttonGroupPanel1);
            CreateButtonWithBackground(2, "Realm", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Realm"), buttonGroupPanel1);
            CreateButtonWithBackground(3, "Upgrade", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Upgrade"), buttonGroupPanel1);
            CreateButtonWithBackground(4, "Aptitude", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Aptitude"), buttonGroupPanel1);
            // CreateButtonWithBackground(5, "Affinity", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Affinity"), buttonGroupPanel1);
            CreateButtonWithBackground(6, "Blessing", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Blessing"), buttonGroupPanel1);
            CreateButtonWithBackground(7, "Core", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Core"), buttonGroupPanel1);
            CreateButtonWithBackground(8, "Physique", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Physique"), buttonGroupPanel1);
            CreateButtonWithBackground(9, "Bloodline", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Bloodline"), buttonGroupPanel1);
            CreateButtonWithBackground(10, "Omnivision", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnivision"), buttonGroupPanel2);
            CreateButtonWithBackground(11, "Omnipotence", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnipotence"), buttonGroupPanel2);
            CreateButtonWithBackground(12, "Omnipresence", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnipresence"), buttonGroupPanel2);
            CreateButtonWithBackground(13, "Omniscience", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omniscience"), buttonGroupPanel2);
            CreateButtonWithBackground(14, "Omnivory", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnivory"), buttonGroupPanel2);
            CreateButtonWithBackground(15, "Angel", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Angel"), buttonGroupPanel2);
            CreateButtonWithBackground(16, "Demon", backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Demon"), buttonGroupPanel2);

            // ButtonEvent.Instance.AssignButtonEvent("Button_1", buttonGroupPanel1, () =>
            // {
            //     FindAnyObjectByType<MainMenuEquipmentManager>().CreateMainMenuEquipmentManager(data);
            // });
            ButtonEvent.Instance.AssignButtonEvent("Button_2", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuRealmManager>().CreateMainMenuRealmManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_3", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuUpgradeManager>().CreateMainMenuUpgradeManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_4", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuAptitudeManager>().CreateMainMenuAptitudeManager(data);
            });
            // ButtonEvent.Instance.AssignButtonEvent("Button_5", buttonGroupPanel1, () =>
            // {
            //     FindAnyObjectByType<MainMenuAffinityManager>().CreateMainMenuAffinityManager(data);
            // });
            ButtonEvent.Instance.AssignButtonEvent("Button_6", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuBlessingManager>().CreateMainMenuBlessingManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_7", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuCoreManager>().CreateMainMenuCoreManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_8", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuPhysiqueManager>().CreateMainMenuPhysiqueManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_9", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuBloodlineManager>().CreateMainMenuBloodlineManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_10", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuOmnivisionManager>().CreateMainMenuOmnivisionManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_11", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuOmnipotenceManager>().CreateMainMenuOmnipotenceManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_12", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuOmnipresenceManager>().CreateMainMenuOmnipresenceManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_13", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuOmniscienceManager>().CreateMainMenuOmniscienceManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_14", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuOmnivoryManager>().CreateMainMenuOmnivoryManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_15", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuAngelManager>().CreateMainMenuAngelManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_16", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuDemonManager>().CreateMainMenuDemonManager(data);
            });
        }
    }
}
