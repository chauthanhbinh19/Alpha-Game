using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMilitariesGalleryController : MonoBehaviour
{
    public static CardMilitariesGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardMilitaryBlockButtonPrefab;
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
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        CardMilitaryBlockButtonPrefab = UIManager.Instance.Get(PrefabConstants.Component.CARD_MILITARY_BLOCK_BUTTON_PREFAB);
    }
    public void CreateCardMilitariesGallery(List<CardMilitaries> cardMilitaries, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        foreach (var cardMilitary in cardMilitaries)
        {
            GameObject cardMilitaryObject = Instantiate(CardMilitaryBlockButtonPrefab, contentPanel);
            Transform transform = cardMilitaryObject.transform;

            TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = cardMilitary.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardMilitary.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;

            TextMeshProUGUI levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardMilitary.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = LocalizationManager.Get(AppDisplayConstants.Title.CARD_MILITARY);

            TextMeshProUGUI typePanel = transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardMilitary.Type.ToString().Replace("_", " ");

            Image rareBackground = transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardMilitary.Rarity));

            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardMilitary, MainPanel);
            });

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(cardMilitary.Rarity));
            rareText.text = cardMilitary.Rarity;

            RawImage blockImage = transform.Find("Block").GetComponent<RawImage>();
            Button unlockButton = transform.Find("UnlockButton").GetComponent<Button>();
            if (cardMilitary.Status.Equals(AppConstants.Status.AVAILABLE))
            {
                blockImage.gameObject.SetActive(false);
                unlockButton.gameObject.SetActive(false);
                image.color = Color.white;
            }
            else if (cardMilitary.Status.Equals(AppConstants.Status.PENDING))
            {
                blockImage.gameObject.SetActive(true);
                unlockButton.gameObject.SetActive(true);
            }
            else if (cardMilitary.Status.Equals(AppConstants.Status.BLOCK))
            {
                blockImage.gameObject.SetActive(true);
                unlockButton.gameObject.SetActive(false);
            }

            unlockButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var militaryGalleryService = CardMilitariesGalleryService.Create();
                await militaryGalleryService.UpdateStatusCardMilitaryGalleryAsync(User.CurrentUserId, cardMilitary.Id);
                blockImage.gameObject.SetActive(false);
                unlockButton.gameObject.SetActive(false);
                image.color = Color.white;

                // 1. Gọi helper xử lý ngầm toàn bộ logic dữ liệu & DB
                PowerResultDTO result = await PowerHelper.RefreshAndSyncTotalPowerAsync(
                    AppConstants.Power.GALLERY_CARD_MILITARIES,
                    PowerHelper.GetCardMilitariesGalleryPowerAsync
                );

                // 2. Tầng UI tự lo việc hiển thị hiệu ứng dựa trên kết quả nhận được
                if (result.HasChanged)
                {
                    double diff = result.Difference;
                    int status = diff > 0 ? 1 : (diff < 0 ? -1 : 0);
                    double deltaPower = Math.Abs(diff);

                    PowerController.Instance.ShowPower(result.CurrentPower, deltaPower, status);
                }
            });

            Button upgradeButton = transform.Find("UpgradeButton").GetComponent<Button>();
            if ((cardMilitary.CurrentStar < cardMilitary.TempStar) && cardMilitary.Status.Equals(AppConstants.Status.AVAILABLE))
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
                await CardMilitariesGalleryService.Create().UpdateCardMilitaryGalleryPowerAsync(User.CurrentUserId, cardMilitary.Id);
            });
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(250, 360);
            gridLayout.spacing = new Vector2(23, 10);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
