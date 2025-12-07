using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArchitecturesGalleryController : MonoBehaviour
{
    public static ArchitecturesGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ArchitectureBlockButtonPrefab;
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
        ArchitectureBlockButtonPrefab = UIManager.Instance.Get("ArchitectureBlockButtonPrefab");
    }
    public void CreateArchitecturesGallery(List<Architectures> architectures, Transform contentPanel)
    {
        foreach (var architecture in architectures)
        {
            try
            {
                GameObject architectureObject = Instantiate(ArchitectureBlockButtonPrefab, contentPanel);

                TextMeshProUGUI Title = architectureObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                Title.text = architecture.Name.Replace("_", " ");

                RawImage image = architectureObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(architecture.Image);
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

                Button button = architectureObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(architecture, MainPanel);
                });

                RawImage rareImage = architectureObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{architecture.Rare}");
                rareImage.texture = rareTexture;

                RawImage blockImage = architectureObject.transform.Find("Block").GetComponent<RawImage>();
                Button Unlock = architectureObject.transform.Find("UnlockButton").GetComponent<Button>();
                if (architecture.Status.Equals("available"))
                {
                    blockImage.gameObject.SetActive(false);
                    Unlock.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (architecture.Status.Equals("pending"))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(true);
                }
                else if (architecture.Status.Equals("block"))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(false);
                }

                RawImage rareBackgroundImage = architectureObject.transform.Find("RareBackground").GetComponent<RawImage>();
                rareImage.gameObject.SetActive(false);
                rareBackgroundImage.gameObject.SetActive(false);

                Unlock.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    var titleGalleryService = ArchitecturesGalleryService.Create();
                    await titleGalleryService.UpdateStatusArchitectureGalleryAsync(architecture.Id);
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

                Button Upgrade = architectureObject.transform.Find("UpgradeButton").GetComponent<Button>();
                if ((architecture.CurrentStar < architecture.TempStar) && architecture.Status.Equals("available"))
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
                    await ArchitecturesGalleryService.Create().UpdateArchitectureGalleryPowerAsync(architecture.Id);
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
