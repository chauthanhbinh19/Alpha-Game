using UnityEngine;
using UnityEngine.UI;

public class AuthenticationManager : MonoBehaviour
{
    public static AuthenticationManager Instance { get; private set; }

    private Transform waitingPanel;
    private Transform rootPanel;
    private Button startButton;
    private Button createSignInButton;

    // Khởi tạo 3 Handler xử lý 3 Panel UI
    private readonly SignInHandler signInHandler = new SignInHandler();
    private readonly SignUpHandler signUpHandler = new SignUpHandler();
    private readonly CreateNameHandler createNameHandler = new CreateNameHandler();

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

    private void Start()
    {
        waitingPanel = UIManager.Instance.GetTransform("WaitingPanel");
        rootPanel = UIManager.Instance.GetTransform("RootPanel");

        startButton = waitingPanel.Find("StartButton").GetComponent<Button>();
        createSignInButton = waitingPanel.Find("SignInButton").GetComponent<Button>();

        createSignInButton.onClick.RemoveAllListeners();
        createSignInButton.onClick.AddListener(() =>
        {
            AudioManager.Instance?.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            OpenSignInPanel();
        });

        CheckLoggedIn();
    }

    public async void CheckLoggedIn()
    {
        if (AuthManager.IsLoggedIn())
        {
            string savedUserId = AuthManager.GetUserId();

            var authResult = await UserService.Create().SignInWithoutUsernameAndPasswordAsync(savedUserId);

            if (!authResult.Success)
            {
                Debug.LogWarning("Saved user ID is invalid. Logging out...");
                AuthManager.Logout();

                if (waitingPanel.Find("SignInPanelPrefab(Clone)") == null)
                {
                    OpenSignInPanel();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(authResult.User.Name))
                {
                    OpenCreateNamePanel(User.SavedUsername, User.SavedPassword);
                }
            }

            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() =>
            {
                AudioManager.Instance?.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                // await PowerManagerService.Create().UpdateUserStatsAsync(User.CurrentUserId);
                MainMenuManager.Instance.CreateMainPanel();
                MainMenuManager.Instance.CreateMainPanelUserInformation(authResult);
                FindFirstObjectByType<LoadingSystem>()?.Loading(waitingPanel, rootPanel);
            });
        }
        else
        {
            if (waitingPanel.Find("SignInPanelPrefab(Clone)") == null)
            {
                OpenSignInPanel();
            }
        }
    }

    public void OpenSignInPanel()
    {
        CloseAllPanels();
        signInHandler.Show(waitingPanel);
    }

    public void OpenSignUpPanel()
    {
        CloseAllPanels();
        signUpHandler.Show(waitingPanel);
    }

    public void OpenCreateNamePanel(string username, string password)
    {
        CloseAllPanels();
        createNameHandler.Show(waitingPanel, username, password);
    }

    private void CloseAllPanels()
    {
        signInHandler.Close();
        signUpHandler.Close();
        createNameHandler.Close();
    }
}