using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardColonelsGalleryController : MonoBehaviour
{
    public static CardColonelsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardColonelBlockButtonPrefab;
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
        CardColonelBlockButtonPrefab = UIManager.Instance.Get("CardColonelBlockButtonPrefab");
    }
    public void CreateCardColonelsGallery(List<CardColonels> cardColonels, Transform contentPanel)
    {
        foreach (var cardColonel in cardColonels)
        {
            GameObject cardColonelObject = Instantiate(CardColonelBlockButtonPrefab, contentPanel);

            TextMeshProUGUI Title = cardColonelObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = cardColonel.Name.Replace("_", " ");

            RawImage Image = cardColonelObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardColonel.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage backgroundImage = cardColonelObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.CARD_COLONEL_BUTTON_BACKGROUND_URL);

            Button button = cardColonelObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardColonel, MainPanel);
            });

            TextMeshProUGUI rareText = cardColonelObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardColonel.Rare));
            rareText.text = cardColonel.Rare;

            RawImage blockImage = cardColonelObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardColonelObject.transform.Find("UnlockButton").GetComponent<Button>();
            if (cardColonel.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (cardColonel.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (cardColonel.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var colonelGalleryService = CardColonelsGalleryService.Create();
                await colonelGalleryService.UpdateStatusCardColonelGalleryAsync(cardColonel.Id);
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;

                var powerManagerService = PowerManagerService.Create();
                var teamsService = TeamsService.Create();

                await powerManagerService.UpdateUserStatsAsync(User.CurrentUserId);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
            });

            Button Upgrade = cardColonelObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((cardColonel.CurrentStar < cardColonel.TempStar) && cardColonel.Status.Equals("available"))
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
                await CardColonelsGalleryService.Create().UpdateCardColonelGalleryPowerAsync(cardColonel.Id);
            });
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(280, 350);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
