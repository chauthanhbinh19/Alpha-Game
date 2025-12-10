using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ForgesGalleryController : MonoBehaviour
{
    public static ForgesGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ForgeBlockButtonPrefab;
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
        ForgeBlockButtonPrefab = UIManager.Instance.Get("ForgeBlockButtonPrefab");
    }
    public void CreateForgeGallery(List<Forges> forges, Transform contentPanel)
    {
        foreach (var forge in forges)
        {
            try
            {
                GameObject forgeObject = Instantiate(ForgeBlockButtonPrefab, contentPanel);

                TextMeshProUGUI Title = forgeObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                Title.text = forge.Name.Replace("_", " ");

                RawImage image = forgeObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(forge.Image);
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

                RawImage backgroundImage = forgeObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.FORGE_BUTTON_BACKGROUND_URL);

                // RawImage frameImage = forgeObject.transform.Find("FrameImage").GetComponent<RawImage>();
                // frameImage.gameObject.SetActive(true);

                Button button = forgeObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(forge, MainPanel);
                });

                RawImage rareImage = forgeObject.transform.Find("Rare").GetComponent<RawImage>();
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{forge.Rare}");
                rareImage.texture = rareTexture;

                RawImage blockImage = forgeObject.transform.Find("Block").GetComponent<RawImage>();
                Button Unlock = forgeObject.transform.Find("UnlockButton").GetComponent<Button>();
                if (forge.Status.Equals("available"))
                {
                    blockImage.gameObject.SetActive(false);
                    Unlock.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (forge.Status.Equals("pending"))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(true);
                }
                else if (forge.Status.Equals("block"))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(false);
                }

                Unlock.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    var forgeGalleryService = ForgesGalleryService.Create();
                    await forgeGalleryService.UpdateStatusForgeGalleryAsync(forge.Id);
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

                Button Upgrade = forgeObject.transform.Find("UpgradeButton").GetComponent<Button>();
                if ((forge.CurrentStar < forge.TempStar) && forge.Status.Equals("available"))
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
                    await ForgesGalleryService.Create().UpdateForgeGalleryPowerAsync(forge.Id);
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
