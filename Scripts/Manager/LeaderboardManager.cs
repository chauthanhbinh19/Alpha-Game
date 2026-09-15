using System.Collections;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject LeaderboardPanelPrefab;
    private GameObject MailTabButtonPrefab;
    private GameObject LeaderboardButtonPrefab;
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
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        LeaderboardPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.General.LEADERBOARD_PANEL_PREFAB);
        MailTabButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.MAIL_TAB_BUTTON_PREFAB);
        LeaderboardButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.LEADERBOARD_BUTTON_PREFAB);
    }
    public async Task CreateLeaderboardPanel()
    {
        GameObject currentObject = Instantiate(LeaderboardPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentTransform = transform.Find("Scroll View/Viewport/Content");
        Transform personalTransform = transform.Find("Personal");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        var top100User = await UserService.Create().GetTop100LeaderboardAsync();
        var userRank = await UserService.Create().GetUserRankAsync(User.CurrentUserId);

        foreach (var user in top100User)
        {
            CreateLeaderboardButton(user, contentTransform, false);
        }

        CreateLeaderboardButton(userRank, personalTransform, true);
    }
    public void CreateLeaderboardButton(UserRankDTO user, Transform contentTransform, bool isPersonal = false)
    {
        GameObject gameObject = Instantiate(LeaderboardButtonPrefab, contentTransform);
        Transform transform = gameObject.transform;

        Transform activeTransform = transform.Find("Active");
        Transform unactiveTransform = transform.Find("Unactive");

        if (isPersonal)
        {
            activeTransform.gameObject.SetActive(false);
            unactiveTransform.gameObject.SetActive(true);
        }
        else
        {
            activeTransform.gameObject.SetActive(true);
            unactiveTransform.gameObject.SetActive(false);
        }

        RawImage borderImage = transform.Find("BorderImage").GetComponent<RawImage>();
        RawImage avatarImage = transform.Find("AvatarImage").GetComponent<RawImage>();

        borderImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(user.UserBorder));
        avatarImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(user.UserAvatar));

        TextMeshProUGUI nameText1 = transform.Find("Active/TitleText").GetComponent<TextMeshProUGUI>();
        nameText1.text = user.UserName;
        TextMeshProUGUI nameText2 = transform.Find("Unactive/TitleText").GetComponent<TextMeshProUGUI>();
        nameText2.text = user.UserName;

        TextMeshProUGUI powerText1 = transform.Find("Active/PowerText").GetComponent<TextMeshProUGUI>();
        powerText1.text = user.Power.ToString();
        TextMeshProUGUI powerText2 = transform.Find("Unactive/PowerText").GetComponent<TextMeshProUGUI>();
        powerText2.text = user.Power.ToString();

        TextMeshProUGUI rankNumberText = transform.Find("RankNumberText").GetComponent<TextMeshProUGUI>();
        rankNumberText.text = user.Rank.ToString();

        RawImage top1BackgroundActive = transform.Find("Active/IconImage/Top1Background").GetComponent<RawImage>();
        RawImage top2BackgroundActive = transform.Find("Active/IconImage/Top2Background").GetComponent<RawImage>();
        RawImage top3BackgroundActive = transform.Find("Active/IconImage/Top3Background").GetComponent<RawImage>();
        RawImage otherTopBackgroundActive = transform.Find("Active/IconImage/OtherTopBackground").GetComponent<RawImage>();

        RawImage top1BackgroundUnactive = transform.Find("Unactive/IconImage/Top1Background").GetComponent<RawImage>();
        RawImage top2BackgroundUnactive = transform.Find("Unactive/IconImage/Top2Background").GetComponent<RawImage>();
        RawImage top3BackgroundUnactive = transform.Find("Unactive/IconImage/Top3Background").GetComponent<RawImage>();
        RawImage otherTopBackgroundUnactive = transform.Find("Unactive/IconImage/OtherTopBackground").GetComponent<RawImage>();

        Button detailButton = gameObject.transform.Find("DetailButton").GetComponent<Button>();
        RawImage top1BackgroundButton = detailButton.transform.Find("Top1Background").GetComponent<RawImage>();
        RawImage top2BackgroundButton = detailButton.transform.Find("Top2Background").GetComponent<RawImage>();
        RawImage top3BackgroundButton = detailButton.transform.Find("Top3Background").GetComponent<RawImage>();
        RawImage otherTopBackgroundButton = detailButton.transform.Find("OtherTopBackground").GetComponent<RawImage>();

        if (user.Rank == 1)
        {
            top1BackgroundActive.gameObject.SetActive(true);
            top2BackgroundActive.gameObject.SetActive(false);
            top3BackgroundActive.gameObject.SetActive(false);
            otherTopBackgroundActive.gameObject.SetActive(false);

            top1BackgroundUnactive.gameObject.SetActive(true);
            top2BackgroundUnactive.gameObject.SetActive(false);
            top3BackgroundUnactive.gameObject.SetActive(false);
            otherTopBackgroundUnactive.gameObject.SetActive(false);

            top1BackgroundButton.gameObject.SetActive(true);
            top2BackgroundButton.gameObject.SetActive(false);
            top3BackgroundButton.gameObject.SetActive(false);
            otherTopBackgroundButton.gameObject.SetActive(false);
        }
        else if (user.Rank == 2)
        {
            top1BackgroundActive.gameObject.SetActive(false);
            top2BackgroundActive.gameObject.SetActive(true);
            top3BackgroundActive.gameObject.SetActive(false);
            otherTopBackgroundActive.gameObject.SetActive(false);

            top1BackgroundUnactive.gameObject.SetActive(false);
            top2BackgroundUnactive.gameObject.SetActive(true);
            top3BackgroundUnactive.gameObject.SetActive(false);
            otherTopBackgroundUnactive.gameObject.SetActive(false);

            top1BackgroundButton.gameObject.SetActive(false);
            top2BackgroundButton.gameObject.SetActive(true);
            top3BackgroundButton.gameObject.SetActive(false);
            otherTopBackgroundButton.gameObject.SetActive(false);
        }
        else if (user.Rank == 3)
        {
            top1BackgroundActive.gameObject.SetActive(false);
            top2BackgroundActive.gameObject.SetActive(false);
            top3BackgroundActive.gameObject.SetActive(true);
            otherTopBackgroundActive.gameObject.SetActive(false);

            top1BackgroundUnactive.gameObject.SetActive(false);
            top2BackgroundUnactive.gameObject.SetActive(false);
            top3BackgroundUnactive.gameObject.SetActive(true);
            otherTopBackgroundUnactive.gameObject.SetActive(false);

            top1BackgroundButton.gameObject.SetActive(false);
            top2BackgroundButton.gameObject.SetActive(false);
            top3BackgroundButton.gameObject.SetActive(true);
            otherTopBackgroundButton.gameObject.SetActive(false);
        }
        else
        {
            top1BackgroundActive.gameObject.SetActive(false);
            top2BackgroundActive.gameObject.SetActive(false);
            top3BackgroundActive.gameObject.SetActive(false);
            otherTopBackgroundActive.gameObject.SetActive(true);

            top1BackgroundUnactive.gameObject.SetActive(false);
            top2BackgroundUnactive.gameObject.SetActive(false);
            top3BackgroundUnactive.gameObject.SetActive(false);
            otherTopBackgroundUnactive.gameObject.SetActive(true);

            top1BackgroundButton.gameObject.SetActive(false);
            top2BackgroundButton.gameObject.SetActive(false);
            top3BackgroundButton.gameObject.SetActive(false);
            otherTopBackgroundButton.gameObject.SetActive(true);
        }
    }
}