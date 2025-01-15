using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class PopupDetailsManager : MonoBehaviour
{
    private GameObject MainMenuDetailPanelPrefab;
    private GameObject ElementDetailsPrefab;
    private GameObject NumberDetailPrefab;
    private Transform MainPanel;
    // Start is called before the first frame update
    void Start()
    {
        MainMenuDetailPanelPrefab = UIManager.Instance.GetGameObject("MainMenuDetailPanelPrefab");
        ElementDetailsPrefab = UIManager.Instance.GetGameObject("ElementDetailsPrefab");
        NumberDetailPrefab = UIManager.Instance.GetGameObject("NumberDetailPrefab");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PopupDetails(object data, Transform panel)
    {
        MainPanel = panel;
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
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = card.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = card.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = card.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = card.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = card.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(card, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                    GridLayoutGroup gridLayout = descriptionPopupPanel.GetComponent<GridLayoutGroup>();
                    if (gridLayout != null)
                    {
                        gridLayout.cellSize = new Vector2(670, 800);
                    }
                }
                else
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
                                elementContentText.text = intValue.ToString();
                        }
                    }
                }
            }
        }
    }
    private void ShowBookDetails(Books book)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = book.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
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
        power.text = book.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = book.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{book.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = book.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(book, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                    GridLayoutGroup gridLayout = descriptionPopupPanel.GetComponent<GridLayoutGroup>();
                    if (gridLayout != null)
                    {
                        gridLayout.cellSize = new Vector2(670, 800);
                    }
                }
                else
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
                                elementContentText.text = intValue.ToString();
                        }
                    }
                }
            }
        }
    }
    private void ShowCardCaptainDetails(CardCaptains captains)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = captains.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = captains.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = captains.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = captains.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{captains.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = captains.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(captains, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
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
                                elementContentText.text = intValue.ToString();
                        }
                    }
                }
            }
        }
    }
    private void ShowPetDetails(Pets pet)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = pet.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
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
        power.text = pet.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = pet.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{pet.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = pet.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(pet, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
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
                                elementContentText.text = intValue.ToString();
                        }
                    }
                }
            }
        }
    }
    private void ShowCollaborationEquipmentDetails(CollaborationEquipment collaborationEquipment)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = collaborationEquipment.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
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
        power.text = collaborationEquipment.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = collaborationEquipment.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaborationEquipment.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = collaborationEquipment.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(collaborationEquipment, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCardMilitaryDetails(CardMilitary military)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = military.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = military.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = military.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = military.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{military.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = military.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(military, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
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
                                elementContentText.text = intValue.ToString();
                        }
                    }
                }
            }
        }
    }
    private void ShowCardSpellDetails(CardSpell spell)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = spell.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = spell.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = spell.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = spell.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spell.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = spell.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(spell, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCollaborationDetails(Collaboration collaboration)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = collaboration.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = collaboration.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = collaboration.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = collaboration.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaboration.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = collaboration.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(collaboration, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCardMonsterDetails(CardMonsters monsters)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = monsters.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = monsters.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = monsters.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = monsters.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{monsters.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = monsters.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(monsters, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
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
                                elementContentText.text = intValue.ToString();
                        }
                    }
                }
            }
        }
    }
    private void ShowEquipmentDetails(Equipments equipments)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = equipments.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
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
        power.text = equipments.power.ToString();

        TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        level.text = equipments.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipments.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));
        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = equipments.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(equipments, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowMedalDetails(Medals medals)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = medals.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
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
        power.text = medals.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = medals.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{medals.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = medals.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(medals, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowSkillDetails(Skills skills)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = skills.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
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
        power.text = skills.power.ToString();

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

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
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowSymbolDetails(Symbols symbols)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = symbols.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = symbols.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = symbols.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{symbols.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = symbols.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(symbols, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowTitleDetails(Titles titles)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = titles.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
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
        power.text = titles.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{titles.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = titles.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(titles, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowBorderDetails(Borders borders)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = borders.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
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
        power.text = borders.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{borders.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = borders.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(borders, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowAchievementDetails(Achievements achievements)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = achievements.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
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
        power.text = achievements.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{achievements.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = achievements.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(achievements, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowMagicFormationCircleDetails(MagicFormationCircle magicFormationCircle)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = magicFormationCircle.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
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
        power.text = magicFormationCircle.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{magicFormationCircle.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = magicFormationCircle.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(magicFormationCircle, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowRelicsDetails(Relics relics)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = relics.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
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
        power.text = relics.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{relics.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = relics.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(relics, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
                {
                    // Tạo một element mới từ prefab
                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);
                    // Gán tên thuộc tính vào TitleText
                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                    if (elementTitleText != null) elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);
                    // Gán giá trị thuộc tính vào ContentText
                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                    if (elementContentText != null) elementContentText.text = value != null ? value.ToString() : "null";
                }
            }
        }
    }
    private void ShowCardColonelsDetails(CardColonels colonels)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = colonels.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = colonels.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = colonels.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{colonels.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = colonels.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(colonels, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
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
                                elementContentText.text = intValue.ToString();
                        }
                    }
                }
            }
        }
    }
    private void ShowCardGeneralsDetails(CardGenerals generals)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = generals.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = generals.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = generals.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{generals.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = generals.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(generals, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
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
                                elementContentText.text = intValue.ToString();
                        }
                    }
                }
            }
        }
    }
    private void ShowCardAdmiralsDetails(CardAdmirals admirals)
    {
        // Tạo popup từ prefab
        GameObject popupObject = Instantiate(MainMenuDetailPanelPrefab, MainPanel);
        Transform numberDetailsPanel = popupObject.transform.Find("DictionaryCards/ScrollViewRight/Viewport/Content");
        GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, numberDetailsPanel);
        Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

        RawImage Image = popupObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = admirals.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = popupObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = admirals.name;

        TextMeshProUGUI power = popupObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = admirals.power.ToString();

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = skills.level.ToString();

        RawImage rareImage = popupObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{admirals.rare}");
        rareImage.texture = rareTexture;

        Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        // Dùng Reflection để lấy tất cả thuộc tính và giá trị
        PropertyInfo[] properties = admirals.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(admirals, null);
            if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
            && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
            && !property.Name.Equals("power") && !property.Name.Equals("status") && !property.Name.Equals("name")
            && !property.Name.Equals("image") && !property.Name.Equals("rare") && !property.Name.Equals("type")
            && !property.Name.Equals("star") && !property.Name.Equals("level"))
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
                    if (ColorUtility.TryParseHtmlString("#844000", out color)) // Chuyển mã hex thành Color
                    {
                        descriptionText.color = color; // Gán màu cho text
                    }

                    // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                    RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(600, 100);
                    rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                }
                else
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
                                elementContentText.text = intValue.ToString();
                        }
                    }
                }
            }
        }
    }
}
