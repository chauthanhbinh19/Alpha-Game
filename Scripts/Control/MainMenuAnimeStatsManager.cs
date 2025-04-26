using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuAnimeStatsManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private GameObject MainMenuAnimePanelPrefab;
    private GameObject AnimeSlotPrefab;
    private GameObject buttonPrefab;
    private GameObject currentObject;
    private GameObject slotObject;
    private GameObject ElementDetails2Prefab;
    private Button UpLevelButton;
    private Button UpMaxLevelButton;
    private Transform LevelCondition;
    private string mainType;
    // Start is called before the first frame update
    void Start()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainMenuAnimePanelPrefab = UIManager.Instance.GetGameObject("MainMenuAnimePanelPrefab");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        AnimeSlotPrefab = UIManager.Instance.GetGameObject("AnimeSlotPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");
    }

    public void CreateMainMenuAnimeStatsManager(object data)
    {
        currentObject = Instantiate(MainMenuAnimePanelPrefab, MainPanel);
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = "Anime";
        UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        CloseButton.onClick.AddListener(() => Destroy(currentObject));

        LevelCondition = currentObject.transform.Find("DictionaryCards/LevelCondition");
        // List<string> uniqueTypes = new List<string>
        // {
        //     "Omnivory I", "Omnivory II", "Omnivory III", "Omnivory IV", "Omnivory V",
        //     "Omnivory VI", "Omnivory VII", "Omnivory VIII", "Omnivory IX", "Omnivory X"
        // };
        Dictionary<string, int> uniqueTypes = new Dictionary<string, int>();
        Features features = new Features();
        uniqueTypes = features.GetFeaturesByType("Anime");
        if (uniqueTypes.Count > 0)
        {
            int index = 0;
            foreach (var kvp in uniqueTypes)
            {
                // Tạo một nút mới từ prefab
                string subtype = kvp.Key;
                int value = kvp.Value;
                GameObject button = Instantiate(buttonPrefab, TabButtonPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnButtonClick(button, data, subtype, value));

                if (index == 0)
                {
                    mainType = subtype;
                    ChangeButtonBackground(button, "Background_V4_166");
                    // mainId = cardHeroes.id;
                    CreateAnimeStats();
                    if (User.CurrentUserLevel >= value)
                    {
                        LevelCondition.gameObject.SetActive(false);
                    }
                    else
                    {
                        LevelCondition.gameObject.SetActive(true);
                        TextMeshProUGUI warningText = LevelCondition.Find("WarningText").GetComponent<TextMeshProUGUI>();
                        warningText.text = $"Your level is too low. Required level: {value}. Please level up and try again.";
                    }
                }
                else
                {
                    ChangeButtonBackground(button, "Background_V4_167");
                }
                CheckLockedButton(data, value, button);
                index = index + 1;
            }
        }
    }
    public void CheckLockedButton(object data, int value, GameObject button)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        RawImage buttonLockedImage = button.transform.Find("Locked")?.GetComponent<RawImage>();
        if (data is CardHeroes cardHeroes)
        {
            // mainId = cardHeroes.id;
            if (cardHeroes.level >= value)
            {
                buttonImage.color = HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is Books books)
        {
            // mainId = books.id;
            if (books.level >= value)
            {
                buttonImage.color = HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardCaptains cardCaptains)
        {
            // mainId = cardCaptains.id;
            if (cardCaptains.level >= value)
            {
                buttonImage.color = HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is Pets pets)
        {
            // mainId = pets.id;
            if (pets.level >= value)
            {
                buttonImage.color = HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardMilitary cardMilitary)
        {
            // mainId = cardMilitary.id;
            if (cardMilitary.level >= value)
            {
                buttonImage.color = HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardSpell cardSpell)
        {
            // mainId = cardSpell.id;
            if (cardSpell.level >= value)
            {
                buttonImage.color = HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardMonsters cardMonsters)
        {
            // mainId = cardMonsters.id;
            if (cardMonsters.level >= value)
            {
                buttonImage.color = HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardColonels cardColonels)
        {
            // mainId = cardColonels.id;
            if (cardColonels.level >= value)
            {
                buttonImage.color = HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardGenerals cardGenerals)
        {
            // mainId = cardGenerals.id;
            if (cardGenerals.level >= value)
            {
                buttonImage.color = HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            // mainId = cardAdmirals.id;
            if (cardAdmirals.level >= value)
            {
                buttonImage.color = HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is Equipments equipments)
        {
            // mainId = cardAdmirals.id;
            if (equipments.level >= value)
            {
                buttonImage.color = HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
    }
    public Color HexToColor(string hex)
    {
        if (!hex.StartsWith("#"))
        {
            hex = "#" + hex;
        }

        Color newColor;
        if (ColorUtility.TryParseHtmlString(hex, out newColor))
        {
            return newColor;
        }

        return Color.white; // Trả về màu trắng nếu mã màu không hợp lệ
    }
    void OnButtonClick(GameObject clickedButton, object data, string type, int value)
    {
        foreach (Transform child in TabButtonPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ChangeButtonBackground(button.gameObject, "Background_V4_167"); // Giả sử bạn có texture trắng
            }
        }

        mainType = type;
        ChangeButtonBackground(clickedButton, "Background_V4_166");

        // mainId = cardHeroes.id;
        CreateAnimeStats();
        if (User.CurrentUserLevel >= value)
        {
            LevelCondition.gameObject.SetActive(false);
        }
        else
        {
            LevelCondition.gameObject.SetActive(true);
            TextMeshProUGUI warningText = LevelCondition.Find("WarningText").GetComponent<TextMeshProUGUI>();
            warningText.text = $"Your level is too low. Required level: {value}. Please level up and try again.";
        }
    }
    private void ChangeButtonBackground(GameObject button, string image)
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
    public void Close(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public void CreateAnimeStats()
    {
        AnimeStats animeStats = new AnimeStats();
        animeStats = animeStats.GetAnimeStats(mainType, User.CurrentUserId);
        slotObject = Instantiate(AnimeSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType, animeStats.level);
        SetMaterialUI(mainType, animeStats.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (animeStats.level == 0) ? 1 : (animeStats.level % levelsPerSkill == 0 ? levelsPerSkill : animeStats.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                AnimeStats newanimeStats = new AnimeStats();
                newanimeStats = EnhanceAnimeStats(animeStats, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                UpLevel(newanimeStats, mainType);
                double newPower = teams.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateAnimeStats();
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            int level = CalculateMaxMaterialLevel(items.quantity, animeStats.level);
            int materialQuantity = CalculateMaxMaterialQuantity(items.quantity, animeStats.level);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                AnimeStats newanimeStats = new AnimeStats();
                newanimeStats = EnhanceAnimeStats(animeStats, level);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                UpLevel(newanimeStats, mainType);
                double newPower = teams.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateAnimeStats();
            }
        });
    }
    public void SetUI(GameObject gameObject, string type, int level = 0)
    {
        RawImage BackgroundImage = gameObject.transform.Find("Background").GetComponent<RawImage>();
        Texture backgroundTexture = Resources.Load<Texture>("UI/Background3/Angel_Wings");
        BackgroundImage.texture = backgroundTexture;

        int totalSkills = 10;
        int levelsPerSkill = 1000;

        // Đặt tất cả kỹ năng về trạng thái mặc định (đen + text "0/1000")
        for (int i = 1; i <= totalSkills; i++)
        {
            Transform aptitudeSkill = gameObject.transform.Find($"UpgradeSkill{i}");
            if (aptitudeSkill == null) continue;

            RawImage aptitudeImage = aptitudeSkill.Find("AptitudeImage").GetComponent<RawImage>();
            TextMeshProUGUI levelText = aptitudeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

            Texture texture = Resources.Load<Texture>($"UI/Rank/Angel");
            aptitudeImage.texture = texture;

            if (aptitudeImage != null) aptitudeImage.color = Color.black;
            if (levelText != null) levelText.text = $"0/{levelsPerSkill}";
        }

        // Xác định số kỹ năng được kích hoạt
        int activeSkillsCount = Mathf.Clamp((level / levelsPerSkill), 1, totalSkills);
        for (int i = 1; i <= activeSkillsCount; i++)
        {
            Transform activeSkill = gameObject.transform.Find($"UpgradeSkill{i}");
            if (activeSkill != null)
            {
                RawImage activeImage = activeSkill.Find("AptitudeImage").GetComponent<RawImage>();
                TextMeshProUGUI activeLevelText = activeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

                if (activeImage != null && level != 0) activeImage.color = Color.white;

                if (activeLevelText != null)
                {
                    // Kiểm tra nếu level là bội số của levelsPerSkill (1000, 2000, ..., 10000)
                    int displayedLevel = (level % levelsPerSkill == 0) ? levelsPerSkill : level % levelsPerSkill;
                    activeLevelText.text = $"{displayedLevel}/{levelsPerSkill}";
                }
            }
        }
        TextMeshProUGUI LevelText = gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        LevelText.text = level.ToString();
    }
    public void SetMaterialUI(string type, int level = 0, int userMaterialQuantity = 0)
    {
        int levelsPerSkill = 1000;
        int materialQuantity = (level == 0) ? 1 : (level % levelsPerSkill == 0 ? levelsPerSkill : level % levelsPerSkill);
        Transform OneLevelMaterial = currentObject.transform.Find("DictionaryCards/OneLevelMaterial");
        Transform MaxLevelMaterial = currentObject.transform.Find("DictionaryCards/MaxLevelMaterial");
        Close(OneLevelMaterial);
        Close(MaxLevelMaterial);
        GameObject oneLevelMaterialObject = Instantiate(ElementDetails2Prefab, OneLevelMaterial);
        GameObject maxLevelMaterialObject = Instantiate(ElementDetails2Prefab, MaxLevelMaterial);

        RawImage oneLevelImage = oneLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture oneLevelTexture = Resources.Load<Texture>($"Item/Material/{type}");
        oneLevelImage.texture = oneLevelTexture;

        RectTransform oneLevelRectTransform = oneLevelImage.GetComponent<RectTransform>();
        oneLevelRectTransform.sizeDelta = new Vector2(50, 50);

        TextMeshProUGUI oneLevelQuantity = oneLevelMaterialObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        oneLevelQuantity.text = userMaterialQuantity + "/" + materialQuantity;

        RawImage maxLevelImage = maxLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture maxLevelTexture = Resources.Load<Texture>($"Item/Material/{type}");
        maxLevelImage.texture = maxLevelTexture;

        TextMeshProUGUI maxLevelQuantity = maxLevelMaterialObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        maxLevelQuantity.text = userMaterialQuantity + "/" + CalculateMaxMaterialQuantity(userMaterialQuantity, level);

        RectTransform maxLevelRectTransform = maxLevelImage.GetComponent<RectTransform>();
        maxLevelRectTransform.sizeDelta = new Vector2(50, 50);
    }
    public int CalculateMaxMaterialQuantity(int materialQuantity, int currentLevel)
    {
        int levelsPerSkill = 500;
        int maxLevel = 10000; // Giới hạn level tối đa

        int totalMaterialUsed = 0;
        int levelsGained = 0;

        for (int level = currentLevel; level < maxLevel; level++)
        {
            int requiredMaterial = level % levelsPerSkill;

            // Nếu level = 0, thì cần 1 material để lên cấp
            if (level == 0)
                requiredMaterial = 1;
            else if (requiredMaterial == 0)
                requiredMaterial = levelsPerSkill; // Đảm bảo level bội số vẫn cần 500 material

            if (totalMaterialUsed + requiredMaterial > materialQuantity)
                break; // Dừng nếu không đủ material để lên level tiếp theo

            totalMaterialUsed += requiredMaterial;
            levelsGained++;
        }

        return totalMaterialUsed; // Tổng số material có thể sử dụng
    }
    public int CalculateMaxMaterialLevel(int materialQuantity, int currentLevel)
    {
        int levelsPerSkill = 500;
        int maxLevel = 10000; // Giới hạn level tối đa

        int totalMaterialUsed = 0;
        int levelsGained = 0;

        for (int level = currentLevel; level < maxLevel; level++)
        {
            int requiredMaterial = level % levelsPerSkill;

            // Nếu level = 0, thì cần 1 material để lên cấp
            if (level == 0)
                requiredMaterial = 1;
            else if (requiredMaterial == 0)
                requiredMaterial = levelsPerSkill; // Đảm bảo level bội số vẫn cần 500 material

            if (totalMaterialUsed + requiredMaterial > materialQuantity)
                break; // Dừng nếu không đủ material để lên level tiếp theo

            totalMaterialUsed += requiredMaterial;
            levelsGained++;
        }

        return levelsGained; // Tổng số material có thể sử dụng
    }
    public AnimeStats EnhanceAnimeStats(AnimeStats animeStats, int level)
    {
        int startLevel = animeStats.level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = 10;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                animeStats.health += 10000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                animeStats.physical_attack += 1500000 * statMultiplier;
                animeStats.physical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                animeStats.magical_attack += 1500000 * statMultiplier;
                animeStats.magical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                animeStats.chemical_attack += 1500000 * statMultiplier;
                animeStats.chemical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                animeStats.atomic_attack += 1500000 * statMultiplier;
                animeStats.atomic_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                animeStats.mental_attack += 1500000 * statMultiplier;
                animeStats.mental_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                animeStats.speed += 1500000 * statMultiplier;
                animeStats.critical_damage_rate += 0.1 * statMultiplier;
                animeStats.critical_rate += 0.1 * statMultiplier;
                animeStats.penetration_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                animeStats.evasion_rate += 0.1 * statMultiplier;
                animeStats.damage_absorption_rate += 0.1 * statMultiplier;
                animeStats.vitality_regeneration_rate += 0.1 * statMultiplier;
                animeStats.accuracy_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                animeStats.lifesteal_rate += 0.1 * statMultiplier;
                animeStats.mana += 1500000 * statMultiplier;
                animeStats.mana_regeneration_rate += 0.1 * statMultiplier;
                animeStats.shield_strength += 1500000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                animeStats.tenacity += 0.5 * statMultiplier;
                animeStats.resistance_rate += 0.1 * statMultiplier;
                animeStats.combo_rate += 0.1 * statMultiplier;
                animeStats.reflection_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                animeStats.damage_to_different_faction_rate += 0.1 * statMultiplier;
                animeStats.resistance_to_different_faction_rate += 0.1 * statMultiplier;
                animeStats.damage_to_same_faction_rate += 0.1 * statMultiplier;
                animeStats.resistance_to_same_faction_rate += 0.1 * statMultiplier;
                animeStats.percent_all_health += 5 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                animeStats.percent_all_physical_attack += 5 * statMultiplier;
                animeStats.percent_all_physical_defense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                animeStats.percent_all_magical_attack += 5 * statMultiplier;
                animeStats.percent_all_magical_defense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                animeStats.percent_all_chemical_attack += 5 * statMultiplier;
                animeStats.percent_all_chemical_defense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                animeStats.percent_all_atomic_attack += 5 * statMultiplier;
                animeStats.percent_all_atomic_defense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                animeStats.percent_all_mental_attack += 5 * statMultiplier;
                animeStats.percent_all_mental_defense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                animeStats.physical_attack += 1500000 * statMultiplier;
                animeStats.magical_attack += 1500000 * statMultiplier;
                animeStats.chemical_attack += 1500000 * statMultiplier;
                animeStats.atomic_attack += 1500000 * statMultiplier;
                animeStats.mental_attack += 1500000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                animeStats.physical_defense += 1500000 * statMultiplier;
                animeStats.magical_defense += 1500000 * statMultiplier;
                animeStats.chemical_defense += 1500000 * statMultiplier;
                animeStats.atomic_defense += 1500000 * statMultiplier;
                animeStats.mental_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                animeStats.speed += 1500000 * statMultiplier;
                animeStats.critical_damage_rate += 0.1 * statMultiplier;
                animeStats.critical_rate += 0.1 * statMultiplier;
                animeStats.penetration_rate += 0.1 * statMultiplier;
                animeStats.evasion_rate += 0.1 * statMultiplier;
                animeStats.damage_absorption_rate += 0.1 * statMultiplier;
                animeStats.vitality_regeneration_rate += 0.1 * statMultiplier;
                animeStats.accuracy_rate += 0.1 * statMultiplier;
                animeStats.lifesteal_rate += 0.1 * statMultiplier;
                animeStats.mana += 1500000 * statMultiplier;
                animeStats.mana_regeneration_rate += 0.1 * statMultiplier;
                animeStats.shield_strength += 1500000 * statMultiplier;
                animeStats.tenacity += 0.5 * statMultiplier;
                animeStats.resistance_rate += 0.1 * statMultiplier;
                animeStats.combo_rate += 0.1 * statMultiplier;
                animeStats.reflection_rate += 0.1 * statMultiplier;
                animeStats.damage_to_different_faction_rate += 0.1 * statMultiplier;
                animeStats.resistance_to_different_faction_rate += 0.1 * statMultiplier;
                animeStats.damage_to_same_faction_rate += 0.1 * statMultiplier;
                animeStats.resistance_to_same_faction_rate += 0.1 * statMultiplier;
            }
        }

        animeStats.level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return animeStats;
    }
    public void UpLevel(AnimeStats animeStats, string type)
    {
        animeStats.InsertOrUpdateAnimeStats(animeStats, type, User.CurrentUserId);
    }
}
