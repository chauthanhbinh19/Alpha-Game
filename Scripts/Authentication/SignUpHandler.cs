using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignUpHandler
{
    private GameObject currentInstance;

    private InputField usernameInput;
    private InputField emailInput;
    private InputField passwordInput;
    private InputField confirmPasswordInput;

    private Button signUpButton;
    private Button backButton;
    private Button closeButton;

    private TextMeshProUGUI errorUsernameText;
    private TextMeshProUGUI errorEmailText;
    private TextMeshProUGUI errorPasswordText;
    private TextMeshProUGUI errorConfirmPasswordText;

    public void Show(Transform parentTransform)
    {
        // 1. Lấy Prefab từ UIManager
        GameObject prefab = UIManager.Instance.Get("SignUpPanelPrefab");
        if (prefab == null)
        {
            Debug.LogError("SignUpPanelPrefab not found in UIManager!");
            return;
        }

        // 2. Instantiate Prefab lên Scene
        currentInstance = Object.Instantiate(prefab, parentTransform);
        Transform panelTransform = currentInstance.transform;

        // 3. Tìm các component con từ panelTransform của Instance
        usernameInput = panelTransform.Find("UsernameInput")?.GetComponent<InputField>();
        emailInput = panelTransform.Find("EmailInput")?.GetComponent<InputField>();
        passwordInput = panelTransform.Find("PasswordInput")?.GetComponent<InputField>();
        confirmPasswordInput = panelTransform.Find("ConfirmPasswordInput")?.GetComponent<InputField>();

        signUpButton = panelTransform.Find("Sign Up")?.GetComponent<Button>();
        backButton = panelTransform.Find("Back")?.GetComponent<Button>();
        closeButton = panelTransform.Find("CloseButton")?.GetComponent<Button>();

        errorUsernameText = panelTransform.Find("ErrorUsername")?.GetComponent<TextMeshProUGUI>();
        errorEmailText = panelTransform.Find("ErrorEmail")?.GetComponent<TextMeshProUGUI>();
        errorPasswordText = panelTransform.Find("ErrorPassword")?.GetComponent<TextMeshProUGUI>();
        errorConfirmPasswordText = panelTransform.Find("ErrorConfirmPassword")?.GetComponent<TextMeshProUGUI>();

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
        if (errorEmailText != null) errorEmailText.text = "";
        if (errorPasswordText != null) errorPasswordText.text = "";
        if (errorConfirmPasswordText != null) errorConfirmPasswordText.text = "";
    }

    private void BindEvents()
    {
        if (signUpButton != null)
        {
            var btnText = signUpButton.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null) btnText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SIGN_UP);

            signUpButton.onClick.RemoveAllListeners();
            signUpButton.onClick.AddListener(async () =>
            {
                PlayClickSound();
                await OnSignUpClick();
            });
        }

        if (backButton != null)
        {
            var btnText = backButton.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null) btnText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BACK);

            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(() =>
            {
                PlayClickSound();
                Close();
                AuthenticationManager.Instance.OpenSignInPanel();
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

    private async Task OnSignUpClick()
    {
        ClearErrors();

        string username = usernameInput != null ? usernameInput.text.Trim() : "";
        string email = emailInput != null ? emailInput.text.Trim() : "";
        string password = passwordInput != null ? passwordInput.text : "";
        string confirmPassword = confirmPasswordInput != null ? confirmPasswordInput.text : "";

        // ==========================================
        // 1. KIỂM TRA LỖI DỮ LIỆU ĐẦU VÀO (CLIENT-SIDE VALIDATION)
        // ==========================================
        bool hasError = false;

        // Kiểm tra Username rỗng
        if (string.IsNullOrEmpty(username))
        {
            if (errorUsernameText != null) 
                errorUsernameText.text = LocalizationManager.Get(MessageConstants.USERNAME_IS_EMPTY);
            hasError = true;
        }
        else if (username.Length < AppConstants.Auth.USERNAME_MIN_LENGTH || username.Length > AppConstants.Auth.USERNAME_MAX_LENGTH)
        {
            if (errorUsernameText != null)
                errorUsernameText.text = LocalizationManager.Get(MessageConstants.USERNAME_LENGTH_INVALID);
            hasError = true;
        }

        // Kiểm tra Email rỗng hoặc sai định dạng
        if (string.IsNullOrEmpty(email))
        {
            if (errorEmailText != null) 
                errorEmailText.text = LocalizationManager.Get(MessageConstants.EMAIL_IS_EMPTY);
            hasError = true;
        }
        else if (!IsValidEmail(email))
        {
            if (errorEmailText != null) 
                errorEmailText.text = LocalizationManager.Get(MessageConstants.INVALID_EMAIL_FORMAT); // Giả định key cho định dạng email
            hasError = true;
        }

        // Kiểm tra Password rỗng
        if (string.IsNullOrEmpty(password))
        {
            if (errorPasswordText != null) 
                errorPasswordText.text = LocalizationManager.Get(MessageConstants.PASSWORD_IS_EMPTY);
            hasError = true;
        }
        else if (password.Length < AppConstants.Auth.PASSWORD_MIN_LENGTH || password.Length > AppConstants.Auth.PASSWORD_MAX_LENGTH)
        {
            if (errorPasswordText != null)
                errorPasswordText.text = LocalizationManager.Get(MessageConstants.PASSWORD_LENGTH_INVALID);
            hasError = true;
        }

        // Kiểm tra Confirm Password rỗng
        if (string.IsNullOrEmpty(confirmPassword))
        {
            if (errorConfirmPasswordText != null) 
                errorConfirmPasswordText.text = LocalizationManager.Get(MessageConstants.CONFIRM_PASSWORD_IS_EMPTY);
            hasError = true;
        }
        // Kiểm tra Confirm Password không trùng khớp với Password
        else if (password != confirmPassword)
        {
            if (errorConfirmPasswordText != null) 
                errorConfirmPasswordText.text = LocalizationManager.Get(MessageConstants.PASSWORDS_DO_NOT_MATCH);
            hasError = true;
        }

        // Nếu có bất kỳ lỗi Client-side nào thì dừng lại, không gọi Backend
        if (hasError) return;

        // ==========================================
        // 2. GỌI SERVICE KHI DỮ LIỆU ĐÃ HỢP LỆ
        // ==========================================
        SetInteractable(false); // Khóa thao tác nút bấm tránh người dùng spam click

        AuthResult result = await UserService.Create().RegisterUserAsync(username, email, password);

        SetInteractable(true); // Mở lại tương tác UI

        if (result.Success)
        {
            Debug.Log("Đăng ký thành công!");
            // Chuyển sang màn hình SignIn
            AuthenticationManager.Instance.OpenSignInPanel();
            Close();
        }
        else
        {
            // Hiển thị lỗi phản hồi từ Server lên đúng trường lỗi
            if (result.ErrorField == AppConstants.MainType.USERNAME)
            {
                if (errorUsernameText != null) errorUsernameText.text = result.ErrorMessage;
            }
            else if (result.ErrorField == AppConstants.MainType.EMAIL)
            {
                if (errorEmailText != null) errorEmailText.text = result.ErrorMessage;
            }
            else if (result.ErrorField == AppConstants.MainType.PASSWORD)
            {
                if (errorPasswordText != null) errorPasswordText.text = result.ErrorMessage;
            }
            else
            {
                // Hiển thị thông báo lỗi chung vào vị trí Username
                if (errorUsernameText != null) errorUsernameText.text = result.ErrorMessage;
            }
        }
    }

    private bool IsValidEmail(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    private void SetInteractable(bool state)
    {
        if (signUpButton != null) signUpButton.interactable = state;
        if (backButton != null) backButton.interactable = state;
        if (closeButton != null) closeButton.interactable = state;
    }

    private void PlayClickSound()
    {
        AudioManager.Instance?.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
    }
}