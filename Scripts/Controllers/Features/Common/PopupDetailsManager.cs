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
    private Transform MainPanel;
    private GameObject popupObject;
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

    public void Initialize()
    {
        MainMenuDetailPanelPrefab = UIManager.Instance.GetGameObject("MainMenuDetailPanelPrefab");
        ElementDetailsPrefab = UIManager.Instance.GetGameObject("ElementDetailsPrefab");
    }
    public void PopupDetails(object data, Transform panel)
    {
        MainPanel = panel;
        popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        // Kiểm tra kiểu của data và ép kiểu phù hợp
        if (data is CardHeroes cardHero)
        {
            // Xử lý đối tượng Card
            ShowCardHeroDetails(cardHero);
        }
        else if (data is Books book)
        {
            // Xử lý đối tượng Book
            ShowBookDetails(book);
        }
        else if (data is CardCaptains cardCaptain)
        {
            // Xử lý đối tượng Captain
            ShowCardCaptainDetails(cardCaptain);
        }
        else if (data is Pets pet)
        {
            // Xử lý đối tượng Pet
            ShowPetDetails(pet);
        }
        else if (data is CollaborationEquipments collaborationEquipmentsequipment)
        {
            // Xử lý đối tượng CollaborationEquipment
            ShowCollaborationEquipmentDetails(collaborationEquipmentsequipment);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            // Xử lý đối tượng Military
            ShowCardMilitaryDetails(cardMilitary);
        }
        else if (data is CardSpells cardSpell)
        {
            // Xử lý đối tượng Spell
            ShowCardSpellDetails(cardSpell);
        }
        else if (data is Collaborations collaboration)
        {
            // Xử lý đối tượng Collaboration
            ShowCollaborationDetails(collaboration);
        }
        else if (data is CardMonsters cardMonster)
        {
            // Xử lý đối tượng Monster
            ShowCardMonsterDetails(cardMonster);
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
        else if (data is MagicFormationCircles magicFormationCircle)
        {
            // Xử lý đối tượng Title
            ShowMagicFormationCircleDetails(magicFormationCircle);
        }
        else if (data is Relics relics)
        {
            // Xử lý đối tượng Title
            ShowRelicsDetails(relics);
        }
        else if (data is CardColonels cardColonel)
        {
            // Xử lý đối tượng colonels
            ShowCardColonelDetails(cardColonel);
        }
        else if (data is CardGenerals cardGeneral)
        {
            // Xử lý đối tượng Generals
            ShowCardGeneralDetails(cardGeneral);
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            // Xử lý đối tượng admirals
            ShowCardAdmiralDetails(cardAdmiral);
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
        else if (data is Talismans talisman)
        {
            // Xử lý đối tượng talisman
            ShowTalismanDetails(talisman);
        }
        else if (data is Puppets puppet)
        {
            // Xử lý đối tượng puppet
            ShowPuppetDetails(puppet);
        }
        else if (data is Alchemies alchemy)
        {
            // Xử lý đối tượng alchemy
            ShowAlchemyDetails(alchemy);
        }
        else if (data is Forges forge)
        {
            // Xử lý đối tượng forge
            ShowForgeDetails(forge);
        }
        else if (data is CardLives cardLife)
        {
            // Xử lý đối tượng card life
            ShowCardLifeDetails(cardLife);
        }
        else if (data is Artworks artwork)
        {
            // Xử lý đối tượng artwork
            ShowArtworkDetails(artwork);
        }
        else if (data is SpiritBeasts spiritBeast)
        {
            // Xử lý đối tượng spirit beast
            ShowSpiritBeastDetails(spiritBeast);
        }
        else if (data is SpiritCards spiritCards)
        {
            // Xử lý đối tượng spirit card
            ShowSpiritCardDetails(spiritCards);
        }
        else if (data is Cards card)
        {
            // Xử lý đối tượng card
            ShowCardDetails(card);
        }
        else if (data is Vehicles vehicle)
        {
            // Xử lý đối tượng vehicle
            ShowVehicleDetails(vehicle);
        }
        else if (data is Architectures architecture)
        {
            // Xử lý đối tượng architecture
            ShowArchitectureDetails(architecture);
        }
        else if (data is Technologies technology)
        {
            // Xử lý đối tượng technology
            ShowTechnologyDetails(technology);
        }
        else if (data is Cores core)
        {
            // Xử lý đối tượng core
            ShowCoreDetails(core);
        }
        else if (data is Weapons weapon)
        {
            // Xử lý đối tượng weapon
            ShowWeaponDetails(weapon);
        }
        else if (data is Robots robot)
        {
            // Xử lý đối tượng robot
            ShowRobotDetails(robot);
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
    private void ShowCardHeroDetails(CardHeroes cardHero)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardHero.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardHero.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardHero.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = cardHero.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardHero.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardHero.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardHero, popupObject);
    }
    private void ShowBookDetails(Books book)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(book.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = book.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(book.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = book.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{book.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = book.GetType().GetProperties();
        CreatePropertyUI(1, properties, book, popupObject);
    }
    private void ShowCardCaptainDetails(CardCaptains cardCaptain)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardCaptain.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardCaptain.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardCaptain.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = cardCaptain.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardCaptain.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardCaptain.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardCaptain, popupObject);
    }
    private void ShowPetDetails(Pets pet)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(pet.Image); // Lấy giá trị của image từ đối tượng Card
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
            if (pet.Type.Equals("Legendary_Dragon") || pet.Type.Equals("Naruto_Bijuu") || pet.Type.Equals("Naruto_Susanoo") || pet.Type.Equals("One_Piece_Ship") || pet.Type.Equals("Prime_Monster"))
            {
                newHeight = 700f;
            }
            // Chiều cao cố định
            float newWidth = newHeight * aspectRatio;

            // Cập nhật kích thước cho RectTransform
            ImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = pet.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(pet.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = pet.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{pet.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = pet.GetType().GetProperties();
        CreatePropertyUI(1, properties, pet, popupObject);
    }
    private void ShowCollaborationEquipmentDetails(CollaborationEquipments collaborationEquipment)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(collaborationEquipment.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = collaborationEquipment.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(collaborationEquipment.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = collaborationEquipment.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaborationEquipment.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = collaborationEquipment.GetType().GetProperties();
        CreatePropertyUI(1, properties, collaborationEquipment, popupObject);
    }
    private void ShowCardMilitaryDetails(CardMilitaries cardMilitary)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMilitary.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardMilitary.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardMilitary.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = cardMilitary.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMilitary.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardMilitary.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardMilitary, popupObject);
    }
    private void ShowCardSpellDetails(CardSpells cardSpell)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardSpell.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardSpell.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardSpell.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = cardSpell.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardSpell.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardSpell.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardSpell, popupObject);
    }
    private void ShowCollaborationDetails(Collaborations collaboration)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(collaboration.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = collaboration.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(collaboration.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = collaboration.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaboration.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = collaboration.GetType().GetProperties();
        CreatePropertyUI(1, properties, collaboration, popupObject);
    }
    private void ShowCardMonsterDetails(CardMonsters cardMonster)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMonster.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardMonster.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardMonster.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = cardMonster.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMonster.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardMonster.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardMonster, popupObject);
    }
    private void ShowEquipmentDetails(Equipments equipment)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(equipment.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = equipment.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(equipment.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = equipment.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });
        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = equipment.GetType().GetProperties();
        CreatePropertyUI(1, properties, equipment, popupObject);
    }
    private void ShowMedalDetails(Medals medal)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(medal.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = medal.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(medal.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = medals.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{medal.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = medal.GetType().GetProperties();
        CreatePropertyUI(1, properties, medal, popupObject);
    }
    private void ShowSkillDetails(Skills skill)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(skill.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = skill.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(skill.Power, false);

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{skill.Rare}");
        rareImage.texture = rareTexture;

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = skill.GetType().GetProperties();
        CreatePropertyUI(1, properties, skill, popupObject);
    }
    private void ShowSymbolDetails(Symbols symbol)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(symbol.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = symbol.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(symbol.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{symbol.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = symbol.GetType().GetProperties();
        CreatePropertyUI(1, properties, symbol, popupObject);
    }
    private void ShowTitleDetails(Titles title)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(title.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = title.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(title.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{title.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = title.GetType().GetProperties();
        CreatePropertyUI(1, properties, title, popupObject);
    }
    private void ShowBorderDetails(Borders border)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(border.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = border.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(border.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{border.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = border.GetType().GetProperties();
        CreatePropertyUI(1, properties, border, popupObject);
    }
    private void ShowAchievementDetails(Achievements achievement)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(achievement.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = achievement.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(achievement.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{achievement.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = achievement.GetType().GetProperties();
        CreatePropertyUI(1, properties, achievement, popupObject);
    }
    private void ShowMagicFormationCircleDetails(MagicFormationCircles magicFormationCircle)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(magicFormationCircle.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = magicFormationCircle.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(magicFormationCircle.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{magicFormationCircle.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = magicFormationCircle.GetType().GetProperties();
        CreatePropertyUI(1, properties, magicFormationCircle, popupObject);
    }
    private void ShowRelicsDetails(Relics relic)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(relic.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = relic.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(relic.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{relic.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = relic.GetType().GetProperties();
        CreatePropertyUI(1, properties, relic, popupObject);
    }
    private void ShowCardColonelDetails(CardColonels cardColonel)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardColonel.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardColonel.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardColonel.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardColonel.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardColonel.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardColonel, popupObject);
    }
    private void ShowCardGeneralDetails(CardGenerals cardGeneral)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardGeneral.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardGeneral.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardGeneral.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardGeneral.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardGeneral.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardGeneral, popupObject);
    }
    private void ShowCardAdmiralDetails(CardAdmirals cardAdmiral)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardAdmiral.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardAdmiral.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardAdmiral.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardAdmiral.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardAdmiral.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardAdmiral, popupObject);
    }
    private void ShowTalismanDetails(Talismans talisman)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(talisman.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = talisman.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(talisman.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{talisman.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = talisman.GetType().GetProperties();
        CreatePropertyUI(1, properties, talisman, popupObject);
    }
    private void ShowPuppetDetails(Puppets puppet)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(puppet.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = puppet.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(puppet.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{puppet.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = puppet.GetType().GetProperties();
        CreatePropertyUI(1, properties, puppet, popupObject);
    }
    private void ShowAlchemyDetails(Alchemies alchemy)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(alchemy.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = alchemy.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(alchemy.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{alchemy.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = alchemy.GetType().GetProperties();
        CreatePropertyUI(1, properties, alchemy, popupObject);
    }
    private void ShowForgeDetails(Forges forge)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(forge.Image); // Lấy giá trị của image từ đối tượng Card
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
        name.text = forge.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(forge.Power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{forge.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = forge.GetType().GetProperties();
        CreatePropertyUI(1, properties, forge, popupObject);
    }
    private void ShowCardLifeDetails(CardLives cardLife)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardLife.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardLife.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardLife.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = cardLife.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardLife.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = cardLife.GetType().GetProperties();
        CreatePropertyUI(1, properties, cardLife, popupObject);
    }
    private void ShowArtworkDetails(Artworks artwork)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(artwork.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = artwork.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(artwork.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = artwork.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{artwork.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = artwork.GetType().GetProperties();
        CreatePropertyUI(1, properties, artwork, popupObject);
    }
    private void ShowSpiritBeastDetails(SpiritBeasts spiritBeast)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(spiritBeast.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = spiritBeast.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(spiritBeast.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = spiritBeast.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spiritBeast.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = spiritBeast.GetType().GetProperties();
        CreatePropertyUI(1, properties, spiritBeast, popupObject);
    }
    private void ShowSpiritCardDetails(SpiritCards spiritCard)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(spiritCard.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = spiritCard.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(spiritCard.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = spiritCard.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spiritCard.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = spiritCard.GetType().GetProperties();
        CreatePropertyUI(1, properties, spiritCard, popupObject);
    }
    private void ShowCardDetails(Cards card)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(card.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = card.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(card.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = card.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = card.GetType().GetProperties();
        CreatePropertyUI(1, properties, card, popupObject);
    }
    private void ShowArchitectureDetails(Architectures architecture)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(architecture.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = architecture.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(architecture.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = architecture.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{architecture.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = architecture.GetType().GetProperties();
        CreatePropertyUI(1, properties, architecture, popupObject);
    }
    private void ShowTechnologyDetails(Technologies technology)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(technology.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = technology.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(technology.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = technology.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{technology.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = technology.GetType().GetProperties();
        CreatePropertyUI(1, properties, technology, popupObject);
    }
    private void ShowVehicleDetails(Vehicles vehicle)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(vehicle.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = vehicle.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(vehicle.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = vehicle.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{vehicle.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = vehicle.GetType().GetProperties();
        CreatePropertyUI(1, properties, vehicle, popupObject);
    }
    private void ShowCoreDetails(Cores core)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(core.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = core.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(core.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = core.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{core.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = core.GetType().GetProperties();
        CreatePropertyUI(1, properties, core, popupObject);
    }
    private void ShowWeaponDetails(Weapons weapon)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(weapon.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = weapon.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(weapon.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = weapon.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{weapon.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = weapon.GetType().GetProperties();
        CreatePropertyUI(1, properties, weapon, popupObject);
    }
    private void ShowRobotDetails(Robots robot)
    {
        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(robot.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = robot.Name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(robot.Power, false);

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = robot.Level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{robot.Rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ClosePopup(popupObject);
        });

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = robot.GetType().GetProperties();
        CreatePropertyUI(1, properties, robot, popupObject);
    }
    public void CreatePropertyUI(int status, PropertyInfo[] properties, object targetObject, GameObject currentObject)
    {
        Transform detailsContent = currentObject.transform.Find("Scroll View/Viewport/Content");

        Transform generalInformationPanel = detailsContent.transform.Find("GeneralInformation");
        Transform statsInformationPanel = detailsContent.transform.Find("StatInformation");
        Transform descriptionInformationPanel = detailsContent.transform.Find("DescriptionInformation");

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
                Color color;
                if (ColorUtility.TryParseHtmlString(ColorConstants.HexColor.descriptionColor, out color)) // Chuyển mã hex thành Color
                {
                    descriptionText.color = color; // Gán màu cho text
                }

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
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.PHYSICAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.PHYSICAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.PHYSICAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MAGICAL_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MAGICAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MAGICAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MAGICAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CHEMICAL_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.CHEMICAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CHEMICAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.CHEMICAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ATOMIC_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.ATOMIC_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ATOMIC_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.ATOMIC_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MENTAL_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MENTAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MENTAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MENTAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.HEALTH))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.HEALTH_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.SPEED)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.SPEED_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CRITICAL_RATE)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DAMAGE_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CRITICAL_DAMAGE_RATE)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.CRITICAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.DAMAGE_ABSORPTION_RATE)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ABSORPTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CRITICAL_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.CRITICAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_CRITICAL_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.CRITICAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.PENETRATION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.PENETRATION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.PENETRATION_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.PENETRATION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.EVASION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.EVASION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_DAMAGE_ABSORPTION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ABSORPTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ABSORBED_DAMAGE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ABSORPTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.VITALITY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.VITALITY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ACCURACY_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ACCURACY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.LIFE_STEAL_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.LIFESTEAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.SHIELD_STRENGTH))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.SHIELD_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.TENACITY))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.TENACITY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.RESISTANCE_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.COMBO_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_COMBO_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.COMBO_DAMAGE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.COMBO_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.STUN_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.STUN_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_STUN_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.STUN_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MANA))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.MANA_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MANA_REGENERATION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.MANA_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.REFLECTION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_REFLECTION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.REFLECTION_DAMAGE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.REFLECTION_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.DAMAGE_TO_DIFFERENT_FACTION_RATE) || title.Equals(AppConstants.StatFields.RESISTANCE_TO_DIFFERENT_FACTION_RATE) ||
         title.Equals(AppConstants.StatFields.DAMAGE_TO_SAME_FACTION_RATE) || title.Equals(AppConstants.StatFields.RESISTANCE_TO_SAME_FACTION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.FACTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.NORMAL_DAMAGE_RATE) || title.Equals(AppConstants.StatFields.NORMAL_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.NORMAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.SKILL_DAMAGE_RATE) || title.Equals(AppConstants.StatFields.SKILL_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.SKILL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        runeImage.gameObject.SetActive(true);
    }
}
