using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLoader : MonoBehaviour
{
    private GameObject buttonPrefab; // Prefab của button
    private Transform mainMenuButtonPanel; // Nơi chứa các button trong scene
    private Transform summonPanel;
    // Start is called before the first frame update
    void Start()
    {
        buttonPrefab = UIManager.Instance.GetGameObject("buttonPrefab");
        mainMenuButtonPanel = UIManager.Instance.GetTransform("mainMenuButtonPanel");
        summonPanel = UIManager.Instance.GetTransform("summonPanel");
        //Main menu
        CreateButton(1, "Card Heroes",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Cards"), mainMenuButtonPanel);
        CreateButton(2, "Books",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Books"), mainMenuButtonPanel);
        CreateButton(3, "Pets",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Pets"), mainMenuButtonPanel);
        CreateButton(4, "Card Captains",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Captains"), mainMenuButtonPanel);
        CreateButton(5, "Collaboration Equipments",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/CollaborationEquipments"), mainMenuButtonPanel);
        CreateButton(6, "Card Military",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Military"), mainMenuButtonPanel);
        CreateButton(7, "Card Spell",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Spell"),mainMenuButtonPanel);
        CreateButton(8, "Collaborations",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Collaboration"), mainMenuButtonPanel);
        CreateButton(9, "Card Monsters",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Monsters"),mainMenuButtonPanel);
        // CreateButton(10, "Equipments",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/equipments"), mainMenuButtonPanel);
        CreateButton(11, "Medals",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Bag"),mainMenuButtonPanel);
        CreateButton(12, "Skills",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Skills"),mainMenuButtonPanel);
        CreateButton(13, "Symbols",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Symbols"),mainMenuButtonPanel);
        CreateButton(14, "Titles",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Titles"),mainMenuButtonPanel);
        CreateButton(15, "Magic Formation Circle",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MagicFormationCircle"),mainMenuButtonPanel);
        CreateButton(16, "Relics",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/Relics"),mainMenuButtonPanel);
        CreateButton(17, "Bag",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Bag"),mainMenuButtonPanel);
        CreateButton(18, "Teams",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Teams"), mainMenuButtonPanel);
        CreateButton(19, "Card Colonels Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/philosophies_of_conflict"),mainMenuButtonPanel);
        CreateButton(20, "Card Generals Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/philosophies_of_contention"),mainMenuButtonPanel);
        CreateButton(21, "Card Admirals Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/philosophies_of_diligence"),mainMenuButtonPanel);

        CreateButton(22, "Summon Card Heroes",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/SummonCards"),summonPanel);
        CreateButton(23, "Summon Books",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/SummonBooks"),summonPanel);
        CreateButton(24, "Summon CardCaptains",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/SummonCaptains"),summonPanel);
        CreateButton(25, "Summon Card Monsters",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/SummonMonsters"),summonPanel);
        CreateButton(26, "Summon Card Military",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/SummonMilitary"),summonPanel);
        CreateButton(27, "Summon Card Spell",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/SummonSpell"),summonPanel);
        CreateButton(28, "Summon Card Colonels",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/SummonColonels"),summonPanel);
        CreateButton(29, "Summon Card Generals",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/SummonGenerals"),summonPanel);
        CreateButton(30, "Summon Card Admirals",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/SummonAdmirals"),summonPanel);
        CreateButton(31, "Shop",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/Shop"),summonPanel);
        CreateButton(32, "Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/Gallery"),summonPanel);
        CreateButton(33, "Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/Collection"),summonPanel);
        CreateButton(34, "Equipments",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/Legend_Equipment"),summonPanel);
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
        RawImage  background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && itemBackground != null)
        {
            background.texture = itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage  image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        Text nameText = newButton.transform.Find("ItemName").GetComponent<Text>();
        if (nameText != null)
        {
            nameText.text = itemName;
        }
    }
    private void CreateButtonWithName( string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(buttonPrefab, panel);
        newButton.name = itemName;

        // Gán màu cho itemBackground
        RawImage  background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && itemBackground != null)
        {
            background.texture = itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage  image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        Text nameText = newButton.transform.Find("ItemName").GetComponent<Text>();
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
    public void CreateGalleryButton(Transform galleryMenuPanel){
        //Gallery menu
        CreateButton(1, "Card Heroes Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CardsGallery"), galleryMenuPanel);
        CreateButton(2, "Books Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/BooksGallery"), galleryMenuPanel);
        CreateButton(3, "Pets Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/PetsGallery"), galleryMenuPanel);
        CreateButton(4, "Card Captains Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CaptainsGallery"), galleryMenuPanel);
        CreateButton(5, "Collaboration Equipments Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsGallery"), galleryMenuPanel);
        CreateButton(6, "Card Military Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MilitaryGallery"), galleryMenuPanel);
        CreateButton(7, "Card Spell Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SpellGallery"),galleryMenuPanel);
        CreateButton(8, "Collaborations Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CollaborationsGallery"), galleryMenuPanel);
        CreateButton(9, "Card Monsters Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MonstersGallery"),galleryMenuPanel);
        CreateButton(10, "Equipments Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/EquipmentsGallery"), galleryMenuPanel);
        CreateButton(11, "Medals Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MedalsGallery"),galleryMenuPanel);
        CreateButton(12, "Skills Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SkillsGallery"),galleryMenuPanel);
        CreateButton(13, "Symbols Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SymbolsGallery"),galleryMenuPanel);
        CreateButton(14, "Titles Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/TitlesGallery"),galleryMenuPanel);
        CreateButton(15, "Magic Formation Circle Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MagicFormationCircleGallery"),galleryMenuPanel);
        CreateButton(16, "Relics Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/RelicsGallery"),galleryMenuPanel);
        CreateButton(17, "Card Colonels Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/teachings_of_conflict"),galleryMenuPanel);
        CreateButton(18, "Card Generals Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/teachings_of_contention"),galleryMenuPanel);
        CreateButton(19, "Card Admirals Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/teachings_of_diligence"),galleryMenuPanel);
        CreateButton(20, "Borders Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/BorderGallery"),galleryMenuPanel);

        FindAnyObjectByType<GalleryManager>().CreateGallery(galleryMenuPanel);
    }
    public void CreateCollectionButton(Transform collectionMenuPanel){
        //Collection menu
        CreateButton(1, "Card Heroes Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CardsCollection"), collectionMenuPanel);
        CreateButton(2, "Books Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/BooksCollection"), collectionMenuPanel);
        CreateButton(3, "Pets Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/PetsCollection"), collectionMenuPanel);
        CreateButton(4, "Card Captains Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CaptainsCollection"), collectionMenuPanel);
        CreateButton(5, "Collaboration Equipments Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsCollection"), collectionMenuPanel);
        CreateButton(6, "Card Military Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MilitaryCollection"), collectionMenuPanel);
        CreateButton(7, "Card Spell Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SpellCollection"),collectionMenuPanel);
        CreateButton(8, "Collaborations Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CollaborationsCollection"), collectionMenuPanel);
        CreateButton(9, "Card Monsters Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MonstersCollection"),collectionMenuPanel);
        CreateButton(10, "Equipments Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/EquipmentsCollection"), collectionMenuPanel);
        CreateButton(11, "Medals Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MedalsCollection"),collectionMenuPanel);
        CreateButton(12, "Skills Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SkillsCollection"),collectionMenuPanel);
        CreateButton(13, "Symbols Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SymbolsCollection"),collectionMenuPanel);
        CreateButton(14, "Titles Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/TitlesCollection"),collectionMenuPanel);
        CreateButton(15, "Magic Formation Circle Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MagicFormationCircleCollection"),collectionMenuPanel);
        CreateButton(16, "Relics Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/RelicsCollection"),collectionMenuPanel);
        CreateButton(17, "Card Colonels Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/guide_to_conflict"),collectionMenuPanel);
        CreateButton(18, "Card Generals Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/guide_to_contention"),collectionMenuPanel);
        CreateButton(19, "Card Admirals Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/guide_to_diligence"),collectionMenuPanel);
        CreateButton(20, "Borders Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/BordersCollection"),collectionMenuPanel);

        FindAnyObjectByType<CollectionManager>().CreateCollection(collectionMenuPanel);
    }
    public void CreateEquipmentsButton(Transform equipmentMenuPanel){
        //Equipment menu
        List<string> uniqueTypes = GetUniqueTypes();
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                string subtype = uniqueTypes[i];
                CreateButtonWithName(subtype,Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/{subtype}"),equipmentMenuPanel);
            }
        }
        FindAnyObjectByType<EquipmentManager>().CreateEquipments(equipmentMenuPanel);
    }
}
