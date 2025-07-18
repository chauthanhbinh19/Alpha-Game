using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AuthenticationManager : MonoBehaviour
{
    public static AuthenticationManager Instance { get; private set; }
    private InputField SI_usernameInput;
    private InputField SI_passwordInput;
    private InputField SU_usernameInput;
    private InputField SU_passwordInput;
    private InputField SU_confirmPasswordInput;
    private Button SI_signInButton;
    private Button SI_signUpButton;
    private Button SU_signInButton;
    private Button SU_signUpButton;
    private Button startButton;
    private Button SI_closeButton;
    private Button SU_closeButton;
    private Button createSignInButton;
    private GameObject signInPanel;
    private GameObject signUpPanel;
    private GameObject createNamePanel;
    private Transform MainPanel;
    private Transform WaitingPanel;
    private Text SI_ErrorUsername;
    private Text SI_ErrorPassword;
    private Text SU_ErrorUsername;
    private Text SU_ErrorPassword;
    private Text SU_ErrorConfirmPassword;
    private Transform userPanel;
    private Transform currencyPanel;
    private GameObject currencyPrefab;
    GameObject currentObject;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        MainPanel = UIManager.Instance.GetTransform("MainScencePanel");
        WaitingPanel = UIManager.Instance.GetTransform("WaitingPanel");
        signInPanel = UIManager.Instance.GetGameObject("SignInPanel");
        signUpPanel = UIManager.Instance.GetGameObject("SignUpPanel");
        createNamePanel = UIManager.Instance.GetGameObject("CreateNamePanel");
        currencyPanel = UIManager.Instance.GetTransform("currencyPanel");
        currencyPrefab = UIManager.Instance.GetGameObject("currencyPrefab");
        userPanel = UIManager.Instance.GetTransform("userPanel");
        startButton = WaitingPanel.transform.Find("StartButton").GetComponent<Button>();
        createSignInButton = WaitingPanel.transform.Find("SignInButton").GetComponent<Button>();

        startButton.onClick.AddListener(() =>
        {
            FindAnyObjectByType<LoadingManager>().Loading(WaitingPanel, MainPanel);
        });
        createSignInButton.onClick.AddListener(() =>
        {
            createSignInPanel();
        });
        if (string.IsNullOrEmpty(User.CurrentUserId))
        {
            // Kiểm tra xem SignInPanel đã tồn tại chưa
            Transform existingSignInPanel = WaitingPanel.Find("SignInPanel(Clone)");
            if (existingSignInPanel == null)
            {
                createSignInPanel();
            }
        }
    }
    public void SI_signUpButtonClicked()
    {
        Destroy(currentObject);
        createSignUpPanel();
    }
    public void SI_signInButtonClicked()
    {
        string username = SI_usernameInput.text;
        string password = SI_passwordInput.text;
        IUserRepository _userRepository = new UserRepository();
        UserService _userService = new UserService(_userRepository);
        var authResult = _userService.SignInUser(username, password);

        if (authResult.Success)
        {
            Destroy(currentObject);
            if (string.IsNullOrEmpty(authResult.User.name))
            {
                AuthenticationManager.Instance.createCreateNamePanel(username, password);
                // signInPanel.SetActive(false);
            }
            Text nameText = userPanel.transform.Find("NameText").GetComponent<Text>();
            nameText.text = authResult.User.name;
            Text levelText = userPanel.transform.Find("LevelText").GetComponent<Text>();
            levelText.text = authResult.User.level.ToString();
            Text powerText = userPanel.transform.Find("PowerText").GetComponent<Text>();
            powerText.text = authResult.User.power.ToString();
            RawImage avatarImage = userPanel.transform.Find("AvatarImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = authResult.User.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            avatarImage.texture = texture;

            RawImage borderImage = userPanel.transform.Find("BorderImage").GetComponent<RawImage>();

            fileNameWithoutExtension = authResult.User.border.Replace(".png", "");
            Texture borderTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            borderImage.texture = borderTexture;

            FindObjectOfType<CurrencyManager>().GetMainCurrency(authResult.User.Currencies, currencyPanel);
            PowerManagerService.Create().UpdateUserStats(User.CurrentUserId);
        }
        else
        {
            if (authResult.ErrorField.Equals(AppConstants.Username))
            {
                SI_ErrorUsername.text = authResult.ErrorMessage;
            }
            else
            {
                SI_ErrorPassword.text = authResult.ErrorMessage;
            }
        }

    }
    void SU_signUpButtonClicked()
    {
        string username = SU_usernameInput.text;
        string password = SU_passwordInput.text;
        string confirmPassword = SU_confirmPasswordInput.text;

        SU_ErrorUsername.text = "";
        SU_ErrorPassword.text = "";
        SU_ErrorConfirmPassword.text = "";

        if (string.IsNullOrEmpty(username))
        {
            SU_ErrorUsername.text = MessageHelper.MessageConstants.UsernameIsEmpty;
            return;
        }
        if (string.IsNullOrEmpty(password))
        {
            SU_ErrorPassword.text = MessageHelper.MessageConstants.PasswordIsEmpty;
            return;
        }
        if (string.IsNullOrEmpty(confirmPassword))
        {
            SU_ErrorConfirmPassword.text = MessageHelper.MessageConstants.ConfirmPasswordIsEmpty;
            return;
        }

        if (password != confirmPassword)
        {
            SU_ErrorPassword.text = MessageHelper.MessageConstants.PasswordNotMatch;
            return;
        }

        IUserRepository _userRepository = new UserRepository();
        UserService _userService = new UserService(_userRepository);
        string registerStatus = _userService.RegisterUser(username, password);
        if (string.IsNullOrEmpty(registerStatus))
        {
            SU_ErrorUsername.text = MessageHelper.MessageConstants.UsernameAlreadyExist;
        }
        else
        {
            SU_usernameInput.text = "";
            SU_passwordInput.text = "";
            SU_confirmPasswordInput.text = "";
            SU_signInButtonClicked();
        }
        // Debug.Log(username +" "+password);
    }
    public void SU_signInButtonClicked()
    {
        Destroy(currentObject);
        createSignInPanel();
    }
    public void createSignInPanel()
    {
        currentObject = Instantiate(signInPanel, WaitingPanel);
        SI_usernameInput = currentObject.transform.Find("UsernameInput").GetComponent<InputField>();
        SI_passwordInput = currentObject.transform.Find("PasswordInput").GetComponent<InputField>();
        SI_signInButton = currentObject.transform.Find("Sign In").GetComponent<Button>();
        SI_signUpButton = currentObject.transform.Find("Sign Up").GetComponent<Button>();
        SI_ErrorUsername = currentObject.transform.Find("ErrorUsername").GetComponent<Text>();
        SI_ErrorPassword = currentObject.transform.Find("ErrorPassword").GetComponent<Text>();
        SI_signUpButton.onClick.AddListener(SI_signUpButtonClicked);
        SI_signInButton.onClick.AddListener(SI_signInButtonClicked);
        SI_closeButton = currentObject.transform.Find("CloseButton").GetComponent<Button>();
        SI_closeButton.onClick.AddListener(() =>
        {
            Destroy(currentObject);
        });
    }
    public void createSignUpPanel()
    {
        currentObject = Instantiate(signUpPanel, WaitingPanel);
        SU_usernameInput = currentObject.transform.Find("UsernameInput").GetComponent<InputField>();
        SU_passwordInput = currentObject.transform.Find("PasswordInput").GetComponent<InputField>();
        SU_confirmPasswordInput = currentObject.transform.Find("ConfirmPasswordInput").GetComponent<InputField>();
        SU_signInButton = currentObject.transform.Find("Back").GetComponent<Button>();
        SU_signUpButton = currentObject.transform.Find("Sign Up").GetComponent<Button>();
        SU_ErrorUsername = currentObject.transform.Find("ErrorUsername").GetComponent<Text>();
        SU_ErrorPassword = currentObject.transform.Find("ErrorPassword").GetComponent<Text>();
        SU_ErrorConfirmPassword = currentObject.transform.Find("ErrorConfirmPassword").GetComponent<Text>();
        SU_signUpButton.onClick.AddListener(SU_signUpButtonClicked);
        SU_signInButton.onClick.AddListener(SU_signInButtonClicked);
        SU_closeButton = signUpPanel.transform.Find("CloseButton").GetComponent<Button>();
        SU_closeButton.onClick.AddListener(() =>
        {
            Destroy(currentObject);
        });
    }
    public void createCreateNamePanel(string username, string password)
    {
        Destroy(currentObject);
        currentObject = Instantiate(createNamePanel, WaitingPanel);
        InputField nameInput = currentObject.transform.Find("NameInput").GetComponent<InputField>();
        Button startButton = currentObject.transform.Find("Start").GetComponent<Button>();

        startButton.onClick.AddListener(() =>
        {
            IUserRepository _userRepository = new UserRepository();
            UserService _userService = new UserService(_userRepository);
            _userService.UpdateUserName(User.CurrentUserId, nameInput.text);
            var authResult = _userService.SignInUser(username, password);
            Text nameText = userPanel.transform.Find("NameText").GetComponent<Text>();
            nameText.text = authResult.User.name;
            Text levelText = userPanel.transform.Find("LevelText").GetComponent<Text>();
            levelText.text = authResult.User.level.ToString();
            Text powerText = userPanel.transform.Find("PowerText").GetComponent<Text>();
            powerText.text = authResult.User.power.ToString();
            RawImage avatarImage = userPanel.transform.Find("AvatarImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = authResult.User.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            avatarImage.texture = texture;

            RawImage borderImage = userPanel.transform.Find("BorderImage").GetComponent<RawImage>();
            fileNameWithoutExtension = authResult.User.border.Replace(".png", "");
            Texture borderTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            borderImage.texture = borderTexture;
            FindObjectOfType<CurrencyManager>().GetMainCurrency(authResult.User.Currencies, currencyPanel);
        });
    }
    public void deleteCreateNamePanel()
    {
        Destroy(currentObject);
    }
}
