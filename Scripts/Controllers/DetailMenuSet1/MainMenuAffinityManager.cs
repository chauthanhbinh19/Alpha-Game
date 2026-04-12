using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading.Tasks;

public class MainMenuAffinityManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform MateriralPanel;
    private GameObject MainMenuAffinityPanelPrefab;
    private GameObject currentObject;
    private GameObject ItemPopupPrefab;
    private RawImage mainImage;
    private TextMeshProUGUI mainLevelText;
    private Button upLevelButton;
    private Button upMaxLevelButton;
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
        MainMenuAffinityPanelPrefab = UIManager.Instance.Get("MainMenuAffinityPanelPrefab");
        ItemPopupPrefab = UIManager.Instance.Get("ItemPopupPrefab");
        // List<Items> itemsList = new List<Items>();
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }
    public void CreateMainMenuAffinityManager(object data)
    {
        currentObject = Instantiate(MainMenuAffinityPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        MateriralPanel = transform.Find("DictionaryCards/SetGroup/Viewport/Content");
        mainImage = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        mainLevelText = transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // SlotPanel = transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainMenuSet1.AFFINITY);
        upLevelButton = transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        upMaxLevelButton = transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Close(MainPanel);
        });
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });

        RawImage background = transform.Find("DictionaryBackground").GetComponent<RawImage>();
        background.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.BACKGROUND_53_URL);
        RawImage closeButtonBackground = closeButton.GetComponent<RawImage>();
        RawImage homeButtonBackground = homeButton.GetComponent<RawImage>();
        closeButtonBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.BACK_BUTTON_BACKGROUND_URL);
        homeButtonBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.HOME_BUTTON_BACKGROUND_URL);
        RawImage scrollViewBackground = transform.Find("DictionaryCards/ScrollViewBackground").GetComponent<RawImage>();
        scrollViewBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SCROLLVIEW_BACKGROUND_1_URL);
        RawImage titleBackground = transform.Find("DictionaryCards/TitleBackground").GetComponent<RawImage>();
        titleBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.TITLE_BUTTON_BACKGROUND_URL);

        if (data is CardHeroes cardHeroes)
        {
            // mainId = cardHeroes.id;
            _=CreateCardHeroesEquipmentsAsync(cardHeroes);
        }
        else if (data is Books books)
        {
            // mainId = books.id;
            _=CreateBooksEquipmentsAsync(books);
        }
        else if (data is CardCaptains cardCaptains)
        {
            // mainId = cardCaptains.id;
            _=CreateCardCaptainsEquipmentsAsync(cardCaptains);
        }
        else if (data is Pets pets)
        {
            // mainId = pets.id;
            _=CreatePetsEquipmentsAsync(pets);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            // mainId = cardMilitary.id;
            _=CreateCardMilitaryEquipmentsAsync(cardMilitary);
        }
        else if (data is CardSpells cardSpell)
        {
            // mainId = cardSpell.id;
            _=CreateCardSpellEquipmentsAsync(cardSpell);
        }
        else if (data is CardMonsters cardMonsters)
        {
            // mainId = cardMonsters.id;
            _=CreateCardMonstersEquipmentsAsync(cardMonsters);
        }
        else if (data is CardColonels cardColonels)
        {
            // mainId = cardColonels.id;
            _=CreateCardColonelsEquipmentsAsync(cardColonels);
        }
        else if (data is CardGenerals cardGenerals)
        {
            // mainId = cardGenerals.id;
            _=CreateCardGeneralsEquipmentsAsync(cardGenerals);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            // mainId = cardAdmirals.id;
            _=CreateCardAdmiralsEquipmentsAsync(cardAdmirals);
        }
    }
    public void Close(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public async Task CreateCardHeroesEquipmentsAsync(CardHeroes cardHero)
    {
        Rank rank = await UserCardHeroesRankService.Create().GetCardHeroRankAsync(mainType, cardHero.Id);
        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(cardHero.Image)}");
        mainImage.texture = texture;
        mainLevelText.text = rank.Level.ToString();
        await CreateMaterialUIAsync();
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình

            await UpLevelAsync(cardHero, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardHeroesEquipmentsAsync(cardHero);
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình

            await UpLevelAsync(cardHero, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardHeroesEquipmentsAsync(cardHero);
        });
    }
    public async Task CreateBooksEquipmentsAsync(Books book)
    {
        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(book.Image)}");
        mainImage.texture = texture;
        Rank rank = await UserBooksRankService.Create().GetBookRankAsync(mainType, book.Id);
        mainLevelText.text = rank.Level.ToString();
        await CreateMaterialUIAsync();
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            
            await UpLevelAsync(book, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateBooksEquipmentsAsync(book);
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            
            await UpLevelAsync(book, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateBooksEquipmentsAsync(book);
        });
    }
    public async Task CreateCardCaptainsEquipmentsAsync(CardCaptains cardCaptain)
    {
        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(cardCaptain.Image)}");
        mainImage.texture = texture;
        Rank rank = await UserCardCaptainsRankService.Create().GetCardCaptainRankAsync(mainType, cardCaptain.Id);
        mainLevelText.text = rank.Level.ToString();
        await CreateMaterialUIAsync();
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình

            await UpLevelAsync(cardCaptain, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardCaptainsEquipmentsAsync(cardCaptain);
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            
            await UpLevelAsync(cardCaptain, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardCaptainsEquipmentsAsync(cardCaptain);
        });
    }
    public async Task CreatePetsEquipmentsAsync(Pets pet)
    {
        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(pet.Image)}");
        mainImage.texture = texture;
        Rank rank = await UserPetsRankService.Create().GetPetRankAsync(mainType, pet.Id);
        mainLevelText.text = rank.Level.ToString();
        await CreateMaterialUIAsync();
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình

            await UpLevelAsync(pet, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreatePetsEquipmentsAsync(pet);
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            
            await UpLevelAsync(pet, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreatePetsEquipmentsAsync(pet);
        });
    }
    public async Task CreateCardMilitaryEquipmentsAsync(CardMilitaries cardMilitary)
    {
        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(cardMilitary.Image)}");
        mainImage.texture = texture;
        Rank rank = await UserCardMilitariesRankService.Create().GetCardMilitaryRankAsync(mainType, cardMilitary.Id);
        mainLevelText.text = rank.Level.ToString();
        await CreateMaterialUIAsync();
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình

            await UpLevelAsync(cardMilitary, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardMilitaryEquipmentsAsync(cardMilitary);
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình

            await UpLevelAsync(cardMilitary, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardMilitaryEquipmentsAsync(cardMilitary);
        });
    }
    public async Task CreateCardSpellEquipmentsAsync(CardSpells cardSpell)
    {
        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(cardSpell.Image)}");
        mainImage.texture = texture;
        Rank rank = await UserCardSpellsRankService.Create().GetCardSpellRankAsync(mainType, cardSpell.Id);
        mainLevelText.text = rank.Level.ToString();
        await CreateMaterialUIAsync();
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình

            await UpLevelAsync(cardSpell, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardSpellEquipmentsAsync(cardSpell);
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            
            await UpLevelAsync(cardSpell, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardSpellEquipmentsAsync(cardSpell);
        });
    }
    public async Task CreateCardMonstersEquipmentsAsync(CardMonsters cardMonster)
    {
        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(cardMonster.Image)}");
        mainImage.texture = texture;
        Rank rank = await UserCardMonstersRankService.Create().GetCardMonsterRankAsync(mainType, cardMonster.Id);
        mainLevelText.text = rank.Level.ToString();
        await CreateMaterialUIAsync();
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            
            await UpLevelAsync(cardMonster, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardMonstersEquipmentsAsync(cardMonster);
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            

            await UpLevelAsync(cardMonster, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardMonstersEquipmentsAsync(cardMonster);
        });
    }
    public async Task CreateCardColonelsEquipmentsAsync(CardColonels cardColonel)
    {
        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(cardColonel.Image)}");
        mainImage.texture = texture;
        Rank rank = await UserCardColonelsRankService.Create().GetCardColonelRankAsync(mainType, cardColonel.Id);
        mainLevelText.text = rank.Level.ToString();
        await CreateMaterialUIAsync();
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            
            await UpLevelAsync(cardColonel, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardColonelsEquipmentsAsync(cardColonel);
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình
            
            await UpLevelAsync(cardColonel, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardColonelsEquipmentsAsync(cardColonel);
        });
    }
    public async Task CreateCardGeneralsEquipmentsAsync(CardGenerals cardGeneral)
    {
        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(cardGeneral.Image)}");
        mainImage.texture = texture;
        Rank rank = await UserCardGeneralsRankService.Create().GetCardGeneralRankAsync(mainType, cardGeneral.Id);
        mainLevelText.text = rank.Level.ToString();
        await CreateMaterialUIAsync();
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            
            await UpLevelAsync(cardGeneral, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardGeneralsEquipmentsAsync(cardGeneral);
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình

            await UpLevelAsync(cardGeneral, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardGeneralsEquipmentsAsync(cardGeneral);
        });
    }
    public async Task CreateCardAdmiralsEquipmentsAsync(CardAdmirals cardAdmiral)
    {
        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(cardAdmiral.Image)}");
        mainImage.texture = texture;
        Rank rank = await UserCardAdmiralsRankService.Create().GetCardAdmiralRankAsync(mainType, cardAdmiral.Id);
        mainLevelText.text = rank.Level.ToString();
        await CreateMaterialUIAsync();
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Gọi EnhanceRank với cấp tạm thời, không chỉnh rank.level trực tiếp
            Rank newRank = EnhanceRank(rank, levelsUp);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng 1 lần duy nhất

            // Cập nhật sức mạnh đội hình
            
            await UpLevelAsync(cardAdmiral, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardAdmiralsEquipmentsAsync(cardAdmiral);
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            Dictionary<string, Features> feature = new Dictionary<string, Features>();
            feature = await FeaturesService.Create().GetFeaturesByTypeAsync(mainType);
            rank.Id = feature[mainType].Id;

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
                await userItemsService.UpdateUserItemQuantityAsync(usedItem);
            }

            // Cập nhật rank sau khi tính toán xong
            Rank newRank = EnhanceRank(rank, tempLevel - rank.Level);
            rank.Level = tempLevel; // Cập nhật cấp cuối cùng

            // Cập nhật sức mạnh đội hình

            await UpLevelAsync(cardAdmiral, newRank, mainType);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            await CreateCardAdmiralsEquipmentsAsync(cardAdmiral);
        });
    }
    public async Task CreateMaterialUIAsync()
    {
        Close(MateriralPanel);
        Items items = new Items();
        itemsList = new List<Items>();
        itemsList = await userItemsService.GetItemForRankAsync("Affinity");
        foreach (Items item in itemsList)
        {
            GameObject itemObject = Instantiate(ItemPopupPrefab, MateriralPanel);

            RawImage itemImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
            Texture itemTexture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(item.Image)}");
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
    public async Task UpLevelAsync(object data, Rank rank, string type)
    {
        if (data is CardHeroes cardHero)
        {
            await UserCardHeroesRankService.Create().InsertOrUpdateCardHeroRankAsync(rank, cardHero.Id);
        }
        else if (data is Books book)
        {
            await UserBooksRankService.Create().InsertOrUpdateBookRankAsync(rank, book.Id);
        }
        else if (data is CardCaptains cardCaptain)
        {
            await UserCardCaptainsRankService.Create().InsertOrUpdateCardCaptainRankAsync(rank, cardCaptain.Id);
        }
        else if (data is Pets pet)
        {
            await UserPetsRankService.Create().InsertOrUpdatePetRankAsync(rank, pet.Id);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            await UserCardMilitariesRankService.Create().InsertOrUpdateCardMilitaryRankAsync(rank, cardMilitary.Id);
        }
        else if (data is CardSpells cardSpell)
        {
            await UserCardSpellsRankService.Create().InsertOrUpdateCardSpellRankAsync(rank, cardSpell.Id);
        }
        else if (data is CardMonsters cardMonster)
        {
            await UserCardMonstersRankService.Create().InsertOrUpdateCardMonsterRankAsync(rank, cardMonster.Id);
        }
        else if (data is CardColonels cardColonel)
        {
            await UserCardColonelsRankService.Create().InsertOrUpdateCardColonelRankAsync(rank, cardColonel.Id);
        }
        else if (data is CardGenerals cardGeneral)
        {
            await UserCardGeneralsRankService.Create().InsertOrUpdateCardGeneralRankAsync(rank, cardGeneral.Id);
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            await UserCardAdmiralsRankService.Create().InsertOrUpdateCardAdmiralRankAsync(rank, cardAdmiral.Id);
        }
    }
}
