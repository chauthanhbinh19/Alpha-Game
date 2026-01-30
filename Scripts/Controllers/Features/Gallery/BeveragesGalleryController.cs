using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BeveragesGalleryController : MonoBehaviour
{
    public static BeveragesGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject BeverageBlockButtonPrefab;
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
        BeverageBlockButtonPrefab = UIManager.Instance.Get("BeverageBlockButtonPrefab");
    }
    public void CreateBeveragesGallery(List<Beverages> beverages, Transform contentPanel)
    {
        foreach (var beverage in beverages)
        {
            try
            {
                GameObject beverageObject = Instantiate(BeverageBlockButtonPrefab, contentPanel);

                TextMeshProUGUI title = beverageObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                title.text = beverage.Name.Replace("_", " ");

                RawImage image = beverageObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(beverage.Image);
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
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

                RawImage backgroundImage = beverageObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.BEVERAGE_BUTTON_BACKGROUND_URL);

                Button button = beverageObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(beverage, MainPanel);
                });

                TextMeshProUGUI rareText = beverageObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
                rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(beverage.Rare));
                rareText.text = beverage.Rare;

                RawImage blockImage = beverageObject.transform.Find("Block").GetComponent<RawImage>();
                Button Unlock = beverageObject.transform.Find("UnlockButton").GetComponent<Button>();
                if (beverage.Status.Equals(AppConstants.Status.AVAILABLE))
                {
                    blockImage.gameObject.SetActive(false);
                    Unlock.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (beverage.Status.Equals(AppConstants.Status.PENDING))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(true);
                }
                else if (beverage.Status.Equals(AppConstants.Status.BLOCK))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(false);
                }

                Unlock.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    var BeverageGalleryService = BeveragesGalleryService.Create();
                    await BeverageGalleryService.UpdateStatusBeverageGalleryAsync(beverage.Id);
                    blockImage.gameObject.SetActive(false);
                    Unlock.gameObject.SetActive(false);
                    image.color = Color.white;

                    var powerManagerService = PowerManagerService.Create();
                    var teamsService = TeamsService.Create();

                    await powerManagerService.UpdateUserStatsAsync(User.CurrentUserId);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                });

                Button Upgrade = beverageObject.transform.Find("UpgradeButton").GetComponent<Button>();
                if ((beverage.CurrentStar < beverage.TempStar) && beverage.Status.Equals(AppConstants.Status.AVAILABLE))
                {
                    Upgrade.gameObject.SetActive(true);
                }
                else
                {
                    Upgrade.gameObject.SetActive(false);
                }

                Upgrade.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await BeveragesGalleryService.Create().UpdateBeverageGalleryPowerAsync(beverage.Id);
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
