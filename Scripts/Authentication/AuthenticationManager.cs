using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Transform WaitingPanel;
    private Transform RootPanel;
    private Text SI_ErrorUsername;
    private Text SI_ErrorPassword;
    private Text SU_ErrorUsername;
    private Text SU_ErrorPassword;
    private Text SU_ErrorConfirmPassword;
    private GameObject currencyPrefab;
    GameObject currentObject;
    private AuthResult authResult;
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
        WaitingPanel = UIManager.Instance.GetTransform("WaitingPanel");
        RootPanel = UIManager.Instance.GetTransform("RootPanel");
        signInPanel = UIManager.Instance.GetGameObject("SignInPanel");
        signUpPanel = UIManager.Instance.GetGameObject("SignUpPanel");
        createNamePanel = UIManager.Instance.GetGameObject("CreateNamePanel");
        currencyPrefab = UIManager.Instance.GetGameObject("currencyPrefab");
        startButton = WaitingPanel.transform.Find("StartButton").GetComponent<Button>();
        createSignInButton = WaitingPanel.transform.Find("SignInButton").GetComponent<Button>();

        startButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            AudioManager.Instance.PlayMusic(AudioConstants.Music.FANTASY_AMBIENT);
            MainMenuManager.Instance.CreateMainPanel();
            MainMenuManager.Instance.CreateMainPanelUserInformation(authResult);
            FindAnyObjectByType<LoadingManager>().Loading(WaitingPanel, RootPanel);
        });
        createSignInButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
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
        authResult = _userService.SignInUser(username, password);

        if (authResult.Success)
        {
            Destroy(currentObject);
            if (string.IsNullOrEmpty(authResult.User.name))
            {
                AuthenticationManager.Instance.createCreateNamePanel(username, password);
                // signInPanel.SetActive(false);
            }

            PowerManagerService.Create().UpdateUserStats(User.CurrentUserId);
        }
        else
        {
            if (authResult.ErrorField.Equals(AppConstants.MainType.USERNAME))
            {
                SI_ErrorUsername.text = authResult.ErrorMessage;
                SI_ErrorPassword.text = "";
            }
            else
            {
                SI_ErrorUsername.text = "";
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
        SI_signUpButton.onClick.AddListener(()=>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            SI_signUpButtonClicked();
        });
        SI_signInButton.onClick.AddListener(()=>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            SI_signInButtonClicked();
        });
        SI_closeButton = currentObject.transform.Find("CloseButton").GetComponent<Button>();
        SI_closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(currentObject);
        });

        TextMeshProUGUI signInButtonText = SI_signInButton.GetComponentInChildren<TextMeshProUGUI>();
        signInButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SignIn);
        TextMeshProUGUI signUpButtonText = SI_signUpButton.GetComponentInChildren<TextMeshProUGUI>();
        signUpButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SignUp);
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
        SU_signUpButton.onClick.AddListener(()=>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            SU_signUpButtonClicked();
        });
        SU_signInButton.onClick.AddListener(()=>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            SU_signInButtonClicked();
        });
        SU_closeButton = signUpPanel.transform.Find("CloseButton").GetComponent<Button>();
        SU_closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(currentObject);
        });

        TextMeshProUGUI signInButtonText = SI_signInButton.GetComponentInChildren<TextMeshProUGUI>();
        signInButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.Back);
        TextMeshProUGUI signUpButtonText = SI_signUpButton.GetComponentInChildren<TextMeshProUGUI>();
        signUpButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SignUp);
    }
    public void createCreateNamePanel(string username, string password)
    {
        Destroy(currentObject);
        currentObject = Instantiate(createNamePanel, WaitingPanel);
        InputField nameInput = currentObject.transform.Find("NameInput").GetComponent<InputField>();
        Button startButton = currentObject.transform.Find("Start").GetComponent<Button>();

        startButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);

            IUserRepository _userRepository = new UserRepository();
            UserService _userService = new UserService(_userRepository);
            _userService.UpdateUserName(User.CurrentUserId, nameInput.text);
            authResult = _userService.SignInUser(username, password);
        });
    }
    public void deleteCreateNamePanel()
    {
        Destroy(currentObject);
    }
}
