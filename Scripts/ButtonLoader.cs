using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLoader : MonoBehaviour
{
    public GameObject buttonPrefab; // Prefab của button
    public Transform mainMenuButton; // Nơi chứa các button trong scene
    // Start is called before the first frame update
    void Start()
    {
        CreateButton(1, "Cards",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/cards"));
        CreateButton(2, "Books",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/books"));
        CreateButton(3, "Pets",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/pets"));
        CreateButton(4, "Captains",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/captains"));
        CreateButton(5, "Collaboration Equipments",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/collaborationequipments"));
        CreateButton(6, "Military",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/military"));
        CreateButton(7, "Spell",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/spell"));
        CreateButton(8, "Teams",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/teams"));
        CreateButton(9, "Monsters",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/monsters"));
        CreateButton(10, "Equipments",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/equipments"));
        CreateButton(11, "Bag",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/bag"));
        CreateButton(12, "Skills",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/skills"));
        CreateButton(13, "Summon",Resources.Load<Texture2D>($"UI/Background4/Background_V4_58"),Resources.Load<Texture2D>($"UI/UI/summon"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateButton(int index, string itemName, Texture2D itemBackground, Texture2D itemImage)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(buttonPrefab, mainMenuButton);
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
}
