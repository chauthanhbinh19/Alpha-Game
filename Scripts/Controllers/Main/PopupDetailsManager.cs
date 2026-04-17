using System;
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
    private Transform MainPanel;
    private GameObject popupObject;
    private Dictionary<Type, Action<object>> popupDetailsMap;

    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            InitializePopupDetailMap();
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
    public void Initialize()
    {
        MainMenuDetailPanelPrefab = UIManager.Instance.Get("MainMenuDetailPanelPrefab");
        ElementDetailsPrefab = UIManager.Instance.Get("ElementDetailsPrefab");
    }
    private void InitializePopupDetailMap()
    {
        popupDetailsMap = new Dictionary<Type, Action<object>>();
        var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var method in methods)
        {
            if (!method.Name.StartsWith("Show") || !method.Name.EndsWith("Details"))
                continue;

            var parameters = method.GetParameters();
            if (parameters.Length != 1)
                continue;

            var dataType = parameters[0].ParameterType;
            popupDetailsMap[dataType] = obj => method.Invoke(this, new[] { obj });
        }
    }
    public void PopupDetails(object data, Transform panel)
    {
        MainPanel = panel;
        popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);

        if (data == null)
        {
            Debug.LogError("PopupDetails: data is null");
            return;
        }

        var dataType = data.GetType();
        if (popupDetailsMap != null && popupDetailsMap.TryGetValue(dataType, out var action))
        {
            action(data);
            return;
        }

        if (popupDetailsMap != null)
        {
            foreach (var entry in popupDetailsMap)
            {
                if (entry.Key.IsAssignableFrom(dataType))
                {
                    entry.Value(data);
                    return;
                }
            }
        }

        Debug.LogError($"Không hỗ trợ loại dữ liệu này: {dataType}");
    }
    private void ShowCardHeroDetails(CardHeroes cardHero)
    {
        Transform transform = popupObject.transform;
        RawImage Image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardHero.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardHero.Name;

        TextMeshProUGUI power = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatterHelper.FormatNumber(cardHero.Power, false);

        TextMeshProUGUI level = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = cardHero.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardHero.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardHero.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardHero, popupObject);
    }
    private void ShowBookDetails(Books book)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(book.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 700f; // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = book.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(book.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = book.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{book.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = book.GetType().GetProperties();
        CreatePropertyUI(1, properties, book, popupObject);
    }
    private void ShowCardCaptainDetails(CardCaptains cardCaptain)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardCaptain.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardCaptain.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardCaptain.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = cardCaptain.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardCaptain.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardCaptain.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardCaptain, popupObject);
    }
    private void ShowPetDetails(Pets pet)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(pet.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            if (pet.Type.Equals("Legendary_Dragon") || pet.Type.Equals("Naruto_Bijuu") || pet.Type.Equals("Naruto_Susanoo") || pet.Type.Equals("One_Piece_Ship") || pet.Type.Equals("Prime_Monster"))
            {
                newHeight = 700f;
            }
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = pet.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(pet.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = pet.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{pet.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = pet.GetType().GetProperties();
        CreatePropertyUI(1, properties, pet, popupObject);
    }
    private void ShowCollaborationEquipmentDetails(CollaborationEquipments collaborationEquipment)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(collaborationEquipment.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = collaborationEquipment.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(collaborationEquipment.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = collaborationEquipment.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{collaborationEquipment.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = collaborationEquipment.GetType().GetProperties();
        CreatePropertyUI(1, properties, collaborationEquipment, popupObject);
    }
    private void ShowCardMilitaryDetails(CardMilitaries cardMilitary)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardMilitary.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardMilitary.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardMilitary.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = cardMilitary.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardMilitary.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardMilitary.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardMilitary, popupObject);
    }
    private void ShowCardSpellDetails(CardSpells cardSpell)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardSpell.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardSpell.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardSpell.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = cardSpell.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardSpell.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardSpell.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardSpell, popupObject);
    }
    private void ShowCollaborationDetails(Collaborations collaboration)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(collaboration.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = collaboration.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(collaboration.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = collaboration.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{collaboration.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = collaboration.GetType().GetProperties();
        CreatePropertyUI(1, properties, collaboration, popupObject);
    }
    private void ShowCardMonsterDetails(CardMonsters cardMonster)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardMonster.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardMonster.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardMonster.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = cardMonster.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardMonster.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardMonster.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardMonster, popupObject);
    }
    private void ShowEquipmentDetails(Equipments equipment)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(equipment.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = equipment.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(equipment.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = equipment.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{equipment.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });
        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = equipment.GetType().GetProperties();
        CreatePropertyUI(1, properties, equipment, popupObject);
    }
    private void ShowMedalDetails(Medals medal)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(medal.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = medal.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(medal.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = medal.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{medal.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = medal.GetType().GetProperties();
        CreatePropertyUI(1, properties, medal, popupObject);
    }
    private void ShowSkillDetails(Skills skill)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(skill.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = skill.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(skill.Power, false);

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = skill.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{skill.Rare}");
        rareImage.texture = rareTexture;

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = skill.GetType().GetProperties();
        CreatePropertyUI(1, properties, skill, popupObject);
    }
    private void ShowSymbolDetails(Symbols symbol)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(symbol.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = symbol.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(symbol.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = symbol.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{symbol.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = symbol.GetType().GetProperties();
        CreatePropertyUI(1, properties, symbol, popupObject);
    }
    private void ShowTitleDetails(Titles title)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(title.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = title.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(title.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = title.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{title.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = title.GetType().GetProperties();
        CreatePropertyUI(1, properties, title, popupObject);
    }
    private void ShowBorderDetails(Borders border)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(border.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = border.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(border.Power, false);

        // TextMeshProUGUI level = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = border.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{border.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = border.GetType().GetProperties();
        CreatePropertyUI(1, properties, border, popupObject);
    }
    private void ShowAchievementDetails(Achievements achievement)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(achievement.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 200f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = achievement.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(achievement.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = achievement.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{achievement.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = achievement.GetType().GetProperties();
        CreatePropertyUI(1, properties, achievement, popupObject);
    }
    private void ShowMagicFormationCircleDetails(MagicFormationCircles magicFormationCircle)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(magicFormationCircle.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = magicFormationCircle.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(magicFormationCircle.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = magicFormationCircle.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{magicFormationCircle.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = magicFormationCircle.GetType().GetProperties();
        CreatePropertyUI(1, properties, magicFormationCircle, popupObject);
    }
    private void ShowRelicDetails(Relics relic)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(relic.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = relic.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(relic.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = relic.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{relic.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = relic.GetType().GetProperties();
        CreatePropertyUI(1, properties, relic, popupObject);
    }
    private void ShowCardColonelDetails(CardColonels cardColonel)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardColonel.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardColonel.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardColonel.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = cardColonel.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardColonel.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardColonel.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardColonel, popupObject);
    }
    private void ShowCardGeneralDetails(CardGenerals cardGeneral)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardGeneral.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardGeneral.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardGeneral.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = cardGeneral.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardGeneral.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardGeneral.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardGeneral, popupObject);
    }
    private void ShowCardAdmiralDetails(CardAdmirals cardAdmiral)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardAdmiral.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardAdmiral.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardAdmiral.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = cardAdmiral.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardAdmiral.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardAdmiral.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardAdmiral, popupObject);
    }
    private void ShowTalismanDetails(Talismans talisman)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(talisman.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = talisman.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(talisman.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = talisman.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{talisman.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = talisman.GetType().GetProperties();
        CreatePropertyUI(1, properties, talisman, popupObject);
    }
    private void ShowPuppetDetails(Puppets puppet)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(puppet.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = puppet.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(puppet.Power, false);

        TextMeshProUGUI level = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = puppet.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{puppet.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = puppet.GetType().GetProperties();
        CreatePropertyUI(1, properties, puppet, popupObject);
    }
    private void ShowAlchemyDetails(Alchemies alchemy)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(alchemy.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = alchemy.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(alchemy.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = alchemy.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{alchemy.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = alchemy.GetType().GetProperties();
        CreatePropertyUI(1, properties, alchemy, popupObject);
    }
    private void ShowForgeDetails(Forges forge)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(forge.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;
        if (texture != null)
        {
            // Lấy RectTransform của RawImage
            RectTransform ImageRectTransform = image.GetComponent<RectTransform>();

            // Tính tỉ lệ khung hình
            float aspectRatio = (float)texture.width / texture.height;

            // Chiều cao cố định là 500, tính chiều rộng theo tỷ lệ
            float newHeight = 300f;
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = forge.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(forge.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = forge.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{forge.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = forge.GetType().GetProperties();
        CreatePropertyUI(1, properties, forge, popupObject);
    }
    private void ShowCardLifeDetails(CardLives cardLife)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardLife.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = cardLife.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(cardLife.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = cardLife.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{cardLife.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardLife.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardLife, popupObject);
    }
    private void ShowArtworkDetails(Artworks artwork)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(artwork.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = artwork.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(artwork.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = artwork.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{artwork.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = artwork.GetType().GetProperties();
        CreatePropertyUI(1, properties, artwork, popupObject);
    }
    private void ShowSpiritBeastDetails(SpiritBeasts spiritBeast)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(spiritBeast.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = spiritBeast.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(spiritBeast.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = spiritBeast.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{spiritBeast.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = spiritBeast.GetType().GetProperties();
        CreatePropertyUI(1, properties, spiritBeast, popupObject);
    }
    private void ShowSpiritCardDetails(SpiritCards spiritCard)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(spiritCard.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = spiritCard.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(spiritCard.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = spiritCard.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{spiritCard.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = spiritCard.GetType().GetProperties();
        CreatePropertyUI(1, properties, spiritCard, popupObject);
    }
    private void ShowArtifactDetails(Artifacts artifact)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(artifact.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = artifact.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(artifact.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = artifact.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{artifact.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = artifact.GetType().GetProperties();
        CreatePropertyUI(1, properties, artifact, popupObject);
    }
    private void ShowArchitectureDetails(Architectures architecture)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(architecture.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = architecture.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(architecture.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = architecture.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{architecture.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = architecture.GetType().GetProperties();
        CreatePropertyUI(1, properties, architecture, popupObject);
    }
    private void ShowTechnologyDetails(Technologies technology)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(technology.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = technology.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(technology.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = technology.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{technology.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = technology.GetType().GetProperties();
        CreatePropertyUI(1, properties, technology, popupObject);
    }
    private void ShowVehicleDetails(Vehicles vehicle)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(vehicle.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = vehicle.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(vehicle.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = vehicle.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{vehicle.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = vehicle.GetType().GetProperties();
        CreatePropertyUI(1, properties, vehicle, popupObject);
    }
    private void ShowCoreDetails(Cores core)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(core.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = core.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(core.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = core.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{core.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = core.GetType().GetProperties();
        CreatePropertyUI(1, properties, core, popupObject);
    }
    private void ShowWeaponDetails(Weapons weapon)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(weapon.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = weapon.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(weapon.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = weapon.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{weapon.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = weapon.GetType().GetProperties();
        CreatePropertyUI(1, properties, weapon, popupObject);
    }
    private void ShowRobotDetails(Robots robot)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(robot.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = robot.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(robot.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = robot.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{robot.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = robot.GetType().GetProperties();
        CreatePropertyUI(1, properties, robot, popupObject);
    }
    private void ShowBadgeDetails(Badges badge)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(badge.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = badge.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(badge.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = badge.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{badge.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = badge.GetType().GetProperties();
        CreatePropertyUI(1, properties, badge, popupObject);
    }
    private void ShowMechaBeastDetails(MechaBeasts mechaBeast)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(mechaBeast.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = mechaBeast.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(mechaBeast.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = mechaBeast.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{mechaBeast.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = mechaBeast.GetType().GetProperties();
        CreatePropertyUI(1, properties, mechaBeast, popupObject);
    }
    private void ShowRuneDetails(Runes rune)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(rune.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = rune.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(rune.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = rune.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{rune.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = rune.GetType().GetProperties();
        CreatePropertyUI(1, properties, rune, popupObject);
    }
    private void ShowBuildingDetails(Buildings building)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(building.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = building.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(building.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = building.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{building.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = building.GetType().GetProperties();
        CreatePropertyUI(1, properties, building, popupObject);
    }
    private void ShowBeverageDetails(Beverages beverage)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(beverage.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = beverage.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(beverage.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = beverage.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{beverage.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = beverage.GetType().GetProperties();
        CreatePropertyUI(1, properties, beverage, popupObject);
    }
    private void ShowFoodDetails(Foods food)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(food.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = food.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(food.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = food.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{food.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = food.GetType().GetProperties();
        CreatePropertyUI(1, properties, food, popupObject);
    }
    private void ShowEmojiDetails(Emojis emoji)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(emoji.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = emoji.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(emoji.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = emoji.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{emoji.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = emoji.GetType().GetProperties();
        CreatePropertyUI(1, properties, emoji, popupObject);
    }
    private void ShowFashionDetails(Fashions fashion)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(fashion.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = fashion.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(fashion.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = fashion.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{fashion.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = fashion.GetType().GetProperties();
        CreatePropertyUI(1, properties, fashion, popupObject);
    }
    private void ShowFurnitureDetails(Furnitures furniture)
    {
        Transform transform = popupObject.transform;
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(furniture.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = furniture.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(furniture.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = furniture.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{furniture.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = furniture.GetType().GetProperties();
        CreatePropertyUI(1, properties, furniture, popupObject);
    }
    public void CreatePropertyUI(int status, PropertyInfo[] properties, object targetObject, GameObject currentObject)
    {
        Transform detailsContent = currentObject.transform.Find("Scroll View/Viewport/Content");

        Transform generalInformationPanel = detailsContent.Find("GeneralInformation");
        Transform statsInformationPanel = detailsContent.Find("StatInformation");
        Transform descriptionInformationPanel = detailsContent.Find("DescriptionInformation");

        foreach (var property in properties)
        {
            object value = property.GetValue(targetObject, null);

            CreateSinglePropertyUI(property, value,
                generalInformationPanel, statsInformationPanel, descriptionInformationPanel);
        }
    }
    public void CreateSinglePropertyUI(PropertyInfo property, object value,
    Transform generalInformationPanel, Transform statsInformationPanel, Transform descriptionInformationPanel)
    {
        if (!property.Name.Equals(AppConstants.StatFields.ID) && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals(AppConstants.StatFields.EXPERIMENT) && !property.Name.Equals(AppConstants.StatFields.QUANTITY) 
                && !property.Name.Equals(AppConstants.StatFields.BLOCK)
                && !property.Name.Equals(AppConstants.StatFields.STATUS) && !property.Name.Equals(AppConstants.StatFields.NAME)
                && !property.Name.Equals(AppConstants.StatFields.IMAGE))
        {
            if (property.Name.Equals(AppConstants.StatFields.DESCRIPTION))
            {
                // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                GameObject descriptionTextObject = new GameObject("DescriptionText");
                descriptionTextObject.transform.SetParent(descriptionInformationPanel, false); // Thêm vào panel với vị trí chính xác

                // Thêm component TextMeshProUGUI vào đối tượng mới
                TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                // Đổi màu chữ bằng mã hex #844000
                descriptionText.color = ColorHelper.HexToColor(ColorConstants.DESCRIPTION_TEXT_COLOR);
                // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(600, 500);
                // rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                // GridLayoutGroup gridLayout = descriptionPopupPanel.GetComponent<GridLayoutGroup>();
                // if (gridLayout != null)
                // {
                //     gridLayout.cellSize = new Vector2(670, 800);
                // }
            }
            else if (property.Name.Equals(AppConstants.StatFields.POWER) 
            || property.Name.Equals(AppConstants.StatFields.RARE) || property.Name.Equals(AppConstants.StatFields.TYPE)
            || property.Name.Equals(AppConstants.StatFields.STAR) || property.Name.Equals(AppConstants.StatFields.LEVEL))
            {
                if (value != null)
                {
                    bool shouldDisplay = false;

                    if (value is int intValue)
                    {
                        shouldDisplay = intValue != -1;
                    }
                    else if (value is double doubleValue)
                    {
                        shouldDisplay = doubleValue != -1;
                    }
                    else if (value is string)
                    {
                        shouldDisplay = true;
                    }

                    if (shouldDisplay)
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, generalInformationPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = value.ToString();
                    }
                }

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
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, statsInformationPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = NumberFormatterHelper.FormatNumber(intValue, false);

                        RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                        CreatePropertyRuneUI(property.Name, runeImage);
                    }
                }
            }
        }
    }
    private static readonly Dictionary<string, string> RuneIconByStat = new Dictionary<string, string>
    {
        { AppConstants.StatFields.PHYSICAL_ATTACK, ImageConstants.Rune.PHYSICAL_ATTACK_RUNE_URL },
        { AppConstants.StatFields.PHYSICAL_DEFENSE, ImageConstants.Rune.PHYSICAL_DEFENSE_RUNE_URL },
        { AppConstants.StatFields.MAGICAL_ATTACK, ImageConstants.Rune.MAGICAL_ATTACK_RUNE_URL },
        { AppConstants.StatFields.MAGICAL_DEFENSE, ImageConstants.Rune.MAGICAL_DEFENSE_RUNE_URL },
        { AppConstants.StatFields.CHEMICAL_ATTACK, ImageConstants.Rune.CHEMICAL_ATTACK_RUNE_URL },
        { AppConstants.StatFields.CHEMICAL_DEFENSE, ImageConstants.Rune.CHEMICAL_DEFENSE_RUNE_URL },
        { AppConstants.StatFields.ATOMIC_ATTACK, ImageConstants.Rune.ATOMIC_ATTACK_RUNE_URL },
        { AppConstants.StatFields.ATOMIC_DEFENSE, ImageConstants.Rune.ATOMIC_DEFENSE_RUNE_URL },
        { AppConstants.StatFields.MENTAL_ATTACK, ImageConstants.Rune.MENTAL_ATTACK_RUNE_URL },
        { AppConstants.StatFields.MENTAL_DEFENSE, ImageConstants.Rune.MENTAL_DEFENSE_RUNE_URL },
        { AppConstants.StatFields.HEALTH, ImageConstants.Rune.HEALTH_RUNE_URL },
        { AppConstants.StatFields.SPEED, ImageConstants.Rune.SPEED_RUNE_URL },
        { AppConstants.StatFields.CRITICAL_RATE, ImageConstants.Rune.DAMAGE_RUNE_URL },
        { AppConstants.StatFields.CRITICAL_DAMAGE_RATE, ImageConstants.Rune.CRITICAL_RUNE_URL },
        { AppConstants.StatFields.DAMAGE_ABSORPTION_RATE, ImageConstants.Rune.ABSORPTION_RUNE_URL },
        { AppConstants.StatFields.CRITICAL_RESISTANCE_RATE, ImageConstants.Rune.CRITICAL_RUNE_URL },
        { AppConstants.StatFields.IGNORE_CRITICAL_RATE, ImageConstants.Rune.CRITICAL_RUNE_URL },
        { AppConstants.StatFields.PENETRATION_RATE, ImageConstants.Rune.PENETRATION_RUNE_URL },
        { AppConstants.StatFields.PENETRATION_RESISTANCE_RATE, ImageConstants.Rune.PENETRATION_RUNE_URL },
        { AppConstants.StatFields.EVASION_RATE, ImageConstants.Rune.EVASION_RUNE_URL },
        { AppConstants.StatFields.IGNORE_DAMAGE_ABSORPTION_RATE, ImageConstants.Rune.ABSORPTION_RUNE_URL },
        { AppConstants.StatFields.ABSORBED_DAMAGE_RATE, ImageConstants.Rune.ABSORPTION_RUNE_URL },
        { AppConstants.StatFields.VITALITY_REGENERATION_RATE, ImageConstants.Rune.VITALITY_RUNE_URL },
        { AppConstants.StatFields.VITALITY_REGENERATION_RESISTANCE_RATE, ImageConstants.Rune.VITALITY_RUNE_URL },
        { AppConstants.StatFields.ACCURACY_RATE, ImageConstants.Rune.ACCURACY_RUNE_URL },
        { AppConstants.StatFields.LIFE_STEAL_RATE, ImageConstants.Rune.LIFESTEAL_RUNE_URL },
        { AppConstants.StatFields.SHIELD_STRENGTH, ImageConstants.Rune.SHIELD_RUNE_URL },
        { AppConstants.StatFields.TENACITY, ImageConstants.Rune.TENACITY_RUNE_URL },
        { AppConstants.StatFields.RESISTANCE_RATE, ImageConstants.Rune.RESISTANCE_RUNE_URL },
        { AppConstants.StatFields.COMBO_RATE, ImageConstants.Rune.COMBO_RUNE_URL },
        { AppConstants.StatFields.IGNORE_COMBO_RATE, ImageConstants.Rune.COMBO_RUNE_URL },
        { AppConstants.StatFields.COMBO_DAMAGE_RATE, ImageConstants.Rune.COMBO_RUNE_URL },
        { AppConstants.StatFields.COMBO_RESISTANCE_RATE, ImageConstants.Rune.COMBO_RUNE_URL },
        { AppConstants.StatFields.STUN_RATE, ImageConstants.Rune.STUN_RUNE_URL },
        { AppConstants.StatFields.IGNORE_STUN_RATE, ImageConstants.Rune.STUN_RUNE_URL },
        { AppConstants.StatFields.MANA, ImageConstants.Rune.MANA_RUNE_URL },
        { AppConstants.StatFields.MANA_REGENERATION_RATE, ImageConstants.Rune.MANA_RUNE_URL },
        { AppConstants.StatFields.REFLECTION_RATE, ImageConstants.Rune.REFLECTION_RUNE_URL },
        { AppConstants.StatFields.IGNORE_REFLECTION_RATE, ImageConstants.Rune.REFLECTION_RUNE_URL },
        { AppConstants.StatFields.REFLECTION_DAMAGE_RATE, ImageConstants.Rune.REFLECTION_RUNE_URL },
        { AppConstants.StatFields.REFLECTION_RESISTANCE_RATE, ImageConstants.Rune.REFLECTION_RUNE_URL },
        { AppConstants.StatFields.DAMAGE_TO_DIFFERENT_FACTION_RATE, ImageConstants.Rune.FACTION_RUNE_URL },
        { AppConstants.StatFields.RESISTANCE_TO_DIFFERENT_FACTION_RATE, ImageConstants.Rune.FACTION_RUNE_URL },
        { AppConstants.StatFields.DAMAGE_TO_SAME_FACTION_RATE, ImageConstants.Rune.FACTION_RUNE_URL },
        { AppConstants.StatFields.RESISTANCE_TO_SAME_FACTION_RATE, ImageConstants.Rune.FACTION_RUNE_URL },
        { AppConstants.StatFields.NORMAL_DAMAGE_RATE, ImageConstants.Rune.NORMAL_RUNE_URL },
        { AppConstants.StatFields.NORMAL_RESISTANCE_RATE, ImageConstants.Rune.NORMAL_RUNE_URL },
        { AppConstants.StatFields.SKILL_DAMAGE_RATE, ImageConstants.Rune.SKILL_RUNE_URL },
        { AppConstants.StatFields.SKILL_RESISTANCE_RATE, ImageConstants.Rune.SKILL_RUNE_URL },
    };

    public void CreatePropertyRuneUI(string title, RawImage runeImage)
    {
        if (RuneIconByStat.TryGetValue(title, out var runeUrl))
        {
            runeImage.texture = TextureHelper.LoadTextureCached(runeUrl);
            runeImage.gameObject.SetActive(true);
        }
        else
        {
            runeImage.gameObject.SetActive(false);
        }
    }
}
