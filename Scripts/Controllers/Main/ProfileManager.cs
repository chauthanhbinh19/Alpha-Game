using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private Transform WaitingPanel;
    private Transform MainPanel;
    private Transform RootPanel;
    private Button CloseButton;
    private TextMeshProUGUI titleText;
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
        ProfilePanelPrefab = UIManager.Instance.GetProfilePanel("ProfilePanelPrefab");
        EditNamePanelPrefab = UIManager.Instance.GetEditNamePanel("EditNamePanelPrefab");
        CurrencyPanelPrefab = UIManager.Instance.GetCurrencyPanel("CurrencyPanelPrefab");
        SettingPanelPrefab = UIManager.Instance.GetSettingPanel("SettingPanelPrefab");
        SettingButtonPrefab = UIManager.Instance.GetSettingPanel("SettingButtonPrefab");
        LanguageButtonPrefab = UIManager.Instance.GetLanguagePanel("LanguageButtonPrefab");
    }
    public void CreateProfile()
    {
        profileObject = Instantiate(ProfilePanelPrefab, MainPanel);
        Transform profileTransform = profileObject.transform.Find("Scroll View/Viewport/Content");
        // titleText = profileObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        CloseButton = profileObject.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(profileObject);
        });
        // HomeButton = profileObject.transform.Find("HomeButton").GetComponent<Button>();
        // HomeButton.onClick.AddListener(() =>
        // {
        //     AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        //     ButtonEvent.Instance.Close(MainPanel);
        // });

        // titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.FEATURE);
        // ButtonLoader.Instance.CreateFeatureButton(profileTransform);
        RawImage avatarImage = profileObject.transform.Find("Group2/AvatarImage").GetComponent<RawImage>();
        RawImage borderImage = profileObject.transform.Find("Group2/BorderImage").GetComponent<RawImage>();
        TextMeshProUGUI nameText = profileObject.transform.Find("Group2/NameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI levelText = profileObject.transform.Find("Group2/LevelText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI powerText = profileObject.transform.Find("Group2/PowerText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI idText = profileObject.transform.Find("Group2/IdText").GetComponent<TextMeshProUGUI>();
        Button editNameButton = profileObject.transform.Find("Group2/EditNameButton").GetComponent<Button>();
        Button moreCurrencyButton = profileObject.transform.Find("Group4/MoreButton").GetComponent<Button>();

        avatarImage.texture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(User.CurrentUserAvatar));
        borderImage.texture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(User.CurrentUserBorder));
        nameText.text = User.CurrentUserName;
        levelText.text = User.CurrentUserLevel.ToString();
        powerText.text = User.CurrentUserPower.ToString();
        idText.text = User.CurrentUserId.ToString();

        var currencies = UserCurrencyService.Create().GetUserCurrency(User.CurrentUserId);
        var silver = currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.SILVER);
        var gold = currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.GOLD);
        var diamond = currencies.FirstOrDefault(c => c.Name == AppConstants.Currency.DIAMOND);

        RawImage silverImage = profileObject.transform.Find("Group4/CurrencyGroup/Silver/CurrencyImage").GetComponent<RawImage>();
        RawImage goldImage = profileObject.transform.Find("Group4/CurrencyGroup/Gold/CurrencyImage").GetComponent<RawImage>();
        RawImage diamondImage = profileObject.transform.Find("Group4/CurrencyGroup/Diamond/CurrencyImage").GetComponent<RawImage>();
        TextMeshProUGUI silverText = profileObject.transform.Find("Group4/CurrencyGroup/Silver/CurrencyText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI goldText = profileObject.transform.Find("Group4/CurrencyGroup/Gold/CurrencyText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI diamondText = profileObject.transform.Find("Group4/CurrencyGroup/Diamond/CurrencyText").GetComponent<TextMeshProUGUI>();

        silverImage.texture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(silver.Image));
        goldImage.texture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(gold.Image));
        diamondImage.texture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(diamond.Image));
        silverText.text = silver.Quantity.ToString();
        goldText.text = gold.Quantity.ToString();
        diamondText.text = diamond.Quantity.ToString();

        editNameButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            CreateEditNamePanel();
        });
        moreCurrencyButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            CreateCurrencyPanel();
        });

        Button logoutButton = profileObject.transform.Find("Group1/LogoutButton").GetComponent<Button>();
        Button settingButton = profileObject.transform.Find("Group1/SettingButton").GetComponent<Button>();
        Button mailButton = profileObject.transform.Find("Group1/MailButton").GetComponent<Button>();
        Button newsButton = profileObject.transform.Find("Group1/NewsButton").GetComponent<Button>();
        Button Photo = profileObject.transform.Find("Group1/PhotoButton").GetComponent<Button>();
        Button FriendButton = profileObject.transform.Find("Group1/FriendButton").GetComponent<Button>();

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
    }
    public void CreateEditNamePanel()
    {
        editNamePanelObject = Instantiate(EditNamePanelPrefab, MainPanel);
        Button closeButton = editNamePanelObject.transform.Find("CloseButton").GetComponent<Button>();
        Button saveButton = editNamePanelObject.transform.Find("SaveButton").GetComponent<Button>();
        TMP_InputField nameText = editNamePanelObject.transform.Find("NameText").GetComponent<TMP_InputField>();
        Transform warningTransform = editNamePanelObject.transform.Find("Warning");
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(editNamePanelObject);
        });
        saveButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var name = nameText.text;
            var isNameExisted = UserService.Create().CheckNameExists(name);
            if (!isNameExisted)
            {
                warningTransform.gameObject.SetActive(false);
                UserService.Create().UpdateUserName(User.CurrentUserId, name);
                Destroy(editNamePanelObject);
                Destroy(profileObject);
                CreateProfile();
            }
            else
            {
                warningTransform.gameObject.SetActive(true);
            }
        });
    }
    public void CreateCurrencyPanel()
    {
        GameObject currencyObject = Instantiate(CurrencyPanelPrefab, MainPanel);
        Button closeButton = currencyObject.transform.Find("CloseButton").GetComponent<Button>();
        Transform currencyPanel = currencyObject.transform.Find("Scroll View/Viewport/Content");
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currencyObject);
        });

        var currencies = UserCurrencyService.Create().GetUserCurrency(User.CurrentUserId);
        CurrencyManager.Instance.createCurrency(currencies, currencyPanel);
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
        Transform settingPanel = settingObject.transform.Find("RightScrollView/Viewport/Content");
        Button closeButton = settingObject.transform.Find("CloseButton").GetComponent<Button>();
        ButtonEvent.Instance.Close(settingPanel);
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
        graphicImage.texture = Resources.Load<Texture>($"UI/Icon/photo");
        RawImage soundImage = soundObject.transform.Find("IconImage").GetComponent<RawImage>();
        soundImage.texture = Resources.Load<Texture>($"UI/Icon/speaker-filled-audio-tool");
        RawImage otherImage = otherObject.transform.Find("IconImage").GetComponent<RawImage>();
        otherImage.texture = Resources.Load<Texture>($"UI/Icon/setting-1");
        RawImage languageImage = languageObject.transform.Find("IconImage").GetComponent<RawImage>();
        languageImage.texture = Resources.Load<Texture>($"UI/Icon/translation");

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
    public void ChangeSettingButtonColor(Button button, bool status = true)
    {
        TextMeshProUGUI iconText = button.transform.Find("IconText").GetComponent<TextMeshProUGUI>();
        RawImage iconImage = button.transform.Find("IconImage").GetComponent<RawImage>();
        Image background = button.transform.Find("Background").GetComponent<Image>();
        Image frameImage = button.transform.Find("FrameImage").GetComponent<Image>();

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
    public void CreateGraphic(GameObject settingObject)
    {

    }
    public void CreateSound(GameObject soundObject)
    {
        Button defaultButton = soundObject.transform.Find("Default/DefaultButton").GetComponent<Button>();
        Slider musicSlider = soundObject.transform.Find("Scroll View/Viewport/Content/MusicSound/Slider").GetComponent<Slider>();
        Slider sfxSlider = soundObject.transform.Find("Scroll View/Viewport/Content/SFXSound/Slider").GetComponent<Slider>();
        Slider voiceSlider = soundObject.transform.Find("Scroll View/Viewport/Content/VoiceSound/Slider").GetComponent<Slider>();

        TextMeshProUGUI musicQuantityText = soundObject.transform.Find("Scroll View/Viewport/Content/MusicSound/QuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI sfxQuantityText = soundObject.transform.Find("Scroll View/Viewport/Content/SFXSound/QuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI voiceQuantityText = soundObject.transform.Find("Scroll View/Viewport/Content/VoiceSound/QuantityText").GetComponent<TextMeshProUGUI>();

        int musicSound = UserSettingsManager.Instance.GetInt("Sound.Music");
        int sfxSound = UserSettingsManager.Instance.GetInt("Sound.SFX");
        int voiceSound = UserSettingsManager.Instance.GetInt("Sound.Voice");

        musicQuantityText.text = musicSound.ToString();
        sfxQuantityText.text = sfxSound.ToString();
        voiceQuantityText.text = voiceSound.ToString();

        musicSlider.value = musicSound / 100f;
        sfxSlider.value = sfxSound / 100f;
        voiceSlider.value = voiceSound / 100f;

        defaultButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            musicSlider.value = 1f;
            sfxSlider.value = 1f;
            voiceSlider.value = 1f;

            string maxValue = "100";
            musicQuantityText.text = maxValue.ToString();
            sfxQuantityText.text = maxValue.ToString();
            voiceQuantityText.text = maxValue.ToString();

            UserSettingsService.Create().UpdateUserSettings(User.CurrentUserId, new UserSettings
            {
                SettingKey = "Sound.Music",
                SettingValue = maxValue.ToString(),
            });

            UserSettingsService.Create().UpdateUserSettings(User.CurrentUserId, new UserSettings
            {
                SettingKey = "Sound.SFX",
                SettingValue = maxValue.ToString(),
            });

            UserSettingsService.Create().UpdateUserSettings(User.CurrentUserId, new UserSettings
            {
                SettingKey = "Sound.Voice",
                SettingValue = maxValue.ToString(),
            });
        });

        musicSlider.onValueChanged.AddListener(value =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            float rounded = Mathf.Round(value * 100f) / 100f;
            int v = Mathf.RoundToInt(rounded * 100);
            musicQuantityText.text = v.ToString();
            UserSettingsManager.Instance.SetInt("Sound.Music", v);
            UserSettingsService.Create().UpdateUserSettings(User.CurrentUserId, new UserSettings
            {
                SettingKey = "Sound.Music",
                SettingValue = v.ToString(),
            });
        });

        sfxSlider.onValueChanged.AddListener(value =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            float rounded = Mathf.Round(value * 100f) / 100f;
            int v = Mathf.RoundToInt(rounded * 100);
            sfxQuantityText.text = v.ToString();
            UserSettingsManager.Instance.SetInt("Sound.SFX", v);
            UserSettingsService.Create().UpdateUserSettings(User.CurrentUserId, new UserSettings
            {
                SettingKey = "Sound.SFX",
                SettingValue = v.ToString(),
            });
        });

        voiceSlider.onValueChanged.AddListener(value =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            float rounded = Mathf.Round(value * 100f) / 100f;
            int v = Mathf.RoundToInt(rounded * 100);
            voiceQuantityText.text = v.ToString();
            UserSettingsManager.Instance.SetInt("Sound.Voice", v);
            UserSettingsService.Create().UpdateUserSettings(User.CurrentUserId, new UserSettings
            {
                SettingKey = "Sound.Voice",
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

        ChangeLanguageButtonColor(englishButton, true);

        defaultButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            UserSettingsService.Create().UpdateUserSettings(User.CurrentUserId, new UserSettings
            {
                SettingKey = "Language",
                SettingValue = "en",
            });

            ChangeLanguageButtonColor(englishButton, true);
            ChangeLanguageButtonColor(vietnameseButton, false);
        });

        englishButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            UserSettingsService.Create().UpdateUserSettings(User.CurrentUserId, new UserSettings
            {
                SettingKey = "Language",
                SettingValue = "en",
            });

            ChangeLanguageButtonColor(englishButton, true);
            ChangeLanguageButtonColor(vietnameseButton, false);
        });

        vietnameseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            UserSettingsService.Create().UpdateUserSettings(User.CurrentUserId, new UserSettings
            {
                SettingKey = "Language",
                SettingValue = "vi",
            });

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
            background.color = Color.yellow;
        }
        else
        {
            iconText.color = Color.white;
            background.color = Color.gray;
        }
    }
    public void CreateResolution()
    {

    }
}
