using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLoader : MonoBehaviour
{
    public GameObject buttonPrefab; // Prefab của button
    public Transform mainMenuButtonPanel; // Nơi chứa các button trong scene
    public Transform galleryMenuPanel;
    public Transform collectionMenuPanel;
    public Transform equipmentMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        //Main menu
        CreateButton(1, "Cards",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/cards"), mainMenuButtonPanel);
        CreateButton(2, "Books",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/books"), mainMenuButtonPanel);
        CreateButton(3, "Pets",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/pets"), mainMenuButtonPanel);
        CreateButton(4, "Captains",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/captains"), mainMenuButtonPanel);
        CreateButton(5, "Collaboration Equipments",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/collaborationequipments"), mainMenuButtonPanel);
        CreateButton(6, "Military",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/military"), mainMenuButtonPanel);
        CreateButton(7, "Spell",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/spell"),mainMenuButtonPanel);
        CreateButton(8, "Teams",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/teams"), mainMenuButtonPanel);
        CreateButton(9, "Monsters",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/monsters"),mainMenuButtonPanel);
        CreateButton(10, "Equipments",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/equipments"), mainMenuButtonPanel);
        CreateButton(11, "Bag",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/bag"),mainMenuButtonPanel);
        CreateButton(12, "Skills",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/skills"),mainMenuButtonPanel);
        CreateButton(13, "Summon",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/summon"),mainMenuButtonPanel);
        //Gallery menu
        CreateButton(1, "Cards Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CardsGallery"), galleryMenuPanel);
        CreateButton(2, "Books Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/BooksGallery"), galleryMenuPanel);
        CreateButton(3, "Pets Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/PetsGallery"), galleryMenuPanel);
        CreateButton(4, "Captains Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CaptainsGallery"), galleryMenuPanel);
        CreateButton(5, "Collaboration Equipments Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsGallery"), galleryMenuPanel);
        CreateButton(6, "Military Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MilitaryGallery"), galleryMenuPanel);
        CreateButton(7, "Spell Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SpellGallery"),galleryMenuPanel);
        CreateButton(8, "Collaborations Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CollaborationsGallery"), galleryMenuPanel);
        CreateButton(9, "Monsters Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MonstersGallery"),galleryMenuPanel);
        CreateButton(10, "Equipments Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/EquipmentsGallery"), galleryMenuPanel);
        CreateButton(11, "Medals Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MedalsGallery"),galleryMenuPanel);
        CreateButton(12, "Skills Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SkillsGallery"),galleryMenuPanel);
        CreateButton(13, "Symbols Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SymbolsGallery"),galleryMenuPanel);
        CreateButton(14, "Titles Gallery",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/TitlesGallery"),galleryMenuPanel);
        //Collection menu
        CreateButton(1, "Cards Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CardsCollection"), collectionMenuPanel);
        CreateButton(2, "Books Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/BooksCollection"), collectionMenuPanel);
        CreateButton(3, "Pets Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/PetsCollection"), collectionMenuPanel);
        CreateButton(4, "Captains Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CaptainsCollection"), collectionMenuPanel);
        CreateButton(5, "Collaboration Equipments Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CollaborationEquipmentsCollection"), collectionMenuPanel);
        CreateButton(6, "Military Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MilitaryCollection"), collectionMenuPanel);
        CreateButton(7, "Spell Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SpellCollection"),collectionMenuPanel);
        CreateButton(8, "Collaborations Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/CollaborationsCollection"), collectionMenuPanel);
        CreateButton(9, "Monsters Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MonstersCollection"),collectionMenuPanel);
        CreateButton(10, "Equipments Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/EquipmentsCollection"), collectionMenuPanel);
        CreateButton(11, "Medals Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/MedalsCollection"),collectionMenuPanel);
        CreateButton(12, "Skills Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SkillsCollection"),collectionMenuPanel);
        CreateButton(13, "Symbols Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/SymbolsCollection"),collectionMenuPanel);
        CreateButton(14, "Titles Collection",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/Button/TitlesCollection"),collectionMenuPanel);
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
}
