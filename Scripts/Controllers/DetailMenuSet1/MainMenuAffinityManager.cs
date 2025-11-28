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
    TeamsService teamsService;
    UserItemsService userItemsService;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainMenuAffinityPanelPrefab = UIManager.Instance.GetGameObjectMainMenu1("MainMenuAffinityPanelPrefab");
        ItemThird = UIManager.Instance.GetGameObject("ItemThird");
        // List<Items> itemsList = new List<Items>();
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }
    public void CreateMainMenuAffinityManager(object data)
    {
        currentObject = Instantiate(MainMenuAffinityPanelPrefab, MainPanel);
        MateriralPanel = currentObject.transform.Find("DictionaryCards/SetGroup/Viewport/Content");
        mainImage = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        mainLevelText = currentObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainMenuSet1.AFFINITY);
        UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Close(MainPanel);
        });
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });

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
        else if (data is CardMilitaries cardMilitary)
        {
            // mainId = cardMilitary.id;
            CreateCardMilitaryEquipments(cardMilitary);
        }
        else if (data is CardSpells cardSpell)
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
        Rank rank = UserCardHeroesRankService.Create().GetCardHeroesRank(mainType, cardHeroes.Id);
        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(cardHeroes.Image)}");
        mainImage.texture = texture;
        mainLevelText.text = rank.Level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình

            UpLevel(cardHeroes, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardHeroesEquipments(cardHeroes);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm để giữ cấp hiện tại
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình

            UpLevel(cardHeroes, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardHeroesEquipments(cardHeroes);
        });
    }
    public void CreateBooksEquipments(Books books)
    {
        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(books.Image)}");
        mainImage.texture = texture;
        Rank rank = UserBooksRankService.Create().GetBooksRank(mainType, books.Id);
        mainLevelText.text = rank.Level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            
            UpLevel(books, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateBooksEquipments(books);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm để giữ cấp hiện tại
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            
            UpLevel(books, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateBooksEquipments(books);
        });
    }
    public void CreateCardCaptainsEquipments(CardCaptains cardCaptains)
    {
        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(cardCaptains.Image)}");
        mainImage.texture = texture;
        Rank rank = UserCardCaptainsRankService.Create().GetCardCaptainsRank(mainType, cardCaptains.Id);
        mainLevelText.text = rank.Level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình

            UpLevel(cardCaptains, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardCaptainsEquipments(cardCaptains);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm để giữ cấp hiện tại
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            
            UpLevel(cardCaptains, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardCaptainsEquipments(cardCaptains);
        });
    }
    public void CreatePetsEquipments(Pets pets)
    {
        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(pets.Image)}");
        mainImage.texture = texture;
        Rank rank = UserPetsRankService.Create().GetPetsRank(mainType, pets.Id);
        mainLevelText.text = rank.Level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình

            UpLevel(pets, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreatePetsEquipments(pets);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm để giữ cấp hiện tại
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            
            UpLevel(pets, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreatePetsEquipments(pets);
        });
    }
    public void CreateCardMilitaryEquipments(CardMilitaries cardMilitary)
    {
        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(cardMilitary.Image)}");
        mainImage.texture = texture;
        Rank rank = UserCardMilitaryRankService.Create().GetCardMilitaryRank(mainType, cardMilitary.Id);
        mainLevelText.text = rank.Level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình

            UpLevel(cardMilitary, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardMilitaryEquipments(cardMilitary);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm để giữ cấp hiện tại
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình

            UpLevel(cardMilitary, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardMilitaryEquipments(cardMilitary);
        });
    }
    public void CreateCardSpellEquipments(CardSpells cardSpell)
    {
        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(cardSpell.Image)}");
        mainImage.texture = texture;
        Rank rank = UserCardSpellRankService.Create().GetCardSpellRank(mainType, cardSpell.Id);
        mainLevelText.text = rank.Level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình

            UpLevel(cardSpell, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardSpellEquipments(cardSpell);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm để giữ cấp hiện tại
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            
            UpLevel(cardSpell, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardSpellEquipments(cardSpell);
        });
    }
    public void CreateCardMonstersEquipments(CardMonsters cardMonsters)
    {
        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(cardMonsters.Image)}");
        mainImage.texture = texture;
        Rank rank = UserCardMonstersRankService.Create().GetCardMonstersRank(mainType, cardMonsters.Id);
        mainLevelText.text = rank.Level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            
            UpLevel(cardMonsters, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardMonstersEquipments(cardMonsters);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm để giữ cấp hiện tại
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            

            UpLevel(cardMonsters, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardMonstersEquipments(cardMonsters);
        });
    }
    public void CreateCardColonelsEquipments(CardColonels cardColonels)
    {
        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(cardColonels.Image)}");
        mainImage.texture = texture;
        Rank rank = UserCardColonelsRankService.Create().GetCardColonelsRank(mainType, cardColonels.Id);
        mainLevelText.text = rank.Level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            
            UpLevel(cardColonels, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardColonelsEquipments(cardColonels);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm để giữ cấp hiện tại
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            
            UpLevel(cardColonels, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardColonelsEquipments(cardColonels);
        });
    }
    public void CreateCardGeneralsEquipments(CardGenerals cardGenerals)
    {
        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(cardGenerals.Image)}");
        mainImage.texture = texture;
        Rank rank = UserCardGeneralsRankService.Create().GetCardGeneralsRank(mainType, cardGenerals.Id);
        mainLevelText.text = rank.Level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            
            UpLevel(cardGenerals, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardGeneralsEquipments(cardGenerals);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm để giữ cấp hiện tại
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình

            UpLevel(cardGenerals, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardGeneralsEquipments(cardGenerals);
        });
    }
    public void CreateCardAdmiralsEquipments(CardAdmirals cardAdmirals)
    {
        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(cardAdmirals.Image)}");
        mainImage.texture = texture;
        Rank rank = UserCardAdmiralsRankService.Create().GetCardAdmiralsRank(mainType, cardAdmirals.Id);
        mainLevelText.text = rank.Level.ToString();
        CreateMaterialUI();
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm giữ cấp hiện tại
            int expNeeded = (tempLevel == 0 ? 1 : tempLevel) * 100; // Exp cần để lên 1 cấp
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào quantity
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            
            UpLevel(cardAdmirals, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardAdmiralsEquipments(cardAdmirals);
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (rank.Level >= 100000)
                return; // Nếu đã đạt giới hạn, không nâng cấp nữa

            int tempLevel = rank.Level; // Biến tạm để giữ cấp hiện tại
            double totalExp = 0;
            List<(Items item, int usedQuantity)> usedItems = new List<(Items, int)>(); // Lưu vật phẩm + số lượng đã sử dụng

            // Tính tổng exp từ vật phẩm dựa vào số lượng còn lại
            foreach (var item in itemsList)
            {
                double itemExp = EvaluateExperiment.GetItemExp(item.Name);
                double availableQuantity = item.Quantity;
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
                usedItem.Quantity -= usedQuantity;
                userItemsService.UpdateUserItemsQuantity(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình

            UpLevel(cardAdmirals, newRank, mainType);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            CreateCardAdmiralsEquipments(cardAdmirals);
        });
    }
    public void CreateMaterialUI()
    {
        Close(MateriralPanel);
        Items items = new Items();
        itemsList = new List<Items>();
        itemsList = userItemsService.GetItemForRank("Affinity");
        foreach (Items item in itemsList)
        {
            GameObject itemObject = Instantiate(ItemThird, MateriralPanel);

            RawImage itemImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
            Texture itemTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(item.Image)}");
            itemImage.texture = itemTexture;

            TextMeshProUGUI itemText = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            itemText.text = item.Quantity.ToString();

            RawImage itemFrameImage = itemObject.transform.Find("Frame").GetComponent<RawImage>();
            itemFrameImage.gameObject.SetActive(false);
        }
    }
    public Rank EnhanceRank(Rank rank, int level)
    {
        int startLevel = rank.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = 1;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                rank.Health += 10000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                rank.PhysicalAttack += 1500000 * statMultiplier;
                rank.PhysicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                rank.MagicalAttack += 1500000 * statMultiplier;
                rank.MagicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                rank.ChemicalAttack += 1500000 * statMultiplier;
                rank.ChemicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                rank.AtomicAttack += 1500000 * statMultiplier;
                rank.AtomicDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                rank.MentalAttack += 1500000 * statMultiplier;
                rank.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                rank.Speed += 1500000 * statMultiplier;
                rank.CriticalDamageRate += 0.1 * statMultiplier;
                rank.CriticalRate += 0.1 * statMultiplier;
                rank.PenetrationRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                rank.EvasionRate += 0.1 * statMultiplier;
                rank.DamageAbsorptionRate += 0.1 * statMultiplier;
                rank.VitalityRegenerationRate += 0.1 * statMultiplier;
                rank.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                rank.LifestealRate += 0.1 * statMultiplier;
                rank.Mana += 1500000 * statMultiplier;
                rank.ManaRegenerationRate += 0.1 * statMultiplier;
                rank.ShieldStrength += 1500000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                rank.Tenacity += 0.5 * statMultiplier;
                rank.ResistanceRate += 0.1 * statMultiplier;
                rank.ComboRate += 0.1 * statMultiplier;
                rank.ReflectionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                rank.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                rank.DamageToSameFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToSameFactionRate += 0.1 * statMultiplier;
                rank.PercentAllHealth += 5 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                rank.PercentAllPhysicalAttack += 5 * statMultiplier;
                rank.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                rank.PercentAllMagicalAttack += 5 * statMultiplier;
                rank.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                rank.PercentAllChemicalAttack += 5 * statMultiplier;
                rank.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                rank.PercentAllAtomicAttack += 5 * statMultiplier;
                rank.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                rank.PercentAllMentalAttack += 5 * statMultiplier;
                rank.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                rank.PhysicalAttack += 1500000 * statMultiplier;
                rank.MagicalAttack += 1500000 * statMultiplier;
                rank.ChemicalAttack += 1500000 * statMultiplier;
                rank.AtomicAttack += 1500000 * statMultiplier;
                rank.MentalAttack += 1500000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                rank.PhysicalDefense += 1500000 * statMultiplier;
                rank.MagicalDefense += 1500000 * statMultiplier;
                rank.ChemicalDefense += 1500000 * statMultiplier;
                rank.AtomicDefense += 1500000 * statMultiplier;
                rank.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                rank.Speed += 1500000 * statMultiplier;
                rank.CriticalDamageRate += 0.1 * statMultiplier;
                rank.CriticalRate += 0.1 * statMultiplier;
                rank.PenetrationRate += 0.1 * statMultiplier;
                rank.EvasionRate += 0.1 * statMultiplier;
                rank.DamageAbsorptionRate += 0.1 * statMultiplier;
                rank.VitalityRegenerationRate += 0.1 * statMultiplier;
                rank.AccuracyRate += 0.1 * statMultiplier;
                rank.LifestealRate += 0.1 * statMultiplier;
                rank.Mana += 1500000 * statMultiplier;
                rank.ManaRegenerationRate += 0.1 * statMultiplier;
                rank.ShieldStrength += 1500000 * statMultiplier;
                rank.Tenacity += 0.5 * statMultiplier;
                rank.ResistanceRate += 0.1 * statMultiplier;
                rank.ComboRate += 0.1 * statMultiplier;
                rank.ReflectionRate += 0.1 * statMultiplier;
                rank.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                rank.DamageToSameFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        rank.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return rank;
    }
    public void UpLevel(object data, Rank rank, string type)
    {
        if (data is CardHeroes cardHeroes)
        {
            UserCardHeroesRankService.Create().InsertOrUpdateCardHeroesRank(rank, type, cardHeroes.Id);
        }
        else if (data is Books books)
        {
            UserBooksRankService.Create().InsertOrUpdateBooksRank(rank, type, books.Id);
        }
        else if (data is CardCaptains cardCaptains)
        {
            UserCardCaptainsRankService.Create().InsertOrUpdateCardCaptainsRank(rank, type, cardCaptains.Id);
        }
        else if (data is Pets pets)
        {
            UserPetsRankService.Create().InsertOrUpdatePetsRank(rank, type, pets.Id);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            UserCardMilitaryRankService.Create().InsertOrUpdateCardMilitaryRank(rank, type, cardMilitary.Id);
        }
        else if (data is CardSpells cardSpell)
        {
            UserCardSpellRankService.Create().InsertOrUpdateCardSpellRank(rank, type, cardSpell.Id);
        }
        else if (data is CardMonsters cardMonsters)
        {
            UserCardMonstersRankService.Create().InsertOrUpdateCardMonstersRank(rank, type, cardMonsters.Id);
        }
        else if (data is CardColonels cardColonels)
        {
            UserCardColonelsRankService.Create().InsertOrUpdateCardColonelsRank(rank, type, cardColonels.Id);
        }
        else if (data is CardGenerals cardGenerals)
        {
            UserCardGeneralsRankService.Create().InsertOrUpdateCardGeneralsRank(rank, type, cardGenerals.Id);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            UserCardAdmiralsRankService.Create().InsertOrUpdateCardAdmiralsRank(rank, type, cardAdmirals.Id);
        }
    }
}
