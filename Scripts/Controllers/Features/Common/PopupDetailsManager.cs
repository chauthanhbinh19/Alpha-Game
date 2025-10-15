using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class PopupDetailsManager : MonoBehaviour
{
    public static PopupDetailsManager Instance { get; private set; }
    private GameObject MainMenuDetailPanelPrefab;
    private GameObject ElementDetailsPrefab;
    private GameObject NumberDetailPrefab;
    private GameObject NumberDetail2Prefab;
    private GameObject NumberDetail3Prefab;
    private Transform MainPanel;
    private GameObject popupObject;
    private Transform firstPopupPanel;
    private Transform elementPopupPanel;
    private Transform element2PopupPanel;
    private Transform element3PopupPanel;
    private Transform element4PopupPanel;
    private Transform descriptionPopupPanel;
    private string descriptionColor = "#F9EED9";
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

    // Update is called once per frame
    void Update()
    {

    }
    public void Initialize()
    {
        MainMenuDetailPanelPrefab = UIManager.Instance.GetGameObject("MainMenuDetailPanelPrefab");
        ElementDetailsPrefab = UIManager.Instance.GetGameObject("ElementDetailsPrefab");
        NumberDetailPrefab = UIManager.Instance.GetGameObject("NumberDetailPrefab");
        NumberDetail2Prefab = UIManager.Instance.GetGameObject("NumberDetail2Prefab");
        NumberDetail3Prefab = UIManager.Instance.GetGameObject("NumberDetail3Prefab");
    }
    public void PopupDetails(object data, Transform panel)
    {
        MainPanel = panel;
        popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject firstDetailsObject = Instantiate(NumberDetail2Prefab, numberDetailsPanel);
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject elementDetails2Object = Instantiate(NumberDetail3Prefab, numberDetailsPanel);
        GameObject elementDetails3Object = Instantiate(NumberDetail3Prefab, numberDetailsPanel);
        GameObject elementDetails4Object = Instantiate(NumberDetail3Prefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetail3Prefab, numberDetailsPanel);
        firstPopupPanel = firstDetailsObject.transform.Find("ElementDetails");
        elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        element2PopupPanel = elementDetails2Object.transform.Find("ElementDetails");
        element3PopupPanel = elementDetails3Object.transform.Find("ElementDetails");
        element4PopupPanel = elementDetails4Object.transform.Find("ElementDetails");
        descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");
        // Kiểm tra kiểu của data và ép kiểu phù hợp
        if (data is CardHeroes card)
        {
            // Xử lý đối tượng Card
            ShowCardHeroesDetails(card);
        }
        else if (data is Books book)
        {
            // Xử lý đối tượng Book
            ShowBookDetails(book);
        }
        else if (data is CardCaptains captain)
        {
            // Xử lý đối tượng Captain
            ShowCardCaptainDetails(captain);
        }
        else if (data is Pets pet)
        {
            // Xử lý đối tượng Pet
            ShowPetDetails(pet);
        }
        else if (data is CollaborationEquipment collaborationEquipmentsequipment)
        {
            // Xử lý đối tượng CollaborationEquipment
            ShowCollaborationEquipmentDetails(collaborationEquipmentsequipment);
        }
        else if (data is CardMilitary military)
        {
            // Xử lý đối tượng Military
            ShowCardMilitaryDetails(military);
        }
        else if (data is CardSpell spell)
        {
            // Xử lý đối tượng Spell
            ShowCardSpellDetails(spell);
        }
        else if (data is Collaboration collaboration)
        {
            // Xử lý đối tượng Collaboration
            ShowCollaborationDetails(collaboration);
        }
        else if (data is CardMonsters monster)
        {
            // Xử lý đối tượng Monster
            ShowCardMonsterDetails(monster);
        }
        else if (data is Equipments equipment)
        {
            // Xử lý đối tượng Equipment
            ShowEquipmentDetails(equipment);
        }
        else if (data is Medals medal)
        {
            // Xử lý đối tượng Medal
            ShowMedalDetails(medal);
        }
        else if (data is Skills skill)
        {
            // Xử lý đối tượng Skill
            ShowSkillDetails(skill);
        }
        else if (data is Symbols symbol)
        {
            // Xử lý đối tượng Symbol
            ShowSymbolDetails(symbol);
        }
        else if (data is Titles title)
        {
            // Xử lý đối tượng Title
            ShowTitleDetails(title);
        }
        else if (data is MagicFormationCircle magicFormationCircle)
        {
            // Xử lý đối tượng Title
            ShowMagicFormationCircleDetails(magicFormationCircle);
        }
        else if (data is Relics relics)
        {
            // Xử lý đối tượng Title
            ShowRelicsDetails(relics);
        }
        else if (data is CardColonels colonels)
        {
            // Xử lý đối tượng colonels
            ShowCardColonelsDetails(colonels);
        }
        else if (data is CardGenerals generals)
        {
            // Xử lý đối tượng Generals
            ShowCardGeneralsDetails(generals);
        }
        else if (data is CardAdmirals admirals)
        {
            // Xử lý đối tượng admirals
            ShowCardAdmiralsDetails(admirals);
        }
        else if (data is Borders borders)
        {
            // Xử lý đối tượng borders
            ShowBorderDetails(borders);
        }
        else if (data is Achievements achievements)
        {
            // Xử lý đối tượng achievements
            ShowAchievementDetails(achievements);
        }
        else if (data is Talisman talisman)
        {
            // Xử lý đối tượng achievements
            ShowTalismanDetails(talisman);
        }
        else if (data is Puppet puppet)
        {
            // Xử lý đối tượng achievements
            ShowPuppetDetails(puppet);
        }
        else if (data is Alchemy alchemy)
        {
            // Xử lý đối tượng achievements
            ShowAlchemyDetails(alchemy);
        }
        else if (data is Forge forge)
        {
            // Xử lý đối tượng achievements
            ShowForgeDetails(forge);
        }
        else if (data is CardLife cardLife)
        {
            // Xử lý đối tượng achievements
            ShowCardLifeDetails(cardLife);
        }
        else if (data is Artwork artwork)
        {
            // Xử lý đối tượng achievements
            ShowArtworkDetails(artwork);
        }
        else if (data is SpiritBeast spiritBeast)
        {
            // Xử lý đối tượng achievements
            ShowSpiritBeastDetails(spiritBeast);
        }
        else
        {
            Debug.LogError("Không hỗ trợ loại dữ liệu này!");
        }

    }
    void ClosePopup(GameObject popup)
    {
        Destroy(popup); // Hủy popupObject khi nút CloseButton được nhấn
    }
    private void ShowCardHeroesDetails(CardHeroes card)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(card.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = card.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(card.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = card.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = card.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(card, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowBookDetails(Books book)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(book.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 700f; // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = book.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(book.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = book.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{book.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = book.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(book, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowCardCaptainDetails(CardCaptains captains)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(captains.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = captains.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(captains.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = captains.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{captains.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = captains.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(captains, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowPetDetails(Pets pet)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(pet.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            if (pet.type.Equals("Legendary_Dragon") || pet.type.Equals("Naruto_Bijuu") || pet.type.Equals("Naruto_Susanoo") || pet.type.Equals("One_Piece_Ship") || pet.type.Equals("Prime_Monster"))
            {
                newHeight = 700f;
            }
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = pet.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(pet.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = pet.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{pet.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = pet.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(pet, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowCollaborationEquipmentDetails(CollaborationEquipment collaborationEquipment)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(collaborationEquipment.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = collaborationEquipment.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(collaborationEquipment.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = collaborationEquipment.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaborationEquipment.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = collaborationEquipment.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(collaborationEquipment, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowCardMilitaryDetails(CardMilitary military)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(military.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = military.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(military.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = military.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{military.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = military.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(military, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowCardSpellDetails(CardSpell spell)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(spell.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = spell.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(spell.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = spell.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spell.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = spell.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(spell, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowCollaborationDetails(Collaboration collaboration)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(collaboration.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = collaboration.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(collaboration.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = collaboration.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaboration.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = collaboration.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(collaboration, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowCardMonsterDetails(CardMonsters monsters)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(monsters.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = monsters.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(monsters.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = monsters.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{monsters.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = monsters.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(monsters, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowEquipmentDetails(Equipments equipments)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(equipments.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = equipments.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(equipments.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = equipments.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipments.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });
        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = equipments.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(equipments, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowMedalDetails(Medals medals)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(medals.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = medals.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(medals.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = medals.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{medals.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = medals.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(medals, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowSkillDetails(Skills skills)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(skills.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = skills.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(skills.power, false);

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{skills.rare}");
        rareImage.texture = rareTexture;

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = skills.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(skills, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowSymbolDetails(Symbols symbols)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(symbols.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = symbols.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(symbols.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{symbols.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = symbols.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(symbols, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowTitleDetails(Titles titles)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(titles.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = titles.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(titles.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{titles.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = titles.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(titles, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowBorderDetails(Borders borders)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(borders.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = borders.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(borders.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{borders.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = borders.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(borders, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowAchievementDetails(Achievements achievements)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(achievements.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = achievements.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(achievements.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{achievements.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = achievements.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(achievements, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowMagicFormationCircleDetails(MagicFormationCircle magicFormationCircle)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(magicFormationCircle.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = magicFormationCircle.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(magicFormationCircle.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{magicFormationCircle.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = magicFormationCircle.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(magicFormationCircle, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowRelicsDetails(Relics relics)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(relics.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = relics.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(relics.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{relics.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = relics.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(relics, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowCardColonelsDetails(CardColonels colonels)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(colonels.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = colonels.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(colonels.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{colonels.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = colonels.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(colonels, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowCardGeneralsDetails(CardGenerals generals)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(generals.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = generals.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(generals.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{generals.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = generals.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(generals, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowCardAdmiralsDetails(CardAdmirals admirals)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(admirals.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = admirals.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(admirals.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{admirals.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = admirals.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(admirals, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowTalismanDetails(Talisman talisman)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(talisman.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = talisman.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(talisman.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{talisman.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = talisman.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(talisman, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowPuppetDetails(Puppet puppet)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(puppet.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = puppet.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(puppet.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{puppet.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = puppet.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(puppet, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowAlchemyDetails(Alchemy alchemy)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(alchemy.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = alchemy.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(alchemy.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{alchemy.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = alchemy.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(alchemy, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowForgeDetails(Forge forge)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(forge.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = Image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = forge.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(forge.power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{forge.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = forge.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(forge, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowCardLifeDetails(CardLife card)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(card.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = card.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(card.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = card.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = card.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(card, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowArtworkDetails(Artwork artwork)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(artwork.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = artwork.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(artwork.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = artwork.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{artwork.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = artwork.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(artwork, null);
            CreatePropertyUI(1, property, value);
        }
    }
    private void ShowSpiritBeastDetails(SpiritBeast spiritBeast)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(spiritBeast.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = spiritBeast.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(spiritBeast.power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = spiritBeast.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spiritBeast.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = spiritBeast.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(spiritBeast, null);
            CreatePropertyUI(1, property, value);
        }
    }
    public void CreatePropertyUI(int status, PropertyInfo property, object value)
    {
        if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image"))
        {
            if (property.Name.Equals("description"))
            {
                // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                GameObject descriptionTextObject = new GameObject("DescriptionText");
                descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                // Thêm component TextMeshProUGUI vào đối tượng mới
                TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                // Đổi màu chữ bằng mã hex #844000
                Color color;
                if (ColorUtility.TryParseHtmlString(descriptionColor, out color)) // Chuyển mã hex thành Color
                {
                    descriptionText.color = color; // Gán màu cho text
                }

                // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(600, 100);
                rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
            }
            else if (property.Name.Equals("power") || property.Name.Equals("rare") || property.Name.Equals("type")
            || property.Name.Equals("star") || property.Name.Equals("level"))
            {
                // Tạo một element mới từ prefab
                GameObject elementObject = Instantiate(ElementDetailsPrefab, firstPopupPanel);

                // Gán tên thuộc tính vào TitleText
                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                if (elementTitleText != null)
                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                // Gán giá trị thuộc tính vào ContentText
                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                if (elementContentText != null)
                    elementContentText.text = value != null ? value.ToString() : "";
            }
            else if (property.Name.Equals(AppConstants.StatFields.HEALTH)
            || property.Name.Equals(AppConstants.StatFields.PHYSICAL_ATTACK)
            || property.Name.Equals(AppConstants.StatFields.PHYSICAL_DEFENSE)
            || property.Name.Equals(AppConstants.StatFields.MAGICAL_ATTACK)
            || property.Name.Equals(AppConstants.StatFields.MAGICAL_DEFENSE)
            || property.Name.Equals(AppConstants.StatFields.CHEMICAL_ATTACK)
            || property.Name.Equals(AppConstants.StatFields.CHEMICAL_DEFENSE)
            || property.Name.Equals(AppConstants.StatFields.ATOMIC_ATTACK)
            || property.Name.Equals(AppConstants.StatFields.ATOMIC_DEFENSE)
            || property.Name.Equals(AppConstants.StatFields.MENTAL_ATTACK)
            || property.Name.Equals(AppConstants.StatFields.MENTAL_DEFENSE))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = NumberFormatter.FormatNumber(intValue, false);

                        RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                        CreatePropertyRuneUI(property.Name, runeImage);
                    }
                }
            }
            else if (property.Name.Equals(AppConstants.StatFields.SPEED)
            || property.Name.Equals(AppConstants.StatFields.CRITICAL_DAMAGE_RATE)
            || property.Name.Equals(AppConstants.StatFields.CRITICAL_RATE)
            || property.Name.Equals(AppConstants.StatFields.CRITICAL_RESISTANCE_RATE)
            || property.Name.Equals(AppConstants.StatFields.IGNORE_CRITICAL_RATE)
            || property.Name.Equals(AppConstants.StatFields.PENETRATION_RATE)
            || property.Name.Equals(AppConstants.StatFields.PENETRATION_RESISTANCE_RATE)
            || property.Name.Equals(AppConstants.StatFields.EVASION_RATE)
            || property.Name.Equals(AppConstants.StatFields.DAMAGE_ABSORPTION_RATE)
            || property.Name.Equals(AppConstants.StatFields.IGNORE_DAMAGE_ABSORPTION_RATE)
            || property.Name.Equals(AppConstants.StatFields.ABSORBED_DAMAGE_RATE)
            || property.Name.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RATE)
            || property.Name.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RESISTANCE_RATE))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, element2PopupPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = NumberFormatter.FormatNumber(intValue, false);

                        RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                        CreatePropertyRuneUI(property.Name, runeImage);
                    }
                }
            }
            else if (property.Name.Equals(AppConstants.StatFields.ACCURACY_RATE)
            || property.Name.Equals(AppConstants.StatFields.LIFE_STEAL_RATE)
            || property.Name.Equals(AppConstants.StatFields.SHIELD_STRENGTH)
            || property.Name.Equals(AppConstants.StatFields.TENACITY)
            || property.Name.Equals(AppConstants.StatFields.RESISTANCE_RATE)
            || property.Name.Equals(AppConstants.StatFields.COMBO_RATE)
            || property.Name.Equals(AppConstants.StatFields.IGNORE_COMBO_RATE)
            || property.Name.Equals(AppConstants.StatFields.COMBO_DAMAGE_RATE)
            || property.Name.Equals(AppConstants.StatFields.COMBO_RESISTANCE_RATE)
            || property.Name.Equals(AppConstants.StatFields.STUN_RATE)
            || property.Name.Equals(AppConstants.StatFields.IGNORE_STUN_RATE)
            || property.Name.Equals(AppConstants.StatFields.MANA)
            || property.Name.Equals(AppConstants.StatFields.MANA_REGENERATION_RATE)
            || property.Name.Equals(AppConstants.StatFields.REFLECTION_RATE)
            || property.Name.Equals(AppConstants.StatFields.IGNORE_REFLECTION_RATE)
            || property.Name.Equals(AppConstants.StatFields.REFLECTION_DAMAGE_RATE)
            || property.Name.Equals(AppConstants.StatFields.REFLECTION_RESISTANCE_RATE))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, element3PopupPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = NumberFormatter.FormatNumber(intValue, false);

                        RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                        CreatePropertyRuneUI(property.Name, runeImage);
                    }
                }
            }
            else if (property.Name.Equals(AppConstants.StatFields.DAMAGE_TO_DIFFERENT_FACTION_RATE)
            || property.Name.Equals(AppConstants.StatFields.RESISTANCE_TO_DIFFERENT_FACTION_RATE)
            || property.Name.Equals(AppConstants.StatFields.DAMAGE_TO_SAME_FACTION_RATE)
            || property.Name.Equals(AppConstants.StatFields.RESISTANCE_TO_SAME_FACTION_RATE)
            || property.Name.Equals(AppConstants.StatFields.NORMAL_DAMAGE_RATE)
            || property.Name.Equals(AppConstants.StatFields.NORMAL_RESISTANCE_RATE)
            || property.Name.Equals(AppConstants.StatFields.SKILL_DAMAGE_RATE)
            || property.Name.Equals(AppConstants.StatFields.SKILL_RESISTANCE_RATE))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, element4PopupPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = NumberFormatter.FormatNumber(intValue, false);

                        RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                        CreatePropertyRuneUI(property.Name, runeImage);
                    }
                }
            }
        }
    }
    public void CreatePropertyRuneUI(string title, RawImage runeImage)
    {
        Texture runeTexture;
        if (title.Equals(AppConstants.StatFields.PHYSICAL_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.PhysicalAttackRuneUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.PHYSICAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.PhysicalDefenseRuneUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MAGICAL_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MagicalAttackRuneUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MAGICAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MagicalDefenseRuneUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CHEMICAL_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.ChemicalAttackRuneUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CHEMICAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.ChemicalDefenseRuneUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ATOMIC_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.AtomicAttackRuneUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ATOMIC_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.AtomicDefenseRuneUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MENTAL_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MentalAttackRuneUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MENTAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MentalDefenseRuneUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.HEALTH))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.HealthRuneUrl);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.SPEED)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.SpeedRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CRITICAL_RATE)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DamageRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CRITICAL_DAMAGE_RATE)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DamageRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.DAMAGE_ABSORPTION_RATE)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DamageRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CRITICAL_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_CRITICAL_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.PENETRATION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DamageRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.PENETRATION_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.EVASION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ProtectionRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_DAMAGE_ABSORPTION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ABSORBED_DAMAGE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DamageRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DamageRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ACCURACY_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.AccuracyRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.LIFE_STEAL_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.AccuracyRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.SHIELD_STRENGTH))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ProtectionRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.TENACITY))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.COMBO_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DamageRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_COMBO_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.COMBO_DAMAGE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DamageRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.COMBO_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.STUN_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.AccuracyRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_STUN_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MANA))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ProtectionRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MANA_REGENERATION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.REFLECTION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DamageRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_REFLECTION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.REFLECTION_DAMAGE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DamageRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.REFLECTION_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ResistanceRuneUrl}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.DAMAGE_TO_DIFFERENT_FACTION_RATE) || title.Equals(AppConstants.StatFields.RESISTANCE_TO_DIFFERENT_FACTION_RATE) ||
         title.Equals(AppConstants.StatFields.DAMAGE_TO_SAME_FACTION_RATE) || title.Equals(AppConstants.StatFields.RESISTANCE_TO_SAME_FACTION_RATE) ||
         title.Equals(AppConstants.StatFields.NORMAL_DAMAGE_RATE) || title.Equals(AppConstants.StatFields.NORMAL_RESISTANCE_RATE) ||
         title.Equals(AppConstants.StatFields.SKILL_DAMAGE_RATE) || title.Equals(AppConstants.StatFields.SKILL_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.SkillRuneUrl}");
            runeImage.texture = runeTexture;
        }
        runeImage.gameObject.SetActive(true);
    }
}
