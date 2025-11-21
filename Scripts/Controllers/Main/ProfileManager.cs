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
    private Transform MainPanel;
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ProfilePanelPrefab = UIManager.Instance.GetProfilePanel("ProfilePanelPrefab");
        EditNamePanelPrefab = UIManager.Instance.GetEditNamePanel("EditNamePanelPrefab");
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
        Button editNameButton = profileObject.transform.Find("Group2/EditNameButton").GetComponent<Button>();

        avatarImage.texture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(User.CurrentUserAvatar));
        borderImage.texture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(User.CurrentUserBorder));
        nameText.text = User.CurrentUserName;
        levelText.text = User.CurrentUserLevel.ToString();
        powerText.text = User.CurrentUserPower.ToString();

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
    }
    public void CreateEditNamePanel()
    {
        editNamePanelObject = Instantiate(EditNamePanelPrefab, MainPanel);
        Button closeButton = editNamePanelObject.transform.Find("CloseButton").GetComponent<Button>();
        Button saveButton = editNamePanelObject.transform.Find("SaveButton").GetComponent<Button>();
        TMP_InputField  nameText = editNamePanelObject.transform.Find("NameText").GetComponent<TMP_InputField>();
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
}
