using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AuthenticationManager : MonoBehaviour
{
    public static AuthenticationManager Instance { get; private set; }
    private InputField signInUsernameInput;
    private InputField signInPasswordInput;
    private InputField signUpUsernameInput;
    private InputField signUpPasswordInput;
    private InputField signUpConfirmPasswordInput;
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
        signInPanel = UIManager.Instance.Get("SignInPanelPrefab");
        signUpPanel = UIManager.Instance.Get("SignUpPanelPrefab");
        createNamePanel = UIManager.Instance.Get("CreateNamePanelPrefab");
        startButton = WaitingPanel.transform.Find("StartButton").GetComponent<Button>();
        createSignInButton = WaitingPanel.transform.Find("SignInButton").GetComponent<Button>();

        createSignInButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            CreateSignInPanel();
        });


        CheckLoggedIn();

    }
    public async void CheckLoggedIn()
    {
        if (AuthManager.IsLoggedIn())
        {
            string savedUserId = AuthManager.GetUserId();

            // Gọi login bằng ID
            var authResult = await UserService.Create().SignInWithoutUsernameAndPasswordAsync(savedUserId);

            // Nếu database KHÔNG TÌM THẤY user → authResult = null
            if (!authResult.Success)
            {
                Debug.LogWarning("Saved user ID is invalid. Logging out...");

                AuthManager.Logout();  // Xóa userId lưu trong máy

                // Tạo lại SignIn panel
                Transform existingSignInPanel = WaitingPanel.Find("SignInPanel(Clone)");
                if (existingSignInPanel == null)
                {
                    CreateSignInPanel();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(authResult.User.Name))
                {
                    CreateCreateNamePanel(User.SavedUsername, User.SavedPassword);
                    // signInPanel.SetActive(false);
                }
            }

            // Nếu user hợp lệ → cho vào game
            startButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                AudioManager.Instance.PlayMusic(AudioConstants.Music.FANTASY_AMBIENT);
                MainMenuManager.Instance.CreateMainPanel();
                MainMenuManager.Instance.CreateMainPanelUserInformation(authResult);
                FindAnyObjectByType<LoadingManager>().Loading(WaitingPanel, RootPanel);
            });
        }
        else
        {
            Transform existingSignInPanel = WaitingPanel.Find("SignInPanel(Clone)");
            if (existingSignInPanel == null)
            {
                CreateSignInPanel();
            }
        }
    }
    public void SI_signUpButtonClicked()
    {
        Destroy(currentObject);
        CreateSignUpPanel();
    }
    public async Task SI_signInButtonClicked()
    {
        string username = signInUsernameInput.text;
        string password = signInPasswordInput.text;
        IUserRepository _userRepository = new UserRepository();
        UserService _userService = new UserService(_userRepository);
        authResult = await _userService.SignInWithUsernameAndPasswordAsync(username, password);

        if (authResult.Success)
        {
            Destroy(currentObject);
            if (string.IsNullOrWhiteSpace(authResult.User.Name))
            {
                CreateCreateNamePanel(username, password);
                // signInPanel.SetActive(false);
            }
            CheckLoggedIn();
            await PowerManagerService.Create().UpdateUserStatsAsync(User.CurrentUserId);
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
    public async Task SU_signUpButtonClicked()
    {
        string username = signUpUsernameInput.text;
        string password = signUpPasswordInput.text;
        string confirmPassword = signUpConfirmPasswordInput.text;

        SU_ErrorUsername.text = "";
        SU_ErrorPassword.text = "";
        SU_ErrorConfirmPassword.text = "";

        if (string.IsNullOrEmpty(username))
        {
            SU_ErrorUsername.text = MessageConstants.USERNAME_IS_EMPTY;
            return;
        }
        if (string.IsNullOrEmpty(password))
        {
            SU_ErrorPassword.text = MessageConstants.PASSWORD_IS_EMPTY;
            return;
        }
        if (string.IsNullOrEmpty(confirmPassword))
        {
            SU_ErrorConfirmPassword.text = MessageConstants.CONFIRM_PASSWORD_IS_EMPTY;
            return;
        }

        if (password != confirmPassword)
        {
            SU_ErrorPassword.text = MessageConstants.PASSWORDS_DO_NOT_MATCH;
            return;
        }

        IUserRepository _userRepository = new UserRepository();
        UserService _userService = new UserService(_userRepository);
        string registerStatus = await _userService.RegisterUserAsync(username, password);
        if (string.IsNullOrEmpty(registerStatus))
        {
            SU_ErrorUsername.text = MessageConstants.USERNAME_ALREADY_EXIST;
        }
        else
        {
            signUpUsernameInput.text = "";
            signUpPasswordInput.text = "";
            signUpConfirmPasswordInput.text = "";
            SU_signInButtonClicked();
        }
    }
    public void SU_signInButtonClicked()
    {
        Destroy(currentObject);
        CreateSignInPanel();
    }
    public void CreateSignInPanel()
    {
        currentObject = Instantiate(signInPanel, WaitingPanel);
        signInUsernameInput = currentObject.transform.Find("UsernameInput").GetComponent<InputField>();
        signInPasswordInput = currentObject.transform.Find("PasswordInput").GetComponent<InputField>();
        SI_signInButton = currentObject.transform.Find("Sign In").GetComponent<Button>();
        SI_signUpButton = currentObject.transform.Find("Sign Up").GetComponent<Button>();
        SI_ErrorUsername = currentObject.transform.Find("ErrorUsername").GetComponent<Text>();
        SI_ErrorPassword = currentObject.transform.Find("ErrorPassword").GetComponent<Text>();
        SI_signUpButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            SI_signUpButtonClicked();
        });
        SI_signInButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await SI_signInButtonClicked();
        });
        SI_closeButton = currentObject.transform.Find("CloseButton").GetComponent<Button>();
        SI_closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });

        TextMeshProUGUI signInButtonText = SI_signInButton.GetComponentInChildren<TextMeshProUGUI>();
        signInButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SIGN_IN);
        TextMeshProUGUI signUpButtonText = SI_signUpButton.GetComponentInChildren<TextMeshProUGUI>();
        signUpButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SIGN_UP);
    }
    public void CreateSignUpPanel()
    {
        currentObject = Instantiate(signUpPanel, WaitingPanel);
        signUpUsernameInput = currentObject.transform.Find("UsernameInput").GetComponent<InputField>();
        signUpPasswordInput = currentObject.transform.Find("PasswordInput").GetComponent<InputField>();
        signUpConfirmPasswordInput = currentObject.transform.Find("ConfirmPasswordInput").GetComponent<InputField>();
        SU_signInButton = currentObject.transform.Find("Back").GetComponent<Button>();
        SU_signUpButton = currentObject.transform.Find("Sign Up").GetComponent<Button>();
        SU_ErrorUsername = currentObject.transform.Find("ErrorUsername").GetComponent<Text>();
        SU_ErrorPassword = currentObject.transform.Find("ErrorPassword").GetComponent<Text>();
        SU_ErrorConfirmPassword = currentObject.transform.Find("ErrorConfirmPassword").GetComponent<Text>();
        SU_signUpButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            await SU_signUpButtonClicked();
        });
        SU_signInButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            SU_signInButtonClicked();
        });
        SU_closeButton = signUpPanel.transform.Find("CloseButton").GetComponent<Button>();
        SU_closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });

        TextMeshProUGUI signInButtonText = SI_signInButton.GetComponentInChildren<TextMeshProUGUI>();
        signInButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BACK);
        TextMeshProUGUI signUpButtonText = SI_signUpButton.GetComponentInChildren<TextMeshProUGUI>();
        signUpButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SIGN_UP);
    }
    public void CreateCreateNamePanel(string username, string password)
    {
        Destroy(currentObject);
        currentObject = Instantiate(createNamePanel, WaitingPanel);
        TMP_InputField nameInput = currentObject.transform.Find("NameInput").GetComponent<TMP_InputField>();
        Button startButton = currentObject.transform.Find("Start").GetComponent<Button>();
        TextMeshProUGUI errorText = currentObject.transform.Find("ErrorText").GetComponent<TextMeshProUGUI>();

        startButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            if (string.IsNullOrWhiteSpace(nameInput.text))
            {
                errorText.text = "Please enter your name.";
                return;
            }
            else
            {
                var isNameExisted = await UserService.Create().CheckNameExistsAsync(nameInput.text);
                if (!isNameExisted)
                {
                    await UserService.Create().UpdateUserNameAsync(User.CurrentUserId, nameInput.text);
                    authResult = await UserService.Create().SignInWithUsernameAndPasswordAsync(username, password);

                    AudioManager.Instance.PlayMusic(AudioConstants.Music.FANTASY_AMBIENT);
                    MainMenuManager.Instance.CreateMainPanel();
                    MainMenuManager.Instance.CreateMainPanelUserInformation(authResult);
                    FindAnyObjectByType<LoadingManager>().Loading(WaitingPanel, RootPanel);

                    Destroy(currentObject);
                }
            }
        });
    }
}
