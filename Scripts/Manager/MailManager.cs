using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailManager : MonoBehaviour
{
    public static MailManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject MailPanelPrefab;
    private GameObject MailTabButtonPrefab;
    private GameObject MailButtonPrefab;
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
        MailPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.General.MAIL_PANEL_PREFAB);
        MailTabButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.MAIL_TAB_BUTTON_PREFAB);
        MailButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.MAIL_BUTTON_PREFAB);
    }
    public async Task CreateMailPanel()
    {
        GameObject currentObject = Instantiate(MailPanelPrefab, MainPanel);
        // Transform contentTransform = transform.Find("Scroll View/Viewport/Content");
        Transform leftTransform = transform.Find("Left Scroll View/Viewport/Content");
        // Transform personalTransform = transform.Find("Personal");
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

        // var mails = await MailService.Create().GetUserMailsAsync(User.CurrentUserId);

        List<string> mailTypes = new List<string>
        {
            AppConstants.Mail.SYSTEM,
            AppConstants.Mail.REWARD,
            AppConstants.Mail.RANKING,
            AppConstants.Mail.SOCIAL,
            AppConstants.Mail.GUILD,
            AppConstants.Mail.TRANSACTION,
        };

        List<(GameObject defaultObj, GameObject selectedObj)> tabUIList = new List<(GameObject, GameObject)>();

        for (int i = 0; i < mailTypes.Count; i++)
        {
            string mailType = mailTypes[i];

            // Instantiate tab button và đặt parent vào Content của Tab Scroll View
            GameObject topupTabButtonObject = Instantiate(MailTabButtonPrefab, leftTransform);

            GameObject defaultObj = topupTabButtonObject.transform.Find("Default").gameObject;
            GameObject selectedObj = topupTabButtonObject.transform.Find("Selected").gameObject;

            TextMeshProUGUI titleText1 = defaultObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI titleText2 = selectedObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();

            titleText1.text = mailType;
            titleText2.text = mailType;

            // Trạng thái mặc định: Phần tử đầu tiên (i == 0) sẽ Active Selected, còn lại Active Default
            bool isFirst = (i == 0);
            defaultObj.SetActive(!isFirst);
            selectedObj.SetActive(isFirst);

            tabUIList.Add((defaultObj, selectedObj));

            // Đăng ký sự kiện Click cho Tab Button
            Button tabBtn = topupTabButtonObject.GetComponent<Button>();
            tabBtn.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND_2);

                // 1. Chuyển tất cả các Tab về dạng Default
                foreach (var tabUI in tabUIList)
                {
                    tabUI.defaultObj.SetActive(true);
                    tabUI.selectedObj.SetActive(false);
                }

                // 2. Bật dạng Selected cho Tab vừa được click
                defaultObj.SetActive(false);
                selectedObj.SetActive(true);

                // TODO: Gọi hàm load/filter danh sách gói nạp theo category này vào contentTransform
                await CreateMailButtonAsync(mailType, leftTransform);
            });
        }

        // Load gói nạp của Tab đầu tiên nếu có danh mục
        if (mailTypes.Count > 0)
        {
            await CreateMailButtonAsync(mailTypes[0], leftTransform);
        }
    }
    public async Task CreateMailButtonAsync(string type, Transform contentTransform)
    {
        var mails = await MailService.Create().GetUserMailsAsync(User.CurrentUserId, type);

        foreach(var mail in mails)
        {
            GameObject mailButtonObject = Instantiate(MailButtonPrefab, contentTransform);

            Transform activeTransform = mailButtonObject.transform.Find("Active");
            Transform unactiveTransform = mailButtonObject.transform.Find("Unactive");

            if (mail.IsRead)
            {
                activeTransform.gameObject.SetActive(false);
                unactiveTransform.gameObject.SetActive(true);
            }
            else
            {
                activeTransform.gameObject.SetActive(true);
                unactiveTransform.gameObject.SetActive(false);
            }

            TextMeshProUGUI activeSubjectText = mailButtonObject.transform.Find("Active/SubjectText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI unactiveSubjectText = mailButtonObject.transform.Find("Unactive/SubjectText").GetComponent<TextMeshProUGUI>();
            activeSubjectText.text = mail.Subject;
            unactiveSubjectText.text = mail.Subject;

            TextMeshProUGUI activeCreateAtText = mailButtonObject.transform.Find("Active/CreateAtText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI unactiveCreateAtText = mailButtonObject.transform.Find("Unactive/CreateAtText").GetComponent<TextMeshProUGUI>();
            activeCreateAtText.text = mail.CreatedAt.ToString("dd-MM-yyyy");
            unactiveCreateAtText.text = mail.CreatedAt.ToString("dd-MM-yyyy");

            TextMeshProUGUI activeTotalItemText = mailButtonObject.transform.Find("Active/TotalItemText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI unactiveTotalItemText = mailButtonObject.transform.Find("Unactive/TotalItemText").GetComponent<TextMeshProUGUI>();
            activeTotalItemText.text = mail.Items.Count.ToString();
            unactiveTotalItemText.text = mail.Items.Count.ToString();

            Button claimButton = mailButtonObject.transform.Find("ClaimButton").GetComponent<Button>();
        }
    }
}