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
    private GameObject ArenaButtonPrefab;
    private GameObject AnimeButtonPrefab;
    private GameObject ReactorButtonPrefab;
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
        mainMenuSubButtonGroupPanel = UIManager.Instance.GetTransform("mainMenuSubButtonGroupPanel");
        summonPanel = UIManager.Instance.GetTransform("summonPanel");
        TabButton4 = UIManager.Instance.GetGameObject("TabButton4");
        TabButton3 = UIManager.Instance.GetGameObject("TabButton3");
        ArenaButtonPrefab = UIManager.Instance.GetGameObject("ArenaButtonPrefab");
        AnimeButtonPrefab = UIManager.Instance.GetGameObject("AnimeButtonPrefab");
        ReactorButtonPrefab = UIManager.Instance.GetGameObjectScienceFiction("ReactorButtonPrefab");

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
        CreateAnimeButtonUI(AppDisplayConstants.Anime.OnePiece, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/One Piece"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.Naruto, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Naruto"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.DragonBall, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Dragon Ball"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.FairyTail, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Fairy Tail"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.SwordArtOnline, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Sword Art Online"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.DemonSlayer, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Demon Slayer"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.Bleach, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Bleach"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.JujutsuKaisen, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Jujutsu Kaisen"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.BlackClover, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Black Clover"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.HunterXHunter, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/Hunter x Hunter"), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.OnePunchMan, backgroundImage, Resources.Load<Texture2D>($"UI/Button/Anime/One Punch Man"), animeMenuPanel);

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
                int index = i;
                GameObject button = Instantiate(buttonPrefab, buttonPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = (index + 1).ToString();

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    set = index + 1;
                    foreach (Transform child in buttonPanel)
                    {
                        Button button = child.GetComponent<Button>();
                        if (button != null)
                        {
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
                CreateButtonWithBackground(1, AppConstants.MainMenuSet1.Equipments, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Equipments"), buttonGroupPanel1);
                CreateButtonWithBackground(2, AppConstants.MainMenuSet1.Realm, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Realm"), buttonGroupPanel1);
                CreateButtonWithBackground(3, AppConstants.MainMenuSet1.Upgrade, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Upgrade"), buttonGroupPanel1);
                CreateButtonWithBackground(4, AppConstants.MainMenuSet1.Aptitude, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Aptitude"), buttonGroupPanel1);
                CreateButtonWithBackground(5, AppConstants.MainMenuSet1.Affinity, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Affinity"), buttonGroupPanel1);
                CreateButtonWithBackground(6, AppConstants.MainMenuSet1.Blessing, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Blessing"), buttonGroupPanel1);
                CreateButtonWithBackground(7, AppConstants.MainMenuSet1.Core, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Core"), buttonGroupPanel1);
                CreateButtonWithBackground(8, AppConstants.MainMenuSet1.Physique, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Physique"), buttonGroupPanel1);
                CreateButtonWithBackground(9, AppConstants.MainMenuSet1.Bloodline, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Bloodline"), buttonGroupPanel1);

                CreateButtonWithBackground(10, AppConstants.MainMenuSet1.Omnivision, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Omnivision"), buttonGroupPanel2);
                CreateButtonWithBackground(11, AppConstants.MainMenuSet1.Omnipotence, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Omnipotence"), buttonGroupPanel2);
                CreateButtonWithBackground(12, AppConstants.MainMenuSet1.Omnipresence, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Omnipresence"), buttonGroupPanel2);
                CreateButtonWithBackground(13, AppConstants.MainMenuSet1.Omniscience, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Omniscience"), buttonGroupPanel2);
                CreateButtonWithBackground(14, AppConstants.MainMenuSet1.Omnivory, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Omnivory"), buttonGroupPanel2);
                CreateButtonWithBackground(15, AppConstants.MainMenuSet1.Angel, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Angel"), buttonGroupPanel2);
                CreateButtonWithBackground(16, AppConstants.MainMenuSet1.Demon, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Demon"), buttonGroupPanel2);

                CreateButtonWithBackground(17, AppConstants.MainMenuSet1.Sword, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Sword"), buttonGroupPanel3);
                CreateButtonWithBackground(18, AppConstants.MainMenuSet1.Spear, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Spear"), buttonGroupPanel3);
                CreateButtonWithBackground(19, AppConstants.MainMenuSet1.Shield, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Shield"), buttonGroupPanel3);
                CreateButtonWithBackground(20, AppConstants.MainMenuSet1.Bow, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Bow"), buttonGroupPanel3);
                CreateButtonWithBackground(21, AppConstants.MainMenuSet1.Gun, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Gun"), buttonGroupPanel3);
                CreateButtonWithBackground(22, AppConstants.MainMenuSet1.Cyber, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Cyber"), buttonGroupPanel3);
                CreateButtonWithBackground(23, AppConstants.MainMenuSet1.Fairy, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Fairy"), buttonGroupPanel3);


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
                CreateButtonWithBackground(24, AppConstants.MainMenuSet2.Dark, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Dark"), buttonGroupPanel1);
                CreateButtonWithBackground(25, AppConstants.MainMenuSet2.Light, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Light"), buttonGroupPanel1);
                CreateButtonWithBackground(26, AppConstants.MainMenuSet2.Fire, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Fire"), buttonGroupPanel1);
                CreateButtonWithBackground(27, AppConstants.MainMenuSet2.Ice, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Ice"), buttonGroupPanel1);
                CreateButtonWithBackground(28, AppConstants.MainMenuSet2.Earth, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Earth"), buttonGroupPanel1);
                CreateButtonWithBackground(29, AppConstants.MainMenuSet2.Thunder, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Thunder"), buttonGroupPanel1);
                CreateButtonWithBackground(30, AppConstants.MainMenuSet2.Life, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Life"), buttonGroupPanel1);
                CreateButtonWithBackground(31, AppConstants.MainMenuSet2.Space, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Space"), buttonGroupPanel1);
                CreateButtonWithBackground(32, AppConstants.MainMenuSet2.Time, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Time"), buttonGroupPanel1);

                CreateButtonWithBackground(33, AppConstants.MainMenuSet2.Nanotech, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Nanotech"), buttonGroupPanel2);
                CreateButtonWithBackground(34, AppConstants.MainMenuSet2.Quantum, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Quantum"), buttonGroupPanel2);
                CreateButtonWithBackground(35, AppConstants.MainMenuSet2.Holography, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Holography"), buttonGroupPanel2);
                CreateButtonWithBackground(36, AppConstants.MainMenuSet2.Plasma, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Plasma"), buttonGroupPanel2);
                CreateButtonWithBackground(37, AppConstants.MainMenuSet2.Biomech, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Biomech"), buttonGroupPanel2);
                CreateButtonWithBackground(38, AppConstants.MainMenuSet2.Cryotech, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Cryotech"), buttonGroupPanel2);
                CreateButtonWithBackground(39, AppConstants.MainMenuSet2.Psionics, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Psionics"), buttonGroupPanel2);

                CreateButtonWithBackground(40, AppConstants.MainMenuSet2.Neurotech, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Neurotech"), buttonGroupPanel3);
                CreateButtonWithBackground(41, AppConstants.MainMenuSet2.Antimatter, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Antimatter"), buttonGroupPanel3);
                CreateButtonWithBackground(42, AppConstants.MainMenuSet2.Phantomware, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Phantomware"), buttonGroupPanel3);
                CreateButtonWithBackground(43, AppConstants.MainMenuSet2.Gravitech, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Gravitech"), buttonGroupPanel3);
                CreateButtonWithBackground(44, AppConstants.MainMenuSet2.Aethernet, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Aethernet"), buttonGroupPanel3);
                CreateButtonWithBackground(45, AppConstants.MainMenuSet2.Starforge, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Starforge"), buttonGroupPanel3);
                CreateButtonWithBackground(46, AppConstants.MainMenuSet2.Orbitalis, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Orbitalis"), buttonGroupPanel3);

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
                CreateButtonWithBackground(47, AppConstants.MainMenuSet3.Azathoth, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Azathoth"), buttonGroupPanel1);
                CreateButtonWithBackground(48, AppConstants.MainMenuSet3.YogSothoth, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Yog-Sothoth"), buttonGroupPanel1);
                CreateButtonWithBackground(49, AppConstants.MainMenuSet3.Nyarlathotep, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Nyarlathotep"), buttonGroupPanel1);
                CreateButtonWithBackground(50, AppConstants.MainMenuSet3.ShubNiggurath, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Shub-Niggurath"), buttonGroupPanel1);
                CreateButtonWithBackground(51, AppConstants.MainMenuSet3.Nihorath, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Nihorath"), buttonGroupPanel1);
                CreateButtonWithBackground(52, AppConstants.MainMenuSet3.Aeonax, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Aeonax"), buttonGroupPanel1);
                CreateButtonWithBackground(53, AppConstants.MainMenuSet3.Seraphiros, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Seraphiros"), buttonGroupPanel1);
                CreateButtonWithBackground(54, AppConstants.MainMenuSet3.Thorindar, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Thorindar"), buttonGroupPanel1);
                CreateButtonWithBackground(55, AppConstants.MainMenuSet3.Zilthros, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Zilthros"), buttonGroupPanel1);

                CreateButtonWithBackground(56, AppConstants.MainMenuSet3.Khorazal, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Khorazal"), buttonGroupPanel2);
                CreateButtonWithBackground(57, AppConstants.MainMenuSet3.Ixithra, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Ixithra"), buttonGroupPanel2);
                CreateButtonWithBackground(58, AppConstants.MainMenuSet3.Omnitheus, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Omnitheus"), buttonGroupPanel2);
                CreateButtonWithBackground(59, AppConstants.MainMenuSet3.Phyrixa, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Phyrixa"), buttonGroupPanel2);
                CreateButtonWithBackground(60, AppConstants.MainMenuSet3.Atherion, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Atherion"), buttonGroupPanel2);
                CreateButtonWithBackground(61, AppConstants.MainMenuSet3.Vorathos, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Vorathos"), buttonGroupPanel2);
                CreateButtonWithBackground(62, AppConstants.MainMenuSet3.Tenebris, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Tenebris"), buttonGroupPanel2);

                CreateButtonWithBackground(63, AppConstants.MainMenuSet3.Xylkor, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Xylkor"), buttonGroupPanel3);
                CreateButtonWithBackground(64, AppConstants.MainMenuSet3.Veltharion, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Veltharion"), buttonGroupPanel3);
                CreateButtonWithBackground(65, AppConstants.MainMenuSet3.Arcanos, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Arcanos"), buttonGroupPanel3);
                CreateButtonWithBackground(66, AppConstants.MainMenuSet3.Dolomath, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Dolomath"), buttonGroupPanel3);
                CreateButtonWithBackground(67, AppConstants.MainMenuSet3.Arathor, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Arathor"), buttonGroupPanel3);
                CreateButtonWithBackground(68, AppConstants.MainMenuSet3.Xyphos, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Xyphos"), buttonGroupPanel3);
                CreateButtonWithBackground(69, AppConstants.MainMenuSet3.Vaelith, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Vaelith"), buttonGroupPanel3);


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
                CreateButtonWithBackground(70, AppConstants.MainMenuSet4.Zarx, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Zarx"), buttonGroupPanel1);
                CreateButtonWithBackground(71, AppConstants.MainMenuSet4.Raik, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Raik"), buttonGroupPanel1);
                CreateButtonWithBackground(72, AppConstants.MainMenuSet4.Drax, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Drax"), buttonGroupPanel1);
                CreateButtonWithBackground(73, AppConstants.MainMenuSet4.Kron, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Kron"), buttonGroupPanel1);
                CreateButtonWithBackground(74, AppConstants.MainMenuSet4.Zolt, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Zolt"), buttonGroupPanel1);
                CreateButtonWithBackground(75, AppConstants.MainMenuSet4.Gorr, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Gorr"), buttonGroupPanel1);
                CreateButtonWithBackground(76, AppConstants.MainMenuSet4.Ryze, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Ryze"), buttonGroupPanel1);
                CreateButtonWithBackground(77, AppConstants.MainMenuSet4.Jaxx, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Jaxx"), buttonGroupPanel1);
                CreateButtonWithBackground(78, AppConstants.MainMenuSet4.Thar, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Thar"), buttonGroupPanel1);

                CreateButtonWithBackground(79, AppConstants.MainMenuSet4.Vorn, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Vorn"), buttonGroupPanel2);
                CreateButtonWithBackground(80, AppConstants.MainMenuSet4.Nyx, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Nyx"), buttonGroupPanel2);
                CreateButtonWithBackground(81, AppConstants.MainMenuSet4.Aros, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Aros"), buttonGroupPanel2);
                CreateButtonWithBackground(82, AppConstants.MainMenuSet4.Hex, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Hex"), buttonGroupPanel2);
                CreateButtonWithBackground(83, AppConstants.MainMenuSet4.Lorn, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Lorn"), buttonGroupPanel2);
                CreateButtonWithBackground(84, AppConstants.MainMenuSet4.Baxx, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Baxx"), buttonGroupPanel2);
                CreateButtonWithBackground(85, AppConstants.MainMenuSet4.Zeph, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Zeph"), buttonGroupPanel2);

                CreateButtonWithBackground(86, AppConstants.MainMenuSet4.Kael, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Kael"), buttonGroupPanel3);
                CreateButtonWithBackground(87, AppConstants.MainMenuSet4.Drav, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Drav"), buttonGroupPanel3);
                CreateButtonWithBackground(88, AppConstants.MainMenuSet4.Torn, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Torn"), buttonGroupPanel3);
                CreateButtonWithBackground(89, AppConstants.MainMenuSet4.Myrr, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Myrr"), buttonGroupPanel3);
                CreateButtonWithBackground(90, AppConstants.MainMenuSet4.Vask, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Vask"), buttonGroupPanel3);
                CreateButtonWithBackground(91, AppConstants.MainMenuSet4.Jorr, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Jorr"), buttonGroupPanel3);
                CreateButtonWithBackground(92, AppConstants.MainMenuSet4.Quen, backgroundImage2, Resources.Load<Texture2D>($"UI/Button/Main/Quen"), buttonGroupPanel3);

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
        buttonGroupPanel1.gameObject.AddComponent<SlideBottomToTopAnimation>();
        buttonGroupPanel2.gameObject.AddComponent<SlideRightToLeftAnimation>();
        buttonGroupPanel3.gameObject.AddComponent<SlideLeftToRightAnimation>();
    }
}
