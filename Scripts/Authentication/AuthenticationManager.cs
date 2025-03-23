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

        startButton.onClick.AddListener(()=>{
            FindAnyObjectByType<LoadingManager>().Loading(WaitingPanel, MainPanel);
        });
        createSignInButton.onClick.AddListener(()=>{
            createSignInPanel();
        });
        if (User.CurrentUserId == 0)
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
        User user = new User(username, password, createNamePanel, signInPanel);
        User loggedInUser = user.SignInUser();
        if (loggedInUser != null)
        {
            // Đăng nhập thành công, hiển thị MainPanel và ẩn WaitingPanel
            // MainPanel.gameObject.SetActive(true);
            // WaitingPanel.gameObject.SetActive(false);
            Destroy(currentObject);
            // WaitingPanel.gameObject.SetActive(false);
            // MainPanel.gameObject.SetActive(true);

            // Lưu thông tin người dùng vào nơi bạn muốn, ví dụ lưu vào một biến static hoặc quản lý trong game
            // Ví dụ: lưu vào UserManager
            // UserManager.Instance.InitializeUser(loggedInUser);
            Text nameText = userPanel.transform.Find("NameText").GetComponent<Text>();
            nameText.text = loggedInUser.name;
            Text levelText = userPanel.transform.Find("LevelText").GetComponent<Text>();
            levelText.text = loggedInUser.level.ToString();
            Text powerText = userPanel.transform.Find("PowerText").GetComponent<Text>();
            powerText.text = loggedInUser.power.ToString();
            RawImage avatarImage = userPanel.transform.Find("AvatarImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = loggedInUser.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            avatarImage.texture = texture;

            RawImage borderImage = userPanel.transform.Find("BorderImage").GetComponent<RawImage>();
            fileNameWithoutExtension = loggedInUser.border.Replace(".png", "");
            Texture borderTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            borderImage.texture = borderTexture;

            FindObjectOfType<CurrencyManager>().GetMainCurrency(loggedInUser.Currencies, currencyPanel);
            PowerManager powerManager = new PowerManager();
            powerManager.UpdateUserStats();
            // foreach (var currency in loggedInUser.Currencies)
            // {
            //     if (currency.name.Equals("Diamond") || currency.name.Equals("Gold") || currency.name.Equals("Silver"))
            //     {
            //         GameObject currencyObject = Instantiate(currencyPrefab, currencyPanel);
            //         RawImage currencyImage = currencyObject.transform.Find("Image").GetComponent<RawImage>();
            //         string currencyWithoutExtension = currency.image.Replace(".png", "");
            //         Texture currencyTexture = Resources.Load<Texture>($"{currencyWithoutExtension}");
            //         currencyImage.texture = currencyTexture;

            //         Text currencyQuantity = currencyObject.transform.Find("Content").GetComponent<Text>();
            //         currencyQuantity.text = currency.quantity.ToString();
            //     }
            // }
        }
        else
        {
            // Đăng nhập thất bại, hiển thị thông báo lỗi
            SI_ErrorUsername.text = "Your account does not exist";
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
            SU_ErrorUsername.text = "Username can not be empty!";
            return;
        }
        if (string.IsNullOrEmpty(password))
        {
            SU_ErrorPassword.text = "Password can not be empty!";
            return;
        }
        if (string.IsNullOrEmpty(confirmPassword))
        {
            SU_ErrorConfirmPassword.text = "Confirm password can not be empty!";
            return;
        }

        if (password != confirmPassword)
        {
            SU_ErrorPassword.text = "Passwords do not match!";
            return;
        }

        User newUser = new User(username, password);
        int registerStatus = newUser.RegisterUser();
        if (registerStatus == 0)
        {
            SU_ErrorUsername.text = "Account already exists!";
        }
        else if (registerStatus == 1)
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
        SI_closeButton.onClick.AddListener(()=>{
            Destroy(currentObject);
        });
    }
    public void createSignUpPanel(){
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
        SU_closeButton.onClick.AddListener(()=>{
            Destroy(currentObject);
        });
    }
    public void createCreateNamePanel(){
        Destroy(currentObject);
        currentObject = Instantiate(createNamePanel, WaitingPanel);
        InputField nameInput = currentObject.transform.Find("NameInput").GetComponent<InputField>();
        Button startButton = currentObject.transform.Find("Start").GetComponent<Button>();

        startButton.onClick.AddListener(()=>{
            User user = new User();
            user.UpdateUserName(nameInput.text);
            User loggedInUser = user.SignInUser();
            Text nameText = userPanel.transform.Find("NameText").GetComponent<Text>();
            nameText.text = loggedInUser.name;
            Text levelText = userPanel.transform.Find("LevelText").GetComponent<Text>();
            levelText.text = loggedInUser.level.ToString();
            Text powerText = userPanel.transform.Find("PowerText").GetComponent<Text>();
            powerText.text = loggedInUser.power.ToString();
            RawImage avatarImage = userPanel.transform.Find("AvatarImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = loggedInUser.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            avatarImage.texture = texture;

            RawImage borderImage = userPanel.transform.Find("BorderImage").GetComponent<RawImage>();
            fileNameWithoutExtension = loggedInUser.border.Replace(".png", "");
            Texture borderTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            borderImage.texture = borderTexture;
            FindObjectOfType<CurrencyManager>().GetMainCurrency(loggedInUser.Currencies, currencyPanel);
        });
    }
    public void deleteCreateNamePanel(){
        Destroy(currentObject);
    }
}
