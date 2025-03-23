using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainMenuAffinityManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform MateriralPanel;
    private GameObject MainMenuAffinityPanelPrefab;
    private GameObject currentObject;
    private GameObject ItemThird;
    private RawImage mainImage;
    private TextMeshProUGUI mainLevelText;
    private Button UpLevelButton;
    private Button UpMaxLevelButton;
    private string mainType = "Affinity";
    private List<Items> itemsList;
    // Start is called before the first frame update
    void Start()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainMenuAffinityPanelPrefab = UIManager.Instance.GetGameObject("MainMenuAffinityPanelPrefab");
        ItemThird = UIManager.Instance.GetGameObject("ItemThird");
        // List<Items> itemsList = new List<Items>();
    }
    public void CreateMainMenuAffinityManager(object data)
    {
        currentObject = Instantiate(MainMenuAffinityPanelPrefab, MainPanel);
        MateriralPanel = currentObject.transform.Find("DictionaryCards/SetGroup/Viewport/Content");
        mainImage = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        mainLevelText = currentObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        CloseButton.onClick.AddListener(() => Destroy(currentObject));

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
        string fileNameWithoutExtension = cardHeroes.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        mainLevelText.text = rank.level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                // Nếu exp của vật phẩm quá lớn, chỉ lấy số lượng cần thiết
                while (availableQuantity > 0 && totalExp < expNeeded)
                {
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Nếu đã đủ exp để lên cấp, dừng lại
                    if (totalExp >= expNeeded)
                        break;
                }

                // Nếu có vật phẩm được dùng, thêm vào danh sách
                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đủ exp để nâng cấp, dừng lại
                if (totalExp >= expNeeded)
                    break;
            }

            // Kiểm tra nếu không đủ exp thì không làm gì
            if (totalExp < expNeeded)
                return;

            // Tính số cấp có thể nâng, nhưng không tiêu thụ toàn bộ exp ngay lập tức
            int levelsUp = 0;
            while (totalExp >= expNeeded && tempLevel < 100000)
            {
                expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Tính lại exp cần cho cấp hiện tại
                if (totalExp < expNeeded)
                    break;

                totalExp -= expNeeded;
                tempLevel++; // Chỉ tăng cấp trên biến tạm
                levelsUp++;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardHeroes, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardHeroesEquipments(cardHeroes);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm để giữ cấp hiện tại
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                while (availableQuantity > 0)
                {
                    int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên cấp hiện tại

                    // Nếu đã đạt level 100000 thì dừng
                    if (tempLevel >= 100000)
                        break;

                    // Nếu vật phẩm này có thể giúp lên cấp tiếp theo, thì sử dụng
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Khi đủ exp để lên cấp, tăng cấp ngay
                    while (totalExp >= expNeeded && tempLevel < 100000)
                    {
                        totalExp -= expNeeded;
                        tempLevel++;
                        expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Cập nhật exp cần thiết cho cấp tiếp theo
                    }
                }

                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đạt level 100000 thì dừng hoàn toàn
                if (tempLevel >= 100000)
                    break;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.level);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardHeroes, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardHeroesEquipments(cardHeroes);
        });
    }
    public void CreateBooksEquipments(Books books)
    {
        string fileNameWithoutExtension = books.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        Rank rank = new Rank();
        rank = rank.GetBooksRank(mainType, books.id);
        mainLevelText.text = rank.level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                // Nếu exp của vật phẩm quá lớn, chỉ lấy số lượng cần thiết
                while (availableQuantity > 0 && totalExp < expNeeded)
                {
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Nếu đã đủ exp để lên cấp, dừng lại
                    if (totalExp >= expNeeded)
                        break;
                }

                // Nếu có vật phẩm được dùng, thêm vào danh sách
                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đủ exp để nâng cấp, dừng lại
                if (totalExp >= expNeeded)
                    break;
            }

            // Kiểm tra nếu không đủ exp thì không làm gì
            if (totalExp < expNeeded)
                return;

            // Tính số cấp có thể nâng, nhưng không tiêu thụ toàn bộ exp ngay lập tức
            int levelsUp = 0;
            while (totalExp >= expNeeded && tempLevel < 100000)
            {
                expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Tính lại exp cần cho cấp hiện tại
                if (totalExp < expNeeded)
                    break;

                totalExp -= expNeeded;
                tempLevel++; // Chỉ tăng cấp trên biến tạm
                levelsUp++;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(books, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateBooksEquipments(books);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm để giữ cấp hiện tại
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                while (availableQuantity > 0)
                {
                    int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên cấp hiện tại

                    // Nếu đã đạt level 100000 thì dừng
                    if (tempLevel >= 100000)
                        break;

                    // Nếu vật phẩm này có thể giúp lên cấp tiếp theo, thì sử dụng
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Khi đủ exp để lên cấp, tăng cấp ngay
                    while (totalExp >= expNeeded && tempLevel < 100000)
                    {
                        totalExp -= expNeeded;
                        tempLevel++;
                        expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Cập nhật exp cần thiết cho cấp tiếp theo
                    }
                }

                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đạt level 100000 thì dừng hoàn toàn
                if (tempLevel >= 100000)
                    break;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.level);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(books, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateBooksEquipments(books);
        });
    }
    public void CreateCardCaptainsEquipments(CardCaptains cardCaptains)
    {
        string fileNameWithoutExtension = cardCaptains.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        Rank rank = new Rank();
        rank = rank.GetCardCaptainsRank(mainType, cardCaptains.id);
        mainLevelText.text = rank.level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                // Nếu exp của vật phẩm quá lớn, chỉ lấy số lượng cần thiết
                while (availableQuantity > 0 && totalExp < expNeeded)
                {
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Nếu đã đủ exp để lên cấp, dừng lại
                    if (totalExp >= expNeeded)
                        break;
                }

                // Nếu có vật phẩm được dùng, thêm vào danh sách
                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đủ exp để nâng cấp, dừng lại
                if (totalExp >= expNeeded)
                    break;
            }

            // Kiểm tra nếu không đủ exp thì không làm gì
            if (totalExp < expNeeded)
                return;

            // Tính số cấp có thể nâng, nhưng không tiêu thụ toàn bộ exp ngay lập tức
            int levelsUp = 0;
            while (totalExp >= expNeeded && tempLevel < 100000)
            {
                expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Tính lại exp cần cho cấp hiện tại
                if (totalExp < expNeeded)
                    break;

                totalExp -= expNeeded;
                tempLevel++; // Chỉ tăng cấp trên biến tạm
                levelsUp++;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardCaptains, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardCaptainsEquipments(cardCaptains);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm để giữ cấp hiện tại
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                while (availableQuantity > 0)
                {
                    int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên cấp hiện tại

                    // Nếu đã đạt level 100000 thì dừng
                    if (tempLevel >= 100000)
                        break;

                    // Nếu vật phẩm này có thể giúp lên cấp tiếp theo, thì sử dụng
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Khi đủ exp để lên cấp, tăng cấp ngay
                    while (totalExp >= expNeeded && tempLevel < 100000)
                    {
                        totalExp -= expNeeded;
                        tempLevel++;
                        expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Cập nhật exp cần thiết cho cấp tiếp theo
                    }
                }

                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đạt level 100000 thì dừng hoàn toàn
                if (tempLevel >= 100000)
                    break;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.level);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardCaptains, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardCaptainsEquipments(cardCaptains);
        });
    }
    public void CreatePetsEquipments(Pets pets)
    {
        string fileNameWithoutExtension = pets.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        Rank rank = new Rank();
        rank = rank.GetPetsRank(mainType, pets.id);
        mainLevelText.text = rank.level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                // Nếu exp của vật phẩm quá lớn, chỉ lấy số lượng cần thiết
                while (availableQuantity > 0 && totalExp < expNeeded)
                {
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Nếu đã đủ exp để lên cấp, dừng lại
                    if (totalExp >= expNeeded)
                        break;
                }

                // Nếu có vật phẩm được dùng, thêm vào danh sách
                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đủ exp để nâng cấp, dừng lại
                if (totalExp >= expNeeded)
                    break;
            }

            // Kiểm tra nếu không đủ exp thì không làm gì
            if (totalExp < expNeeded)
                return;

            // Tính số cấp có thể nâng, nhưng không tiêu thụ toàn bộ exp ngay lập tức
            int levelsUp = 0;
            while (totalExp >= expNeeded && tempLevel < 100000)
            {
                expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Tính lại exp cần cho cấp hiện tại
                if (totalExp < expNeeded)
                    break;

                totalExp -= expNeeded;
                tempLevel++; // Chỉ tăng cấp trên biến tạm
                levelsUp++;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(pets, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreatePetsEquipments(pets);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm để giữ cấp hiện tại
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                while (availableQuantity > 0)
                {
                    int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên cấp hiện tại

                    // Nếu đã đạt level 100000 thì dừng
                    if (tempLevel >= 100000)
                        break;

                    // Nếu vật phẩm này có thể giúp lên cấp tiếp theo, thì sử dụng
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Khi đủ exp để lên cấp, tăng cấp ngay
                    while (totalExp >= expNeeded && tempLevel < 100000)
                    {
                        totalExp -= expNeeded;
                        tempLevel++;
                        expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Cập nhật exp cần thiết cho cấp tiếp theo
                    }
                }

                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đạt level 100000 thì dừng hoàn toàn
                if (tempLevel >= 100000)
                    break;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.level);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(pets, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreatePetsEquipments(pets);
        });
    }
    public void CreateCardMilitaryEquipments(CardMilitary cardMilitary)
    {
        string fileNameWithoutExtension = cardMilitary.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        Rank rank = new Rank();
        rank = rank.GetCardMilitaryRank(mainType, cardMilitary.id);
        mainLevelText.text = rank.level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                // Nếu exp của vật phẩm quá lớn, chỉ lấy số lượng cần thiết
                while (availableQuantity > 0 && totalExp < expNeeded)
                {
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Nếu đã đủ exp để lên cấp, dừng lại
                    if (totalExp >= expNeeded)
                        break;
                }

                // Nếu có vật phẩm được dùng, thêm vào danh sách
                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đủ exp để nâng cấp, dừng lại
                if (totalExp >= expNeeded)
                    break;
            }

            // Kiểm tra nếu không đủ exp thì không làm gì
            if (totalExp < expNeeded)
                return;

            // Tính số cấp có thể nâng, nhưng không tiêu thụ toàn bộ exp ngay lập tức
            int levelsUp = 0;
            while (totalExp >= expNeeded && tempLevel < 100000)
            {
                expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Tính lại exp cần cho cấp hiện tại
                if (totalExp < expNeeded)
                    break;

                totalExp -= expNeeded;
                tempLevel++; // Chỉ tăng cấp trên biến tạm
                levelsUp++;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardMilitary, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardMilitaryEquipments(cardMilitary);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm để giữ cấp hiện tại
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                while (availableQuantity > 0)
                {
                    int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên cấp hiện tại

                    // Nếu đã đạt level 100000 thì dừng
                    if (tempLevel >= 100000)
                        break;

                    // Nếu vật phẩm này có thể giúp lên cấp tiếp theo, thì sử dụng
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Khi đủ exp để lên cấp, tăng cấp ngay
                    while (totalExp >= expNeeded && tempLevel < 100000)
                    {
                        totalExp -= expNeeded;
                        tempLevel++;
                        expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Cập nhật exp cần thiết cho cấp tiếp theo
                    }
                }

                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đạt level 100000 thì dừng hoàn toàn
                if (tempLevel >= 100000)
                    break;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.level);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardMilitary, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardMilitaryEquipments(cardMilitary);
        });
    }
    public void CreateCardSpellEquipments(CardSpell cardSpell)
    {
        string fileNameWithoutExtension = cardSpell.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        Rank rank = new Rank();
        rank = rank.GetCardSpellRank(mainType, cardSpell.id);
        mainLevelText.text = rank.level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                // Nếu exp của vật phẩm quá lớn, chỉ lấy số lượng cần thiết
                while (availableQuantity > 0 && totalExp < expNeeded)
                {
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Nếu đã đủ exp để lên cấp, dừng lại
                    if (totalExp >= expNeeded)
                        break;
                }

                // Nếu có vật phẩm được dùng, thêm vào danh sách
                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đủ exp để nâng cấp, dừng lại
                if (totalExp >= expNeeded)
                    break;
            }

            // Kiểm tra nếu không đủ exp thì không làm gì
            if (totalExp < expNeeded)
                return;

            // Tính số cấp có thể nâng, nhưng không tiêu thụ toàn bộ exp ngay lập tức
            int levelsUp = 0;
            while (totalExp >= expNeeded && tempLevel < 100000)
            {
                expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Tính lại exp cần cho cấp hiện tại
                if (totalExp < expNeeded)
                    break;

                totalExp -= expNeeded;
                tempLevel++; // Chỉ tăng cấp trên biến tạm
                levelsUp++;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardSpell, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardSpellEquipments(cardSpell);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm để giữ cấp hiện tại
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                while (availableQuantity > 0)
                {
                    int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên cấp hiện tại

                    // Nếu đã đạt level 100000 thì dừng
                    if (tempLevel >= 100000)
                        break;

                    // Nếu vật phẩm này có thể giúp lên cấp tiếp theo, thì sử dụng
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Khi đủ exp để lên cấp, tăng cấp ngay
                    while (totalExp >= expNeeded && tempLevel < 100000)
                    {
                        totalExp -= expNeeded;
                        tempLevel++;
                        expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Cập nhật exp cần thiết cho cấp tiếp theo
                    }
                }

                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đạt level 100000 thì dừng hoàn toàn
                if (tempLevel >= 100000)
                    break;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.level);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardSpell, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardSpellEquipments(cardSpell);
        });
    }
    public void CreateCardMonstersEquipments(CardMonsters cardMonsters)
    {
        string fileNameWithoutExtension = cardMonsters.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        Rank rank = new Rank();
        rank = rank.GetCardMonstersRank(mainType, cardMonsters.id);
        mainLevelText.text = rank.level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                // Nếu exp của vật phẩm quá lớn, chỉ lấy số lượng cần thiết
                while (availableQuantity > 0 && totalExp < expNeeded)
                {
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Nếu đã đủ exp để lên cấp, dừng lại
                    if (totalExp >= expNeeded)
                        break;
                }

                // Nếu có vật phẩm được dùng, thêm vào danh sách
                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đủ exp để nâng cấp, dừng lại
                if (totalExp >= expNeeded)
                    break;
            }

            // Kiểm tra nếu không đủ exp thì không làm gì
            if (totalExp < expNeeded)
                return;

            // Tính số cấp có thể nâng, nhưng không tiêu thụ toàn bộ exp ngay lập tức
            int levelsUp = 0;
            while (totalExp >= expNeeded && tempLevel < 100000)
            {
                expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Tính lại exp cần cho cấp hiện tại
                if (totalExp < expNeeded)
                    break;

                totalExp -= expNeeded;
                tempLevel++; // Chỉ tăng cấp trên biến tạm
                levelsUp++;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardMonsters, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardMonstersEquipments(cardMonsters);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm để giữ cấp hiện tại
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                while (availableQuantity > 0)
                {
                    int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên cấp hiện tại

                    // Nếu đã đạt level 100000 thì dừng
                    if (tempLevel >= 100000)
                        break;

                    // Nếu vật phẩm này có thể giúp lên cấp tiếp theo, thì sử dụng
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Khi đủ exp để lên cấp, tăng cấp ngay
                    while (totalExp >= expNeeded && tempLevel < 100000)
                    {
                        totalExp -= expNeeded;
                        tempLevel++;
                        expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Cập nhật exp cần thiết cho cấp tiếp theo
                    }
                }

                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đạt level 100000 thì dừng hoàn toàn
                if (tempLevel >= 100000)
                    break;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.level);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardMonsters, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardMonstersEquipments(cardMonsters);
        });
    }
    public void CreateCardColonelsEquipments(CardColonels cardColonels)
    {
        string fileNameWithoutExtension = cardColonels.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        Rank rank = new Rank();
        rank = rank.GetCardColonelsRank(mainType, cardColonels.id);
        mainLevelText.text = rank.level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                // Nếu exp của vật phẩm quá lớn, chỉ lấy số lượng cần thiết
                while (availableQuantity > 0 && totalExp < expNeeded)
                {
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Nếu đã đủ exp để lên cấp, dừng lại
                    if (totalExp >= expNeeded)
                        break;
                }

                // Nếu có vật phẩm được dùng, thêm vào danh sách
                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đủ exp để nâng cấp, dừng lại
                if (totalExp >= expNeeded)
                    break;
            }

            // Kiểm tra nếu không đủ exp thì không làm gì
            if (totalExp < expNeeded)
                return;

            // Tính số cấp có thể nâng, nhưng không tiêu thụ toàn bộ exp ngay lập tức
            int levelsUp = 0;
            while (totalExp >= expNeeded && tempLevel < 100000)
            {
                expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Tính lại exp cần cho cấp hiện tại
                if (totalExp < expNeeded)
                    break;

                totalExp -= expNeeded;
                tempLevel++; // Chỉ tăng cấp trên biến tạm
                levelsUp++;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardColonels, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardColonelsEquipments(cardColonels);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm để giữ cấp hiện tại
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                while (availableQuantity > 0)
                {
                    int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên cấp hiện tại

                    // Nếu đã đạt level 100000 thì dừng
                    if (tempLevel >= 100000)
                        break;

                    // Nếu vật phẩm này có thể giúp lên cấp tiếp theo, thì sử dụng
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Khi đủ exp để lên cấp, tăng cấp ngay
                    while (totalExp >= expNeeded && tempLevel < 100000)
                    {
                        totalExp -= expNeeded;
                        tempLevel++;
                        expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Cập nhật exp cần thiết cho cấp tiếp theo
                    }
                }

                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đạt level 100000 thì dừng hoàn toàn
                if (tempLevel >= 100000)
                    break;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.level);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardColonels, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardColonelsEquipments(cardColonels);
        });
    }
    public void CreateCardGeneralsEquipments(CardGenerals cardGenerals)
    {
        string fileNameWithoutExtension = cardGenerals.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        Rank rank = new Rank();
        rank = rank.GetCardGeneralsRank(mainType, cardGenerals.id);
        mainLevelText.text = rank.level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                // Nếu exp của vật phẩm quá lớn, chỉ lấy số lượng cần thiết
                while (availableQuantity > 0 && totalExp < expNeeded)
                {
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Nếu đã đủ exp để lên cấp, dừng lại
                    if (totalExp >= expNeeded)
                        break;
                }

                // Nếu có vật phẩm được dùng, thêm vào danh sách
                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đủ exp để nâng cấp, dừng lại
                if (totalExp >= expNeeded)
                    break;
            }

            // Kiểm tra nếu không đủ exp thì không làm gì
            if (totalExp < expNeeded)
                return;

            // Tính số cấp có thể nâng, nhưng không tiêu thụ toàn bộ exp ngay lập tức
            int levelsUp = 0;
            while (totalExp >= expNeeded && tempLevel < 100000)
            {
                expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Tính lại exp cần cho cấp hiện tại
                if (totalExp < expNeeded)
                    break;

                totalExp -= expNeeded;
                tempLevel++; // Chỉ tăng cấp trên biến tạm
                levelsUp++;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardGenerals, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardGeneralsEquipments(cardGenerals);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm để giữ cấp hiện tại
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                while (availableQuantity > 0)
                {
                    int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên cấp hiện tại

                    // Nếu đã đạt level 100000 thì dừng
                    if (tempLevel >= 100000)
                        break;

                    // Nếu vật phẩm này có thể giúp lên cấp tiếp theo, thì sử dụng
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Khi đủ exp để lên cấp, tăng cấp ngay
                    while (totalExp >= expNeeded && tempLevel < 100000)
                    {
                        totalExp -= expNeeded;
                        tempLevel++;
                        expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Cập nhật exp cần thiết cho cấp tiếp theo
                    }
                }

                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đạt level 100000 thì dừng hoàn toàn
                if (tempLevel >= 100000)
                    break;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.level);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardGenerals, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardGeneralsEquipments(cardGenerals);
        });
    }
    public void CreateCardAdmiralsEquipments(CardAdmirals cardAdmirals)
    {
        string fileNameWithoutExtension = cardAdmirals.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        Rank rank = new Rank();
        rank = rank.GetCardAdmiralsRank(mainType, cardAdmirals.id);
        mainLevelText.text = rank.level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                // Nếu exp của vật phẩm quá lớn, chỉ lấy số lượng cần thiết
                while (availableQuantity > 0 && totalExp < expNeeded)
                {
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Nếu đã đủ exp để lên cấp, dừng lại
                    if (totalExp >= expNeeded)
                        break;
                }

                // Nếu có vật phẩm được dùng, thêm vào danh sách
                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đủ exp để nâng cấp, dừng lại
                if (totalExp >= expNeeded)
                    break;
            }

            // Kiểm tra nếu không đủ exp thì không làm gì
            if (totalExp < expNeeded)
                return;

            // Tính số cấp có thể nâng, nhưng không tiêu thụ toàn bộ exp ngay lập tức
            int levelsUp = 0;
            while (totalExp >= expNeeded && tempLevel < 100000)
            {
                expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Tính lại exp cần cho cấp hiện tại
                if (totalExp < expNeeded)
                    break;

                totalExp -= expNeeded;
                tempLevel++; // Chỉ tăng cấp trên biến tạm
                levelsUp++;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardAdmirals, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardAdmiralsEquipments(cardAdmirals);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            if (rank.level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.level; // Biến tạm để giữ cấp hiện tại
            int totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                int itemExp = item.GetItemExp(item.name);
                int availableQuantity = item.quantity;
                int usedQuantity = 0;

                while (availableQuantity > 0)
                {
                    int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên cấp hiện tại

                    // Nếu đã đạt level 100000 thì dừng
                    if (tempLevel >= 100000)
                        break;

                    // Nếu vật phẩm này có thể giúp lên cấp tiếp theo, thì sử dụng
                    totalExp += itemExp;
                    availableQuantity--;
                    usedQuantity++;

                    // Khi đủ exp để lên cấp, tăng cấp ngay
                    while (totalExp >= expNeeded && tempLevel < 100000)
                    {
                        totalExp -= expNeeded;
                        tempLevel++;
                        expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Cập nhật exp cần thiết cho cấp tiếp theo
                    }
                }

                if (usedQuantity > 0)
                {
                    usedItems.Add((item, usedQuantity));
                }

                // Nếu đã đạt level 100000 thì dừng hoàn toàn
                if (tempLevel >= 100000)
                    break;
            }

            // Cập nhật số lượng vật phẩm đã sử dụng
            foreach (var (usedItem, usedQuantity) in usedItems)
            {
                usedItem.quantity -= usedQuantity;
                usedItem.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.level);
            rank.level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            PowerManager powerManager = new PowerManager();
            Teams teams = new Teams();
            double currentPower = teams.GetTeamsPower();
            UpLevel(cardAdmirals, newRank, mainType);
            double newPower = teams.GetTeamsPower();
            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardAdmiralsEquipments(cardAdmirals);
        });
    }
    public void CreateMaterialUI()
    {
        Close(MateriralPanel);
        Items items = new Items();
        itemsList = new List<Items>();
        itemsList = items.GetItemForRank("Affinity");
        foreach (Items item in itemsList)
        {
            GameObject itemObject = Instantiate(ItemThird, MateriralPanel);

            RawImage itemImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = item.image.Replace(".png", "");
            Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            itemImage.texture = itemTexture;

            TextMeshProUGUI itemText = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            itemText.text = item.quantity.ToString();

            RawImage itemFrameImage = itemObject.transform.Find("Frame").GetComponent<RawImage>();
            itemFrameImage.gameObject.SetActive(false);
        }
    }
    public Rank EnhanceRank(Rank rank, int level)
    {
        int startLevel = rank.level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = 1;  // Hệ số nhân dựa trên cấp độ hiện tại

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
