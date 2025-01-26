using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class MainMenuDetailsManager : MonoBehaviour
{
    private GameObject MainMenuDetailPanel2Prefab;
    private GameObject TabButton4;
    private GameObject ElementDetailsPrefab;
    private GameObject NumberDetailPrefab;
    private GameObject NumberDetail2Prefab;
    private Transform MainPanel;
    private Transform RightButtonContent;
    private Transform DetailsPanel;
    private Transform LevelPanel;
    private Transform SkillsPanel;
    private Transform UpgradePanel;
    private Transform DetailsContent;
    private Transform LevelElementContent;
    private Transform LevelMaterialContent;
    private Transform SkillsContent;
    private Transform UpgradeElementContent;
    private Transform UpgradeMaterialContent;
    private GameObject currentObject;
    private GameObject ItemThird;
    private GameObject StarPrefab;
    private GameObject ElementDetails2Prefab;
    private string mainType;
    private string descriptionColor = "#844000";
    private double increasePerLevel = 0.1;
    private double increasePerUpgrade = 1.1;
    // Start is called before the first frame update
    void Start()
    {
        MainMenuDetailPanel2Prefab = UIManager.Instance.GetGameObject("MainMenuDetailPanel2Prefab");
        TabButton4 = UIManager.Instance.GetGameObject("TabButton4");
        ElementDetailsPrefab = UIManager.Instance.GetGameObject("ElementDetailsPrefab");
        NumberDetailPrefab = UIManager.Instance.GetGameObject("NumberDetailPrefab");
        NumberDetail2Prefab = UIManager.Instance.GetGameObject("NumberDetail2Prefab");
        ItemThird = UIManager.Instance.GetGameObject("ItemThird");
        StarPrefab = UIManager.Instance.GetGameObject("StarPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");


    }

    public void PopupDetails(object data, Transform panel)
    {
        MainPanel = panel;
        currentObject = Instantiate(MainMenuDetailPanel2Prefab, MainPanel);
        RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        DetailsPanel = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel");
        LevelPanel = currentObject.transform.Find("DictionaryCards/Content/LevelPanel");
        SkillsPanel = currentObject.transform.Find("DictionaryCards/Content/SkillsPanel");
        UpgradePanel = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel");
        DetailsContent = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel/Scroll View/Viewport/Content");
        LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        SkillsContent = currentObject.transform.Find("DictionaryCards/Content/SkillsPanel");
        UpgradeElementContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        UpgradeMaterialContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewMaterial/Viewport/Content");
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(currentObject));
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        // Kiểm tra kiểu của data và ép kiểu phù hợp
        if (data is CardHeroes card)
        {
            // Xử lý đối tượng Card
            mainType = "CardHeroes";
            ShowCardHeroesDetails(card);
        }
        else if (data is Books book)
        {
            // Xử lý đối tượng Book
            ShowBooksDetails(book);
        }
        else if (data is CardCaptains captain)
        {
            // Xử lý đối tượng Captain
            ShowCardCaptainsDetails(captain);
        }
        else if (data is Pets pet)
        {
            // Xử lý đối tượng Pet
            ShowPetsDetails(pet);
        }
        else if (data is CollaborationEquipment collaborationEquipmentsequipment)
        {
            // Xử lý đối tượng CollaborationEquipment
            ShowCollaborationEquipmentsDetails(collaborationEquipmentsequipment);
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
            ShowCollaborationsDetails(collaboration);
        }
        else if (data is CardMonsters monster)
        {
            // Xử lý đối tượng Monster
            ShowCardMonstersDetails(monster);
        }
        else if (data is Equipments equipment)
        {
            // Xử lý đối tượng Equipment
            ShowEquipmentsDetails(equipment);
        }
        else if (data is Medals medal)
        {
            // Xử lý đối tượng Medal
            ShowMedalsDetails(medal);
        }
        else if (data is Skills skill)
        {
            // Xử lý đối tượng Skill
            ShowSkillsDetails(skill);
        }
        else if (data is Symbols symbol)
        {
            // Xử lý đối tượng Symbol
            ShowSymbolsDetails(symbol);
        }
        else if (data is Titles title)
        {
            // Xử lý đối tượng Title
            ShowTitlesDetails(title);
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
        // else if (data is Borders borders)
        // {
        //     // Xử lý đối tượng borders
        //     ShowBordersDetails(borders);
        // }
        else if (data is Achievements achievements)
        {
            // Xử lý đối tượng achievements
            ShowAchievementsDetails(achievements);
        }
        else
        {
            Debug.LogError("Không hỗ trợ loại dữ liệu này!");
        }

    }
    void AssignButtonEvent(string buttonName, Transform panel, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = panel.Find(buttonName);
        if (buttonTransform != null)
        {
            Button button = buttonTransform.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(action);
            }
        }
        else
        {
            Debug.LogWarning($"Button {buttonName} not found!");
        }
    }
    private void CreateButton(int index, string itemName, Transform panel)
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
    public void ShowCardHeroesDetails(CardHeroes cardHeroes)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(cardHeroes));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(cardHeroes));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetSkills(cardHeroes));
        AssignButtonEvent("Button_4", RightButtonContent, () => GetUpgrade(cardHeroes));

        GetDetails(cardHeroes);
    }
    public void ShowCardCaptainsDetails(CardCaptains cardCaptains){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(cardCaptains));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(cardCaptains));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetSkills(cardCaptains));
        AssignButtonEvent("Button_4", RightButtonContent, () => GetUpgrade(cardCaptains));

        GetDetails(cardCaptains);
    }
    public void ShowCardColonelsDetails(CardColonels cardColonels){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(cardColonels));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(cardColonels));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetSkills(cardColonels));
        AssignButtonEvent("Button_4", RightButtonContent, () => GetUpgrade(cardColonels));

        GetDetails(cardColonels);
    }
    public void ShowCardGeneralsDetails(CardGenerals cardGenerals){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(cardGenerals));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(cardGenerals));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetSkills(cardGenerals));
        AssignButtonEvent("Button_4", RightButtonContent, () => GetUpgrade(cardGenerals));

        GetDetails(cardGenerals);
    }
    public void ShowCardAdmiralsDetails(CardAdmirals cardAdmirals){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(cardAdmirals));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(cardAdmirals));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetSkills(cardAdmirals));
        AssignButtonEvent("Button_4", RightButtonContent, () => GetUpgrade(cardAdmirals));

        GetDetails(cardAdmirals);
    }
    public void ShowCardMonstersDetails(CardMonsters cardMonsters){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(cardMonsters));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(cardMonsters));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetSkills(cardMonsters));
        AssignButtonEvent("Button_4", RightButtonContent, () => GetUpgrade(cardMonsters));

        GetDetails(cardMonsters);
    }
    public void ShowCardMilitaryDetails(CardMilitary cardMilitary){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(cardMilitary));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(cardMilitary));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetSkills(cardMilitary));
        AssignButtonEvent("Button_4", RightButtonContent, () => GetUpgrade(cardMilitary));

        GetDetails(cardMilitary);
    }
    public void ShowCardSpellDetails(CardSpell cardSpell){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(cardSpell));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(cardSpell));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetUpgrade(cardSpell));

        GetDetails(cardSpell);
    }
    public void ShowBooksDetails(Books books){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(books));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(books));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetSkills(books));
        AssignButtonEvent("Button_4", RightButtonContent, () => GetUpgrade(books));

        GetDetails(books);
    }
    public void ShowPetsDetails(Pets pets){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(pets));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(pets));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetSkills(pets));
        AssignButtonEvent("Button_4", RightButtonContent, () => GetUpgrade(pets));

        GetDetails(pets);
    }
    public void ShowCollaborationEquipmentsDetails(CollaborationEquipment collaborationEquipment){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(collaborationEquipment));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(collaborationEquipment));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetUpgrade(collaborationEquipment));

        GetDetails(collaborationEquipment);
    }
    public void ShowCollaborationsDetails(Collaboration collaboration){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(collaboration));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(collaboration));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetUpgrade(collaboration));

        GetDetails(collaboration);
    }
    public void ShowMedalsDetails(Medals medals){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(medals));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(medals));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetUpgrade(medals));

        GetDetails(medals);
    }
    public void ShowEquipmentsDetails(Equipments equipments){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(equipments));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(equipments));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetUpgrade(equipments));

        GetDetails(equipments);
    }
    public void ShowSymbolsDetails(Symbols symbols){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(symbols));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(symbols));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetUpgrade(symbols));

        GetDetails(symbols);
    }
    public void ShowTitlesDetails(Titles titles){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(titles));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(titles));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetUpgrade(titles));

        GetDetails(titles);
    }
    public void ShowSkillsDetails(Skills skills){
        
    }
    public void ShowMagicFormationCircleDetails(MagicFormationCircle magicFormationCircle){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(magicFormationCircle));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(magicFormationCircle));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetUpgrade(magicFormationCircle));

        GetDetails(magicFormationCircle);
    }
    public void ShowRelicsDetails(Relics relics){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(relics));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(relics));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetUpgrade(relics));

        GetDetails(relics);
    }
    public void ShowAchievementsDetails(Achievements achievements){
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () => GetDetails(achievements));
        AssignButtonEvent("Button_2", RightButtonContent, () => GetLevel(achievements));
        AssignButtonEvent("Button_3", RightButtonContent, () => GetUpgrade(achievements));

        GetDetails(achievements);
    }
    public void Close(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public void GetDetails(object obj)
    {
        DetailsPanel.gameObject.SetActive(true);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        Close(LevelElementContent);
        Close(LevelMaterialContent);
        Close(SkillsPanel);
        Close(UpgradeElementContent);
        Close(UpgradeMaterialContent);
        if (obj is CardHeroes cardHeroes)
        {
            GameObject firstDetailsObject = Instantiate(NumberDetail2Prefab, DetailsContent);
            GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, DetailsContent);
            GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, DetailsContent);
            Transform firstPopupPanel = firstDetailsObject.transform.Find("ElementDetails");
            Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
            Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = cardHeroes.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = cardHeroes.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = cardHeroes.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardHeroes.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardHeroes.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardHeroes, null);
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
                        GridLayoutGroup gridLayout = descriptionPopupPanel.GetComponent<GridLayoutGroup>();
                        if (gridLayout != null)
                        {
                            gridLayout.cellSize = new Vector2(670, 800);
                        }
                    }
                    else if (property.Name.Equals("power") || property.Name.Equals("rare") || property.Name.Equals("type")
                    || property.Name.Equals("star") || property.Name.Equals("level"))
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, firstPopupPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = value != null ? value.ToString() : "";
                    }
                    else
                    {
                        // Kiểm tra nếu value không phải null
                        if (value != null)
                        {
                            if (value is double intValue && intValue != -1)
                            {
                                if (property.Name.Contains("all"))
                                {
                                    // Tạo một element mới từ prefab
                                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);

                                    // Gán tên thuộc tính vào TitleText
                                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                    if (elementTitleText != null)
                                        elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

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
    }
    public void GetLevel(object obj)
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(true);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        Close(DetailsContent);
        Close(SkillsPanel);
        Close(UpgradeElementContent);
        Close(UpgradeMaterialContent);

        if (obj is CardHeroes cardHeroes)
        {
            PropertyInfo[] properties = cardHeroes.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardHeroes, null);
                if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
                && !property.Name.Equals("status") && !property.Name.Equals("name")
                && !property.Name.Equals("image") && !property.Name.Equals("description") && !property.Name.Equals("power")
                && !property.Name.Equals("rare") && !property.Name.Equals("type")
                && !property.Name.Equals("star") && !property.Name.Equals("level"))
                {
                    // Kiểm tra nếu value không phải null
                    if (value != null)
                    {
                        if (value is double intValue && intValue != -1)
                        {
                            if (!property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, LevelElementContent);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                {
                                    double newintValue = increasePerLevel * intValue;
                                    elementContentText.text = "+" + newintValue.ToString();
                                    Color greenColor;
                                    if (ColorUtility.TryParseHtmlString("#32CD32", out greenColor)) // Màu xanh lá LimeGreen
                                    {
                                        elementContentText.color = greenColor;
                                        elementContentText.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, Color.green); // Màu phát sáng
                                        elementContentText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.5f); // Độ mạnh phát sáng (giảm giá trị
                                    }
                                }
                            }
                        }
                    }
                }
                else if (property.Name.Equals("level"))
                {
                    TextMeshProUGUI currentLevelText = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/Level/CurrentLevelText").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI nextLevelText = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/Level/NextLevelText").GetComponent<TextMeshProUGUI>();
                    currentLevelText.text = value.ToString();
                    int nextLevel = (int)value + 1;
                    if ((int)value == 100000)
                    {
                        nextLevelText.text = "Max";
                    }
                    else
                    {
                        nextLevelText.text = nextLevel.ToString();
                    }
                }
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items.Add(item.GetUserItemByName("Exp Bottle lv1"));
            items.Add(item.GetUserItemByName("Exp Bottle lv2"));
            items.Add(item.GetUserItemByName("Exp Bottle lv3"));
            items.Add(item.GetUserItemByName("Exp Bottle lv4"));
            items.Add(item.GetUserItemByName("Exp Bottle lv5"));
            items.Add(item.GetUserItemByName("Exp Bottle lv6"));
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ItemThird, LevelMaterialContent);

                RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
                string fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString();
            }
        }
    }
    public void GetSkills(object obj)
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(true);
        UpgradePanel.gameObject.SetActive(false);
        Close(DetailsContent);
        Close(LevelElementContent);
        Close(LevelMaterialContent);
        Close(UpgradeElementContent);
        Close(UpgradeMaterialContent);
        if (obj is CardHeroes cardHeroes)
        {

        }
    }
    public void GetUpgrade(object obj)
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(true);
        Close(DetailsContent);
        Close(LevelElementContent);
        Close(LevelMaterialContent);
        Close(SkillsPanel);
        if (obj is CardHeroes cardHeroes)
        {
            PropertyInfo[] properties = cardHeroes.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardHeroes, null);
                if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
                && !property.Name.Equals("status") && !property.Name.Equals("name")
                && !property.Name.Equals("image") && !property.Name.Equals("description") && !property.Name.Equals("power")
                && !property.Name.Equals("rare") && !property.Name.Equals("type")
                && !property.Name.Equals("star") && !property.Name.Equals("level"))
                {
                    // Kiểm tra nếu value không phải null
                    if (value != null)
                    {
                        if (value is double intValue && intValue != -1)
                        {
                            if (!property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, UpgradeElementContent);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                {
                                    double newintValue = increasePerUpgrade * intValue;
                                    elementContentText.text = "+" + newintValue.ToString();
                                    Color greenColor;
                                    if (ColorUtility.TryParseHtmlString("#32CD32", out greenColor)) // Màu xanh lá LimeGreen
                                    {
                                        elementContentText.color = greenColor;
                                        elementContentText.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, Color.green); // Màu phát sáng
                                        elementContentText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.5f); // Độ mạnh phát sáng (giảm giá trị
                                    }
                                }
                            }
                        }
                    }
                }
                else if (property.Name.Equals("star"))
                {
                    TextMeshProUGUI currentLevelText = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/CurrentLevelText").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI nextLevelText = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/NextLevelText").GetComponent<TextMeshProUGUI>();
                    currentLevelText.text = value.ToString();
                    int nextLevel = (int)value + 1;
                    if ((int)value == 100000)
                    {
                        nextLevelText.text = "Max";
                    }
                    else
                    {
                        nextLevelText.text = nextLevel.ToString();
                    }
                }
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items.Add(item.GetUserItemByName("Breakthrough Token"));
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardHeroes.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = cardHeroes.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardHeroes.quantity.ToString() + "/" + (cardHeroes.star + 1).ToString();

            int imageIndex = ((cardHeroes.star + 1) % 5);
            int starIndex = ((cardHeroes.star - 1) % 7);
            if (cardHeroes.star == 0)
            {
                starIndex = 0;
            }
            Transform currentStar = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/CurrentStar");
            Transform nextStar = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/NextStar");
            for (int i = 0; i < imageIndex - 1; i++)
            {
                GameObject starObject = Instantiate(StarPrefab, currentStar);

                RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
                GetStarImage(starImage,starIndex);
            }
            for (int i = 0; i < imageIndex; i++)
            {
                GameObject starObject = Instantiate(StarPrefab, nextStar);

                RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
                GetStarImage(starImage,starIndex);
            }
        }
    }
    public void GetStarImage(RawImage starImage, int starIndex)
    {
        Texture starTexture = Resources.Load<Texture>($"UI/UI/Star1");
        switch (starIndex)
        {
            case 0:
                starTexture = Resources.Load<Texture>($"UI/UI/Star1");
                starImage.texture = starTexture;
                break;
            case 1:
                starTexture = Resources.Load<Texture>($"UI/UI/Star2");
                starImage.texture = starTexture;
                break;
            case 2:
                starTexture = Resources.Load<Texture>($"UI/UI/Star3");
                starImage.texture = starTexture;
                break;
            case 3:
                starTexture = Resources.Load<Texture>($"UI/UI/Star4");
                starImage.texture = starTexture;
                break;
            case 4:
                starTexture = Resources.Load<Texture>($"UI/UI/Star5");
                starImage.texture = starTexture;
                break;
            case 5:
                starTexture = Resources.Load<Texture>($"UI/UI/Star6");
                starImage.texture = starTexture;
                break;
            case 6:
                starTexture = Resources.Load<Texture>($"UI/UI/Star7");
                starImage.texture = starTexture;
                break;
            default:
                starTexture = Resources.Load<Texture>($"UI/UI/Star1");
                starImage.texture = starTexture;
                break;
        }
    }
}
