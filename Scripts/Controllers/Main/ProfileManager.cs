using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance { get; private set; }
    private GameObject ProfilePanelPrefab;
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
        
    }

    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ProfilePanelPrefab = UIManager.Instance.GetGameObject("ProfilePanelPrefab");
    }
    public void CreateProfile()
    {
        GameObject profileObject = Instantiate(ProfilePanelPrefab, MainPanel);
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
    }
}
