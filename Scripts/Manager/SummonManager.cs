using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using System.Threading.Tasks;

public class SummonManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform DictionaryContentPanel;
    private Button CloseButton;
    private Button HomeButton;
    private Button SummonOneButton;
    private Button SummonTenButton;
    private Transform RightScrollViewContentPanel;
    private Transform LeftScrollViewContentPanel;
    private GameObject SummonTabButtonPrefab;

    private GameObject SummonPanelPrefab;
    private Transform PositionPanel;
    private GameObject SummonObject;
    private Transform CurrencyPanel;
    //Variable for pagination
    private int CurrentPage = 1;
    private int TotalPage;
    private const int PAGE_SIZE = 100;
    private TextMeshProUGUI PageText;
    private string MainType;
    private TextMeshProUGUI TitleText;
    private string Type = AppConstants.Type.ALL;
    private TMP_FontAsset EuroStyleNormalFont;
    private int FontSize = 24;
    public static SummonManager Instance { get; private set; }
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
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        // mainMenuCampaignPanel = UIManager.Instance.GetTransform("mainMenuCampaignPanel");
        SummonTabButtonPrefab = UIManager.Instance.Get("SummonTabButtonPrefab");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        SummonPanelPrefab = UIManager.Instance.Get("SummonPanelPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
    }
    public void GetButtonEvent(GameObject popupButtonObject)
    {
        Transform contentPanel = popupButtonObject.transform.Find("Scroll View/Viewport/Content");
        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_HERO));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, () => GetType(AppConstants.MainType.SUMMON_BOOK));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_CAPTAIN));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_MONSTER));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_MILITARY));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_SPELL));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_COLONEL));
        ButtonEvent.Instance.AssignButtonEvent("Button_8", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_GENERAL));
        ButtonEvent.Instance.AssignButtonEvent("Button_9", contentPanel, () => GetType(AppConstants.MainType.SUMMON_CARD_ADMIRAL));
    }
    public void GetType(string type)
    {
        MainType = type; // Gán giá trị cho mainType
        _ = GetButtonTypeAsync(); // Gọi hàm xử lý
        if (TitleText != null)
        {
            TitleText.text = LocalizationManager.Get(type);
        }
    }
    public async Task GetButtonTypeAsync()
    {
        // MainMenuPanel.SetActive(true);
        if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_HERO) || MainType.Equals(AppConstants.MainType.SUMMON_BOOK)
            || MainType.Equals(AppConstants.MainType.SUMMON_CARD_CAPTAIN) || MainType.Equals(AppConstants.MainType.SUMMON_CARD_COLONEL)
            || MainType.Equals(AppConstants.MainType.SUMMON_CARD_GENERAL) || MainType.Equals(AppConstants.MainType.SUMMON_CARD_ADMIRAL)
            || MainType.Equals(AppConstants.MainType.SUMMON_CARD_MILITARY) || MainType.Equals(AppConstants.MainType.SUMMON_CARD_MONSTER)
            || MainType.Equals(AppConstants.MainType.SUMMON_CARD_SPELL))
        {
            SummonObject = Instantiate(SummonPanelPrefab, MainPanel);
            Transform transform = SummonObject.transform;
            DictionaryContentPanel = transform.Find("DictionaryCards/Scroll View/Viewport/MainContent");
            LeftScrollViewContentPanel = transform.Find("Scroll View/Viewport/ButtonContent");
            PositionPanel = transform.Find("DictionaryCards/Position");

            TitleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
            CloseButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            SummonOneButton = transform.Find("DictionaryCards/SummonButton").GetComponent<Button>();
            SummonTenButton = transform.Find("DictionaryCards/Summon10Button").GetComponent<Button>();
            CloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(SummonObject);
            });
            HomeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
            HomeButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Close(MainPanel);
                await HomeManager.Instance.CreateHomePanelAsync();
            });
            // SummonAreaPanel = transform.Find("SummonArea");
            CurrencyPanel = transform.Find("DictionaryCards/Currency");

            TextMeshProUGUI summonOneButtonText = SummonOneButton.GetComponentInChildren<TextMeshProUGUI>();
            summonOneButtonText.font = EuroStyleNormalFont;
            summonOneButtonText.fontSize = FontSize;
            summonOneButtonText.fontStyle = FontStyles.Bold;
            summonOneButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SUMMON_ONE);
            TextMeshProUGUI summonTenButtonText = SummonTenButton.GetComponentInChildren<TextMeshProUGUI>();
            summonTenButtonText.font = EuroStyleNormalFont;
            summonTenButtonText.fontSize = FontSize;
            summonTenButtonText.fontStyle = FontStyles.Bold;
            summonTenButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SUMMON_TEN);

            List<string> types = await TypeManager.GetUniqueTypesAsync(MainType);

            if (types.Count > 0 && !MainType.Equals(AppConstants.MainType.EQUIPMENT))
            {
                for (int i = 0; i < types.Count; i++)
                {
                    // Tạo một nút mới từ prefab
                    string subType = types[i];
                    GameObject button = null;
                    button = Instantiate(SummonTabButtonPrefab, LeftScrollViewContentPanel);
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                    buttonText.text = subType.Replace("_", " ");

                    Button btn = button.GetComponent<Button>();
                    btn.onClick.AddListener(() =>
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                        OnButtonClick(button, subType);
                    });

                    if (i == 0)
                    {
                        Type = subType;
                        ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.SUMMON_BUTTON_AFTER_CLICK_URL);
                        _ = LoadCurrentPageAsync();

                    }
                    else
                    {
                        ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.SUMMON_BUTTON_BEFORE_CLICK_URL);
                    }
                }
            }
            else
            {
                _ = LoadCurrentPageAsync();
            }

            RawImage dictionaryBackground = transform.Find("DictionaryBackground").GetComponent<RawImage>();
            if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_HERO))
            {
                Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SUMMON_CARD_HEROES_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (MainType.Equals(AppConstants.MainType.SUMMON_BOOK))
            {
                Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SUMMON_BOOKS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_CAPTAIN))
            {
                Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SUMMON_CARD_CAPTAINS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_MONSTER))
            {
                Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SUMMON_CARD_MONSTERS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_MILITARY))
            {
                Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SUMMON_CARD_MILITARIES_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_SPELL))
            {
                Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SUMMON_CARD_SPELLS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_COLONEL))
            {
                Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SUMMON_CARD_COLONELS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_GENERAL))
            {
                Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SUMMON_CARD_GENERALS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
            else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_ADMIRAL))
            {
                Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SUMMON_CARD_ADMIRALS_BACKGROUND_URL);
                dictionaryBackground.texture = texture;
            }
        }
        LoadAnimation();
    }
    void OnButtonClick(GameObject clickedButton, string subType)
    {
        foreach (Transform child in LeftScrollViewContentPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, ImageConstants.Button.SUMMON_BUTTON_BEFORE_CLICK_URL);
            }
        }

        Type = subType;
        CurrentPage = 1;
        ClearAllPrefabs();
        
        ButtonLoader.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.SUMMON_BUTTON_AFTER_CLICK_URL);
        _ = LoadCurrentPageAsync();
    }
    public async Task LoadCurrentPageAsync()
    {
        int totalRecord = 0;
        int listCount = 0;
        // offset = 0;
        ButtonEvent.Instance.Close(DictionaryContentPanel);
        if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_HERO))
        {
            List<CardHeroes> cardHeroes = await CardHeroesService.Create().GetCardHeroesRandomAsync(Type, 3);
            UserCardHeroesController.Instance.CreateUserCardHeroesForSummon(cardHeroes, PositionPanel);



            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_HERO_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonOneButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_HERO_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });

            });
            SummonTenButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_HERO_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (MainType.Equals(AppConstants.MainType.SUMMON_BOOK))
        {
            List<Books> books = await BooksService.Create().GetBooksRandomAsync(Type, 3);
            UserBooksController.Instance.CreateUserBooksForSummon(books, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_HERO_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonOneButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_HERO_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            SummonTenButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_HERO_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_CAPTAIN))
        {
            List<CardCaptains> cardCaptains = await CardCaptainsService.Create().GetCardCaptainsRandomAsync(Type, 3);
            UserCardCaptainsController.Instance.CreateUserCardCaptainsForSummon(cardCaptains, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_CAPTAIN_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonOneButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_CAPTAIN_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            SummonTenButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_CAPTAIN_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_MILITARY))
        {
            List<CardMilitaries> cardMilitaries = await CardMilitariesService.Create().GetCardMilitariesRandomAsync(Type, 3);
            UserCardMilitariesController.Instance.CreateUserCardMilitaryForSummon(cardMilitaries, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MILITARY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonOneButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MILITARY_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            SummonTenButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MILITARY_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_SPELL))
        {
            List<CardSpells> cardSpells = await CardSpellsService.Create().GetCardSpellsRandomAsync(Type, 3);
            UserCardSpellsController.Instance.CreateUserCardSpellForSummon(cardSpells, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_SPELL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonOneButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_SPELL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            SummonTenButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_SPELL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_MONSTER))
        {
            List<CardMonsters> cardMonsters = await CardMonstersService.Create().GetCardMonstersRandomAsync(Type, 3);
            UserCardMonstersController.Instance.CreateUserCardMonstersForSummon(cardMonsters, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MONSTER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonOneButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MONSTER_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            SummonTenButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MONSTER_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_COLONEL))
        {
            List<CardColonels> cardColonels = await CardColonelsService.Create().GetCardColonelsRandomAsync(Type, 3);
            UserCardColonelsController.Instance.CreateUserCardColonelsForSummon(cardColonels, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_COLONEL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonOneButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_COLONEL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            SummonTenButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_COLONEL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_GENERAL))
        {
            List<CardGenerals> cardGenerals = await CardGeneralsService.Create().GetCardGeneralsRandomAsync(Type, 3);
            UserCardGeneralsController.Instance.CreateUserCardGeneralsForSummon(cardGenerals, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_GENERAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonOneButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_GENERAL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            SummonTenButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_GENERAL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }
        else if (MainType.Equals(AppConstants.MainType.SUMMON_CARD_ADMIRAL))
        {
            List<CardAdmirals> cardAdmirals = await CardAdmiralsService.Create().GetCardAdmiralsRandomAsync(Type, 3);
            UserCardAdmiralsController.Instance.CreateUserCardAdmiralsForSummon(cardAdmirals, PositionPanel);

            List<Items> items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_ADMIRAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                CurrencyPanel
            );

            CreateTicketUI(items);

            SummonOneButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 1, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_ADMIRAL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
            SummonTenButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                FindObjectOfType<GachaSystem>().Summon(MainType, Type, SummonObject, 10, items, async (success) =>
                {
                    if (success)
                    {
                        CurrenciesManager.Instance.GetTicketsCurrency(
                        new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_ADMIRAL_TICKET) },
                        CurrencyPanel
                        );
                    }
                    else
                    {
                        // Debug.Log("Summon thất bại!");
                    }
                });
            });
        }

        if (listCount > 0)
        {
            TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);
            PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
        }
    }
    public void ClearAllPrefabs()
    {
        // Duyệt qua tất cả các con cái của cardsContent
        if (DictionaryContentPanel != null)
        {
            foreach (Transform child in DictionaryContentPanel)
            {
                Destroy(child.gameObject);
            }
        }
        if (PositionPanel != null)
        {
            foreach (Transform child in PositionPanel)
            {
                Destroy(child.gameObject);
            }
        }
    }
    public void Close(Transform content)
    {
        CurrentPage = 1;
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public void CreateTicketUI(List<Items> items)
    {
        foreach (Items item in items)
        {
            RawImage oneTicketImage = SummonObject.transform.Find("DictionaryCards/OneTicketImage").GetComponent<RawImage>();
            RawImage tenTicketImage = SummonObject.transform.Find("DictionaryCards/TenTicketImage").GetComponent<RawImage>();
            TextMeshProUGUI oneTicketText = SummonObject.transform.Find("DictionaryCards/OneTicketText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI tenTicketText = SummonObject.transform.Find("DictionaryCards/TenTicketText").GetComponent<TextMeshProUGUI>();

            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(item.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            oneTicketImage.texture = texture;
            tenTicketImage.texture = texture;
            oneTicketText.text = "1";
            tenTicketText.text = "10";
        }
    }
    public void LoadAnimation()
    {
        if (LeftScrollViewContentPanel != null)
        {
            LeftScrollViewContentPanel.gameObject.AddComponent<SlideLeftToRightAnimation>();
        }

        if (RightScrollViewContentPanel != null)
        {
            RightScrollViewContentPanel.gameObject.AddComponent<SlideRightToLeftAnimation>();
        }
    }
}
