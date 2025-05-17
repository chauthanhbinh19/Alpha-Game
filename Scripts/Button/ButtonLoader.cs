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
    private GameObject ArenaButtonPrefab;
    private GameObject AnimeButtonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        buttonPrefab = UIManager.Instance.GetGameObject("buttonPrefab");
        mainMenuButtonPanel = UIManager.Instance.GetTransform("mainMenuButtonPanel");
        mainMenuCampaignPanel = UIManager.Instance.GetTransform("mainMenuCampaignPanel");
        summonPanel = UIManager.Instance.GetTransform("summonPanel");
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
        return Equipments.GetUniqueEquipmentsTypes();
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
        List<string> uniqueMode = Arena.GetUniqueTypes();
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
}
