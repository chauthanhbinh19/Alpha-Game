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

        CreateButton(1, AppConstants.Campaigns, Resources.Load<Texture2D>($"UI/Background4/Background_V4_110"), Resources.Load<Texture2D>($"UI/UI/Campaign"), mainMenuCampaignPanel);
        //Main menu
        CreateButton(1, AppConstants.CardHeroes, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Cards"), mainMenuButtonPanel);
        CreateButton(2, AppConstants.Books, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Books"), mainMenuButtonPanel);
        CreateButton(3, AppConstants.Pets, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Pets"), mainMenuButtonPanel);
        CreateButton(4, AppConstants.CardCaptains, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Captains"), mainMenuButtonPanel);
        CreateButton(5, AppConstants.CardColonels, backgroundImage, Resources.Load<Texture2D>($"UI/Button/philosophies_of_conflict"), mainMenuButtonPanel);
        CreateButton(6, AppConstants.CardGenerals, backgroundImage, Resources.Load<Texture2D>($"UI/Button/philosophies_of_contention"), mainMenuButtonPanel);
        CreateButton(7, AppConstants.CardAdmirals, backgroundImage, Resources.Load<Texture2D>($"UI/Button/philosophies_of_diligence"), mainMenuButtonPanel);
        CreateButton(8, AppConstants.CardMilitaries, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Military"), mainMenuButtonPanel);
        CreateButton(9, AppConstants.CardSpells, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Spell"), mainMenuButtonPanel);
        CreateButton(10, AppConstants.CardMonsters, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Monsters"), mainMenuButtonPanel);
        // CreateButton(13, "equipments",backgroundImage,Resources.Load<Texture2D>($"UI/UI/equipments"), mainMenuButtonPanel);
        CreateButton(11, AppConstants.Bag, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Bag"), mainMenuButtonPanel);
        CreateButton(12, AppConstants.Teams, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Teams"), mainMenuButtonPanel);
        CreateButton(13, AppConstants.More, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Teams"), mainMenuButtonPanel);


        CreateButton(14, AppConstants.SummonCardHeroes, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonCards"), summonPanel);
        CreateButton(15, AppConstants.SummonBooks, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonBooks"), summonPanel);
        CreateButton(16, AppConstants.SummonCardCaptains, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonCaptains"), summonPanel);
        CreateButton(17, AppConstants.SummonCardMonsters, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonMonsters"), summonPanel);
        CreateButton(18, AppConstants.SummonCardMilitaries, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonMilitary"), summonPanel);
        CreateButton(19, AppConstants.SummonCardSpells, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonSpell"), summonPanel);
        CreateButton(20, AppConstants.SummonCardColonels, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonColonels"), summonPanel);
        CreateButton(21, AppConstants.SummonCardGenerals, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonGenerals"), summonPanel);
        CreateButton(22, AppConstants.SummonCardAdmirals, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonAdmirals"), summonPanel);

        CreateButton(23, AppConstants.Shop, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Shop"), summonPanel);

        CreateButton(24, AppConstants.Gallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery"), summonPanel);
        CreateButton(25, AppConstants.Collection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection"), summonPanel);
        CreateButton(26, AppConstants.Equipments, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Legend_Equipment"), summonPanel);
        CreateButton(27, AppConstants.Anime, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Lobby_Icon-That_Time_I_Got_Reincarnated_as_a_Slime_Collab"), summonPanel);
        CreateButton(28, AppConstants.Arena, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Arena"), summonPanel);
        CreateButton(29, AppConstants.Guild, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Guild"), summonPanel);
        CreateButton(30, AppConstants.Tower, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Tower"), summonPanel);
        CreateButton(31, AppConstants.Event, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Lobby_Icon-Event_002"), summonPanel);
        CreateButton(32, AppConstants.DailyCheckin, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Daily_Checkin"), summonPanel);
        CreateButton(33, AppConstants.MysticMarket, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Mystic_Market"), summonPanel);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateMoreButton(Transform moreMenuPanel)
    {
        CreateButton(1, AppConstants.CollaborationEquipments, backgroundImage, Resources.Load<Texture2D>($"UI/UI/CollaborationEquipments"), moreMenuPanel);
        CreateButton(2, AppConstants.Collaborations, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Collaboration"), moreMenuPanel);
        CreateButton(3, AppConstants.Medals, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Bag"), moreMenuPanel);
        CreateButton(4, AppConstants.Skills, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Skills"), moreMenuPanel);
        CreateButton(5, AppConstants.Symbols, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Symbols"), moreMenuPanel);
        CreateButton(6, AppConstants.Titles, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Titles"), moreMenuPanel);
        CreateButton(7, AppConstants.MagicFormationCircles, backgroundImage, Resources.Load<Texture2D>($"UI/Button/MagicFormationCircle"), moreMenuPanel);
        CreateButton(8, AppConstants.Relics, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Relics"), moreMenuPanel);
        CreateButton(9, AppConstants.Talismans, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Talisman"), moreMenuPanel);
        CreateButton(10, AppConstants.Puppets, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Puppet"), moreMenuPanel);
        CreateButton(11, AppConstants.Alchemies, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Alchemy"), moreMenuPanel);
        CreateButton(12, AppConstants.Forges, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Forge"), moreMenuPanel);
        CreateButton(13, AppConstants.CardLives, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Life"), moreMenuPanel);
        CreateButton(14, AppConstants.MasterBoard, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Master_Board"), moreMenuPanel);
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
            nameText.text = LocalizationManager.Get(itemName);
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
            nameText.text = LocalizationManager.Get(itemName);
        }

        //Tạo animation cho border image
        RawImage borderImage = newButton.transform.Find("BorderImage").GetComponent<RawImage>();
        // Gán script RotateUI
        borderImage.gameObject.AddComponent<RotateAnimation>();
    }
    public void CreateGalleryButton(Transform galleryMenuPanel)
    {
        //Gallery menu
        CreateButton(1, AppConstants.CardHeroesGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/CardsGallery"), galleryMenuPanel);
        CreateButton(2, AppConstants.BooksGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/BooksGallery"), galleryMenuPanel);
        CreateButton(3, AppConstants.PetsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/PetsGallery"), galleryMenuPanel);
        CreateButton(4, AppConstants.CardCaptainsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/CaptainsGallery"), galleryMenuPanel);
        CreateButton(5, AppConstants.CollaborationEquipmentsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsGallery"), galleryMenuPanel);
        CreateButton(6, AppConstants.CardMilitaryGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/MilitaryGallery"), galleryMenuPanel);
        CreateButton(7, AppConstants.CardSpellGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/SpellGallery"), galleryMenuPanel);
        CreateButton(8, AppConstants.CollaborationsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/CollaborationsGallery"), galleryMenuPanel);
        CreateButton(9, AppConstants.CardMonstersGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/MonstersGallery"), galleryMenuPanel);
        CreateButton(10, AppConstants.EquipmentsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/EquipmentsGallery"), galleryMenuPanel);
        CreateButton(11, AppConstants.MedalsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/MedalsGallery"), galleryMenuPanel);
        CreateButton(12, AppConstants.SkillsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/SkillsGallery"), galleryMenuPanel);
        CreateButton(13, AppConstants.SymbolsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/SymbolsGallery"), galleryMenuPanel);
        CreateButton(14, AppConstants.TitlesGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/TitlesGallery"), galleryMenuPanel);
        CreateButton(15, AppConstants.MagicFormationCircleGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/MagicFormationCircleGallery"), galleryMenuPanel);
        CreateButton(16, AppConstants.RelicsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/RelicsGallery"), galleryMenuPanel);
        CreateButton(17, AppConstants.CardColonelsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/teachings_of_conflict"), galleryMenuPanel);
        CreateButton(18, AppConstants.CardGeneralsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/teachings_of_contention"), galleryMenuPanel);
        CreateButton(19, AppConstants.CardAdmiralsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/teachings_of_diligence"), galleryMenuPanel);
        CreateButton(20, AppConstants.BordersGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/BorderGallery"), galleryMenuPanel);
        CreateButton(21, AppConstants.TalismanGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/TalismanGallery"), galleryMenuPanel);
        CreateButton(22, AppConstants.PuppetGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/PuppetGallery"), galleryMenuPanel);
        CreateButton(23, AppConstants.AlchemyGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/AlchemyGallery"), galleryMenuPanel);
        CreateButton(24, AppConstants.ForgeGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/ForgeGallery"), galleryMenuPanel);
        CreateButton(25, AppConstants.LifeGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/LifeGallery"), galleryMenuPanel);

        FindAnyObjectByType<GalleryManager>().CreateGallery(galleryMenuPanel);
    }
    public void CreateCollectionButton(Transform collectionMenuPanel)
    {
        //Collection menu
        CreateButton(1, AppConstants.CardHeroesCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/CardsCollection"), collectionMenuPanel);
        CreateButton(2, AppConstants.BooksCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/BooksCollection"), collectionMenuPanel);
        CreateButton(3, AppConstants.PetsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/PetsCollection"), collectionMenuPanel);
        CreateButton(4, AppConstants.CardCaptainsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/CaptainsCollection"), collectionMenuPanel);
        CreateButton(5, AppConstants.CollaborationEquipmentsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsCollection"), collectionMenuPanel);
        CreateButton(6, AppConstants.CardMilitaryCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/MilitaryCollection"), collectionMenuPanel);
        CreateButton(7, AppConstants.CardSpellCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/SpellCollection"), collectionMenuPanel);
        CreateButton(8, AppConstants.CollaborationsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/CollaborationsCollection"), collectionMenuPanel);
        CreateButton(9, AppConstants.CardMonstersCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/MonstersCollection"), collectionMenuPanel);
        CreateButton(10, AppConstants.EquipmentsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/EquipmentsCollection"), collectionMenuPanel);
        CreateButton(11, AppConstants.MedalsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/MedalsCollection"), collectionMenuPanel);
        CreateButton(12, AppConstants.SkillsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/SkillsCollection"), collectionMenuPanel);
        CreateButton(13, AppConstants.SymbolsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/SymbolsCollection"), collectionMenuPanel);
        CreateButton(14, AppConstants.TitlesCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/TitlesCollection"), collectionMenuPanel);
        CreateButton(15, AppConstants.MagicFormationCircleCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/MagicFormationCircleCollection"), collectionMenuPanel);
        CreateButton(16, AppConstants.RelicsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/RelicsCollection"), collectionMenuPanel);
        CreateButton(17, AppConstants.CardColonelsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/guide_to_conflict"), collectionMenuPanel);
        CreateButton(18, AppConstants.CardGeneralsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/guide_to_contention"), collectionMenuPanel);
        CreateButton(19, AppConstants.CardAdmiralsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/guide_to_diligence"), collectionMenuPanel);
        CreateButton(20, AppConstants.BordersCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/BordersCollection"), collectionMenuPanel);
        CreateButton(21, AppConstants.TalismanCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/TalismanCollection"), collectionMenuPanel);
        CreateButton(22, AppConstants.PuppetCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/PuppetCollection"), collectionMenuPanel);
        CreateButton(23, AppConstants.AlchemyCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/AlchemyCollection"), collectionMenuPanel);
        CreateButton(24, AppConstants.ForgeCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/ForgeCollection"), collectionMenuPanel);
        CreateButton(25, AppConstants.LifeCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/LifeCollection"), collectionMenuPanel);

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
        CreateAnimeButtonUI(AppConstants.OnePiece, backgroundImage, Resources.Load<Texture2D>($"UI/Button/One Piece"), animeMenuPanel);
        CreateAnimeButtonUI(AppConstants.Naruto, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Naruto"), animeMenuPanel);
        CreateAnimeButtonUI(AppConstants.DragonBall, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Dragon Ball"), animeMenuPanel);
        CreateAnimeButtonUI(AppConstants.FairyTail, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Fairy Tail"), animeMenuPanel);
        CreateAnimeButtonUI(AppConstants.SwordArtOnline, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Sword Art Online"), animeMenuPanel);
        CreateAnimeButtonUI(AppConstants.DemonSlayer, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Demon Slayer"), animeMenuPanel);
        CreateAnimeButtonUI(AppConstants.Bleach, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Bleach"), animeMenuPanel);
        CreateAnimeButtonUI(AppConstants.JujutsuKaisen, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Jujutsu Kaisen"), animeMenuPanel);
        CreateAnimeButtonUI(AppConstants.BlackClover, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Black Clover"), animeMenuPanel);
        CreateAnimeButtonUI(AppConstants.HunterXHunter, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Hunter x Hunter"), animeMenuPanel);
        CreateAnimeButtonUI(AppConstants.OnePunchMan, backgroundImage, Resources.Load<Texture2D>($"UI/Button/One Punch Man"), animeMenuPanel);

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
            nameText.text = LocalizationManager.Get(itemName.ToLower());
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
                CreateButtonWithBackground(1, AppConstants.Equipments, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Equipments"), buttonGroupPanel1);
                CreateButtonWithBackground(2, AppConstants.Realm, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Realm"), buttonGroupPanel1);
                CreateButtonWithBackground(3, AppConstants.Upgrade, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Upgrade"), buttonGroupPanel1);
                CreateButtonWithBackground(4, AppConstants.Aptitude, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Aptitude"), buttonGroupPanel1);
                CreateButtonWithBackground(5, AppConstants.Affinity, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Affinity"), buttonGroupPanel1);
                CreateButtonWithBackground(6, AppConstants.Blessing, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Blessing"), buttonGroupPanel1);
                CreateButtonWithBackground(7, AppConstants.Core, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Core"), buttonGroupPanel1);
                CreateButtonWithBackground(8, AppConstants.Physique, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Physique"), buttonGroupPanel1);
                CreateButtonWithBackground(9, AppConstants.Bloodline, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Bloodline"), buttonGroupPanel1);

                CreateButtonWithBackground(10, AppConstants.Omnivision, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnivision"), buttonGroupPanel2);
                CreateButtonWithBackground(11, AppConstants.Omnipotence, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnipotence"), buttonGroupPanel2);
                CreateButtonWithBackground(12, AppConstants.Omnipresence, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnipresence"), buttonGroupPanel2);
                CreateButtonWithBackground(13, AppConstants.Omniscience, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omniscience"), buttonGroupPanel2);
                CreateButtonWithBackground(14, AppConstants.Omnivory, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnivory"), buttonGroupPanel2);
                CreateButtonWithBackground(15, AppConstants.Angel, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Angel"), buttonGroupPanel2);
                CreateButtonWithBackground(16, AppConstants.Demon, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Demon"), buttonGroupPanel2);

                CreateButtonWithBackground(17, AppConstants.Sword, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Sword"), buttonGroupPanel3);
                CreateButtonWithBackground(18, AppConstants.Spear, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Spear"), buttonGroupPanel3);
                CreateButtonWithBackground(19, AppConstants.Shield, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Shield"), buttonGroupPanel3);
                CreateButtonWithBackground(20, AppConstants.Bow, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Bow"), buttonGroupPanel3);
                CreateButtonWithBackground(21, AppConstants.Gun, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Gun"), buttonGroupPanel3);
                CreateButtonWithBackground(22, AppConstants.Cyber, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Cyber"), buttonGroupPanel3);
                CreateButtonWithBackground(23, AppConstants.Fairy, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Fairy"), buttonGroupPanel3);


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
                CreateButtonWithBackground(24, AppConstants.Dark, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Dark"), buttonGroupPanel1);
                CreateButtonWithBackground(25, AppConstants.Light, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Light"), buttonGroupPanel1);
                CreateButtonWithBackground(26, AppConstants.Fire, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Fire"), buttonGroupPanel1);
                CreateButtonWithBackground(27, AppConstants.Ice, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Ice"), buttonGroupPanel1);
                CreateButtonWithBackground(28, AppConstants.Earth, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Earth"), buttonGroupPanel1);
                CreateButtonWithBackground(29, AppConstants.Thunder, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Thunder"), buttonGroupPanel1);
                CreateButtonWithBackground(30, AppConstants.Life, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Life"), buttonGroupPanel1);
                CreateButtonWithBackground(31, AppConstants.Space, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Space"), buttonGroupPanel1);
                CreateButtonWithBackground(32, AppConstants.Time, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Time"), buttonGroupPanel1);

                CreateButtonWithBackground(33, AppConstants.Nanotech, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Nanotech"), buttonGroupPanel2);
                CreateButtonWithBackground(34, AppConstants.Quantum, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Quantum"), buttonGroupPanel2);
                CreateButtonWithBackground(35, AppConstants.Holography, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Holography"), buttonGroupPanel2);
                CreateButtonWithBackground(36, AppConstants.Plasma, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Plasma"), buttonGroupPanel2);
                CreateButtonWithBackground(37, AppConstants.Biomech, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Biomech"), buttonGroupPanel2);
                CreateButtonWithBackground(38, AppConstants.Cryotech, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Cryotech"), buttonGroupPanel2);
                CreateButtonWithBackground(39, AppConstants.Psionics, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Psionics"), buttonGroupPanel2);

                CreateButtonWithBackground(40, AppConstants.Neurotech, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Neurotech"), buttonGroupPanel3);
                CreateButtonWithBackground(41, AppConstants.Antimatter, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Antimatter"), buttonGroupPanel3);
                CreateButtonWithBackground(42, AppConstants.Phantomware, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Phantomware"), buttonGroupPanel3);
                CreateButtonWithBackground(43, AppConstants.Gravitech, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Gravitech"), buttonGroupPanel3);
                CreateButtonWithBackground(44, AppConstants.Aethernet, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Aethernet"), buttonGroupPanel3);
                CreateButtonWithBackground(45, AppConstants.Starforge, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Starforge"), buttonGroupPanel3);
                CreateButtonWithBackground(46, AppConstants.Orbitalis, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Orbitalis"), buttonGroupPanel3);

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
                CreateButtonWithBackground(47, AppConstants.Azathoth, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Azathoth"), buttonGroupPanel1);
                CreateButtonWithBackground(48, AppConstants.YogSothoth, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Yog-Sothoth"), buttonGroupPanel1);
                CreateButtonWithBackground(49, AppConstants.Nyarlathotep, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Nyarlathotep"), buttonGroupPanel1);
                CreateButtonWithBackground(50, AppConstants.ShubNiggurath, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Shub-Niggurath"), buttonGroupPanel1);
                CreateButtonWithBackground(51, AppConstants.Nihorath, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Nihorath"), buttonGroupPanel1);
                CreateButtonWithBackground(52, AppConstants.Aeonax, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Aeonax"), buttonGroupPanel1);
                CreateButtonWithBackground(53, AppConstants.Seraphiros, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Seraphiros"), buttonGroupPanel1);
                CreateButtonWithBackground(54, AppConstants.Thorindar, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Thorindar"), buttonGroupPanel1);
                CreateButtonWithBackground(55, AppConstants.Zilthros, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Zilthros"), buttonGroupPanel1);

                CreateButtonWithBackground(56, AppConstants.Khorazal, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Khorazal"), buttonGroupPanel2);
                CreateButtonWithBackground(57, AppConstants.Ixithra, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Ixithra"), buttonGroupPanel2);
                CreateButtonWithBackground(58, AppConstants.Omnitheus, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Omnitheus"), buttonGroupPanel2);
                CreateButtonWithBackground(59, AppConstants.Phyrixa, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Phyrixa"), buttonGroupPanel2);
                CreateButtonWithBackground(60, AppConstants.Atherion, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Atherion"), buttonGroupPanel2);
                CreateButtonWithBackground(61, AppConstants.Vorathos, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Vorathos"), buttonGroupPanel2);
                CreateButtonWithBackground(62, AppConstants.Tenebris, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Tenebris"), buttonGroupPanel2);

                CreateButtonWithBackground(63, AppConstants.Xylkor, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Xylkor"), buttonGroupPanel3);
                CreateButtonWithBackground(64, AppConstants.Veltharion, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Veltharion"), buttonGroupPanel3);
                CreateButtonWithBackground(65, AppConstants.Arcanos, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Arcanos"), buttonGroupPanel3);
                CreateButtonWithBackground(66, AppConstants.Dolomath, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Dolomath"), buttonGroupPanel3);
                CreateButtonWithBackground(67, AppConstants.Arathor, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Arathor"), buttonGroupPanel3);
                CreateButtonWithBackground(68, AppConstants.Xyphos, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Xyphos"), buttonGroupPanel3);
                CreateButtonWithBackground(69, AppConstants.Vaelith, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Vaelith"), buttonGroupPanel3);


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
                CreateButtonWithBackground(70, AppConstants.Zarx, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Zarx"), buttonGroupPanel1);
                CreateButtonWithBackground(71, AppConstants.Raik, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Raik"), buttonGroupPanel1);
                CreateButtonWithBackground(72, AppConstants.Drax, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Drax"), buttonGroupPanel1);
                CreateButtonWithBackground(73, AppConstants.Kron, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Kron"), buttonGroupPanel1);
                CreateButtonWithBackground(74, AppConstants.Zolt, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Zolt"), buttonGroupPanel1);
                CreateButtonWithBackground(75, AppConstants.Gorr, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Gorr"), buttonGroupPanel1);
                CreateButtonWithBackground(76, AppConstants.Ryze, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Ryze"), buttonGroupPanel1);
                CreateButtonWithBackground(77, AppConstants.Jaxx, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Jaxx"), buttonGroupPanel1);
                CreateButtonWithBackground(78, AppConstants.Thar, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Thar"), buttonGroupPanel1);

                CreateButtonWithBackground(79, AppConstants.Vorn, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Vorn"), buttonGroupPanel2);
                CreateButtonWithBackground(80, AppConstants.Nyx, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Nyx"), buttonGroupPanel2);
                CreateButtonWithBackground(81, AppConstants.Aros, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Aros"), buttonGroupPanel2);
                CreateButtonWithBackground(82, AppConstants.Hex, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Hex"), buttonGroupPanel2);
                CreateButtonWithBackground(83, AppConstants.Lorn, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Lorn"), buttonGroupPanel2);
                CreateButtonWithBackground(84, AppConstants.Baxx, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Baxx"), buttonGroupPanel2);
                CreateButtonWithBackground(85, AppConstants.Zeph, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Zeph"), buttonGroupPanel2);

                CreateButtonWithBackground(86, AppConstants.Kael, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Kael"), buttonGroupPanel3);
                CreateButtonWithBackground(87, AppConstants.Drav, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Drav"), buttonGroupPanel3);
                CreateButtonWithBackground(88, AppConstants.Torn, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Torn"), buttonGroupPanel3);
                CreateButtonWithBackground(89, AppConstants.Myrr, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Myrr"), buttonGroupPanel3);
                CreateButtonWithBackground(90, AppConstants.Vask, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Vask"), buttonGroupPanel3);
                CreateButtonWithBackground(91, AppConstants.Jorr, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Jorr"), buttonGroupPanel3);
                CreateButtonWithBackground(92, AppConstants.Quen, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Quen"), buttonGroupPanel3);

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
