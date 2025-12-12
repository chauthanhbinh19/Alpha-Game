using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FurnituresGalleryController : MonoBehaviour
{
    public static FurnituresGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject FurnitureBlockButtonPrefab;
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
        FurnitureBlockButtonPrefab = UIManager.Instance.Get("FurnitureBlockButtonPrefab");
    }
    public void CreateFurnitureGallery(List<Furnitures> Furnitures, Transform contentPanel)
    {
        foreach (var Furniture in Furnitures)
        {
            try
            {
                GameObject FurnitureObject = Instantiate(FurnitureBlockButtonPrefab, contentPanel);

                TextMeshProUGUI Title = FurnitureObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                Title.text = Furniture.Name.Replace("_", " ");

                RawImage image = FurnitureObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Furniture.Image);
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

                RawImage backgroundImage = FurnitureObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.FURNITURE_BUTTON_BACKGROUND_URL);

                Button button = FurnitureObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(Furniture, MainPanel);
                });

                RawImage rareImage = FurnitureObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Furniture.Rare}");
                rareImage.texture = rareTexture;

                RawImage blockImage = FurnitureObject.transform.Find("Block").GetComponent<RawImage>();
                Button Unlock = FurnitureObject.transform.Find("UnlockButton").GetComponent<Button>();
                if (Furniture.Status.Equals("available"))
                {
                    blockImage.gameObject.SetActive(false);
                    Unlock.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (Furniture.Status.Equals("pending"))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(true);
                }
                else if (Furniture.Status.Equals("block"))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(false);
                }

                Unlock.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await FurnitureGalleryService.Create().UpdateStatusFurnitureGalleryAsync(Furniture.Id);
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

                Button Upgrade = FurnitureObject.transform.Find("UpgradeButton").GetComponent<Button>();
                if ((Furniture.CurrentStar < Furniture.TempStar) && Furniture.Status.Equals("available"))
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
                    await FurnitureGalleryService.Create().UpdateFurnitureGalleryPowerAsync(Furniture.Id);
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
