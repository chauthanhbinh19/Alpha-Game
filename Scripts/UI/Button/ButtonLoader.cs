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
        CreateButton(1, "Campaigns", Resources.Load<Texture2D>($"UI/Background4/Background_V4_110"), Resources.Load<Texture2D>($"UI/UI/Campaign"), mainMenuCampaignPanel);
        //Main menu
        CreateButton(1, "Card Heroes", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Cards"), mainMenuButtonPanel);
        CreateButton(2, "Books", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Books"), mainMenuButtonPanel);
        CreateButton(3, "Pets", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Pets"), mainMenuButtonPanel);
        CreateButton(4, "Card Captains", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Captains"), mainMenuButtonPanel);
        CreateButton(5, "Card Colonels", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/philosophies_of_conflict"), mainMenuButtonPanel);
        CreateButton(6, "Card Generals", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/philosophies_of_contention"), mainMenuButtonPanel);
        CreateButton(7, "Card Admirals", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/philosophies_of_diligence"), mainMenuButtonPanel);
        CreateButton(8, "Collaboration Equipments", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/CollaborationEquipments"), mainMenuButtonPanel);
        CreateButton(9, "Card Military", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Military"), mainMenuButtonPanel);
        CreateButton(10, "Card Spell", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Spell"), mainMenuButtonPanel);
        CreateButton(11, "Collaborations", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Collaboration"), mainMenuButtonPanel);
        CreateButton(12, "Card Monsters", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Monsters"), mainMenuButtonPanel);
        // CreateButton(13, "Equipments",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/equipments"), mainMenuButtonPanel);
        CreateButton(14, "Medals", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Bag"), mainMenuButtonPanel);
        CreateButton(15, "Skills", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Skills"), mainMenuButtonPanel);
        CreateButton(16, "Symbols", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Symbols"), mainMenuButtonPanel);
        CreateButton(17, "Titles", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Titles"), mainMenuButtonPanel);
        CreateButton(18, "Bag", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Bag"), mainMenuButtonPanel);
        CreateButton(19, "Teams", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Teams"), mainMenuButtonPanel);
        CreateButton(20, "Magic Formation Circle", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/MagicFormationCircle"), summonPanel);
        CreateButton(21, "Relics", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Relics"), summonPanel);
        CreateButton(22, "Talisman", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Talisman"), summonPanel);
        CreateButton(23, "Puppet", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Puppet"), summonPanel);
        CreateButton(24, "Alchemy", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Alchemy"), summonPanel);
        CreateButton(25, "Forge", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Forge"), summonPanel);
        CreateButton(26, "Card Life", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Life"), summonPanel);

        CreateButton(27, "Summon Card Heroes", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/SummonCards"), summonPanel);
        CreateButton(28, "Summon Books", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/SummonBooks"), summonPanel);
        CreateButton(29, "Summon CardCaptains", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/SummonCaptains"), summonPanel);
        CreateButton(30, "Summon Card Monsters", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/SummonMonsters"), summonPanel);
        CreateButton(31, "Summon Card Military", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/SummonMilitary"), summonPanel);
        CreateButton(32, "Summon Card Spell", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/SummonSpell"), summonPanel);
        CreateButton(33, "Summon Card Colonels", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/SummonColonels"), summonPanel);
        CreateButton(34, "Summon Card Generals", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/SummonGenerals"), summonPanel);
        CreateButton(35, "Summon Card Admirals", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/SummonAdmirals"), summonPanel);
        CreateButton(36, "Shop", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Shop"), summonPanel);
        CreateButton(37, "Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Gallery"), summonPanel);
        CreateButton(38, "Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Collection"), summonPanel);
        CreateButton(39, "Equipments", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Legend_Equipment"), summonPanel);
        CreateButton(40, "Anime", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Lobby_Icon-That_Time_I_Got_Reincarnated_as_a_Slime_Collab"), summonPanel);
        CreateButton(41, "Arena", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Arena"), summonPanel);
        CreateButton(42, "Guild", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Guild"), summonPanel);
        CreateButton(43, "Tower", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Tower"), summonPanel);
        CreateButton(44, "Event", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/UI/Lobby_Icon-Event_002"), summonPanel);
    }

    // Update is called once per frame
    void Update()
    {

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
            nameText.text = itemName;
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
        CreateButton(1, "Card Heroes Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/CardsGallery"), galleryMenuPanel);
        CreateButton(2, "Books Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/BooksGallery"), galleryMenuPanel);
        CreateButton(3, "Pets Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/PetsGallery"), galleryMenuPanel);
        CreateButton(4, "Card Captains Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/CaptainsGallery"), galleryMenuPanel);
        CreateButton(5, "Collaboration Equipments Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsGallery"), galleryMenuPanel);
        CreateButton(6, "Card Military Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/MilitaryGallery"), galleryMenuPanel);
        CreateButton(7, "Card Spell Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/SpellGallery"), galleryMenuPanel);
        CreateButton(8, "Collaborations Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/CollaborationsGallery"), galleryMenuPanel);
        CreateButton(9, "Card Monsters Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/MonstersGallery"), galleryMenuPanel);
        CreateButton(10, "Equipments Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/EquipmentsGallery"), galleryMenuPanel);
        CreateButton(11, "Medals Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/MedalsGallery"), galleryMenuPanel);
        CreateButton(12, "Skills Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/SkillsGallery"), galleryMenuPanel);
        CreateButton(13, "Symbols Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/SymbolsGallery"), galleryMenuPanel);
        CreateButton(14, "Titles Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/TitlesGallery"), galleryMenuPanel);
        CreateButton(15, "Magic Formation Circle Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/MagicFormationCircleGallery"), galleryMenuPanel);
        CreateButton(16, "Relics Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/RelicsGallery"), galleryMenuPanel);
        CreateButton(17, "Card Colonels Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/teachings_of_conflict"), galleryMenuPanel);
        CreateButton(18, "Card Generals Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/teachings_of_contention"), galleryMenuPanel);
        CreateButton(19, "Card Admirals Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/teachings_of_diligence"), galleryMenuPanel);
        CreateButton(20, "Borders Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/BorderGallery"), galleryMenuPanel);
        CreateButton(21, "Talisman Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/TalismanGallery"), galleryMenuPanel);
        CreateButton(22, "Puppet Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/PuppetGallery"), galleryMenuPanel);
        CreateButton(23, "Alchemy Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/AlchemyGallery"), galleryMenuPanel);
        CreateButton(24, "Forge Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/ForgeGallery"), galleryMenuPanel);
        CreateButton(25, "Life Gallery", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/LifeGallery"), galleryMenuPanel);

        FindAnyObjectByType<GalleryManager>().CreateGallery(galleryMenuPanel);
    }
    public void CreateCollectionButton(Transform collectionMenuPanel)
    {
        //Collection menu
        CreateButton(1, "Card Heroes Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/CardsCollection"), collectionMenuPanel);
        CreateButton(2, "Books Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/BooksCollection"), collectionMenuPanel);
        CreateButton(3, "Pets Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/PetsCollection"), collectionMenuPanel);
        CreateButton(4, "Card Captains Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/CaptainsCollection"), collectionMenuPanel);
        CreateButton(5, "Collaboration Equipments Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsCollection"), collectionMenuPanel);
        CreateButton(6, "Card Military Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/MilitaryCollection"), collectionMenuPanel);
        CreateButton(7, "Card Spell Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/SpellCollection"), collectionMenuPanel);
        CreateButton(8, "Collaborations Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/CollaborationsCollection"), collectionMenuPanel);
        CreateButton(9, "Card Monsters Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/MonstersCollection"), collectionMenuPanel);
        CreateButton(10, "Equipments Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/EquipmentsCollection"), collectionMenuPanel);
        CreateButton(11, "Medals Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/MedalsCollection"), collectionMenuPanel);
        CreateButton(12, "Skills Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/SkillsCollection"), collectionMenuPanel);
        CreateButton(13, "Symbols Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/SymbolsCollection"), collectionMenuPanel);
        CreateButton(14, "Titles Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/TitlesCollection"), collectionMenuPanel);
        CreateButton(15, "Magic Formation Circle Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/MagicFormationCircleCollection"), collectionMenuPanel);
        CreateButton(16, "Relics Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/RelicsCollection"), collectionMenuPanel);
        CreateButton(17, "Card Colonels Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/guide_to_conflict"), collectionMenuPanel);
        CreateButton(18, "Card Generals Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/guide_to_contention"), collectionMenuPanel);
        CreateButton(19, "Card Admirals Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/guide_to_diligence"), collectionMenuPanel);
        CreateButton(20, "Borders Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/BordersCollection"), collectionMenuPanel);
        CreateButton(21, "Talisman Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/TalismanCollection"), collectionMenuPanel);
        CreateButton(22, "Puppet Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/PuppetCollection"), collectionMenuPanel);
        CreateButton(23, "Alchemy Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/AlchemyCollection"), collectionMenuPanel);
        CreateButton(24, "Forge Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/ForgeCollection"), collectionMenuPanel);
        CreateButton(25, "Life Collection", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/LifeCollection"), collectionMenuPanel);

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
                CreateButtonWithName(subtype, Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/{subtype}"), equipmentMenuPanel);
            }
        }
        FindAnyObjectByType<EquipmentManager>().CreateEquipments(equipmentMenuPanel);
    }
    public void CreateAnimeButton(Transform animeMenuPanel)
    {
        CreateAnimeButtonUI("One Piece", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/One Piece"), animeMenuPanel);
        CreateAnimeButtonUI("Naruto", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Naruto"), animeMenuPanel);
        CreateAnimeButtonUI("Dragon Ball", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Dragon Ball"), animeMenuPanel);
        CreateAnimeButtonUI("Fairy Tail", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Fairy Tail"), animeMenuPanel);
        CreateAnimeButtonUI("Sword Art Online", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Sword Art Online"), animeMenuPanel);
        CreateAnimeButtonUI("Demon Slayer", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Demon Slayer"), animeMenuPanel);
        CreateAnimeButtonUI("Bleach", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Bleach"), animeMenuPanel);
        CreateAnimeButtonUI("Jujutsu Kaisen", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Jujutsu Kaisen"), animeMenuPanel);
        CreateAnimeButtonUI("Black Clover", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Black Clover"), animeMenuPanel);
        CreateAnimeButtonUI("Hunter x Hunter", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/Hunter x Hunter"), animeMenuPanel);
        CreateAnimeButtonUI("One Punch Man", Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/One Punch Man"), animeMenuPanel);
        FindAnyObjectByType<MainMenuAnimeStatsManager>().CreateAnimeButton(animeMenuPanel);
    }
    public void CreateArenaButton(Transform arenaMenuPanel)
    {

        var uniqueMode = ArenaService.Create().GetUniqueTypes();
        foreach (var type in uniqueMode)
        {
            CreateArenaButtonUI(type, Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"), Resources.Load<Texture2D>($"UI/Button/{type}"), arenaMenuPanel);
        }
        FindAnyObjectByType<ArenaManager>().CreateArenaButton(arenaMenuPanel);
    }
    public void CreateTowerButton(Transform towerMenuPanel)
    {
        // CreateArenaButton(1, "Tower 1",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/Tower_1"), towerMenuPanel);
        // CreateArenaButton(2, "Tower 2",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/Tower_2"), towerMenuPanel);
        // CreateArenaButton(3, "Tower 3",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/Tower_3"), towerMenuPanel);
        // CreateArenaButton(4, "Tower 4",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/Tower_4"), towerMenuPanel);
        // CreateArenaButton(5, "Tower 5",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/Tower_5"), towerMenuPanel);
        // CreateArenaButton(6, "Tower 6",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/Tower_6"), towerMenuPanel);
        // CreateArenaButton(7, "Tower 7",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/Tower_7"), towerMenuPanel);
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
                CreateButtonWithBackground(1, "Equipments", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Equipments"), buttonGroupPanel1);
                CreateButtonWithBackground(2, "Realm", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Realm"), buttonGroupPanel1);
                CreateButtonWithBackground(3, "Upgrade", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Upgrade"), buttonGroupPanel1);
                CreateButtonWithBackground(4, "Aptitude", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Aptitude"), buttonGroupPanel1);
                CreateButtonWithBackground(5, "Affinity", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Affinity"), buttonGroupPanel1);
                CreateButtonWithBackground(6, "Blessing", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Blessing"), buttonGroupPanel1);
                CreateButtonWithBackground(7, "Core", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Core"), buttonGroupPanel1);
                CreateButtonWithBackground(8, "Physique", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Physique"), buttonGroupPanel1);
                CreateButtonWithBackground(9, "Bloodline", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Bloodline"), buttonGroupPanel1);
                CreateButtonWithBackground(10, "Omnivision", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnivision"), buttonGroupPanel2);
                CreateButtonWithBackground(11, "Omnipotence", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnipotence"), buttonGroupPanel2);
                CreateButtonWithBackground(12, "Omnipresence", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnipresence"), buttonGroupPanel2);
                CreateButtonWithBackground(13, "Omniscience", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omniscience"), buttonGroupPanel2);
                CreateButtonWithBackground(14, "Omnivory", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnivory"), buttonGroupPanel2);
                CreateButtonWithBackground(15, "Angel", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Angel"), buttonGroupPanel2);
                CreateButtonWithBackground(16, "Demon", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Demon"), buttonGroupPanel2);
                CreateButtonWithBackground(17, "Sword", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Sword"), buttonGroupPanel3);
                CreateButtonWithBackground(18, "Spear", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Spear"), buttonGroupPanel3);
                CreateButtonWithBackground(19, "Shield", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Shield"), buttonGroupPanel3);
                CreateButtonWithBackground(20, "Bow", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Bow"), buttonGroupPanel3);
                CreateButtonWithBackground(21, "Gun", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Gun"), buttonGroupPanel3);
                CreateButtonWithBackground(22, "Cyber", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Cyber"), buttonGroupPanel3);
                CreateButtonWithBackground(23, "Fairy", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Fairy"), buttonGroupPanel3);

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
                CreateButtonWithBackground(24, "Dark", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Dark"), buttonGroupPanel1);
                CreateButtonWithBackground(25, "Light", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Light"), buttonGroupPanel1);
                CreateButtonWithBackground(26, "Fire", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Fire"), buttonGroupPanel1);
                CreateButtonWithBackground(27, "Ice", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Ice"), buttonGroupPanel1);
                CreateButtonWithBackground(28, "Earth", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Earth"), buttonGroupPanel1);
                CreateButtonWithBackground(29, "Thunder", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Thunder"), buttonGroupPanel1);
                CreateButtonWithBackground(30, "Life", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Life"), buttonGroupPanel1);
                CreateButtonWithBackground(31, "Space", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Space"), buttonGroupPanel1);
                CreateButtonWithBackground(32, "Time", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Time"), buttonGroupPanel1);
                CreateButtonWithBackground(33, "Nanotech", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Nanotech"), buttonGroupPanel2);
                CreateButtonWithBackground(34, "Quantum", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Quantum"), buttonGroupPanel2);
                CreateButtonWithBackground(35, "Holography", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Holography"), buttonGroupPanel2);
                CreateButtonWithBackground(36, "Plasma", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Plasma"), buttonGroupPanel2);
                CreateButtonWithBackground(37, "Biomech", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Biomech"), buttonGroupPanel2);
                CreateButtonWithBackground(38, "Cryotech", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Cryotech"), buttonGroupPanel2);
                CreateButtonWithBackground(39, "Psionics", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Psionics"), buttonGroupPanel2);
                CreateButtonWithBackground(40, "Neurotech", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Neurotech"), buttonGroupPanel3);
                CreateButtonWithBackground(41, "Antimatter", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Antimatter"), buttonGroupPanel3);
                CreateButtonWithBackground(42, "Phantomware", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Phantomware"), buttonGroupPanel3);
                CreateButtonWithBackground(43, "Gravitech", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Gravitech"), buttonGroupPanel3);
                CreateButtonWithBackground(44, "Aethernet", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Aethernet"), buttonGroupPanel3);
                CreateButtonWithBackground(45, "Starforge", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Starforge"), buttonGroupPanel3);
                CreateButtonWithBackground(46, "Orbitalis", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Orbitalis"), buttonGroupPanel3);
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
                CreateButtonWithBackground(47, "Azathoth", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Azathoth"), buttonGroupPanel1);
                CreateButtonWithBackground(48, "Yog-Sothoth", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Yog-Sothoth"), buttonGroupPanel1);
                CreateButtonWithBackground(49, "Nyarlathotep", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Nyarlathotep"), buttonGroupPanel1);
                CreateButtonWithBackground(50, "Shub-Niggurath", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Shub-Niggurath"), buttonGroupPanel1);
                CreateButtonWithBackground(51, "Nihorath", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Nihorath"), buttonGroupPanel1);
                CreateButtonWithBackground(52, "Aeonax", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Aeonax"), buttonGroupPanel1);
                CreateButtonWithBackground(53, "Seraphiros", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Seraphiros"), buttonGroupPanel1);
                CreateButtonWithBackground(54, "Thorindar", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Thorindar"), buttonGroupPanel1);
                CreateButtonWithBackground(55, "Zilthros", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Zilthros"), buttonGroupPanel1);
                CreateButtonWithBackground(56, "Khorazal", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Khorazal"), buttonGroupPanel2);
                CreateButtonWithBackground(57, "Ixithra", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Ixithra"), buttonGroupPanel2);
                CreateButtonWithBackground(58, "Omnitheus", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnitheus"), buttonGroupPanel2);
                CreateButtonWithBackground(59, "Phyrixa", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Phyrixa"), buttonGroupPanel2);
                CreateButtonWithBackground(60, "Atherion", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Atherion"), buttonGroupPanel2);
                CreateButtonWithBackground(61, "Vorathos", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Vorathos"), buttonGroupPanel2);
                CreateButtonWithBackground(62, "Tenebris", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Tenebris"), buttonGroupPanel2);
                CreateButtonWithBackground(63, "Xylkor", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Xylkor"), buttonGroupPanel3);
                CreateButtonWithBackground(64, "Veltharion", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Veltharion"), buttonGroupPanel3);
                CreateButtonWithBackground(65, "Arcanos", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Arcanos"), buttonGroupPanel3);
                CreateButtonWithBackground(66, "Dolomath", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Dolomath"), buttonGroupPanel3);
                CreateButtonWithBackground(67, "Arathor", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Arathor"), buttonGroupPanel3);
                CreateButtonWithBackground(68, "Xyphos", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Xyphos"), buttonGroupPanel3);
                CreateButtonWithBackground(69, "Vaelith", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Vaelith"), buttonGroupPanel3);
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
                CreateButtonWithBackground(70, "Zarx", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Zarx"), buttonGroupPanel1);
                CreateButtonWithBackground(71, "Raik", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Raik"), buttonGroupPanel1);
                CreateButtonWithBackground(72, "Drax", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Drax"), buttonGroupPanel1);
                CreateButtonWithBackground(73, "Kron", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Kron"), buttonGroupPanel1);
                CreateButtonWithBackground(74, "Zolt", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Zolt"), buttonGroupPanel1);
                CreateButtonWithBackground(75, "Gorr", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Gorr"), buttonGroupPanel1);
                CreateButtonWithBackground(76, "Ryze", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Ryze"), buttonGroupPanel1);
                CreateButtonWithBackground(77, "Jaxx", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Jaxx"), buttonGroupPanel1);
                CreateButtonWithBackground(78, "Thar", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Thar"), buttonGroupPanel1);
                CreateButtonWithBackground(79, "Vorn", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Vorn"), buttonGroupPanel2);
                CreateButtonWithBackground(80, "Nyx", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Nyx"), buttonGroupPanel2);
                CreateButtonWithBackground(81, "Aros", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Aros"), buttonGroupPanel2);
                CreateButtonWithBackground(82, "Hex", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Hex"), buttonGroupPanel2);
                CreateButtonWithBackground(83, "Lorn", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Lorn"), buttonGroupPanel2);
                CreateButtonWithBackground(84, "Baxx", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Baxx"), buttonGroupPanel2);
                CreateButtonWithBackground(85, "Zeph", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Zeph"), buttonGroupPanel2);
                CreateButtonWithBackground(86, "Kael", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Kael"), buttonGroupPanel3);
                CreateButtonWithBackground(87, "Drav", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Drav"), buttonGroupPanel3);
                CreateButtonWithBackground(88, "Torn", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Torn"), buttonGroupPanel3);
                CreateButtonWithBackground(89, "Myrr", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Myrr"), buttonGroupPanel3);
                CreateButtonWithBackground(90, "Vask", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Vask"), buttonGroupPanel3);
                CreateButtonWithBackground(91, "Jorr", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Jorr"), buttonGroupPanel3);
                CreateButtonWithBackground(92, "Quen", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Quen"), buttonGroupPanel3);
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
            // CreateButtonWithBackground(1, "Equipments", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Equipments"), buttonGroupPanel1);
            CreateButtonWithBackground(2, "Realm", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Realm"), buttonGroupPanel1);
            CreateButtonWithBackground(3, "Upgrade", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Upgrade"), buttonGroupPanel1);
            CreateButtonWithBackground(4, "Aptitude", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Aptitude"), buttonGroupPanel1);
            // CreateButtonWithBackground(5, "Affinity", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Affinity"), buttonGroupPanel1);
            CreateButtonWithBackground(6, "Blessing", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Blessing"), buttonGroupPanel1);
            CreateButtonWithBackground(7, "Core", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Core"), buttonGroupPanel1);
            CreateButtonWithBackground(8, "Physique", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Physique"), buttonGroupPanel1);
            CreateButtonWithBackground(9, "Bloodline", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Bloodline"), buttonGroupPanel1);
            CreateButtonWithBackground(10, "Omnivision", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnivision"), buttonGroupPanel2);
            CreateButtonWithBackground(11, "Omnipotence", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnipotence"), buttonGroupPanel2);
            CreateButtonWithBackground(12, "Omnipresence", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnipresence"), buttonGroupPanel2);
            CreateButtonWithBackground(13, "Omniscience", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omniscience"), buttonGroupPanel2);
            CreateButtonWithBackground(14, "Omnivory", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnivory"), buttonGroupPanel2);
            CreateButtonWithBackground(15, "Angel", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Angel"), buttonGroupPanel2);
            CreateButtonWithBackground(16, "Demon", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Demon"), buttonGroupPanel2);

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
