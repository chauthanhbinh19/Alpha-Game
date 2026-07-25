using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignInHandler
{
    private GameObject currentInstance;

    private InputField usernameInput;
    private InputField passwordInput;

    private Button signInButton;
    private Button signUpButton;
    private Button closeButton;

    private Text errorUsernameText;
    private Text errorPasswordText;

    public void Show(Transform parentTransform)
    {
        // 1. Lấy Prefab từ UIManager
        GameObject prefab = UIManager.Instance.Get("SignInPanelPrefab");
        if (prefab == null)
        {
            Debug.LogError("SignInPanelPrefab not found in UIManager!");
            return;
        }

        // 2. Instantiate Prefab lên Scene
        currentInstance = Object.Instantiate(prefab, parentTransform);
        Transform panelTransform = currentInstance.transform;

        // 3. Tìm các component con dựa trên Transform của Instance vừa tạo
        usernameInput = panelTransform.Find("UsernameInput")?.GetComponent<InputField>();
        passwordInput = panelTransform.Find("PasswordInput")?.GetComponent<InputField>();

        signInButton = panelTransform.Find("Sign In")?.GetComponent<Button>();
        signUpButton = panelTransform.Find("Sign Up")?.GetComponent<Button>();
        closeButton = panelTransform.Find("CloseButton")?.GetComponent<Button>();

        errorUsernameText = panelTransform.Find("ErrorUsername")?.GetComponent<Text>();
        errorPasswordText = panelTransform.Find("ErrorPassword")?.GetComponent<Text>();

        ClearErrors();
        BindEvents();
    }

    public void Close()
    {
        if (currentInstance != null)
        {
            Object.Destroy(currentInstance);
        }
    }

    private void ClearErrors()
    {
        if (errorUsernameText != null) errorUsernameText.text = "";
        if (errorPasswordText != null) errorPasswordText.text = "";
    }

    private void BindEvents()
    {
        if (signInButton != null)
        {
            var btnText = signInButton.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null) btnText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SIGN_IN);

            signInButton.onClick.RemoveAllListeners();
            signInButton.onClick.AddListener(async () =>
            {
                PlayClickSound();
                await OnSignInClick();
            });
        }

        if (signUpButton != null)
        {
            var btnText = signUpButton.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null) btnText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SIGN_UP);

            signUpButton.onClick.RemoveAllListeners();
            signUpButton.onClick.AddListener(() =>
            {
                PlayClickSound();
                Close();
                AuthenticationManager.Instance.OpenSignUpPanel();
            });
        }

        if (closeButton != null)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(() =>
            {
                PlayClickSound();
                Close();
            });
        }
    }

    private async Task OnSignInClick()
    {
        ClearErrors();

        string username = usernameInput != null ? usernameInput.text.Trim() : "";
        string password = passwordInput != null ? passwordInput.text : "";

        bool isValid = true;

        if (string.IsNullOrEmpty(username))
        {
            if (errorUsernameText != null) errorUsernameText.text = MessageConstants.USERNAME_IS_EMPTY;
            isValid = false;
        }

        if (string.IsNullOrEmpty(password))
        {
            if (errorPasswordText != null) errorPasswordText.text = MessageConstants.PASSWORD_IS_EMPTY;
            isValid = false;
        }

        if (!isValid) return;

        SetInteractable(false);

        IUserRepository userRepository = new UserRepository();
        UserService userService = new UserService(userRepository);
        AuthResult authResult = await userService.SignInWithUsernameAndPasswordAsync(username, password);

        SetInteractable(true);

        if (authResult.Success)
        {
            Close();

            if (string.IsNullOrWhiteSpace(authResult.User.Name))
            {
                AuthenticationManager.Instance.OpenCreateNamePanel(username, password);
            }

            AuthenticationManager.Instance.CheckLoggedIn();
            await PowerManagerService.Create().UpdateUserStatsAsync(User.CurrentUserId);
        }
        else
        {
            if (authResult.ErrorField != null && authResult.ErrorField.Equals(AppConstants.MainType.USERNAME))
            {
                if (errorUsernameText != null) errorUsernameText.text = authResult.ErrorMessage;
            }
            else
            {
                if (errorPasswordText != null) errorPasswordText.text = authResult.ErrorMessage;
            }
        }
    }

    private void SetInteractable(bool state)
    {
        if (signInButton != null) signInButton.interactable = state;
        if (signUpButton != null) signUpButton.interactable = state;
    }

    private void PlayClickSound()
    {
        AudioManager.Instance?.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
    }
}