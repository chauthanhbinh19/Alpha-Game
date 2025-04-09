using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuOmniscienceManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private GameObject MainMenuOmnisciencePanelPrefab;
    private GameObject OmniscienceSlotPrefab;
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
        MainMenuOmnisciencePanelPrefab = UIManager.Instance.GetGameObject("MainMenuOmnisciencePanelPrefab");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        OmniscienceSlotPrefab = UIManager.Instance.GetGameObject("OmniscienceSlotPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");
    }

    public void CreateMainMenuOmniscienceManager(object data)
    {
        currentObject = Instantiate(MainMenuOmnisciencePanelPrefab, MainPanel);
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        CloseButton.onClick.AddListener(() => Destroy(currentObject));

        LevelCondition = currentObject.transform.Find("DictionaryCards/LevelCondition");
        // List<string> uniqueTypes = new List<string>
        // {
        //     "Omniscience I", "Omniscience II", "Omniscience III", "Omniscience IV", "Omniscience V",
        //     "Omniscience VI", "Omniscience VII", "Omniscience VIII", "Omniscience IX", "Omniscience X"
        // };
        Dictionary<string, int> uniqueTypes = new Dictionary<string, int>();
        Features features = new Features();
        uniqueTypes = features.GetFeaturesByType("Omniscience");
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
                    if (data is CardHeroes cardHeroes)
                    {
                        // mainId = cardHeroes.id;
                        CreateCardHeroesEquipments(cardHeroes);
                        if (cardHeroes.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            LevelCondition.gameObject.SetActive(true);
                        }
                    }
                    else if (data is Books books)
                    {
                        // mainId = books.id;
                        CreateBooksEquipments(books);
                        if (books.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            LevelCondition.gameObject.SetActive(true);
                        }
                    }
                    else if (data is CardCaptains cardCaptains)
                    {
                        // mainId = cardCaptains.id;
                        CreateCardCaptainsEquipments(cardCaptains);
                        if (cardCaptains.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            LevelCondition.gameObject.SetActive(true);
                        }
                    }
                    else if (data is Pets pets)
                    {
                        // mainId = pets.id;
                        CreatePetsEquipments(pets);
                        if (pets.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            LevelCondition.gameObject.SetActive(true);
                        }
                    }
                    else if (data is CardMilitary cardMilitary)
                    {
                        // mainId = cardMilitary.id;
                        CreateCardMilitaryEquipments(cardMilitary);
                        if (cardMilitary.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            LevelCondition.gameObject.SetActive(true);
                        }
                    }
                    else if (data is CardSpell cardSpell)
                    {
                        // mainId = cardSpell.id;
                        CreateCardSpellEquipments(cardSpell);
                        if (cardSpell.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            LevelCondition.gameObject.SetActive(true);
                        }
                    }
                    else if (data is CardMonsters cardMonsters)
                    {
                        // mainId = cardMonsters.id;
                        CreateCardMonstersEquipments(cardMonsters);
                        if (cardMonsters.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            LevelCondition.gameObject.SetActive(true);
                        }
                    }
                    else if (data is CardColonels cardColonels)
                    {
                        // mainId = cardColonels.id;
                        CreateCardColonelsEquipments(cardColonels);
                        if (cardColonels.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            LevelCondition.gameObject.SetActive(true);
                        }
                    }
                    else if (data is CardGenerals cardGenerals)
                    {
                        // mainId = cardGenerals.id;
                        CreateCardGeneralsEquipments(cardGenerals);
                        if (cardGenerals.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            LevelCondition.gameObject.SetActive(true);
                        }
                    }
                    else if (data is CardAdmirals cardAdmirals)
                    {
                        // mainId = cardAdmirals.id;
                        CreateCardAdmiralsEquipments(cardAdmirals);
                        if (cardAdmirals.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            LevelCondition.gameObject.SetActive(true);
                        }
                    }
                    else if (data is Equipments equipments)
                    {
                        // mainId = cardAdmirals.id;
                        CreateEquipmentsEquipments(equipments);
                        if (equipments.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            LevelCondition.gameObject.SetActive(true);
                        }
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

        if (data is CardHeroes cardHeroes)
        {
            // mainId = cardHeroes.id;
            CreateCardHeroesEquipments(cardHeroes);
            if (cardHeroes.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                LevelCondition.gameObject.SetActive(true);
            }
        }
        else if (data is Books books)
        {
            // mainId = books.id;
            CreateBooksEquipments(books);
            if (books.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                LevelCondition.gameObject.SetActive(true);
            }
        }
        else if (data is CardCaptains cardCaptains)
        {
            // mainId = cardCaptains.id;
            CreateCardCaptainsEquipments(cardCaptains);
            if (cardCaptains.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                LevelCondition.gameObject.SetActive(true);
            }
        }
        else if (data is Pets pets)
        {
            // mainId = pets.id;
            CreatePetsEquipments(pets);
            if (pets.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                LevelCondition.gameObject.SetActive(true);
            }
        }
        else if (data is CardMilitary cardMilitary)
        {
            // mainId = cardMilitary.id;
            CreateCardMilitaryEquipments(cardMilitary);
            if (cardMilitary.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                LevelCondition.gameObject.SetActive(true);
            }
        }
        else if (data is CardSpell cardSpell)
        {
            // mainId = cardSpell.id;
            CreateCardSpellEquipments(cardSpell);
            if (cardSpell.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                LevelCondition.gameObject.SetActive(true);
            }
        }
        else if (data is CardMonsters cardMonsters)
        {
            // mainId = cardMonsters.id;
            CreateCardMonstersEquipments(cardMonsters);
            if (cardMonsters.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                LevelCondition.gameObject.SetActive(true);
            }
        }
        else if (data is CardColonels cardColonels)
        {
            // mainId = cardColonels.id;
            CreateCardColonelsEquipments(cardColonels);
            if (cardColonels.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                LevelCondition.gameObject.SetActive(true);
            }
        }
        else if (data is CardGenerals cardGenerals)
        {
            // mainId = cardGenerals.id;
            CreateCardGeneralsEquipments(cardGenerals);
            if (cardGenerals.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                LevelCondition.gameObject.SetActive(true);
            }
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            // mainId = cardAdmirals.id;
            CreateCardAdmiralsEquipments(cardAdmirals);
            if (cardAdmirals.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                LevelCondition.gameObject.SetActive(true);
            }
        }
        else if (data is Equipments equipments)
        {
            // mainId = cardAdmirals.id;
            CreateEquipmentsEquipments(equipments);
            if (equipments.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                LevelCondition.gameObject.SetActive(true);
            }
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
        slotObject = Instantiate(OmniscienceSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType, rank.level);
        SetMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardHeroes, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
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
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardHeroes, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardHeroesEquipments(cardHeroes);
            }
        });
    }
    public void CreateBooksEquipments(Books books)
    {
        Rank rank = new Rank();
        rank = rank.GetBooksRank(mainType, books.id);
        slotObject = Instantiate(OmniscienceSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType);
        SetMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(books, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateBooksEquipments(books);
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
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(books, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateBooksEquipments(books);
            }
        });
    }
    public void CreateCardCaptainsEquipments(CardCaptains cardCaptains)
    {
        Rank rank = new Rank();
        rank = rank.GetCardCaptainsRank(mainType, cardCaptains.id);
        slotObject = Instantiate(OmniscienceSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType);
        SetMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardCaptains, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardCaptainsEquipments(cardCaptains);
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
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardCaptains, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardCaptainsEquipments(cardCaptains);
            }
        });
    }
    public void CreatePetsEquipments(Pets pets)
    {
        Rank rank = new Rank();
        rank = rank.GetPetsRank(mainType, pets.id);
        slotObject = Instantiate(OmniscienceSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType);
        SetMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(pets, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreatePetsEquipments(pets);
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
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(pets, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreatePetsEquipments(pets);
            }
        });
    }
    public void CreateCardMilitaryEquipments(CardMilitary cardMilitary)
    {
        Rank rank = new Rank();
        rank = rank.GetCardMilitaryRank(mainType, cardMilitary.id);
        slotObject = Instantiate(OmniscienceSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType);
        SetMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardMilitary, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardMilitaryEquipments(cardMilitary);
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
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardMilitary, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardMilitaryEquipments(cardMilitary);
            }
        });
    }
    public void CreateCardSpellEquipments(CardSpell cardSpell)
    {
        Rank rank = new Rank();
        rank = rank.GetCardMonstersRank(mainType, cardSpell.id);
        slotObject = Instantiate(OmniscienceSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType);
        SetMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardSpell, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardSpellEquipments(cardSpell);
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
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardSpell, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardSpellEquipments(cardSpell);
            }
        });
    }
    public void CreateCardMonstersEquipments(CardMonsters cardMonsters)
    {
        Rank rank = new Rank();
        rank = rank.GetCardMonstersRank(mainType, cardMonsters.id);
        slotObject = Instantiate(OmniscienceSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType);
        SetMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardMonsters, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardMonstersEquipments(cardMonsters);
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
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardMonsters, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardMonstersEquipments(cardMonsters);
            }
        });
    }
    public void CreateCardColonelsEquipments(CardColonels cardColonels)
    {
        Rank rank = new Rank();
        rank = rank.GetCardColonelsRank(mainType, cardColonels.id);
        slotObject = Instantiate(OmniscienceSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType);
        SetMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardColonels, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardColonelsEquipments(cardColonels);
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
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardColonels, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardColonelsEquipments(cardColonels);
            }
        });
    }
    public void CreateCardGeneralsEquipments(CardGenerals cardGenerals)
    {
        Rank rank = new Rank();
        rank = rank.GetCardGeneralsRank(mainType, cardGenerals.id);
        slotObject = Instantiate(OmniscienceSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType);
        SetMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardGenerals, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardGeneralsEquipments(cardGenerals);
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
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardGenerals, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardGeneralsEquipments(cardGenerals);
            }
        });
    }
    public void CreateCardAdmiralsEquipments(CardAdmirals cardAdmirals)
    {
        Rank rank = new Rank();
        rank = rank.GetCardAdmiralsRank(mainType, cardAdmirals.id);
        slotObject = Instantiate(OmniscienceSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType);
        SetMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardAdmirals, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardAdmiralsEquipments(cardAdmirals);
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
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(cardAdmirals, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardAdmiralsEquipments(cardAdmirals);
            }
        });
    }
    public void CreateEquipmentsEquipments(Equipments equipments)
    {
        Rank rank = new Rank();
        rank = rank.GetCardAdmiralsRank(mainType, equipments.id);
        slotObject = Instantiate(OmniscienceSlotPrefab, SlotPanel);
        Items items = new Items();
        items = items.GetUserItemByName(mainType);
        SetUI(slotObject, mainType);
        SetMaterialUI(mainType, rank.level, items.quantity);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (rank.level == 0) ? 1 : (rank.level % levelsPerSkill == 0 ? levelsPerSkill : rank.level % levelsPerSkill);
            if (items.quantity >= materialQuantity)
            {
                items.quantity = items.quantity - materialQuantity;
                items.UpdateUserItemsQuantity(items);
                Rank newRank = new Rank();
                newRank = EnhanceRank(rank, 1);
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(equipments, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateEquipmentsEquipments(equipments);
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
                Teams teams = new Teams();
                double currentPower = teams.GetTeamsPower();
                UpLevel(equipments, newRank, mainType);
                double newPower = teams.GetTeamsPower();
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateEquipmentsEquipments(equipments);
            }
        });
    }
    public void SetUI(GameObject gameObject, string type, int level = 0)
    {
        RawImage BackgroundImage = gameObject.transform.Find("Background").GetComponent<RawImage>();
        Texture backgroundTexture = Resources.Load<Texture>("UI/Background3/Tree_7");
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

            Texture texture = Resources.Load<Texture>($"UI/Rank/Omniscience");
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
    public Rank EnhanceRank(Rank rank, int level)
    {
        int startLevel = rank.level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = 10;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                rank.health += 10000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                rank.physical_attack += 1500000 * statMultiplier;
                rank.physical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                rank.magical_attack += 1500000 * statMultiplier;
                rank.magical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                rank.chemical_attack += 1500000 * statMultiplier;
                rank.chemical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                rank.atomic_attack += 1500000 * statMultiplier;
                rank.atomic_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                rank.mental_attack += 1500000 * statMultiplier;
                rank.mental_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                rank.speed += 1500000 * statMultiplier;
                rank.critical_damage_rate += 0.1 * statMultiplier;
                rank.critical_rate += 0.1 * statMultiplier;
                rank.penetration_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                rank.evasion_rate += 0.1 * statMultiplier;
                rank.damage_absorption_rate += 0.1 * statMultiplier;
                rank.vitality_regeneration_rate += 0.1 * statMultiplier;
                rank.accuracy_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                rank.lifesteal_rate += 0.1 * statMultiplier;
                rank.mana += 1500000 * statMultiplier;
                rank.mana_regeneration_rate += 0.1 * statMultiplier;
                rank.shield_strength += 1500000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                rank.tenacity += 0.5 * statMultiplier;
                rank.resistance_rate += 0.1 * statMultiplier;
                rank.combo_rate += 0.1 * statMultiplier;
                rank.reflection_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                rank.damage_to_different_faction_rate += 0.1 * statMultiplier;
                rank.resistance_to_different_faction_rate += 0.1 * statMultiplier;
                rank.damage_to_same_faction_rate += 0.1 * statMultiplier;
                rank.resistance_to_same_faction_rate += 0.1 * statMultiplier;
                rank.percent_all_health += 5 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                rank.percent_all_physical_attack += 5 * statMultiplier;
                rank.percent_all_physical_defense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                rank.percent_all_magical_attack += 5 * statMultiplier;
                rank.percent_all_magical_defense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                rank.percent_all_chemical_attack += 5 * statMultiplier;
                rank.percent_all_chemical_defense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                rank.percent_all_atomic_attack += 5 * statMultiplier;
                rank.percent_all_atomic_defense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                rank.percent_all_mental_attack += 5 * statMultiplier;
                rank.percent_all_mental_defense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                rank.physical_attack += 1500000 * statMultiplier;
                rank.magical_attack += 1500000 * statMultiplier;
                rank.chemical_attack += 1500000 * statMultiplier;
                rank.atomic_attack += 1500000 * statMultiplier;
                rank.mental_attack += 1500000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                rank.physical_defense += 1500000 * statMultiplier;
                rank.magical_defense += 1500000 * statMultiplier;
                rank.chemical_defense += 1500000 * statMultiplier;
                rank.atomic_defense += 1500000 * statMultiplier;
                rank.mental_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                rank.speed += 1500000 * statMultiplier;
                rank.critical_damage_rate += 0.1 * statMultiplier;
                rank.critical_rate += 0.1 * statMultiplier;
                rank.penetration_rate += 0.1 * statMultiplier;
                rank.evasion_rate += 0.1 * statMultiplier;
                rank.damage_absorption_rate += 0.1 * statMultiplier;
                rank.vitality_regeneration_rate += 0.1 * statMultiplier;
                rank.accuracy_rate += 0.1 * statMultiplier;
                rank.lifesteal_rate += 0.1 * statMultiplier;
                rank.mana += 1500000 * statMultiplier;
                rank.mana_regeneration_rate += 0.1 * statMultiplier;
                rank.shield_strength += 1500000 * statMultiplier;
                rank.tenacity += 0.5 * statMultiplier;
                rank.resistance_rate += 0.1 * statMultiplier;
                rank.combo_rate += 0.1 * statMultiplier;
                rank.reflection_rate += 0.1 * statMultiplier;
                rank.damage_to_different_faction_rate += 0.1 * statMultiplier;
                rank.resistance_to_different_faction_rate += 0.1 * statMultiplier;
                rank.damage_to_same_faction_rate += 0.1 * statMultiplier;
                rank.resistance_to_same_faction_rate += 0.1 * statMultiplier;
            }
        }

        rank.level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
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
