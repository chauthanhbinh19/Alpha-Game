using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateNameHandler
{
    private GameObject currentInstance;

    private TMP_InputField nameInput;
    private Button startButton;
    private TextMeshProUGUI errorText;

    public void Show(Transform parentTransform, string username, string password)
    {
        // 1. Lấy Prefab từ UIManager
        GameObject prefab = UIManager.Instance.Get("CreateNamePanelPrefab");
        if (prefab == null)
        {
            Debug.LogError("CreateNamePanelPrefab not found in UIManager!");
            return;
        }

        // 2. Instantiate Prefab lên Scene
        currentInstance = Object.Instantiate(prefab, parentTransform);
        Transform panelTransform = currentInstance.transform;

        // 3. Bind UI
        nameInput = panelTransform.Find("NameInput")?.GetComponent<TMP_InputField>();
        startButton = panelTransform.Find("Start")?.GetComponent<Button>();
        errorText = panelTransform.Find("ErrorText")?.GetComponent<TextMeshProUGUI>();

        if (errorText != null) errorText.text = "";

        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(async () =>
            {
                PlayClickSound();
                await OnStartClick(username, password);
            });
        }
    }

    public void Close()
    {
        if (currentInstance != null)
        {
            Object.Destroy(currentInstance);
        }
    }

    private async Task OnStartClick(string username, string password)
    {
        if (errorText != null) errorText.text = "";

        string inputName = nameInput != null ? nameInput.text.Trim() : "";

        if (string.IsNullOrWhiteSpace(inputName))
        {
            if (errorText != null) errorText.text = "Please enter your name.";
            return;
        }

        if (startButton != null) startButton.interactable = false;

        bool isNameExisted = await UserService.Create().CheckNameExistsAsync(inputName);
        if (isNameExisted)
        {
            if (errorText != null) errorText.text = "Name already exists!";
            if (startButton != null) startButton.interactable = true;
            return;
        }

        await UserService.Create().UpdateUserNameAsync(User.CurrentUserId, inputName);
        AuthResult authResult = await UserService.Create().SignInWithUsernameAndPasswordAsync(username, password);

        await GameDataCacheConfig.Instance.LoadDataAsync();

        AudioManager.Instance?.PlayMusic(AudioConstants.Music.FANTASY_AMBIENT);
        MainMenuManager.Instance.CreateMainPanel();
        MainMenuManager.Instance.CreateMainPanelUserInformation(authResult);

        Transform waitingPanel = UIManager.Instance.GetTransform("WaitingPanel");
        Transform rootPanel = UIManager.Instance.GetTransform("RootPanel");
        Object.FindFirstObjectByType<LoadingSystem>()?.Loading(waitingPanel, rootPanel);

        Close();
    }

    private void PlayClickSound()
    {
        AudioManager.Instance?.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
    }
}