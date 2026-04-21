using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance { get; private set; }
    private GameObject ProfilePanelPrefab;
    private GameObject EditNamePanelPrefab;
    private GameObject profileObject;
    private GameObject editNamePanelObject;
    private GameObject CurrencyPanelPrefab;
    private GameObject SettingPanelPrefab;
    private GameObject SettingButtonPrefab;
    private GameObject LanguageButtonPrefab;
    private GameObject NewsPanelPrefab;
    private GameObject NewsButtonPrefab;
    private Transform WaitingPanel;
    private Transform MainPanel;
    private Transform RootPanel;
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
        WaitingPanel = UIManager.Instance.GetTransform("WaitingPanel");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        RootPanel = UIManager.Instance.GetTransform("RootPanel");
        ProfilePanelPrefab = UIManager.Instance.Get("ProfilePanelPrefab");
        EditNamePanelPrefab = UIManager.Instance.Get("EditNamePanelPrefab");
        CurrencyPanelPrefab = UIManager.Instance.Get("CurrencyPanelPrefab");
        SettingPanelPrefab = UIManager.Instance.Get("SettingPanelPrefab");
        SettingButtonPrefab = UIManager.Instance.Get("SettingButtonPrefab");
        LanguageButtonPrefab = UIManager.Instance.Get("LanguageButtonPrefab");
        NewsPanelPrefab = UIManager.Instance.Get("NewsPanelPrefab");
        NewsButtonPrefab = UIManager.Instance.Get("NewsButtonPrefab");
    }
    public async Task CreateProfileAsync()
    {
        profileObject = Instantiate(ProfilePanelPrefab, MainPanel);
        Transform transform = profileObject.transform;
        Transform profileTransform = transform.Find("Scroll View/Viewport/Content");
        // titleText = profileObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        // CloseButton = profileObject.transform.Find("CloseButton").GetComponent<Button>();
        // CloseButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     Destroy(profileObject);
        // });
        // HomeButton = profileObject.transform.Find("HomeButton").GetComponent<Button>();
        // HomeButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     ButtonEvent.Instance.Close(MainPanel);
        // });

        // titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.FEATURE);
        // ButtonLoader.Instance.CreateFeatureButton(profileTransform);
        RawImage avatarImage = transform.Find("Group2/AvatarImage").GetComponent<RawImage>();
        RawImage borderImage = transform.Find("Group2/BorderImage").GetComponent<RawImage>();
        TextMeshProUGUI nameText = transform.Find("Group2/NameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI levelText = transform.Find("Group2/LevelText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI powerText = transform.Find("Group2/PowerText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI idText = transform.Find("Group2/IdText").GetComponent<TextMeshProUGUI>();
        Button editNameButton = transform.Find("Group2/EditNameButton").GetComponent<Button>();
        Button moreCurrencyButton = transform.Find("Group4/MoreButton").GetComponent<Button>();

        avatarImage.texture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(User.CurrentUserAvatar));
        borderImage.texture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(User.CurrentUserBorder));
        nameText.text = User.CurrentUserName;
        levelText.text = User.CurrentUserLevel.ToString();
        powerText.text = User.CurrentUserPower.ToString();
        idText.text = User.CurrentUserId.ToString();

        var currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(User.CurrentUserId);
        var silver = currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.SILVER);
        var gold = currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.GOLD);
        var diamond = currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.DIAMOND);

        RawImage silverImage = transform.Find("Group4/CurrencyGroup/Silver/CurrencyImage").GetComponent<RawImage>();
        RawImage goldImage = transform.Find("Group4/CurrencyGroup/Gold/CurrencyImage").GetComponent<RawImage>();
        RawImage diamondImage = transform.Find("Group4/CurrencyGroup/Diamond/CurrencyImage").GetComponent<RawImage>();
        TextMeshProUGUI silverText = transform.Find("Group4/CurrencyGroup/Silver/CurrencyText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI goldText = transform.Find("Group4/CurrencyGroup/Gold/CurrencyText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI diamondText = transform.Find("Group4/CurrencyGroup/Diamond/CurrencyText").GetComponent<TextMeshProUGUI>();

        silverImage.texture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(silver.Image));
        goldImage.texture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(gold.Image));
        diamondImage.texture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(diamond.Image));
        silverText.text = silver.Quantity.ToString();
        goldText.text = gold.Quantity.ToString();
        diamondText.text = diamond.Quantity.ToString();

        editNameButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            CreateEditNamePanel();
        });
        moreCurrencyButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await CreateCurrencyPanelAsync();
        });

        Button logoutButton = transform.Find("Group1/LogoutButton").GetComponent<Button>();
        Button settingButton = transform.Find("Group1/SettingButton").GetComponent<Button>();
        Button mailButton = transform.Find("Group1/MailButton").GetComponent<Button>();
        Button newsButton = transform.Find("Group1/NewsButton").GetComponent<Button>();
        Button Photo = transform.Find("Group1/PhotoButton").GetComponent<Button>();
        Button FriendButton = transform.Find("Group1/FriendButton").GetComponent<Button>();

        logoutButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            CreateLogoutPanel();
        });

        settingButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            CreateSettingPanel();
        });

        newsButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            CreateNewsPanel();
        });
    }
    public void CreateEditNamePanel()
    {
        editNamePanelObject = Instantiate(EditNamePanelPrefab, MainPanel);
        Transform transform = editNamePanelObject.transform;
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        Button saveButton = transform.Find("SaveButton").GetComponent<Button>();
        TMP_InputField nameText = transform.Find("NameText").GetComponent<TMP_InputField>();
        Transform warningTransform = transform.Find("Warning");
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(editNamePanelObject);
        });
        saveButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var name = nameText.text;
            var isNameExisted = await UserService.Create().CheckNameExistsAsync(name);
            if (!isNameExisted)
            {
                warningTransform.gameObject.SetActive(false);
                await UserService.Create().UpdateUserNameAsync(User.CurrentUserId, name);
                Destroy(editNamePanelObject);
                Destroy(profileObject);
                await CreateProfileAsync();
            }
            else
            {
                warningTransform.gameObject.SetActive(true);
            }
        });
    }
    public async Task CreateCurrencyPanelAsync()
    {
        GameObject currencyObject = Instantiate(CurrencyPanelPrefab, MainPanel);
        Transform transform = currencyObject.transform;
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        Transform currencyPanel = transform.Find("Scroll View/Viewport/Content");
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currencyObject);
        });

        var currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(User.CurrentUserId);
        CurrenciesManager.Instance.createCurrency(currencies, currencyPanel);
    }
    public void CreateLogoutPanel()
    {
        AuthManager.Logout();
        WaitingPanel.gameObject.SetActive(true);
        ButtonEvent.Instance.Close(MainPanel);
        ButtonEvent.Instance.Close(RootPanel);
        AuthenticationManager.Instance.CheckLoggedIn();
    }
    public void CreateSettingPanel()
    {
        GameObject settingObject = Instantiate(SettingPanelPrefab, MainPanel);
        Transform transform = settingObject.transform;
        Transform settingPanel = transform.Find("RightScrollView/Viewport/Content");
        ButtonEvent.Instance.Close(settingPanel);
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        GameObject graphicObject = Instantiate(SettingButtonPrefab, settingPanel);
        GameObject soundObject = Instantiate(SettingButtonPrefab, settingPanel);
        GameObject otherObject = Instantiate(SettingButtonPrefab, settingPanel);
        GameObject languageObject = Instantiate(SettingButtonPrefab, settingPanel);

        TextMeshProUGUI graphicText = graphicObject.transform.Find("IconText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI soundText = soundObject.transform.Find("IconText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI otherText = otherObject.transform.Find("IconText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI languageText = languageObject.transform.Find("IconText").GetComponent<TextMeshProUGUI>();

        graphicText.text = "Graphics";
        soundText.text = "Sound";
        otherText.text = "Others";
        languageText.text = "Language";

        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(settingObject);
        });

        RawImage graphicImage = graphicObject.transform.Find("IconImage").GetComponent<RawImage>();
        graphicImage.texture = TextureHelper.LoadTextureCached($"UI/Icon/photo");
        RawImage soundImage = soundObject.transform.Find("IconImage").GetComponent<RawImage>();
        soundImage.texture = TextureHelper.LoadTextureCached($"UI/Icon/speaker-filled-audio-tool");
        RawImage otherImage = otherObject.transform.Find("IconImage").GetComponent<RawImage>();
        otherImage.texture = TextureHelper.LoadTextureCached($"UI/Icon/setting-1");
        RawImage languageImage = languageObject.transform.Find("IconImage").GetComponent<RawImage>();
        languageImage.texture = TextureHelper.LoadTextureCached($"UI/Icon/translation");

        Button graphicButton = graphicObject.transform.GetComponent<Button>();
        Button soundButton = soundObject.transform.GetComponent<Button>();
        Button otherButton = otherObject.transform.GetComponent<Button>();
        Button languageButton = languageObject.transform.GetComponent<Button>();

        Transform graphicTransform = settingObject.transform.Find("Graphic");
        Transform soundTransform = settingObject.transform.Find("Sound");
        Transform otherTransform = settingObject.transform.Find("Other");
        Transform languageTransform = settingObject.transform.Find("Language");

        //Get default
        graphicTransform.gameObject.SetActive(true);
        soundTransform.gameObject.SetActive(false);
        otherTransform.gameObject.SetActive(false);
        languageTransform.gameObject.SetActive(false);

        ChangeSettingButtonColor(graphicButton, true);
        ChangeSettingButtonColor(soundButton, false);
        ChangeSettingButtonColor(otherButton, false);
        ChangeSettingButtonColor(languageButton, false);
        CreateGraphic(graphicTransform.gameObject);

        graphicButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            graphicTransform.gameObject.SetActive(true);
            soundTransform.gameObject.SetActive(false);
            otherTransform.gameObject.SetActive(false);
            languageTransform.gameObject.SetActive(false);

            ChangeSettingButtonColor(graphicButton, true);
            ChangeSettingButtonColor(soundButton, false);
            ChangeSettingButtonColor(otherButton, false);
            ChangeSettingButtonColor(languageButton, false);

            CreateGraphic(graphicTransform.gameObject);
        });

        soundButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            graphicTransform.gameObject.SetActive(false);
            soundTransform.gameObject.SetActive(true);
            otherTransform.gameObject.SetActive(false);
            languageTransform.gameObject.SetActive(false);

            ChangeSettingButtonColor(graphicButton, false);
            ChangeSettingButtonColor(soundButton, true);
            ChangeSettingButtonColor(otherButton, false);
            ChangeSettingButtonColor(languageButton, false);

            CreateSound(soundTransform.gameObject);
        });

        otherButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            graphicTransform.gameObject.SetActive(false);
            soundTransform.gameObject.SetActive(false);
            otherTransform.gameObject.SetActive(true);
            languageTransform.gameObject.SetActive(false);

            ChangeSettingButtonColor(graphicButton, false);
            ChangeSettingButtonColor(soundButton, false);
            ChangeSettingButtonColor(otherButton, true);
            ChangeSettingButtonColor(languageButton, false);
        });

        languageButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            graphicTransform.gameObject.SetActive(false);
            soundTransform.gameObject.SetActive(false);
            otherTransform.gameObject.SetActive(false);
            languageTransform.gameObject.SetActive(true);

            ChangeSettingButtonColor(graphicButton, false);
            ChangeSettingButtonColor(soundButton, false);
            ChangeSettingButtonColor(otherButton, false);
            ChangeSettingButtonColor(languageButton, true);

            CreateLanguage(languageTransform.gameObject);
        });
    }
    public void CreateNewsPanel()
    {
        GameObject newsObject = Instantiate(NewsPanelPrefab, MainPanel);
        Transform transform = newsObject.transform;
        Transform newsPanel = transform.Find("RightScrollView/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        ButtonEvent.Instance.Close(newsPanel);

        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(newsObject);
        });
    }
    public void ChangeSettingButtonColor(Button button, bool status = true)
    {
        Transform transform = button.transform;
        TextMeshProUGUI iconText = transform.Find("IconText").GetComponent<TextMeshProUGUI>();
        RawImage iconImage = transform.Find("IconImage").GetComponent<RawImage>();
        Image background = transform.Find("Background").GetComponent<Image>();
        Image frameImage = transform.Find("FrameImage").GetComponent<Image>();

        if (status)
        {
            iconText.color = Color.black;
            iconImage.color = Color.black;
            background.color = Color.white;
            frameImage.gameObject.SetActive(true);
        }
        else
        {
            iconText.color = Color.white;
            iconImage.color = Color.white;
            background.color = Color.black;
            frameImage.gameObject.SetActive(false);
        }
    }
    public void CreateGraphic(GameObject graphicObject)
    {
        Transform transform = graphicObject.transform;
        Transform resolutionTransform = transform.Find("Scroll View/Viewport/Content/Resolution");
        Transform textureTransform = transform.Find("Scroll View/Viewport/Content/Texture");
        Transform damageFlytextTransform = transform.Find("Scroll View/Viewport/Content/DamageFlytext");
        Transform inGameCinematicTransform = transform.Find("Scroll View/Viewport/Content/InGameCinematic");

        CreateResolution(resolutionTransform.gameObject);
        CreateTexture(textureTransform.gameObject);
    }
    public void CreateSound(GameObject soundObject)
    {
        Transform transform = soundObject.transform;
        Button defaultButton = transform.Find("Default/DefaultButton").GetComponent<Button>();
        Slider musicSlider = transform.Find("Scroll View/Viewport/Content/MusicSound/Slider").GetComponent<Slider>();
        Slider sfxSlider = transform.Find("Scroll View/Viewport/Content/SFXSound/Slider").GetComponent<Slider>();
        Slider voiceSlider = transform.Find("Scroll View/Viewport/Content/VoiceSound/Slider").GetComponent<Slider>();

        TextMeshProUGUI musicQuantityText = transform.Find("Scroll View/Viewport/Content/MusicSound/QuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI sfxQuantityText = transform.Find("Scroll View/Viewport/Content/SFXSound/QuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI voiceQuantityText = transform.Find("Scroll View/Viewport/Content/VoiceSound/QuantityText").GetComponent<TextMeshProUGUI>();

        int musicSound = UserSettingsManager.Instance.GetInt(AppConstants.Setting.MUSIC);
        int sfxSound = UserSettingsManager.Instance.GetInt(AppConstants.Setting.SFX);
        int voiceSound = UserSettingsManager.Instance.GetInt(AppConstants.Setting.VOICE);

        musicQuantityText.text = musicSound.ToString();
        sfxQuantityText.text = sfxSound.ToString();
        voiceQuantityText.text = voiceSound.ToString();

        musicSlider.value = musicSound / 100f;
        sfxSlider.value = sfxSound / 100f;
        voiceSlider.value = voiceSound / 100f;

        AudioManager.Instance.SetMusicVolume(musicSlider.value);
        AudioManager.Instance.SetSfxVolume(sfxSlider.value);
        AudioManager.Instance.SetVoiceVolume(voiceSlider.value);

        defaultButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            musicSlider.value = 1f;
            sfxSlider.value = 1f;
            voiceSlider.value = 1f;

            string maxValue = "100";
            musicQuantityText.text = maxValue.ToString();
            sfxQuantityText.text = maxValue.ToString();
            voiceQuantityText.text = maxValue.ToString();

            UserSettingsManager.Instance.SetInt(AppConstants.Setting.MUSIC, 100);
            UserSettingsManager.Instance.SetInt(AppConstants.Setting.SFX, 100);
            UserSettingsManager.Instance.SetInt(AppConstants.Setting.VOICE, 100);

            AudioManager.Instance.SetMusicVolume(1f);
            AudioManager.Instance.SetSfxVolume(1f);
            AudioManager.Instance.SetVoiceVolume(1f);

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.MUSIC,
                SettingValue = maxValue.ToString(),
            });

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.SFX,
                SettingValue = maxValue.ToString(),
            });

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.VOICE,
                SettingValue = maxValue.ToString(),
            });
        });

        musicSlider.onValueChanged.AddListener(async value =>
        {
            AudioManager.Instance.SetMusicVolume(value);
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            float rounded = Mathf.Round(value * 100f) / 100f;
            int v = Mathf.RoundToInt(rounded * 100);
            musicQuantityText.text = v.ToString();
            UserSettingsManager.Instance.SetInt(AppConstants.Setting.MUSIC, v);
            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.MUSIC,
                SettingValue = v.ToString(),
            });
        });

        sfxSlider.onValueChanged.AddListener(async value =>
        {
            AudioManager.Instance.SetSfxVolume(value);
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            float rounded = Mathf.Round(value * 100f) / 100f;
            int v = Mathf.RoundToInt(rounded * 100);
            sfxQuantityText.text = v.ToString();
            UserSettingsManager.Instance.SetInt(AppConstants.Setting.SFX, v);
            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.SFX,
                SettingValue = v.ToString(),
            });
        });

        voiceSlider.onValueChanged.AddListener(async value =>
        {
            AudioManager.Instance.SetVoiceVolume(value);
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            float rounded = Mathf.Round(value * 100f) / 100f;
            int v = Mathf.RoundToInt(rounded * 100);
            voiceQuantityText.text = v.ToString();
            UserSettingsManager.Instance.SetInt(AppConstants.Setting.VOICE, v);
            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.VOICE,
                SettingValue = v.ToString(),
            });
        });
    }
    public void CreateOther()
    {

    }
    public void CreateLanguage(GameObject languageObject)
    {
        Button defaultButton = languageObject.transform.Find("Default/DefaultButton").GetComponent<Button>();
        Transform languageTransform = languageObject.transform.Find("Scroll View/Viewport/Content");
        ButtonEvent.Instance.Close(languageTransform);
        GameObject englishObject = Instantiate(LanguageButtonPrefab, languageTransform);
        GameObject vietnameseObject = Instantiate(LanguageButtonPrefab, languageTransform);

        TextMeshProUGUI englishText = englishObject.transform.Find("IconText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI vietnameseText = vietnameseObject.transform.Find("IconText").GetComponent<TextMeshProUGUI>();

        englishText.text = "English";
        vietnameseText.text = "Vietnamese";

        Button englishButton = englishObject.GetComponent<Button>();
        Button vietnameseButton = vietnameseObject.GetComponent<Button>();

        string language = UserSettingsManager.Instance.GetString(AppConstants.Setting.LANGUAGE);
        if (language.Equals("en"))
        {
            ChangeLanguageButtonColor(englishButton, true);
            ChangeLanguageButtonColor(vietnameseButton, false);
        }
        else if (language.Equals("vi"))
        {
            ChangeLanguageButtonColor(englishButton, false);
            ChangeLanguageButtonColor(vietnameseButton, true);
        }

        defaultButton.onClick.RemoveAllListeners();
        englishButton.onClick.RemoveAllListeners();
        vietnameseButton.onClick.RemoveAllListeners();

        defaultButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.LANGUAGE,
                SettingValue = "en",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.LANGUAGE, "en");

            ChangeLanguageButtonColor(englishButton, true);
            ChangeLanguageButtonColor(vietnameseButton, false);
        });

        englishButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.LANGUAGE,
                SettingValue = "en",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.LANGUAGE, "en");

            ChangeLanguageButtonColor(englishButton, true);
            ChangeLanguageButtonColor(vietnameseButton, false);
        });

        vietnameseButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.LANGUAGE,
                SettingValue = "vi",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.LANGUAGE, "vi");

            ChangeLanguageButtonColor(englishButton, false);
            ChangeLanguageButtonColor(vietnameseButton, true);
        });
    }
    public void ChangeLanguageButtonColor(Button button, bool status = true)
    {
        TextMeshProUGUI iconText = button.transform.Find("IconText").GetComponent<TextMeshProUGUI>();
        Image background = button.transform.Find("Background").GetComponent<Image>();

        if (status)
        {
            iconText.color = Color.black;
            background.color = ColorHelper.HexToColor("#FFB300");
        }
        else
        {
            iconText.color = Color.white;
            background.color = Color.gray;
        }
    }
    public void CreateResolution(GameObject resolutionObject)
    {
        Button veryLowButton = resolutionObject.transform.Find("ButtonGroup/VeryLowButton").GetComponent<Button>();
        Button lowButton = resolutionObject.transform.Find("ButtonGroup/LowButton").GetComponent<Button>();
        Button mediumButton = resolutionObject.transform.Find("ButtonGroup/MediumButton").GetComponent<Button>();
        Button highButton = resolutionObject.transform.Find("ButtonGroup/HighButton").GetComponent<Button>();
        Button veryHighButton = resolutionObject.transform.Find("ButtonGroup/VeryHighButton").GetComponent<Button>();

        string resolution = UserSettingsManager.Instance.GetString(AppConstants.Setting.RESOLUTION);
        if (resolution.Equals("Very Low"))
        {
            ChangeResolutionButtonColor(veryLowButton, true);
            ChangeResolutionButtonColor(lowButton, false);
            ChangeResolutionButtonColor(mediumButton, false);
            ChangeResolutionButtonColor(highButton, false);
            ChangeResolutionButtonColor(veryHighButton, false);
        }
        else if (resolution.Equals("Low"))
        {
            ChangeResolutionButtonColor(veryLowButton, false);
            ChangeResolutionButtonColor(lowButton, true);
            ChangeResolutionButtonColor(mediumButton, false);
            ChangeResolutionButtonColor(highButton, false);
            ChangeResolutionButtonColor(veryHighButton, false);
        }
        else if (resolution.Equals("Medium"))
        {
            ChangeResolutionButtonColor(veryLowButton, false);
            ChangeResolutionButtonColor(lowButton, false);
            ChangeResolutionButtonColor(mediumButton, true);
            ChangeResolutionButtonColor(highButton, false);
            ChangeResolutionButtonColor(veryHighButton, false);
        }
        else if (resolution.Equals("High"))
        {
            ChangeResolutionButtonColor(veryLowButton, false);
            ChangeResolutionButtonColor(lowButton, false);
            ChangeResolutionButtonColor(mediumButton, false);
            ChangeResolutionButtonColor(highButton, true);
            ChangeResolutionButtonColor(veryHighButton, false);
        }
        else if (resolution.Equals("Very High"))
        {
            ChangeResolutionButtonColor(veryLowButton, false);
            ChangeResolutionButtonColor(lowButton, false);
            ChangeResolutionButtonColor(mediumButton, false);
            ChangeResolutionButtonColor(highButton, false);
            ChangeResolutionButtonColor(veryHighButton, true);
        }

        veryLowButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            ChangeResolutionButtonColor(veryLowButton, true);
            ChangeResolutionButtonColor(lowButton, false);
            ChangeResolutionButtonColor(mediumButton, false);
            ChangeResolutionButtonColor(highButton, false);
            ChangeResolutionButtonColor(veryHighButton, false);

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.RESOLUTION,
                SettingValue = "Very Low",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.RESOLUTION, "Very Low");
        });

        lowButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            ChangeResolutionButtonColor(veryLowButton, false);
            ChangeResolutionButtonColor(lowButton, true);
            ChangeResolutionButtonColor(mediumButton, false);
            ChangeResolutionButtonColor(highButton, false);
            ChangeResolutionButtonColor(veryHighButton, false);

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.RESOLUTION,
                SettingValue = "Low",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.RESOLUTION, "Low");
        });

        mediumButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            ChangeResolutionButtonColor(veryLowButton, false);
            ChangeResolutionButtonColor(lowButton, false);
            ChangeResolutionButtonColor(mediumButton, true);
            ChangeResolutionButtonColor(highButton, false);
            ChangeResolutionButtonColor(veryHighButton, false);

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.RESOLUTION,
                SettingValue = "Medium",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.RESOLUTION, "Medium");
        });

        highButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            ChangeResolutionButtonColor(veryLowButton, false);
            ChangeResolutionButtonColor(lowButton, false);
            ChangeResolutionButtonColor(mediumButton, false);
            ChangeResolutionButtonColor(highButton, true);
            ChangeResolutionButtonColor(veryHighButton, false);

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.RESOLUTION,
                SettingValue = "High",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.RESOLUTION, "High");
        });

        veryHighButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            ChangeResolutionButtonColor(veryLowButton, false);
            ChangeResolutionButtonColor(lowButton, false);
            ChangeResolutionButtonColor(mediumButton, false);
            ChangeResolutionButtonColor(highButton, false);
            ChangeResolutionButtonColor(veryHighButton, true);

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.RESOLUTION,
                SettingValue = "Very High",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.RESOLUTION, "Very High");
        });
    }
    public void ChangeResolutionButtonColor(Button button, bool status = true)
    {
        Transform transform = button.transform;
        TextMeshProUGUI iconText = transform.Find("IconText").GetComponent<TextMeshProUGUI>();
        Image topImage = transform.Find("TopImage").GetComponent<Image>();
        RawImage iconImage = transform.Find("IconImage").GetComponent<RawImage>();
        Outline backgroundOutline = GetComponent<Outline>();
        Outline topImageOutline = transform.Find("TopImage").GetComponent<Outline>();

        if (status)
        {
            iconText.color = Color.black;
            topImage.color = ColorHelper.HexToColor("#FFB300");
            backgroundOutline.enabled = true;
            topImageOutline.enabled = true;
            iconImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Icon.ICON_ACTIVE_URL);
        }
        else
        {
            iconText.color = Color.white;
            topImage.color = ColorHelper.HexToColor("#646464");
            backgroundOutline.enabled = false;
            topImageOutline.enabled = false;
            iconImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Icon.ICON_UNACTIVE_URL);
        }
    }
    public void CreateTexture(GameObject textureObject)
    {
        Transform transform = textureObject.transform;
        Button lowButton = transform.Find("ButtonGroup/LowButton").GetComponent<Button>();
        Button mediumButton = transform.Find("ButtonGroup/MediumButton").GetComponent<Button>();
        Button highButton = transform.Find("ButtonGroup/HighButton").GetComponent<Button>();
        Button veryHighButton = transform.Find("ButtonGroup/VeryHighButton").GetComponent<Button>();

        string resolution = UserSettingsManager.Instance.GetString(AppConstants.Setting.TEXTURE);
        if (resolution.Equals("Low"))
        {
            ChangeTextureButtonColor(lowButton, true);
            ChangeTextureButtonColor(mediumButton, false);
            ChangeTextureButtonColor(highButton, false);
            ChangeTextureButtonColor(veryHighButton, false);
        }
        else if (resolution.Equals("Medium"))
        {
            ChangeTextureButtonColor(lowButton, false);
            ChangeTextureButtonColor(mediumButton, true);
            ChangeTextureButtonColor(highButton, false);
            ChangeTextureButtonColor(veryHighButton, false);
        }
        else if (resolution.Equals("High"))
        {
            ChangeTextureButtonColor(lowButton, false);
            ChangeTextureButtonColor(mediumButton, false);
            ChangeTextureButtonColor(highButton, true);
            ChangeTextureButtonColor(veryHighButton, false);
        }
        else if (resolution.Equals("Very High"))
        {
            ChangeTextureButtonColor(lowButton, false);
            ChangeTextureButtonColor(mediumButton, false);
            ChangeTextureButtonColor(highButton, false);
            ChangeTextureButtonColor(veryHighButton, true);
        }

        lowButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            ChangeTextureButtonColor(lowButton, true);
            ChangeTextureButtonColor(mediumButton, false);
            ChangeTextureButtonColor(highButton, false);
            ChangeTextureButtonColor(veryHighButton, false);

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.TEXTURE,
                SettingValue = "Low",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.TEXTURE, "Low");
        });

        mediumButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            ChangeTextureButtonColor(lowButton, false);
            ChangeTextureButtonColor(mediumButton, true);
            ChangeTextureButtonColor(highButton, false);
            ChangeTextureButtonColor(veryHighButton, false);

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.TEXTURE,
                SettingValue = "Medium",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.TEXTURE, "Medium");
        });

        highButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            ChangeTextureButtonColor(lowButton, false);
            ChangeTextureButtonColor(mediumButton, false);
            ChangeTextureButtonColor(highButton, true);
            ChangeTextureButtonColor(veryHighButton, false);

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.TEXTURE,
                SettingValue = "High",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.TEXTURE, "High");
        });

        veryHighButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            ChangeTextureButtonColor(lowButton, false);
            ChangeTextureButtonColor(mediumButton, false);
            ChangeTextureButtonColor(highButton, false);
            ChangeTextureButtonColor(veryHighButton, true);

            await UserSettingsService.Create().UpdateUserSettingAsync(User.CurrentUserId, new UserSettings
            {
                SettingKey = AppConstants.Setting.TEXTURE,
                SettingValue = "Very High",
            });
            UserSettingsManager.Instance.SetString(AppConstants.Setting.TEXTURE, "Very High");
        });
    }
    public void ChangeTextureButtonColor(Button button, bool status = true)
    {
        Transform transform = button.transform;
        TextMeshProUGUI iconText = transform.Find("IconText").GetComponent<TextMeshProUGUI>();
        Image background = transform.GetComponent<Image>();
        RawImage iconImage = transform.Find("IconImage").GetComponent<RawImage>();
        Outline backgroundOutline = transform.GetComponent<Outline>();

        if (status)
        {
            iconText.color = Color.black;
            background.color = ColorHelper.HexToColor("#FFB300");
            backgroundOutline.enabled = true;
            iconImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Icon.ICON_ACTIVE_URL);
        }
        else
        {
            iconText.color = Color.white;
            background.color = ColorHelper.HexToColor("#646464");
            backgroundOutline.enabled = false;
            iconImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Icon.ICON_UNACTIVE_URL);
        }
    }
}
