using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodsGalleryController : MonoBehaviour
{
    public static FoodsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject FoodBlockButtonPrefab;
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
        FoodBlockButtonPrefab = UIManager.Instance.Get("FoodBlockButtonPrefab");
    }
    public void CreateFoodsGallery(List<Foods> Foods, Transform contentPanel)
    {
        foreach (var Food in Foods)
        {
            try
            {
                GameObject FoodObject = Instantiate(FoodBlockButtonPrefab, contentPanel);

                TextMeshProUGUI title = FoodObject.transform.Find("FoodText").GetComponent<TextMeshProUGUI>();
                title.text = Food.Name.Replace("_", " ");

                RawImage image = FoodObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Food.Image);
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

                RawImage backgroundImage = FoodObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.FOOD_BUTTON_BACKGROUND_URL);

                Button button = FoodObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(Food, MainPanel);
                });

                RawImage rareImage = FoodObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Food.Rare}");
                rareImage.texture = rareTexture;

                RawImage blockImage = FoodObject.transform.Find("Block").GetComponent<RawImage>();
                Button Unlock = FoodObject.transform.Find("UnlockButton").GetComponent<Button>();
                if (Food.Status.Equals("available"))
                {
                    blockImage.gameObject.SetActive(false);
                    Unlock.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (Food.Status.Equals("pending"))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(true);
                }
                else if (Food.Status.Equals("block"))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(false);
                }

                RawImage rareBackgroundImage = FoodObject.transform.Find("RareBackground").GetComponent<RawImage>();
                rareImage.gameObject.SetActive(false);
                rareBackgroundImage.gameObject.SetActive(false);

                Unlock.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    var FoodGalleryService = FoodsGalleryService.Create();
                    await FoodGalleryService.UpdateStatusFoodGalleryAsync(Food.Id);
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

                Button Upgrade = FoodObject.transform.Find("UpgradeButton").GetComponent<Button>();
                if ((Food.CurrentStar < Food.TempStar) && Food.Status.Equals("available"))
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
                    await FoodsGalleryService.Create().UpdateFoodGalleryPowerAsync(Food.Id);
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
