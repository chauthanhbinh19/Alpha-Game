using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAptitudeManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private GameObject AptitudeSlotPrefab;
    private GameObject buttonPrefab;
    private GameObject MainMenuAptitudePanelPrefab;
    private GameObject currentObject;
    private GameObject slotObject;
    private GameObject ElementDetails2Prefab;
    private Button UpLevelButton;
    private Button UpMaxLevelButton;
    private string mainType;
    // Start is called before the first frame update
    void Start()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainMenuAptitudePanelPrefab = UIManager.Instance.GetGameObject("MainMenuAptitudePanelPrefab");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        AptitudeSlotPrefab = UIManager.Instance.GetGameObject("AptitudeSlotPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");
    }

    public void CreateMainMenuAptitudeManager(object data)
    {
        currentObject = Instantiate(MainMenuAptitudePanelPrefab, MainPanel);
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        CloseButton.onClick.AddListener(() => Destroy(currentObject));

        List<string> uniqueTypes = new List<string>
        {
            "Celestial Radiance", "Chaos Force", "Cosmic Insight", "Embrace Death", "Fate Weaver",
            "Frost Sovereignty", "Illusion Craft", "Infernal Wrath", "Law Karma", "Lifeforce Dominion",
            "Lightning Dominion", "Lunar Blessing", "Mind Dominion", "Storm Mastery", "Strength Titans",
            "Sun Sovereignty", "Tidal Dominion", "Time Ascendancy", "Void Manipulation", "Yin-Yang Harmony"
        };
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                // Tạo một nút mới từ prefab
                string subtype = uniqueTypes[i];
                GameObject button = Instantiate(buttonPrefab, TabButtonPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnButtonClick(button, data, subtype));

                if (i == 0)
                {
                    mainType = subtype;
                    ChangeButtonBackground(button, "Background_V4_166");
                    if (data is CardHeroes cardHeroes)
                    {
                        // mainId = cardHeroes.id;
                        CreateCardHeroesEquipments(cardHeroes);
                    }
                    else if (data is Books books)
                    {
                        // mainId = books.id;
                        CreateBooksEquipments(books);
                    }
                    else if (data is CardCaptains cardCaptains)
                    {
                        // mainId = cardCaptains.id;
                        CreateCardCaptainsEquipments(cardCaptains);
                    }
                    else if (data is Pets pets)
                    {
                        // mainId = pets.id;
                        CreatePetsEquipments(pets);
                    }
                    else if (data is CardMilitary cardMilitary)
                    {
                        // mainId = cardMilitary.id;
                        CreateCardMilitaryEquipments(cardMilitary);
                    }
                    else if (data is CardSpell cardSpell)
                    {
                        // mainId = cardSpell.id;
                        CreateCardSpellEquipments(cardSpell);
                    }
                    else if (data is CardMonsters cardMonsters)
                    {
                        // mainId = cardMonsters.id;
                        CreateCardMonstersEquipments(cardMonsters);
                    }
                    else if (data is CardColonels cardColonels)
                    {
                        // mainId = cardColonels.id;
                        CreateCardColonelsEquipments(cardColonels);
                    }
                    else if (data is CardGenerals cardGenerals)
                    {
                        // mainId = cardGenerals.id;
                        CreateCardGeneralsEquipments(cardGenerals);
                    }
                    else if (data is CardAdmirals cardAdmirals)
                    {
                        // mainId = cardAdmirals.id;
                        CreateCardAdmiralsEquipments(cardAdmirals);
                    }
                }
                else
                {
                    ChangeButtonBackground(button, "Background_V4_167");
                }
            }
        }
    }
    void OnButtonClick(GameObject clickedButton, object data, string type)
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

        if (data is CardHeroes cardHeroes)
        {
            // mainId = cardHeroes.id;
            CreateCardHeroesEquipments(cardHeroes);
        }
        else if (data is Books books)
        {
            // mainId = books.id;
            CreateBooksEquipments(books);
        }
        else if (data is CardCaptains cardCaptains)
        {
            // mainId = cardCaptains.id;
            CreateCardCaptainsEquipments(cardCaptains);
        }
        else if (data is Pets pets)
        {
            // mainId = pets.id;
            CreatePetsEquipments(pets);
        }
        else if (data is CardMilitary cardMilitary)
        {
            // mainId = cardMilitary.id;
            CreateCardMilitaryEquipments(cardMilitary);
        }
        else if (data is CardSpell cardSpell)
        {
            // mainId = cardSpell.id;
            CreateCardSpellEquipments(cardSpell);
        }
        else if (data is CardMonsters cardMonsters)
        {
            // mainId = cardMonsters.id;
            CreateCardMonstersEquipments(cardMonsters);
        }
        else if (data is CardColonels cardColonels)
        {
            // mainId = cardColonels.id;
            CreateCardColonelsEquipments(cardColonels);
        }
        else if (data is CardGenerals cardGenerals)
        {
            // mainId = cardGenerals.id;
            CreateCardGeneralsEquipments(cardGenerals);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            // mainId = cardAdmirals.id;
            CreateCardAdmiralsEquipments(cardAdmirals);
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
    public void CreateCardHeroesEquipments(CardHeroes cardHeroes)
    {
        Rank rank = new Rank();
        rank = rank.GetCardHeroesRank(mainType, cardHeroes.id);
        slotObject = Instantiate(AptitudeSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetAptitudeUI(slotObject, mainType, rank.level);
        SetAptitudeMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 500;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                UpLevel(cardHeroes, newRank, mainType);
                Destroy(slotObject);
                CreateCardHeroesEquipments(cardHeroes);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            int level = CalculateMaxMaterialLevel(items.quantity, rank.level);
            int materialQuantity = CalculateMaxMaterialQuantity(items.quantity, rank.level);

            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, level);
                UpLevel(cardHeroes, newRank, mainType);
                Destroy(slotObject);
                CreateCardHeroesEquipments(cardHeroes);
            }
        });
    }
    public void CreateBooksEquipments(Books books)
    {
        Rank rank = new Rank();
        rank = rank.GetBooksRank(mainType, books.id);
        slotObject = Instantiate(AptitudeSlotPrefab, SlotPanel);
        SetAptitudeUI(slotObject, mainType);
    }
    public void CreateCardCaptainsEquipments(CardCaptains cardCaptains)
    {
        Rank rank = new Rank();
        rank = rank.GetCardCaptainsRank(mainType, cardCaptains.id);
        slotObject = Instantiate(AptitudeSlotPrefab, SlotPanel);
        SetAptitudeUI(slotObject, mainType);
    }
    public void CreatePetsEquipments(Pets pets)
    {
        Rank rank = new Rank();
        rank = rank.GetPetsRank(mainType, pets.id);
        slotObject = Instantiate(AptitudeSlotPrefab, SlotPanel);
        SetAptitudeUI(slotObject, mainType);

    }
    public void CreateCardMilitaryEquipments(CardMilitary cardMilitary)
    {
        Rank rank = new Rank();
        rank = rank.GetCardMilitaryRank(mainType, cardMilitary.id);
        slotObject = Instantiate(AptitudeSlotPrefab, SlotPanel);
        SetAptitudeUI(slotObject, mainType);
    }
    public void CreateCardSpellEquipments(CardSpell cardSpell)
    {
        Rank rank = new Rank();
        rank = rank.GetCardMonstersRank(mainType, cardSpell.id);
        slotObject = Instantiate(AptitudeSlotPrefab, SlotPanel);
        SetAptitudeUI(slotObject, mainType);
    }
    public void CreateCardMonstersEquipments(CardMonsters cardMonsters)
    {
        Rank rank = new Rank();
        rank = rank.GetCardMonstersRank(mainType, cardMonsters.id);
        slotObject = Instantiate(AptitudeSlotPrefab, SlotPanel);
        SetAptitudeUI(slotObject, mainType);
    }
    public void CreateCardColonelsEquipments(CardColonels cardColonels)
    {
        Rank rank = new Rank();
        rank = rank.GetCardColonelsRank(mainType, cardColonels.id);
        slotObject = Instantiate(AptitudeSlotPrefab, SlotPanel);
        SetAptitudeUI(slotObject, mainType);
    }
    public void CreateCardGeneralsEquipments(CardGenerals cardGenerals)
    {
        Rank rank = new Rank();
        rank = rank.GetCardGeneralsRank(mainType, cardGenerals.id);
        slotObject = Instantiate(AptitudeSlotPrefab, SlotPanel);
        SetAptitudeUI(slotObject, mainType);
    }
    public void CreateCardAdmiralsEquipments(CardAdmirals cardAdmirals)
    {
        Rank rank = new Rank();
        rank = rank.GetCardAdmiralsRank(mainType, cardAdmirals.id);
        slotObject = Instantiate(AptitudeSlotPrefab, SlotPanel);
        SetAptitudeUI(slotObject, mainType);
    }
    public void InstantiateAptitudeUI(GameObject gameObject, string type)
    {
        int totalSkills = 20;

        for (int i = 1; i <= totalSkills; i++)
        {
            Transform aptitudeSkill = gameObject.transform.Find($"AptitudeSkill{i}");
            if (aptitudeSkill == null) continue;

            RawImage aptitudeImage = aptitudeSkill.Find("AptitudeImage").GetComponent<RawImage>();
            TextMeshProUGUI levelText = aptitudeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

            string imageFile = type.Replace(" ", "_") + "_" + i;
            string imagePath = type + "/" + imageFile;
            Texture texture = Resources.Load<Texture>($"Aptitude/{imagePath}");
            aptitudeImage.texture = texture;

            if (aptitudeImage != null) aptitudeImage.color = Color.black;
            if (levelText != null) levelText.text = "";
        }
    }
    public void SetAptitudeUI(GameObject gameObject, string type, int level = 0)
    {
        int totalSkills = 20;
        int levelsPerSkill = 500;

        for (int i = 1; i <= totalSkills; i++)
        {
            Transform aptitudeSkill = gameObject.transform.Find($"AptitudeSkill{i}");
            if (aptitudeSkill == null) continue;

            RawImage aptitudeImage = aptitudeSkill.Find("AptitudeImage").GetComponent<RawImage>();
            TextMeshProUGUI levelText = aptitudeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

            string imageFile = type.Replace(" ", "_") + "_" + i;
            string imagePath = type + "/" + imageFile;
            Texture texture = Resources.Load<Texture>($"Aptitude/{imagePath}");
            aptitudeImage.texture = texture;

            if (aptitudeImage != null) aptitudeImage.color = Color.black;
            if (levelText != null) levelText.text = "0" + levelsPerSkill;
        }

        int activeSkillsCount = Mathf.Clamp((level / levelsPerSkill) + 1, 1, totalSkills);
        for (int i = 1; i <= activeSkillsCount; i++)
        {
            Transform activeSkill = gameObject.transform.Find($"AptitudeSkill{i}");
            if (activeSkill != null)
            {
                RawImage activeImage = activeSkill.Find("AptitudeImage").GetComponent<RawImage>();
                TextMeshProUGUI activeLevelText = activeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

                if (activeImage != null && level != 0) activeImage.color = Color.white;
                if (activeLevelText != null)
                {
                    activeLevelText.text = $"{level % levelsPerSkill}/{levelsPerSkill}";
                }
            }
        }
    }
    public void SetAptitudeMaterialUI(string type, int level = 0, int userMaterialQuantity = 0)
    {
        int levelsPerSkill = 500;
        int materialQuantity = (level == 0) ? 1 : (level % levelsPerSkill == 0 ? levelsPerSkill : level % levelsPerSkill);
        Transform OneLevelMaterial = currentObject.transform.Find("DictionaryCards/OneLevelMaterial");
        Transform MaxLevelMaterial = currentObject.transform.Find("DictionaryCards/MaxLevelMaterial");
        GameObject oneLevelMaterialObject = Instantiate(ElementDetails2Prefab, OneLevelMaterial);
        GameObject maxLevelMaterialObject = Instantiate(ElementDetails2Prefab, MaxLevelMaterial);

        RawImage oneLevelImage = oneLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture oneLevelTexture = Resources.Load<Texture>($"Item/Material/{type.Replace(" ", "_")}");
        oneLevelImage.texture = oneLevelTexture;

        RectTransform oneLevelRectTransform = oneLevelImage.GetComponent<RectTransform>();
        oneLevelRectTransform.sizeDelta = new Vector2(50, 50);

        TextMeshProUGUI oneLevelQuantity = oneLevelMaterialObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        oneLevelQuantity.text = userMaterialQuantity + "/" + materialQuantity;

        RawImage maxLevelImage = maxLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture maxLevelTexture = Resources.Load<Texture>($"Item/Material/{type.Replace(" ", "_")}");
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
    public Rank EnhanceRank(Rank rank, int level)
    {
        if (rank.level >= 0 && rank.level <= 500)
        {
            rank.health = rank.health + 10000000 * level;
        }
        else if (rank.level > 500 && rank.level <= 1000)
        {
            rank.physical_attack = rank.physical_attack + 1500000 * level;
            rank.physical_defense = rank.physical_defense + 1500000 * level;
        }
        else if (rank.level > 1000 && rank.level <= 1500)
        {
            rank.magical_attack = rank.magical_attack + 1500000 * level;
            rank.magical_defense = rank.magical_defense + 1500000 * level;
        }
        else if (rank.level > 1500 && rank.level <= 2000)
        {
            rank.chemical_attack = rank.chemical_attack + 1500000 * level;
            rank.chemical_defense = rank.chemical_defense + 1500000 * level;
        }
        else if (rank.level > 2000 && rank.level <= 2500)
        {
            rank.atomic_attack = rank.atomic_attack + 1500000 * level;
            rank.atomic_attack = rank.atomic_attack + 1500000 * level;
        }
        else if (rank.level > 2500 && rank.level <= 3000)
        {
            rank.mental_attack = rank.mental_attack + 1500000 * level;
            rank.mental_defense = rank.mental_defense + 1500000 * level;
        }
        else if (rank.level > 3000 && rank.level <= 3500)
        {
            rank.speed = rank.speed + 1500000 * level;
            rank.critical_damage = rank.critical_damage + 2000000 * level;
        }
        else if (rank.level > 3500 && rank.level <= 4000)
        {
            rank.critical_rate = rank.critical_rate + 0.1 * level;
            rank.armor_penetration = rank.armor_penetration + 1500000 * level;
        }
        else if (rank.level > 4000 && rank.level <= 4500)
        {
            rank.avoid = rank.avoid + 0.1 * level;
            rank.absorbs_damage = rank.physical_defense + 2000000 * level;
        }
        else if (rank.level > 4500 && rank.level <= 5000)
        {
            rank.regenerate_vitality = rank.regenerate_vitality + 1500000 * level;
            rank.accuracy = rank.accuracy + 0.1 * level;
        }
        else if (rank.level > 5000 && rank.level <= 5500)
        {
            rank.mana = rank.mana + 1500000 * level;
            rank.percent_all_health = rank.percent_all_health + 5 * level;
        }
        else if (rank.level > 6000 && rank.level <= 6500)
        {
            rank.percent_all_physical_attack = rank.percent_all_physical_attack + 5 * level;
            rank.percent_all_physical_defense = rank.percent_all_physical_defense + 5 * level;
        }
        else if (rank.level > 6500 && rank.level <= 7000)
        {
            rank.percent_all_magical_attack = rank.percent_all_magical_attack + 5 * level;
            rank.percent_all_magical_defense = rank.percent_all_magical_defense + 5 * level;
        }
        else if (rank.level > 7000 && rank.level <= 7500)
        {
            rank.percent_all_chemical_attack = rank.percent_all_chemical_attack + 5 * level;
            rank.percent_all_chemical_defense = rank.percent_all_chemical_defense + 5 * level;
        }
        else if (rank.level > 7500 && rank.level <= 8000)
        {
            rank.percent_all_atomic_attack = rank.percent_all_atomic_attack + 5 * level;
            rank.percent_all_atomic_defense = rank.percent_all_atomic_defense + 5 * level;
        }
        else if (rank.level > 8000 && rank.level <= 8500)
        {
            rank.percent_all_mental_attack = rank.percent_all_mental_attack + 5 * level;
            rank.percent_all_mental_defense = rank.percent_all_mental_defense + 5 * level;
        }
        else if (rank.level > 8500 && rank.level <= 9000)
        {
            rank.physical_attack = rank.physical_attack + 1500000 * level;
            rank.magical_attack = rank.magical_attack + 1500000 * level;
            rank.chemical_attack = rank.chemical_attack + 1500000 * level;
            rank.atomic_attack = rank.atomic_attack + 1500000 * level;
            rank.mental_attack = rank.mental_attack + 1500000 * level;
        }
        else if (rank.level > 9000 && rank.level <= 9500)
        {
            rank.physical_defense = rank.physical_defense + 1500000 * level;
            rank.magical_defense = rank.magical_defense + 1500000 * level;
            rank.chemical_defense = rank.chemical_defense + 1500000 * level;
            rank.atomic_defense = rank.atomic_defense + 1500000 * level;
            rank.mental_defense = rank.mental_defense + 1500000 * level;
        }
        else if (rank.level > 9500 && rank.level <= 10000)
        {
            rank.speed = rank.speed + 1500000 * level;
            rank.critical_damage = rank.critical_damage + 2000000 * level;
            rank.critical_rate = rank.critical_rate + 0.1 * level;
            rank.armor_penetration = rank.armor_penetration + 1500000 * level;
            rank.avoid = rank.avoid + 0.1 * level;
            rank.absorbs_damage = rank.physical_defense + 2000000 * level;
            rank.regenerate_vitality = rank.regenerate_vitality + 1500000 * level;
            rank.accuracy = rank.accuracy + 0.1 * level;
        }
        return rank;
    }
    public void UpLevel(object data, Rank rank, string type)
    {
        if (data is CardHeroes cardHeroes)
        {
            rank.InsertOrUpdateCardHeroesRank(rank, type, cardHeroes.id);
        }
        else if (data is Books books)
        {
            rank.InsertOrUpdateBooksRank(rank, type, books.id);
        }
        else if (data is CardCaptains cardCaptains)
        {
            rank.InsertOrUpdateCardCaptainsRank(rank, type, cardCaptains.id);
        }
        else if (data is Pets pets)
        {
            rank.InsertOrUpdatePetsRank(rank, type, pets.id);
        }
        else if (data is CardMilitary cardMilitary)
        {
            rank.InsertOrUpdateCardMilitaryRank(rank, type, cardMilitary.id);
        }
        else if (data is CardSpell cardSpell)
        {
            rank.InsertOrUpdateCardSpellRank(rank, type, cardSpell.id);
        }
        else if (data is CardMonsters cardMonsters)
        {
            rank.InsertOrUpdateCardMonstersRank(rank, type, cardMonsters.id);
        }
        else if (data is CardColonels cardColonels)
        {
            rank.InsertOrUpdateCardColonelsRank(rank, type, cardColonels.id);
        }
        else if (data is CardGenerals cardGenerals)
        {
            rank.InsertOrUpdateCardGeneralsRank(rank, type, cardGenerals.id);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            rank.InsertOrUpdateCardAdmiralsRank(rank, type, cardAdmirals.id);
        }
    }
}
