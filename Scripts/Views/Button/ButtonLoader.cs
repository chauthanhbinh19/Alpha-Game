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
    private Transform mainMenuSubButtonGroupPanel;
    private Transform summonPanel;
    private GameObject TabButton4;
    private GameObject TabButton3;
    private GameObject TabButton6;
    private GameObject AdvancedButtonFirst;
    private GameObject ArenaButtonPrefab;
    private GameObject AnimeButtonPrefab;
    private GameObject ReactorButtonPrefab;
    private GameObject PopupMenuPanelPrefab;
    private Transform MainPanel;
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
        mainMenuSubButtonGroupPanel = UIManager.Instance.GetTransform("mainMenuSubButtonGroupPanel");
        summonPanel = UIManager.Instance.GetTransform("summonPanel");
        TabButton4 = UIManager.Instance.GetGameObject("TabButton4");
        TabButton3 = UIManager.Instance.GetGameObject("TabButton3");
        TabButton6 = UIManager.Instance.GetGameObject("TabButton6");
        AdvancedButtonFirst = UIManager.Instance.GetGameObject("AdvancedButtonFirst");
        ArenaButtonPrefab = UIManager.Instance.GetGameObject("ArenaButtonPrefab");
        AnimeButtonPrefab = UIManager.Instance.GetGameObject("AnimeButtonPrefab");
        ReactorButtonPrefab = UIManager.Instance.GetGameObjectScienceFiction("ReactorButtonPrefab");
        PopupMenuPanelPrefab = UIManager.Instance.GetGameObject("PopupMenuPanelPrefab");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");

        backgroundImage = Resources.Load<Texture2D>($"UI/Background4/Background_V4_422");
        backgroundImage2 = Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder");

        CreateButton(1, AppConstants.MainType.Campaigns, Resources.Load<Texture2D>($"UI/Background4/Background_V4_110"), Resources.Load<Texture2D>($"UI/UI/Campaign"), mainMenuCampaignPanel);
        //Main menu
        CreateButton(1, AppConstants.MainType.CardHeroes, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Cards"), mainMenuButtonPanel);
        CreateButton(2, AppConstants.MainType.Books, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Books"), mainMenuButtonPanel);
        CreateButton(3, AppConstants.MainType.Pets, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Pets"), mainMenuButtonPanel);
        CreateButton(4, AppConstants.MainType.CardCaptains, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Captains"), mainMenuButtonPanel);
        CreateButton(5, AppConstants.MainType.CardColonels, backgroundImage, Resources.Load<Texture2D>($"UI/Button/philosophies_of_conflict"), mainMenuButtonPanel);
        CreateButton(6, AppConstants.MainType.CardGenerals, backgroundImage, Resources.Load<Texture2D>($"UI/Button/philosophies_of_contention"), mainMenuButtonPanel);
        CreateButton(7, AppConstants.MainType.CardAdmirals, backgroundImage, Resources.Load<Texture2D>($"UI/Button/philosophies_of_diligence"), mainMenuButtonPanel);
        CreateButton(8, AppConstants.MainType.CardMilitaries, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Military"), mainMenuButtonPanel);
        CreateButton(9, AppConstants.MainType.CardSpells, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Spell"), mainMenuButtonPanel);
        CreateButton(10, AppConstants.MainType.CardMonsters, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Monsters"), mainMenuButtonPanel);
        // CreateButton(13, "equipments",backgroundImage,Resources.Load<Texture2D>($"UI/UI/equipments"), mainMenuButtonPanel);
        CreateButton(11, AppConstants.MainType.Bag, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Bag"), mainMenuButtonPanel);
        CreateButton(12, AppConstants.MainType.Teams, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Teams"), mainMenuButtonPanel);
        CreateButton(13, AppConstants.MainType.More, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Teams"), mainMenuButtonPanel);


        CreateButton(14, AppConstants.MainType.SummonCardHeroes, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonCards"), summonPanel);
        CreateButton(15, AppConstants.MainType.SummonBooks, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonBooks"), summonPanel);
        CreateButton(16, AppConstants.MainType.SummonCardCaptains, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonCaptains"), summonPanel);
        CreateButton(17, AppConstants.MainType.SummonCardMonsters, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonMonsters"), summonPanel);
        CreateButton(18, AppConstants.MainType.SummonCardMilitaries, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonMilitary"), summonPanel);
        CreateButton(19, AppConstants.MainType.SummonCardSpells, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonSpell"), summonPanel);
        CreateButton(20, AppConstants.MainType.SummonCardColonels, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonColonels"), summonPanel);
        CreateButton(21, AppConstants.MainType.SummonCardGenerals, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonGenerals"), summonPanel);
        CreateButton(22, AppConstants.MainType.SummonCardAdmirals, backgroundImage, Resources.Load<Texture2D>($"UI/UI/SummonAdmirals"), summonPanel);

        CreateButton(23, AppConstants.MainType.Shop, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Shop"), summonPanel);

        CreateButton(24, AppConstants.MainType.Gallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery"), summonPanel);
        CreateButton(25, AppConstants.MainType.Collection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection"), summonPanel);
        CreateButton(26, AppConstants.MainType.Equipments, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Legend_Equipment"), summonPanel);
        CreateButton(27, AppConstants.MainType.Anime, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Lobby_Icon-That_Time_I_Got_Reincarnated_as_a_Slime_Collab"), summonPanel);
        CreateButton(28, AppConstants.MainType.Arena, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Arena"), summonPanel);
        CreateButton(29, AppConstants.MainType.Guild, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Guild"), summonPanel);
        CreateButton(30, AppConstants.MainType.Tower, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Tower"), summonPanel);
        CreateButton(31, AppConstants.MainType.Event, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Lobby_Icon-Event_002"), summonPanel);
        CreateButton(32, AppConstants.MainType.DailyCheckin, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Daily_Checkin"), summonPanel);
        CreateButton(33, AppConstants.Market.RareMarket, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Rare_Market"), summonPanel);
        CreateButton(34, AppConstants.Market.UltraRareMarket, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Ultra_Rare_Market"), summonPanel);
        CreateButton(35, AppConstants.Market.LegendaryMarket, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Legendary_Market"), summonPanel);
        CreateButton(36, AppConstants.Market.MysticMarket, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Mystic_Market"), summonPanel);

        CreateButton(1, AppConstants.MainType.Email, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Mail"), mainMenuSubButtonGroupPanel);
        CreateButton(2, AppConstants.MainType.Chat, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Chat"), mainMenuSubButtonGroupPanel);
        CreateButton(1, AppConstants.MainType.Email, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Mail"), mainMenuSubButtonGroupPanel);
        CreateButton(1, AppConstants.MainType.Email, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Mail"), mainMenuSubButtonGroupPanel);
        CreateButton(1, AppConstants.MainType.Email, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Mail"), mainMenuSubButtonGroupPanel);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateMoreButton(Transform moreMenuPanel)
    {
        CreateButton(1, AppConstants.MainType.CollaborationEquipments, backgroundImage, Resources.Load<Texture2D>($"UI/UI/CollaborationEquipments"), moreMenuPanel);
        CreateButton(2, AppConstants.MainType.Collaborations, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Collaboration"), moreMenuPanel);
        CreateButton(3, AppConstants.MainType.Medals, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Bag"), moreMenuPanel);
        CreateButton(4, AppConstants.MainType.Skills, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Skills"), moreMenuPanel);
        CreateButton(5, AppConstants.MainType.Symbols, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Symbols"), moreMenuPanel);
        CreateButton(6, AppConstants.MainType.Titles, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Titles"), moreMenuPanel);
        CreateButton(7, AppConstants.MainType.MagicFormationCircles, backgroundImage, Resources.Load<Texture2D>($"UI/Button/MagicFormationCircle"), moreMenuPanel);
        CreateButton(8, AppConstants.MainType.Relics, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Relics"), moreMenuPanel);
        CreateButton(9, AppConstants.MainType.Talismans, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Talisman"), moreMenuPanel);
        CreateButton(10, AppConstants.MainType.Puppets, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Puppet"), moreMenuPanel);
        CreateButton(11, AppConstants.MainType.Alchemies, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Alchemy"), moreMenuPanel);
        CreateButton(12, AppConstants.MainType.Forges, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Forge"), moreMenuPanel);
        CreateButton(13, AppConstants.MainType.CardLives, backgroundImage, Resources.Load<Texture2D>($"UI/UI/Life"), moreMenuPanel);
        CreateButton(14, AppConstants.MainType.MasterBoard, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Master_Board"), moreMenuPanel);
        CreateButton(15, AppConstants.MainType.Artwork, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Artwork"), moreMenuPanel);
        CreateButton(16, AppConstants.MainType.SpiritBeast, backgroundImage, Resources.Load<Texture2D>($"UI/Button/SpiritBeast"), moreMenuPanel);
        CreateButton(17, AppConstants.MainType.ScienceFiction, backgroundImage, Resources.Load<Texture2D>($"UI/Button/ScienceFiction"), moreMenuPanel);
        moreMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
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
    private void CreateAnimeButtonUI(string itemDisplayName, string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
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
            nameText.text = LocalizationManager.Get(itemDisplayName);
        }

        //Tạo animation cho border image
        RawImage borderImage = newButton.transform.Find("BorderImage").GetComponent<RawImage>();
        // Gán script RotateUI
        borderImage.gameObject.AddComponent<RotateAnimation>();
    }
    private void CreateScienceFictionButtonUI(string itemName, int number, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ReactorButtonPrefab, panel);
        newButton.name = itemName;

        // Gán màu cho itemBackground
        // RawImage  background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        // if (background != null && itemBackground != null)
        // {
        //     background.texture = itemBackground;
        // }

        RawImage image = newButton.transform.Find("Image").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        TextMeshProUGUI nameText = newButton.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }

        TextMeshProUGUI numberText = newButton.transform.Find("NumberText").GetComponent<TextMeshProUGUI>();
        if (numberText != null)
        {
            numberText.text = number.ToString("D2");
        }

        // RawImage borderImage = newButton.transform.Find("BorderImage").GetComponent<RawImage>();
    }
    public void CreateGalleryButton(Transform galleryMenuPanel)
    {
        //Gallery menu
        CreateButton(1, AppDisplayConstants.Gallery.CardHeroesGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/CardsGallery"), galleryMenuPanel);
        CreateButton(2, AppDisplayConstants.Gallery.BooksGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/BooksGallery"), galleryMenuPanel);
        CreateButton(3, AppDisplayConstants.Gallery.PetsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/PetsGallery"), galleryMenuPanel);
        CreateButton(4, AppDisplayConstants.Gallery.CardCaptainsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/CaptainsGallery"), galleryMenuPanel);
        CreateButton(5, AppDisplayConstants.Gallery.CollaborationEquipmentsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/CollaborationEquipmentsGallery"), galleryMenuPanel);
        CreateButton(6, AppDisplayConstants.Gallery.CardMilitaryGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/MilitaryGallery"), galleryMenuPanel);
        CreateButton(7, AppDisplayConstants.Gallery.CardSpellGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/SpellGallery"), galleryMenuPanel);
        CreateButton(8, AppDisplayConstants.Gallery.CollaborationsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/CollaborationsGallery"), galleryMenuPanel);
        CreateButton(9, AppDisplayConstants.Gallery.CardMonstersGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/MonstersGallery"), galleryMenuPanel);
        CreateButton(10, AppDisplayConstants.Gallery.EquipmentsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/EquipmentsGallery"), galleryMenuPanel);
        CreateButton(11, AppDisplayConstants.Gallery.MedalsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/MedalsGallery"), galleryMenuPanel);
        CreateButton(12, AppDisplayConstants.Gallery.SkillsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/SkillsGallery"), galleryMenuPanel);
        CreateButton(13, AppDisplayConstants.Gallery.SymbolsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/SymbolsGallery"), galleryMenuPanel);
        CreateButton(14, AppDisplayConstants.Gallery.TitlesGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/TitlesGallery"), galleryMenuPanel);
        CreateButton(15, AppDisplayConstants.Gallery.MagicFormationCircleGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/MagicFormationCircleGallery"), galleryMenuPanel);
        CreateButton(16, AppDisplayConstants.Gallery.RelicsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/RelicsGallery"), galleryMenuPanel);
        CreateButton(17, AppDisplayConstants.Gallery.CardColonelsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/ColonelsGallery"), galleryMenuPanel);
        CreateButton(18, AppDisplayConstants.Gallery.CardGeneralsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/GeneralsGallery"), galleryMenuPanel);
        CreateButton(19, AppDisplayConstants.Gallery.CardAdmiralsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/AdmiralsGallery"), galleryMenuPanel);
        CreateButton(20, AppDisplayConstants.Gallery.BordersGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/BorderGallery"), galleryMenuPanel);
        CreateButton(21, AppDisplayConstants.Gallery.TalismanGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/TalismanGallery"), galleryMenuPanel);
        CreateButton(22, AppDisplayConstants.Gallery.PuppetGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/PuppetGallery"), galleryMenuPanel);
        CreateButton(23, AppDisplayConstants.Gallery.AlchemyGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/AlchemyGallery"), galleryMenuPanel);
        CreateButton(24, AppDisplayConstants.Gallery.ForgeGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/ForgeGallery"), galleryMenuPanel);
        CreateButton(25, AppDisplayConstants.Gallery.LifeGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/LifeGallery"), galleryMenuPanel);
        CreateButton(26, AppDisplayConstants.Gallery.ArtworkGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/ArtworkGallery"), galleryMenuPanel);
        CreateButton(27, AppDisplayConstants.Gallery.SpiritBeastGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/SpiritBeastGallery"), galleryMenuPanel);
        CreateButton(28, AppDisplayConstants.Gallery.AvatarsGallery, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Gallery/AvatarsGallery"), galleryMenuPanel);

        FindAnyObjectByType<GalleryManager>().CreateGallery(galleryMenuPanel);
        galleryMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateCollectionButton(Transform collectionMenuPanel)
    {
        //Collection menu
        CreateButton(1, AppDisplayConstants.Collection.CardHeroesCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/CardsCollection"), collectionMenuPanel);
        CreateButton(2, AppDisplayConstants.Collection.BooksCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/BooksCollection"), collectionMenuPanel);
        CreateButton(3, AppDisplayConstants.Collection.PetsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/PetsCollection"), collectionMenuPanel);
        CreateButton(4, AppDisplayConstants.Collection.CardCaptainsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/CaptainsCollection"), collectionMenuPanel);
        CreateButton(5, AppDisplayConstants.Collection.CollaborationEquipmentsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/CollaborationEquipmentsCollection"), collectionMenuPanel);
        CreateButton(6, AppDisplayConstants.Collection.CardMilitaryCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/MilitaryCollection"), collectionMenuPanel);
        CreateButton(7, AppDisplayConstants.Collection.CardSpellCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/SpellCollection"), collectionMenuPanel);
        CreateButton(8, AppDisplayConstants.Collection.CollaborationsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/CollaborationsCollection"), collectionMenuPanel);
        CreateButton(9, AppDisplayConstants.Collection.CardMonstersCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/MonstersCollection"), collectionMenuPanel);
        CreateButton(10, AppDisplayConstants.Collection.EquipmentsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/EquipmentsCollection"), collectionMenuPanel);
        CreateButton(11, AppDisplayConstants.Collection.MedalsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/MedalsCollection"), collectionMenuPanel);
        CreateButton(12, AppDisplayConstants.Collection.SkillsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/SkillsCollection"), collectionMenuPanel);
        CreateButton(13, AppDisplayConstants.Collection.SymbolsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/SymbolsCollection"), collectionMenuPanel);
        CreateButton(14, AppDisplayConstants.Collection.TitlesCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/TitlesCollection"), collectionMenuPanel);
        CreateButton(15, AppDisplayConstants.Collection.MagicFormationCircleCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/MagicFormationCircleCollection"), collectionMenuPanel);
        CreateButton(16, AppDisplayConstants.Collection.RelicsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/RelicsCollection"), collectionMenuPanel);
        CreateButton(17, AppDisplayConstants.Collection.CardColonelsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/ColonelsCollection"), collectionMenuPanel);
        CreateButton(18, AppDisplayConstants.Collection.CardGeneralsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/GeneralsCollection"), collectionMenuPanel);
        CreateButton(19, AppDisplayConstants.Collection.CardAdmiralsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/AdmiralsCollection"), collectionMenuPanel);
        CreateButton(20, AppDisplayConstants.Collection.BordersCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/BordersCollection"), collectionMenuPanel);
        CreateButton(21, AppDisplayConstants.Collection.TalismanCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/TalismanCollection"), collectionMenuPanel);
        CreateButton(22, AppDisplayConstants.Collection.PuppetCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/PuppetCollection"), collectionMenuPanel);
        CreateButton(23, AppDisplayConstants.Collection.AlchemyCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/AlchemyCollection"), collectionMenuPanel);
        CreateButton(24, AppDisplayConstants.Collection.ForgeCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/ForgeCollection"), collectionMenuPanel);
        CreateButton(25, AppDisplayConstants.Collection.LifeCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/LifeCollection"), collectionMenuPanel);
        CreateButton(26, AppDisplayConstants.Collection.ArtworkCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/ArtworkCollection"), collectionMenuPanel);
        CreateButton(27, AppDisplayConstants.Collection.SpiritBeastCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/SpiritBeastCollection"), collectionMenuPanel);
        CreateButton(28, AppDisplayConstants.Collection.AvatarsCollection, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Collection/AvatarsCollection"), collectionMenuPanel);

        FindAnyObjectByType<CollectionManager>().CreateCollection(collectionMenuPanel);
        collectionMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
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
                CreateButtonWithName(subtype, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Equipments/{subtype}"), equipmentMenuPanel);
            }
        }
        FindAnyObjectByType<EquipmentManager>().CreateEquipments(equipmentMenuPanel);
        equipmentMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateAnimeButton(Transform animeMenuPanel)
    {
        CreateAnimeButtonUI(AppDisplayConstants.Anime.OnePiece, AppConstants.Anime.OnePiece, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/One Piece"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.Naruto, AppConstants.Anime.Naruto, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Naruto"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.DragonBall, AppConstants.Anime.DragonBall, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Dragon Ball"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.FairyTail, AppConstants.Anime.FairyTail, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Fairy Tail"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.SwordArtOnline, AppConstants.Anime.SwordArtOnline, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Sword Art Online"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.DemonSlayer, AppConstants.Anime.DemonSlayer, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Demon Slayer"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.Bleach, AppConstants.Anime.Bleach, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Bleach"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.JujutsuKaisen, AppConstants.Anime.JujutsuKaisen, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Jujutsu Kaisen"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.BlackClover, AppConstants.Anime.BlackClover, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Black Clover"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.HunterXHunter, AppConstants.Anime.HunterXHunter, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Hunter x Hunter"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.OnePunchMan, AppConstants.Anime.OnePunchMan, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/One Punch Man"), animeMenuPanel);

        FindAnyObjectByType<MainMenuAnimeStatsManager>().CreateAnimeButton(animeMenuPanel);
        animeMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateScienceFictionButton(Transform reactorMenuPanel)
    {
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber1, 1, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber1"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber2, 2, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber2"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber3, 3, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber3"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber4, 4, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber4"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber5, 5, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber5"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber6, 6, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber6"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber7, 7, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber7"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber8, 8, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber8"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber9, 9, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber9"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber10, 10, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber10"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber11, 11, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber11"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber12, 12, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber12"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber13, 13, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber13"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber14, 14, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber14"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber15, 15, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber15"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber16, 16, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber16"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber17, 17, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber17"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber18, 18, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber18"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber19, 19, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber19"), reactorMenuPanel);
        CreateScienceFictionButtonUI(AppDisplayConstants.ScienceFiction.ReactorNumber20, 20, backgroundImage, Resources.Load<Texture2D>($"UI/Background3/ReactorNumber20"), reactorMenuPanel);

        FindAnyObjectByType<MainMenuAnimeStatsManager>().CreateAnimeButton(reactorMenuPanel);
        reactorMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateArenaButton(Transform arenaMenuPanel)
    {

        var uniqueMode = ArenaService.Create().GetUniqueTypes();
        foreach (var type in uniqueMode)
        {
            CreateArenaButtonUI(type, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Arena/{type}"), arenaMenuPanel);
        }
        FindAnyObjectByType<ArenaManager>().CreateArenaButton(arenaMenuPanel);
        arenaMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
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
                ChangeButtonBackground(button.gameObject, ImageConstants.Button.DetailTabbuttonUrlBeforeClick);
            }
        }
        // Đổi background cho button được nhấn
        if (clickedButton != null)
        {
            ChangeButtonBackground(clickedButton.gameObject, ImageConstants.Button.DetailTabbuttonUrlAfterClick); // Background clicked
        }
    }
    public void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = Resources.Load<Texture>($"{image}");
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
            int setButtonNumber = 5;
            for (int i = 0; i < setButtonNumber; i++)
            {
                int index = i;
                GameObject button = Instantiate(TabButton6, buttonPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = (index + 1).ToString();

                Button btn = button.GetComponent<Button>();

                if (index == 0)
                {
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.AdvancedButtonSet1);
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
                        CreateButtonGroup(data);
                    });
                }
                else if (index == 1)
                {
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.AdvancedButtonSet2);
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
                        CreateButtonGroup(data);
                    });
                }
                else if (index == 2)
                {
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.AdvancedButtonSet3);
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
                        CreateButtonGroup(data);
                    });
                }
                else if (index == 3)
                {
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.AdvancedButtonSet4);
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
                        CreateButtonGroup(data);
                    });
                }
                else if (index == 4)
                {
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.AdvancedButtonSet1);
                    btn.onClick.AddListener(() =>
                    {
                        set = index + 1;
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
                        CreateButtonGroup(data);
                    });
                }
            }
        }
    }
    public void CreateButtonGroup(object data)
    {
        GameObject popUpPanelGameObject = Instantiate(PopupMenuPanelPrefab, MainPanel);
        Transform content = popUpPanelGameObject.transform.Find("Scroll View/Viewport/Content");
        GridLayoutGroup gridLayout = content.GetComponent<GridLayoutGroup>();
        gridLayout.cellSize = new Vector2(280, 450);
        // content.position = new Vector3(transform.position.x, 200f, transform.position.z);

        TextMeshProUGUI titleText = popUpPanelGameObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        titleText.text = "Set " + set.ToString();

        Button CloseButton = popUpPanelGameObject.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            Destroy(popUpPanelGameObject);
        });

        if (data is CardHeroes cardHeroes)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardCaptains cardCaptains)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardColonels cardColonels)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardGenerals cardGenerals)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardMonsters cardMonsters)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardMilitary cardMilitary)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is CardSpell cardSpell)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is Books books)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is Pets pets)
        {
            CreateButtonGroupDetails(data, content);
        }
        else if (data is Equipments equipments)
        {
            CreateButtonGroupDetails(data, content);
        }
    }
    private void CreateButtonWithBackground(int index, string itemName, string itemBackground, Texture2D itemImage, Transform panel)
    {
        if (panel == null)
        {
            Debug.Log("Panel is null for index: " + index);
            return;
        }
        // Tạo button từ prefab
        GameObject newButton = Instantiate(AdvancedButtonFirst, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("Background").GetComponent<RawImage>();
        Texture texture = Resources.Load<Texture>($"{itemBackground}");
        if (background != null && itemBackground != null)
        {
            background.texture = texture;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("MainImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void CreateButtonGroupDetails(object data, Transform content)
    {
        if (data is CardHeroes cardHeroes || data is CardCaptains cardCaptains ||
        data is CardColonels cardColonels || data is CardGenerals cardGenerals ||
        data is CardAdmirals cardAdmirals || data is CardMonsters cardMonsters ||
        data is CardMilitary cardMilitary || data is CardSpell cardSpell ||
        data is Books books || data is Pets pets
        )
        {
            if (set == 1)
            {
                CreateButtonWithBackground(1, AppDisplayConstants.MainMenuSet1.Equipments, ImageConstants.Background.AdvancedBackground1, Resources.Load<Texture2D>($"UI/Button/Main/Equipments"), content);
                CreateButtonWithBackground(2, AppDisplayConstants.MainMenuSet1.Realm, ImageConstants.Background.AdvancedBackground2, Resources.Load<Texture2D>($"UI/Button/Main/Realm"), content);
                CreateButtonWithBackground(3, AppDisplayConstants.MainMenuSet1.Upgrade, ImageConstants.Background.AdvancedBackground3, Resources.Load<Texture2D>($"UI/Button/Main/Upgrade"), content);
                CreateButtonWithBackground(4, AppDisplayConstants.MainMenuSet1.Aptitude, ImageConstants.Background.AdvancedBackground4, Resources.Load<Texture2D>($"UI/Button/Main/Aptitude"), content);
                CreateButtonWithBackground(5, AppDisplayConstants.MainMenuSet1.Affinity, ImageConstants.Background.AdvancedBackground5, Resources.Load<Texture2D>($"UI/Button/Main/Affinity"), content);
                CreateButtonWithBackground(6, AppDisplayConstants.MainMenuSet1.Blessing, ImageConstants.Background.AdvancedBackground6, Resources.Load<Texture2D>($"UI/Button/Main/Blessing"), content);
                CreateButtonWithBackground(7, AppDisplayConstants.MainMenuSet1.Core, ImageConstants.Background.AdvancedBackground7, Resources.Load<Texture2D>($"UI/Button/Main/Core"), content);
                CreateButtonWithBackground(8, AppDisplayConstants.MainMenuSet1.Physique, ImageConstants.Background.AdvancedBackground8, Resources.Load<Texture2D>($"UI/Button/Main/Physique"), content);
                CreateButtonWithBackground(9, AppDisplayConstants.MainMenuSet1.Bloodline, ImageConstants.Background.AdvancedBackground9, Resources.Load<Texture2D>($"UI/Button/Main/Bloodline"), content);

                CreateButtonWithBackground(10, AppDisplayConstants.MainMenuSet1.Omnivision, ImageConstants.Background.AdvancedBackground10, Resources.Load<Texture2D>($"UI/Button/Main/Omnivision"), content);
                CreateButtonWithBackground(11, AppDisplayConstants.MainMenuSet1.Omnipotence, ImageConstants.Background.AdvancedBackground11, Resources.Load<Texture2D>($"UI/Button/Main/Omnipotence"), content);
                CreateButtonWithBackground(12, AppDisplayConstants.MainMenuSet1.Omnipresence, ImageConstants.Background.AdvancedBackground12, Resources.Load<Texture2D>($"UI/Button/Main/Omnipresence"), content);
                CreateButtonWithBackground(13, AppDisplayConstants.MainMenuSet1.Omniscience, ImageConstants.Background.AdvancedBackground13, Resources.Load<Texture2D>($"UI/Button/Main/Omniscience"), content);
                CreateButtonWithBackground(14, AppDisplayConstants.MainMenuSet1.Omnivory, ImageConstants.Background.AdvancedBackground14, Resources.Load<Texture2D>($"UI/Button/Main/Omnivory"), content);
                CreateButtonWithBackground(15, AppDisplayConstants.MainMenuSet1.Angel, ImageConstants.Background.AdvancedBackground15, Resources.Load<Texture2D>($"UI/Button/Main/Angel"), content);
                CreateButtonWithBackground(16, AppDisplayConstants.MainMenuSet1.Demon, ImageConstants.Background.AdvancedBackground16, Resources.Load<Texture2D>($"UI/Button/Main/Demon"), content);

                CreateButtonWithBackground(17, AppDisplayConstants.MainMenuSet1.Sword, ImageConstants.Background.AdvancedBackground17, Resources.Load<Texture2D>($"UI/Button/Main/Sword"), content);
                CreateButtonWithBackground(18, AppDisplayConstants.MainMenuSet1.Spear, ImageConstants.Background.AdvancedBackground18, Resources.Load<Texture2D>($"UI/Button/Main/Spear"), content);
                CreateButtonWithBackground(19, AppDisplayConstants.MainMenuSet1.Shield, ImageConstants.Background.AdvancedBackground19, Resources.Load<Texture2D>($"UI/Button/Main/Shield"), content);
                CreateButtonWithBackground(20, AppDisplayConstants.MainMenuSet1.Bow, ImageConstants.Background.AdvancedBackground20, Resources.Load<Texture2D>($"UI/Button/Main/Bow"), content);
                CreateButtonWithBackground(21, AppDisplayConstants.MainMenuSet1.Gun, ImageConstants.Background.AdvancedBackground21, Resources.Load<Texture2D>($"UI/Button/Main/Gun"), content);
                CreateButtonWithBackground(22, AppDisplayConstants.MainMenuSet1.Cyber, ImageConstants.Background.AdvancedBackground22, Resources.Load<Texture2D>($"UI/Button/Main/Cyber"), content);
                CreateButtonWithBackground(23, AppDisplayConstants.MainMenuSet1.Fairy, ImageConstants.Background.AdvancedBackground23, Resources.Load<Texture2D>($"UI/Button/Main/Fairy"), content);


                ButtonEvent.Instance.AssignButtonEvent("Button_1", content, () =>
                {
                    FindAnyObjectByType<MainMenuEquipmentManager>().CreateMainMenuEquipmentManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_2", content, () =>
                {
                    FindAnyObjectByType<MainMenuRealmManager>().CreateMainMenuRealmManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_3", content, () =>
                {
                    FindAnyObjectByType<MainMenuUpgradeManager>().CreateMainMenuUpgradeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_4", content, () =>
                {
                    FindAnyObjectByType<MainMenuAptitudeManager>().CreateMainMenuAptitudeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_5", content, () =>
                {
                    FindAnyObjectByType<MainMenuAffinityManager>().CreateMainMenuAffinityManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_6", content, () =>
                {
                    FindAnyObjectByType<MainMenuBlessingManager>().CreateMainMenuBlessingManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_7", content, () =>
                {
                    FindAnyObjectByType<MainMenuCoreManager>().CreateMainMenuCoreManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_8", content, () =>
                {
                    FindAnyObjectByType<MainMenuPhysiqueManager>().CreateMainMenuPhysiqueManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_9", content, () =>
                {
                    FindAnyObjectByType<MainMenuBloodlineManager>().CreateMainMenuBloodlineManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_10", content, () =>
                {
                    FindAnyObjectByType<MainMenuOmnivisionManager>().CreateMainMenuOmnivisionManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_11", content, () =>
                {
                    FindAnyObjectByType<MainMenuOmnipotenceManager>().CreateMainMenuOmnipotenceManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_12", content, () =>
                {
                    FindAnyObjectByType<MainMenuOmnipresenceManager>().CreateMainMenuOmnipresenceManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_13", content, () =>
                {
                    FindAnyObjectByType<MainMenuOmniscienceManager>().CreateMainMenuOmniscienceManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_14", content, () =>
                {
                    FindAnyObjectByType<MainMenuOmnivoryManager>().CreateMainMenuOmnivoryManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_15", content, () =>
                {
                    FindAnyObjectByType<MainMenuAngelManager>().CreateMainMenuAngelManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_16", content, () =>
                {
                    FindAnyObjectByType<MainMenuDemonManager>().CreateMainMenuDemonManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_17", content, () =>
                {
                    FindAnyObjectByType<MainMenuSwordManager>().CreateMainMenuSwordManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_18", content, () =>
                {
                    FindAnyObjectByType<MainMenuSpearManager>().CreateMainMenuSpearManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_19", content, () =>
                {
                    FindAnyObjectByType<MainMenuShieldManager>().CreateMainMenuShieldManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_20", content, () =>
                {
                    FindAnyObjectByType<MainMenuBowManager>().CreateMainMenuBowManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_21", content, () =>
                {
                    FindAnyObjectByType<MainMenuGunManager>().CreateMainMenuGunManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_22", content, () =>
                {
                    FindAnyObjectByType<MainMenuCyberManager>().CreateMainMenuCyberManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_23", content, () =>
                {
                    FindAnyObjectByType<MainMenuFairyManager>().CreateMainMenuFairyManager(data);
                });
            }
            else if (set == 2)
            {
                CreateButtonWithBackground(24, AppDisplayConstants.MainMenuSet2.Dark, ImageConstants.Background.AdvancedBackground1, Resources.Load<Texture2D>($"UI/Button/Main/Dark"), content);
                CreateButtonWithBackground(25, AppDisplayConstants.MainMenuSet2.Light, ImageConstants.Background.AdvancedBackground2, Resources.Load<Texture2D>($"UI/Button/Main/Light"), content);
                CreateButtonWithBackground(26, AppDisplayConstants.MainMenuSet2.Fire, ImageConstants.Background.AdvancedBackground3, Resources.Load<Texture2D>($"UI/Button/Main/Fire"), content);
                CreateButtonWithBackground(27, AppDisplayConstants.MainMenuSet2.Ice, ImageConstants.Background.AdvancedBackground4, Resources.Load<Texture2D>($"UI/Button/Main/Ice"), content);
                CreateButtonWithBackground(28, AppDisplayConstants.MainMenuSet2.Earth, ImageConstants.Background.AdvancedBackground5, Resources.Load<Texture2D>($"UI/Button/Main/Earth"), content);
                CreateButtonWithBackground(29, AppDisplayConstants.MainMenuSet2.Thunder, ImageConstants.Background.AdvancedBackground6, Resources.Load<Texture2D>($"UI/Button/Main/Thunder"), content);
                CreateButtonWithBackground(30, AppDisplayConstants.MainMenuSet2.Life, ImageConstants.Background.AdvancedBackground7, Resources.Load<Texture2D>($"UI/Button/Main/Life"), content);
                CreateButtonWithBackground(31, AppDisplayConstants.MainMenuSet2.Space, ImageConstants.Background.AdvancedBackground8, Resources.Load<Texture2D>($"UI/Button/Main/Space"), content);
                CreateButtonWithBackground(32, AppDisplayConstants.MainMenuSet2.Time, ImageConstants.Background.AdvancedBackground9, Resources.Load<Texture2D>($"UI/Button/Main/Time"), content);

                CreateButtonWithBackground(33, AppDisplayConstants.MainMenuSet2.Nanotech, ImageConstants.Background.AdvancedBackground10, Resources.Load<Texture2D>($"UI/Button/Main/Nanotech"), content);
                CreateButtonWithBackground(34, AppDisplayConstants.MainMenuSet2.Quantum, ImageConstants.Background.AdvancedBackground11, Resources.Load<Texture2D>($"UI/Button/Main/Quantum"), content);
                CreateButtonWithBackground(35, AppDisplayConstants.MainMenuSet2.Holography, ImageConstants.Background.AdvancedBackground12, Resources.Load<Texture2D>($"UI/Button/Main/Holography"), content);
                CreateButtonWithBackground(36, AppDisplayConstants.MainMenuSet2.Plasma, ImageConstants.Background.AdvancedBackground13, Resources.Load<Texture2D>($"UI/Button/Main/Plasma"), content);
                CreateButtonWithBackground(37, AppDisplayConstants.MainMenuSet2.Biomech, ImageConstants.Background.AdvancedBackground14, Resources.Load<Texture2D>($"UI/Button/Main/Biomech"), content);
                CreateButtonWithBackground(38, AppDisplayConstants.MainMenuSet2.Cryotech, ImageConstants.Background.AdvancedBackground15, Resources.Load<Texture2D>($"UI/Button/Main/Cryotech"), content);
                CreateButtonWithBackground(39, AppDisplayConstants.MainMenuSet2.Psionics, ImageConstants.Background.AdvancedBackground16, Resources.Load<Texture2D>($"UI/Button/Main/Psionics"), content);

                CreateButtonWithBackground(40, AppDisplayConstants.MainMenuSet2.Neurotech, ImageConstants.Background.AdvancedBackground17, Resources.Load<Texture2D>($"UI/Button/Main/Neurotech"), content);
                CreateButtonWithBackground(41, AppDisplayConstants.MainMenuSet2.Antimatter, ImageConstants.Background.AdvancedBackground18, Resources.Load<Texture2D>($"UI/Button/Main/Antimatter"), content);
                CreateButtonWithBackground(42, AppDisplayConstants.MainMenuSet2.Phantomware, ImageConstants.Background.AdvancedBackground19, Resources.Load<Texture2D>($"UI/Button/Main/Phantomware"), content);
                CreateButtonWithBackground(43, AppDisplayConstants.MainMenuSet2.Gravitech, ImageConstants.Background.AdvancedBackground20, Resources.Load<Texture2D>($"UI/Button/Main/Gravitech"), content);
                CreateButtonWithBackground(44, AppDisplayConstants.MainMenuSet2.Aethernet, ImageConstants.Background.AdvancedBackground21, Resources.Load<Texture2D>($"UI/Button/Main/Aethernet"), content);
                CreateButtonWithBackground(45, AppDisplayConstants.MainMenuSet2.Starforge, ImageConstants.Background.AdvancedBackground22, Resources.Load<Texture2D>($"UI/Button/Main/Starforge"), content);
                CreateButtonWithBackground(46, AppDisplayConstants.MainMenuSet2.Orbitalis, ImageConstants.Background.AdvancedBackground23, Resources.Load<Texture2D>($"UI/Button/Main/Orbitalis"), content);

                ButtonEvent.Instance.AssignButtonEvent("Button_24", content, () =>
                {
                    FindAnyObjectByType<MainMenuDarkManager>().CreateMainMenuDarkManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_25", content, () =>
                {
                    FindAnyObjectByType<MainMenuLightManager>().CreateMainMenuLightManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_26", content, () =>
                {
                    FindAnyObjectByType<MainMenuFireManager>().CreateMainMenuFireManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_27", content, () =>
                {
                    FindAnyObjectByType<MainMenuIceManager>().CreateMainMenuIceManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_28", content, () =>
                {
                    FindAnyObjectByType<MainMenuEarthManager>().CreateMainMenuEarthManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_29", content, () =>
                {
                    FindAnyObjectByType<MainMenuThunderManager>().CreateMainMenuThunderManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_30", content, () =>
                {
                    FindAnyObjectByType<MainMenuLifeManager>().CreateMainMenuLifeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_31", content, () =>
                {
                    FindAnyObjectByType<MainMenuSpaceManager>().CreateMainMenuSpaceManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_32", content, () =>
                {
                    FindAnyObjectByType<MainMenuTimeManager>().CreateMainMenuTimeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_33", content, () =>
                {
                    FindAnyObjectByType<MainMenuNanotechManager>().CreateMainMenuNanotechManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_34", content, () =>
                {
                    FindAnyObjectByType<MainMenuQuantumManager>().CreateMainMenuQuantumManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_35", content, () =>
                {
                    FindAnyObjectByType<MainMenuHolographyManager>().CreateMainMenuHolographyManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_36", content, () =>
                {
                    FindAnyObjectByType<MainMenuPlasmaManager>().CreateMainMenuPlasmaManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_37", content, () =>
                {
                    FindAnyObjectByType<MainMenuBiomechManager>().CreateMainMenuBiomechManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_38", content, () =>
                {
                    FindAnyObjectByType<MainMenuCryotechManager>().CreateMainMenuCryotechManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_39", content, () =>
                {
                    FindAnyObjectByType<MainMenuPsionicsManager>().CreateMainMenuPsionicsManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_40", content, () =>
                {
                    FindAnyObjectByType<MainMenuNeurotechManager>().CreateMainMenuNeurotechManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_41", content, () =>
                {
                    FindAnyObjectByType<MainMenuAntimatterManager>().CreateMainMenuAntimatterManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_42", content, () =>
                {
                    FindAnyObjectByType<MainMenuPhantomwareManager>().CreateMainMenuPhantomwareManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_43", content, () =>
                {
                    FindAnyObjectByType<MainMenuGravitechManager>().CreateMainMenuGravitechManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_44", content, () =>
                {
                    FindAnyObjectByType<MainMenuAethernetManager>().CreateMainMenuAethernetManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_45", content, () =>
                {
                    FindAnyObjectByType<MainMenuStarforgeManager>().CreateMainMenuStarforgeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_46", content, () =>
                {
                    FindAnyObjectByType<MainMenuOrbitalisManager>().CreateMainMenuOrbitalisManager(data);
                });
            }
            else if (set == 3)
            {
                CreateButtonWithBackground(47, AppDisplayConstants.MainMenuSet3.Azathoth, ImageConstants.Background.AdvancedBackground1, Resources.Load<Texture2D>($"UI/Button/Main/Azathoth"), content);
                CreateButtonWithBackground(48, AppDisplayConstants.MainMenuSet3.YogSothoth, ImageConstants.Background.AdvancedBackground2, Resources.Load<Texture2D>($"UI/Button/Main/Yog-Sothoth"), content);
                CreateButtonWithBackground(49, AppDisplayConstants.MainMenuSet3.Nyarlathotep, ImageConstants.Background.AdvancedBackground3, Resources.Load<Texture2D>($"UI/Button/Main/Nyarlathotep"), content);
                CreateButtonWithBackground(50, AppDisplayConstants.MainMenuSet3.ShubNiggurath, ImageConstants.Background.AdvancedBackground4, Resources.Load<Texture2D>($"UI/Button/Main/Shub-Niggurath"), content);
                CreateButtonWithBackground(51, AppDisplayConstants.MainMenuSet3.Nihorath, ImageConstants.Background.AdvancedBackground5, Resources.Load<Texture2D>($"UI/Button/Main/Nihorath"), content);
                CreateButtonWithBackground(52, AppDisplayConstants.MainMenuSet3.Aeonax, ImageConstants.Background.AdvancedBackground6, Resources.Load<Texture2D>($"UI/Button/Main/Aeonax"), content);
                CreateButtonWithBackground(53, AppDisplayConstants.MainMenuSet3.Seraphiros, ImageConstants.Background.AdvancedBackground7, Resources.Load<Texture2D>($"UI/Button/Main/Seraphiros"), content);
                CreateButtonWithBackground(54, AppDisplayConstants.MainMenuSet3.Thorindar, ImageConstants.Background.AdvancedBackground8, Resources.Load<Texture2D>($"UI/Button/Main/Thorindar"), content);
                CreateButtonWithBackground(55, AppDisplayConstants.MainMenuSet3.Zilthros, ImageConstants.Background.AdvancedBackground9, Resources.Load<Texture2D>($"UI/Button/Main/Zilthros"), content);

                CreateButtonWithBackground(56, AppDisplayConstants.MainMenuSet3.Khorazal, ImageConstants.Background.AdvancedBackground10, Resources.Load<Texture2D>($"UI/Button/Main/Khorazal"), content);
                CreateButtonWithBackground(57, AppDisplayConstants.MainMenuSet3.Ixithra, ImageConstants.Background.AdvancedBackground11, Resources.Load<Texture2D>($"UI/Button/Main/Ixithra"), content);
                CreateButtonWithBackground(58, AppDisplayConstants.MainMenuSet3.Omnitheus, ImageConstants.Background.AdvancedBackground12, Resources.Load<Texture2D>($"UI/Button/Main/Omnitheus"), content);
                CreateButtonWithBackground(59, AppDisplayConstants.MainMenuSet3.Phyrixa, ImageConstants.Background.AdvancedBackground13, Resources.Load<Texture2D>($"UI/Button/Main/Phyrixa"), content);
                CreateButtonWithBackground(60, AppDisplayConstants.MainMenuSet3.Atherion, ImageConstants.Background.AdvancedBackground14, Resources.Load<Texture2D>($"UI/Button/Main/Atherion"), content);
                CreateButtonWithBackground(61, AppDisplayConstants.MainMenuSet3.Vorathos, ImageConstants.Background.AdvancedBackground15, Resources.Load<Texture2D>($"UI/Button/Main/Vorathos"), content);
                CreateButtonWithBackground(62, AppDisplayConstants.MainMenuSet3.Tenebris, ImageConstants.Background.AdvancedBackground16, Resources.Load<Texture2D>($"UI/Button/Main/Tenebris"), content);

                CreateButtonWithBackground(63, AppDisplayConstants.MainMenuSet3.Xylkor, ImageConstants.Background.AdvancedBackground17, Resources.Load<Texture2D>($"UI/Button/Main/Xylkor"), content);
                CreateButtonWithBackground(64, AppDisplayConstants.MainMenuSet3.Veltharion, ImageConstants.Background.AdvancedBackground18, Resources.Load<Texture2D>($"UI/Button/Main/Veltharion"), content);
                CreateButtonWithBackground(65, AppDisplayConstants.MainMenuSet3.Arcanos, ImageConstants.Background.AdvancedBackground19, Resources.Load<Texture2D>($"UI/Button/Main/Arcanos"), content);
                CreateButtonWithBackground(66, AppDisplayConstants.MainMenuSet3.Dolomath, ImageConstants.Background.AdvancedBackground20, Resources.Load<Texture2D>($"UI/Button/Main/Dolomath"), content);
                CreateButtonWithBackground(67, AppDisplayConstants.MainMenuSet3.Arathor, ImageConstants.Background.AdvancedBackground21, Resources.Load<Texture2D>($"UI/Button/Main/Arathor"), content);
                CreateButtonWithBackground(68, AppDisplayConstants.MainMenuSet3.Xyphos, ImageConstants.Background.AdvancedBackground22, Resources.Load<Texture2D>($"UI/Button/Main/Xyphos"), content);
                CreateButtonWithBackground(69, AppDisplayConstants.MainMenuSet3.Vaelith, ImageConstants.Background.AdvancedBackground23, Resources.Load<Texture2D>($"UI/Button/Main/Vaelith"), content);


                ButtonEvent.Instance.AssignButtonEvent("Button_47", content, () =>
                {
                    FindAnyObjectByType<MainMenuAzathothManager>().CreateMainMenuAzathothManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_48", content, () =>
                {
                    FindAnyObjectByType<MainMenuYogSothothManager>().CreateMainMenuYogSothothManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_49", content, () =>
                {
                    FindAnyObjectByType<MainMenuNyarlathotepManager>().CreateMainMenuNyarlathotepManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_50", content, () =>
                {
                    FindAnyObjectByType<MainMenuShubNiggurathManager>().CreateMainMenuShubNiggurathManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_51", content, () =>
                {
                    FindAnyObjectByType<MainMenuNihorathManager>().CreateMainMenuNihorathManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_52", content, () =>
                {
                    FindAnyObjectByType<MainMenuAeonaxManager>().CreateMainMenuAeonaxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_53", content, () =>
                {
                    FindAnyObjectByType<MainMenuSeraphirosManager>().CreateMainMenuSeraphirosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_54", content, () =>
                {
                    FindAnyObjectByType<MainMenuThorindarManager>().CreateMainMenuThorindarManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_55", content, () =>
                {
                    FindAnyObjectByType<MainMenuZilthrosManager>().CreateMainMenuZilthrosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_56", content, () =>
                {
                    FindAnyObjectByType<MainMenuKhorazalManager>().CreateMainMenuKhorazalManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_57", content, () =>
                {
                    FindAnyObjectByType<MainMenuIxithraManager>().CreateMainMenuIxithraManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_58", content, () =>
                {
                    FindAnyObjectByType<MainMenuOmnitheusManager>().CreateMainMenuOmnitheusManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_59", content, () =>
                {
                    FindAnyObjectByType<MainMenuPhyrixaManager>().CreateMainMenuPhyrixaManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_60", content, () =>
                {
                    FindAnyObjectByType<MainMenuAtherionManager>().CreateMainMenuAtherionManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_61", content, () =>
                {
                    FindAnyObjectByType<MainMenuVorathosManager>().CreateMainMenuVorathosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_62", content, () =>
                {
                    FindAnyObjectByType<MainMenuTenebrisManager>().CreateMainMenuTenebrisManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_63", content, () =>
                {
                    FindAnyObjectByType<MainMenuXylkorManager>().CreateMainMenuXylkorManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_64", content, () =>
                {
                    FindAnyObjectByType<MainMenuVeltharionManager>().CreateMainMenuVeltharionManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_65", content, () =>
                {
                    FindAnyObjectByType<MainMenuArcanosManager>().CreateMainMenuArcanosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_66", content, () =>
                {
                    FindAnyObjectByType<MainMenuDolomathManager>().CreateMainMenuDolomathManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_67", content, () =>
                {
                    FindAnyObjectByType<MainMenuArathorManager>().CreateMainMenuArathorManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_68", content, () =>
                {
                    FindAnyObjectByType<MainMenuXyphosManager>().CreateMainMenuXyphosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_69", content, () =>
                {
                    FindAnyObjectByType<MainMenuVaelithManager>().CreateMainMenuVaelithManager(data);
                });
            }
            else if (set == 4)
            {
                CreateButtonWithBackground(70, AppDisplayConstants.MainMenuSet4.Zarx, ImageConstants.Background.AdvancedBackground1, Resources.Load<Texture2D>($"UI/Button/Main/Zarx"), content);
                CreateButtonWithBackground(71, AppDisplayConstants.MainMenuSet4.Raik, ImageConstants.Background.AdvancedBackground2, Resources.Load<Texture2D>($"UI/Button/Main/Raik"), content);
                CreateButtonWithBackground(72, AppDisplayConstants.MainMenuSet4.Drax, ImageConstants.Background.AdvancedBackground3, Resources.Load<Texture2D>($"UI/Button/Main/Drax"), content);
                CreateButtonWithBackground(73, AppDisplayConstants.MainMenuSet4.Kron, ImageConstants.Background.AdvancedBackground4, Resources.Load<Texture2D>($"UI/Button/Main/Kron"), content);
                CreateButtonWithBackground(74, AppDisplayConstants.MainMenuSet4.Zolt, ImageConstants.Background.AdvancedBackground5, Resources.Load<Texture2D>($"UI/Button/Main/Zolt"), content);
                CreateButtonWithBackground(75, AppDisplayConstants.MainMenuSet4.Gorr, ImageConstants.Background.AdvancedBackground6, Resources.Load<Texture2D>($"UI/Button/Main/Gorr"), content);
                CreateButtonWithBackground(76, AppDisplayConstants.MainMenuSet4.Ryze, ImageConstants.Background.AdvancedBackground7, Resources.Load<Texture2D>($"UI/Button/Main/Ryze"), content);
                CreateButtonWithBackground(77, AppDisplayConstants.MainMenuSet4.Jaxx, ImageConstants.Background.AdvancedBackground8, Resources.Load<Texture2D>($"UI/Button/Main/Jaxx"), content);
                CreateButtonWithBackground(78, AppDisplayConstants.MainMenuSet4.Thar, ImageConstants.Background.AdvancedBackground9, Resources.Load<Texture2D>($"UI/Button/Main/Thar"), content);

                CreateButtonWithBackground(79, AppDisplayConstants.MainMenuSet4.Vorn, ImageConstants.Background.AdvancedBackground10, Resources.Load<Texture2D>($"UI/Button/Main/Vorn"), content);
                CreateButtonWithBackground(80, AppDisplayConstants.MainMenuSet4.Nyx, ImageConstants.Background.AdvancedBackground11, Resources.Load<Texture2D>($"UI/Button/Main/Nyx"), content);
                CreateButtonWithBackground(81, AppDisplayConstants.MainMenuSet4.Aros, ImageConstants.Background.AdvancedBackground12, Resources.Load<Texture2D>($"UI/Button/Main/Aros"), content);
                CreateButtonWithBackground(82, AppDisplayConstants.MainMenuSet4.Hex, ImageConstants.Background.AdvancedBackground13, Resources.Load<Texture2D>($"UI/Button/Main/Hex"), content);
                CreateButtonWithBackground(83, AppDisplayConstants.MainMenuSet4.Lorn, ImageConstants.Background.AdvancedBackground14, Resources.Load<Texture2D>($"UI/Button/Main/Lorn"), content);
                CreateButtonWithBackground(84, AppDisplayConstants.MainMenuSet4.Baxx, ImageConstants.Background.AdvancedBackground15, Resources.Load<Texture2D>($"UI/Button/Main/Baxx"), content);
                CreateButtonWithBackground(85, AppDisplayConstants.MainMenuSet4.Zeph, ImageConstants.Background.AdvancedBackground16, Resources.Load<Texture2D>($"UI/Button/Main/Zeph"), content);

                CreateButtonWithBackground(86, AppDisplayConstants.MainMenuSet4.Kael, ImageConstants.Background.AdvancedBackground17, Resources.Load<Texture2D>($"UI/Button/Main/Kael"), content);
                CreateButtonWithBackground(87, AppDisplayConstants.MainMenuSet4.Drav, ImageConstants.Background.AdvancedBackground18, Resources.Load<Texture2D>($"UI/Button/Main/Drav"), content);
                CreateButtonWithBackground(88, AppDisplayConstants.MainMenuSet4.Torn, ImageConstants.Background.AdvancedBackground19, Resources.Load<Texture2D>($"UI/Button/Main/Torn"), content);
                CreateButtonWithBackground(89, AppDisplayConstants.MainMenuSet4.Myrr, ImageConstants.Background.AdvancedBackground20, Resources.Load<Texture2D>($"UI/Button/Main/Myrr"), content);
                CreateButtonWithBackground(90, AppDisplayConstants.MainMenuSet4.Vask, ImageConstants.Background.AdvancedBackground21, Resources.Load<Texture2D>($"UI/Button/Main/Vask"), content);
                CreateButtonWithBackground(91, AppDisplayConstants.MainMenuSet4.Jorr, ImageConstants.Background.AdvancedBackground22, Resources.Load<Texture2D>($"UI/Button/Main/Jorr"), content);
                CreateButtonWithBackground(92, AppDisplayConstants.MainMenuSet4.Quen, ImageConstants.Background.AdvancedBackground23, Resources.Load<Texture2D>($"UI/Button/Main/Quen"), content);

                ButtonEvent.Instance.AssignButtonEvent("Button_70", content, () =>
                {
                    FindAnyObjectByType<MainMenuZarxManager>().CreateMainMenuZarxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_71", content, () =>
                {
                    FindAnyObjectByType<MainMenuRaikManager>().CreateMainMenuRaikManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_72", content, () =>
                {
                    FindAnyObjectByType<MainMenuDraxManager>().CreateMainMenuDraxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_73", content, () =>
                {
                    FindAnyObjectByType<MainMenuKronManager>().CreateMainMenuKronManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_74", content, () =>
                {
                    FindAnyObjectByType<MainMenuZoltManager>().CreateMainMenuZoltManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_75", content, () =>
                {
                    FindAnyObjectByType<MainMenuGorrManager>().CreateMainMenuGorrManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_76", content, () =>
                {
                    FindAnyObjectByType<MainMenuRyzeManager>().CreateMainMenuRyzeManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_77", content, () =>
                {
                    FindAnyObjectByType<MainMenuJaxxManager>().CreateMainMenuJaxxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_78", content, () =>
                {
                    FindAnyObjectByType<MainMenuTharManager>().CreateMainMenuTharManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_79", content, () =>
                {
                    FindAnyObjectByType<MainMenuVornManager>().CreateMainMenuVornManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_80", content, () =>
                {
                    FindAnyObjectByType<MainMenuNyxManager>().CreateMainMenuNyxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_81", content, () =>
                {
                    FindAnyObjectByType<MainMenuArosManager>().CreateMainMenuArosManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_82", content, () =>
                {
                    FindAnyObjectByType<MainMenuHexManager>().CreateMainMenuHexManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_83", content, () =>
                {
                    FindAnyObjectByType<MainMenuLornManager>().CreateMainMenuLornManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_84", content, () =>
                {
                    FindAnyObjectByType<MainMenuBaxxManager>().CreateMainMenuBaxxManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_85", content, () =>
                {
                    FindAnyObjectByType<MainMenuZephManager>().CreateMainMenuZephManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_86", content, () =>
                {
                    FindAnyObjectByType<MainMenuKaelManager>().CreateMainMenuKaelManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_87", content, () =>
                {
                    FindAnyObjectByType<MainMenuDravManager>().CreateMainMenuDravManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_88", content, () =>
                {
                    FindAnyObjectByType<MainMenuTornManager>().CreateMainMenuTornManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_89", content, () =>
                {
                    FindAnyObjectByType<MainMenuMyrrManager>().CreateMainMenuMyrrManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_90", content, () =>
                {
                    FindAnyObjectByType<MainMenuVaskManager>().CreateMainMenuVaskManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_91", content, () =>
                {
                    FindAnyObjectByType<MainMenuJorrManager>().CreateMainMenuJorrManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_92", content, () =>
                {
                    FindAnyObjectByType<MainMenuQuenManager>().CreateMainMenuQuenManager(data);
                });
            }
            else if (set == 5)
            {
                CreateButtonWithBackground(93, AppDisplayConstants.Master.MasterOfBeast, ImageConstants.Background.AdvancedBackground1, Resources.Load<Texture2D>($"UI/Button/Main/Zarx"), content);
                CreateButtonWithBackground(94, AppDisplayConstants.Master.MasterOfDragon, ImageConstants.Background.AdvancedBackground2, Resources.Load<Texture2D>($"UI/Button/Main/Raik"), content);
                CreateButtonWithBackground(95, AppDisplayConstants.Master.MasterOfMagic, ImageConstants.Background.AdvancedBackground3, Resources.Load<Texture2D>($"UI/Button/Main/Drax"), content);
                CreateButtonWithBackground(96, AppDisplayConstants.Master.MasterOfMusic, ImageConstants.Background.AdvancedBackground4, Resources.Load<Texture2D>($"UI/Button/Main/Kron"), content);
                CreateButtonWithBackground(97, AppDisplayConstants.Master.MasterOfScience, ImageConstants.Background.AdvancedBackground5, Resources.Load<Texture2D>($"UI/Button/Main/Zolt"), content);
                CreateButtonWithBackground(98, AppDisplayConstants.Master.MasterOfSpirit, ImageConstants.Background.AdvancedBackground6, Resources.Load<Texture2D>($"UI/Button/Main/Gorr"), content);
                CreateButtonWithBackground(99, AppDisplayConstants.Master.MasterOfWeapon, ImageConstants.Background.AdvancedBackground7, Resources.Load<Texture2D>($"UI/Button/Main/Ryze"), content);
                CreateButtonWithBackground(100, AppDisplayConstants.Master.MasterOfChemical, ImageConstants.Background.AdvancedBackground8, Resources.Load<Texture2D>($"UI/Button/Main/Jaxx"), content);
                CreateButtonWithBackground(101, AppDisplayConstants.Master.MasterOfPhysical, ImageConstants.Background.AdvancedBackground9, Resources.Load<Texture2D>($"UI/Button/Main/Thar"), content);

                CreateButtonWithBackground(102, AppDisplayConstants.Master.MasterOfAtomic, ImageConstants.Background.AdvancedBackground10, Resources.Load<Texture2D>($"UI/Button/Main/Vorn"), content);
                CreateButtonWithBackground(103, AppDisplayConstants.Master.MasterOfMental, ImageConstants.Background.AdvancedBackground11, Resources.Load<Texture2D>($"UI/Button/Main/Nyx"), content);
                // CreateButtonWithBackground(104, AppDisplayConstants.MainMenuSet4.Aros, ImageConstants.Background.AdvancedBackground12, Resources.Load<Texture2D>($"UI/Button/Main/Aros"), content);
                // CreateButtonWithBackground(105, AppDisplayConstants.MainMenuSet4.Hex, ImageConstants.Background.AdvancedBackground13, Resources.Load<Texture2D>($"UI/Button/Main/Hex"), content);
                // CreateButtonWithBackground(83, AppDisplayConstants.MainMenuSet4.Lorn, ImageConstants.Background.AdvancedBackground14, Resources.Load<Texture2D>($"UI/Button/Main/Lorn"), content);
                // CreateButtonWithBackground(84, AppDisplayConstants.MainMenuSet4.Baxx, ImageConstants.Background.AdvancedBackground15, Resources.Load<Texture2D>($"UI/Button/Main/Baxx"), content);
                // CreateButtonWithBackground(85, AppDisplayConstants.MainMenuSet4.Zeph, ImageConstants.Background.AdvancedBackground16, Resources.Load<Texture2D>($"UI/Button/Main/Zeph"), content);

                // CreateButtonWithBackground(86, AppDisplayConstants.MainMenuSet4.Kael, ImageConstants.Background.AdvancedBackground17, Resources.Load<Texture2D>($"UI/Button/Main/Kael"), content);
                // CreateButtonWithBackground(87, AppDisplayConstants.MainMenuSet4.Drav, ImageConstants.Background.AdvancedBackground18, Resources.Load<Texture2D>($"UI/Button/Main/Drav"), content);
                // CreateButtonWithBackground(88, AppDisplayConstants.MainMenuSet4.Torn, ImageConstants.Background.AdvancedBackground19, Resources.Load<Texture2D>($"UI/Button/Main/Torn"), content);
                // CreateButtonWithBackground(89, AppDisplayConstants.MainMenuSet4.Myrr, ImageConstants.Background.AdvancedBackground20, Resources.Load<Texture2D>($"UI/Button/Main/Myrr"), content);
                // CreateButtonWithBackground(90, AppDisplayConstants.MainMenuSet4.Vask, ImageConstants.Background.AdvancedBackground21, Resources.Load<Texture2D>($"UI/Button/Main/Vask"), content);
                // CreateButtonWithBackground(91, AppDisplayConstants.MainMenuSet4.Jorr, ImageConstants.Background.AdvancedBackground22, Resources.Load<Texture2D>($"UI/Button/Main/Jorr"), content);
                // CreateButtonWithBackground(92, AppDisplayConstants.MainMenuSet4.Quen, ImageConstants.Background.AdvancedBackground23, Resources.Load<Texture2D>($"UI/Button/Main/Quen"), content);

                ButtonEvent.Instance.AssignButtonEvent("Button_93", content, () =>
                {
                    MasterOfBeastManager.Instance.CreateMasterOfBeastManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_94", content, () =>
                {
                    MasterOfDragonManager.Instance.CreateMasterOfDragonManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_95", content, () =>
                {
                    MasterOfMagicManager.Instance.CreateMasterOfMagicManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_96", content, () =>
                {
                    MasterOfMusicManager.Instance.CreateMasterOfMusicManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_97", content, () =>
                {
                    MasterOfScienceManager.Instance.CreateMasterOfScienceManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_98", content, () =>
                {
                    MasterOfSpiritManager.Instance.CreateMasterOfSpiritManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_99", content, () =>
                {
                    MasterOfWeaponManager.Instance.CreateMasterOfWeaponManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_100", content, () =>
                {
                    MasterOfChemicalManager.Instance.CreateMasterOfChemicalManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_101", content, () =>
                {
                    MasterOfPhysicalManager.Instance.CreateMasterOfPhysicalManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_102", content, () =>
                {
                    MasterOfAtomicManager.Instance.CreateMasterOfAtomicManager(data);
                });
                ButtonEvent.Instance.AssignButtonEvent("Button_103", content, () =>
                {
                    MasterOfMentalManager.Instance.CreateMasterOfMentalManager(data);
                });
                // ButtonEvent.Instance.AssignButtonEvent("Button_81", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuArosManager>().CreateMainMenuArosManager(data);
                // });
                // ButtonEvent.Instance.AssignButtonEvent("Button_82", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuHexManager>().CreateMainMenuHexManager(data);
                // });
                // ButtonEvent.Instance.AssignButtonEvent("Button_83", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuLornManager>().CreateMainMenuLornManager(data);
                // });
                // ButtonEvent.Instance.AssignButtonEvent("Button_84", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuBaxxManager>().CreateMainMenuBaxxManager(data);
                // });
                // ButtonEvent.Instance.AssignButtonEvent("Button_85", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuZephManager>().CreateMainMenuZephManager(data);
                // });
                // ButtonEvent.Instance.AssignButtonEvent("Button_86", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuKaelManager>().CreateMainMenuKaelManager(data);
                // });
                // ButtonEvent.Instance.AssignButtonEvent("Button_87", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuDravManager>().CreateMainMenuDravManager(data);
                // });
                // ButtonEvent.Instance.AssignButtonEvent("Button_88", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuTornManager>().CreateMainMenuTornManager(data);
                // });
                // ButtonEvent.Instance.AssignButtonEvent("Button_89", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuMyrrManager>().CreateMainMenuMyrrManager(data);
                // });
                // ButtonEvent.Instance.AssignButtonEvent("Button_90", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuVaskManager>().CreateMainMenuVaskManager(data);
                // });
                // ButtonEvent.Instance.AssignButtonEvent("Button_91", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuJorrManager>().CreateMainMenuJorrManager(data);
                // });
                // ButtonEvent.Instance.AssignButtonEvent("Button_92", content, () =>
                // {
                //     FindAnyObjectByType<MainMenuQuenManager>().CreateMainMenuQuenManager(data);
                // });
            }
        }
        else if (data is Equipments equipments)
        {
            // CreateButtonWithBackground(1, AppDisplayConstants.MainMenuSet1.Equipments, ImageConstants.Background.AdvancedBackground1, Resources.Load<Texture2D>($"UI/Button/Main/Equipments"), content);
            CreateButtonWithBackground(2, AppDisplayConstants.MainMenuSet1.Realm, ImageConstants.Background.AdvancedBackground2, Resources.Load<Texture2D>($"UI/Button/Main/Realm"), content);
            CreateButtonWithBackground(3, AppDisplayConstants.MainMenuSet1.Upgrade, ImageConstants.Background.AdvancedBackground3, Resources.Load<Texture2D>($"UI/Button/Main/Upgrade"), content);
            CreateButtonWithBackground(4, AppDisplayConstants.MainMenuSet1.Aptitude, ImageConstants.Background.AdvancedBackground4, Resources.Load<Texture2D>($"UI/Button/Main/Aptitude"), content);
            // CreateButtonWithBackground(5, AppDisplayConstants.MainMenuSet1.Affinity, ImageConstants.Background.AdvancedBackground5, Resources.Load<Texture2D>($"UI/Button/Main/Affinity"), content);
            CreateButtonWithBackground(6, AppDisplayConstants.MainMenuSet1.Blessing, ImageConstants.Background.AdvancedBackground6, Resources.Load<Texture2D>($"UI/Button/Main/Blessing"), content);
            CreateButtonWithBackground(7, AppDisplayConstants.MainMenuSet1.Core, ImageConstants.Background.AdvancedBackground7, Resources.Load<Texture2D>($"UI/Button/Main/Core"), content);
            CreateButtonWithBackground(8, AppDisplayConstants.MainMenuSet1.Physique, ImageConstants.Background.AdvancedBackground8, Resources.Load<Texture2D>($"UI/Button/Main/Physique"), content);
            CreateButtonWithBackground(9, AppDisplayConstants.MainMenuSet1.Bloodline, ImageConstants.Background.AdvancedBackground9, Resources.Load<Texture2D>($"UI/Button/Main/Bloodline"), content);

            CreateButtonWithBackground(10, AppDisplayConstants.MainMenuSet1.Omnivision, ImageConstants.Background.AdvancedBackground10, Resources.Load<Texture2D>($"UI/Button/Main/Omnivision"), content);
            CreateButtonWithBackground(11, AppDisplayConstants.MainMenuSet1.Omnipotence, ImageConstants.Background.AdvancedBackground11, Resources.Load<Texture2D>($"UI/Button/Main/Omnipotence"), content);
            CreateButtonWithBackground(12, AppDisplayConstants.MainMenuSet1.Omnipresence, ImageConstants.Background.AdvancedBackground12, Resources.Load<Texture2D>($"UI/Button/Main/Omnipresence"), content);
            CreateButtonWithBackground(13, AppDisplayConstants.MainMenuSet1.Omniscience, ImageConstants.Background.AdvancedBackground13, Resources.Load<Texture2D>($"UI/Button/Main/Omniscience"), content);
            CreateButtonWithBackground(14, AppDisplayConstants.MainMenuSet1.Omnivory, ImageConstants.Background.AdvancedBackground14, Resources.Load<Texture2D>($"UI/Button/Main/Omnivory"), content);
            CreateButtonWithBackground(15, AppDisplayConstants.MainMenuSet1.Angel, ImageConstants.Background.AdvancedBackground15, Resources.Load<Texture2D>($"UI/Button/Main/Angel"), content);
            CreateButtonWithBackground(16, AppDisplayConstants.MainMenuSet1.Demon, ImageConstants.Background.AdvancedBackground16, Resources.Load<Texture2D>($"UI/Button/Main/Demon"), content);

            CreateButtonWithBackground(17, AppDisplayConstants.MainMenuSet1.Sword, ImageConstants.Background.AdvancedBackground17, Resources.Load<Texture2D>($"UI/Button/Main/Sword"), content);
            CreateButtonWithBackground(18, AppDisplayConstants.MainMenuSet1.Spear, ImageConstants.Background.AdvancedBackground18, Resources.Load<Texture2D>($"UI/Button/Main/Spear"), content);
            CreateButtonWithBackground(19, AppDisplayConstants.MainMenuSet1.Shield, ImageConstants.Background.AdvancedBackground19, Resources.Load<Texture2D>($"UI/Button/Main/Shield"), content);
            CreateButtonWithBackground(20, AppDisplayConstants.MainMenuSet1.Bow, ImageConstants.Background.AdvancedBackground20, Resources.Load<Texture2D>($"UI/Button/Main/Bow"), content);
            CreateButtonWithBackground(21, AppDisplayConstants.MainMenuSet1.Gun, ImageConstants.Background.AdvancedBackground21, Resources.Load<Texture2D>($"UI/Button/Main/Gun"), content);
            CreateButtonWithBackground(22, AppDisplayConstants.MainMenuSet1.Cyber, ImageConstants.Background.AdvancedBackground22, Resources.Load<Texture2D>($"UI/Button/Main/Cyber"), content);
            CreateButtonWithBackground(23, AppDisplayConstants.MainMenuSet1.Fairy, ImageConstants.Background.AdvancedBackground23, Resources.Load<Texture2D>($"UI/Button/Main/Fairy"), content);

            // ButtonEvent.Instance.AssignButtonEvent("Button_1", content, () =>
            // {
            //     FindAnyObjectByType<MainMenuEquipmentManager>().CreateMainMenuEquipmentManager(data);
            // });
            ButtonEvent.Instance.AssignButtonEvent("Button_2", content, () =>
            {
                FindAnyObjectByType<MainMenuRealmManager>().CreateMainMenuRealmManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_3", content, () =>
            {
                FindAnyObjectByType<MainMenuUpgradeManager>().CreateMainMenuUpgradeManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_4", content, () =>
            {
                FindAnyObjectByType<MainMenuAptitudeManager>().CreateMainMenuAptitudeManager(data);
            });
            // ButtonEvent.Instance.AssignButtonEvent("Button_5", content, () =>
            // {
            //     FindAnyObjectByType<MainMenuAffinityManager>().CreateMainMenuAffinityManager(data);
            // });
            ButtonEvent.Instance.AssignButtonEvent("Button_6", content, () =>
            {
                FindAnyObjectByType<MainMenuBlessingManager>().CreateMainMenuBlessingManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_7", content, () =>
            {
                FindAnyObjectByType<MainMenuCoreManager>().CreateMainMenuCoreManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_8", content, () =>
            {
                FindAnyObjectByType<MainMenuPhysiqueManager>().CreateMainMenuPhysiqueManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_9", content, () =>
            {
                FindAnyObjectByType<MainMenuBloodlineManager>().CreateMainMenuBloodlineManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_10", content, () =>
            {
                FindAnyObjectByType<MainMenuOmnivisionManager>().CreateMainMenuOmnivisionManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_11", content, () =>
            {
                FindAnyObjectByType<MainMenuOmnipotenceManager>().CreateMainMenuOmnipotenceManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_12", content, () =>
            {
                FindAnyObjectByType<MainMenuOmnipresenceManager>().CreateMainMenuOmnipresenceManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_13", content, () =>
            {
                FindAnyObjectByType<MainMenuOmniscienceManager>().CreateMainMenuOmniscienceManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_14", content, () =>
            {
                FindAnyObjectByType<MainMenuOmnivoryManager>().CreateMainMenuOmnivoryManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_15", content, () =>
            {
                FindAnyObjectByType<MainMenuAngelManager>().CreateMainMenuAngelManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_16", content, () =>
            {
                FindAnyObjectByType<MainMenuDemonManager>().CreateMainMenuDemonManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_17", content, () =>
            {
                FindAnyObjectByType<MainMenuSwordManager>().CreateMainMenuSwordManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_18", content, () =>
            {
                FindAnyObjectByType<MainMenuSpearManager>().CreateMainMenuSpearManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_19", content, () =>
            {
                FindAnyObjectByType<MainMenuShieldManager>().CreateMainMenuShieldManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_20", content, () =>
            {
                FindAnyObjectByType<MainMenuBowManager>().CreateMainMenuBowManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_21", content, () =>
            {
                FindAnyObjectByType<MainMenuGunManager>().CreateMainMenuGunManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_22", content, () =>
            {
                FindAnyObjectByType<MainMenuCyberManager>().CreateMainMenuCyberManager(data);
            });
            ButtonEvent.Instance.AssignButtonEvent("Button_23", content, () =>
            {
                FindAnyObjectByType<MainMenuFairyManager>().CreateMainMenuFairyManager(data);
            });
        }
        content.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
