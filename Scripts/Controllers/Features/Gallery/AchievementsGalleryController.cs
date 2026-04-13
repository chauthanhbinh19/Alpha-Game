using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsGalleryController : MonoBehaviour
{
    public static AchievementsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject AchievementBlockButtonPrefab;
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
        Initialize();
    }

    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        AchievementBlockButtonPrefab = UIManager.Instance.Get("AchievementBlockButtonPrefab");
    }
    public void CreateAchievementsGallery(List<Achievements> achievements, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        // Cache texture background dùng chung một lần duy nhất ngoài vòng lặp
        Texture bgTexture = TextureHelper.LoadTextureCached(ImageConstants.Background.ACHIEVEMENT_BUTTON_BACKGROUND_URL);

        foreach (var achievement in achievements)
        {
            try
            {
                GameObject achievementObject = Instantiate(AchievementBlockButtonPrefab, contentPanel);
                Transform transform = achievementObject.transform;

                TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                titleText.text = achievement.Name.Replace("_", " ");

                RawImage image = transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(achievement.Image);
                Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                image.texture = texture;

                // Kích thước của RawImage (khung hiển thị)
                RectTransform rect = image.GetComponent<RectTransform>();
                float maxWidth = rect.rect.width;
                float maxHeight = rect.rect.height;

                // Kích thước thật của texture
                float texWidth = texture.width;
                float texHeight = texture.height;

                // Tính scale để texture nằm gọn trong khung
                float widthRatio = maxWidth / texWidth;
                float heightRatio = maxHeight / texHeight;
                float finalScale = Mathf.Min(widthRatio, heightRatio);  // scale nhỏ nhất

                // Áp dụng scale theo tỉ lệ đúng
                image.SetNativeSize();
                image.transform.localScale = new Vector3(finalScale, finalScale, 1f);

                RawImage backgroundImage = transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = bgTexture;

                Button button = transform.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(achievement, MainPanel);
                });

                TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
                rareText.color = ColorHelper.HexToColor(QualityEvaluator.CheckRareColor(achievement.Rare));
                rareText.text = achievement.Rare;

                RawImage blockImage = transform.Find("Block").GetComponent<RawImage>();
                Button unlockButton = transform.Find("UnlockButton").GetComponent<Button>();
                if (achievement.Status.Equals(AppConstants.Status.AVAILABLE))
                {
                    blockImage.gameObject.SetActive(false);
                    unlockButton.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (achievement.Status.Equals(AppConstants.Status.PENDING))
                {
                    blockImage.gameObject.SetActive(true);
                    unlockButton.gameObject.SetActive(true);
                }
                else if (achievement.Status.Equals(AppConstants.Status.BLOCK))
                {
                    blockImage.gameObject.SetActive(true);
                    unlockButton.gameObject.SetActive(false);
                }

                unlockButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await AvatarsGalleryService.Create().UpdateStatusAvatarGalleryAsync(achievement.Id);
                    blockImage.gameObject.SetActive(false);
                    unlockButton.gameObject.SetActive(false);
                    image.color = Color.white;

                    var powerManagerService = PowerManagerService.Create();
                    var teamsService = TeamsService.Create();

                    await powerManagerService.UpdateUserStatsAsync(User.CurrentUserId);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                });

                Button upgradeButton = transform.Find("UpgradeButton").GetComponent<Button>();
                if ((achievement.CurrentStar < achievement.TempStar) && achievement.Status.Equals(AppConstants.Status.AVAILABLE))
                {
                    upgradeButton.gameObject.SetActive(true);
                }
                else
                {
                    upgradeButton.gameObject.SetActive(false);
                }

                upgradeButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await AvatarsGalleryService.Create().UpdateAvatarGalleryPowerAsync(achievement.Id);
                });
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 240);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
