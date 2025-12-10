using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardsGalleryController : MonoBehaviour
{
    public static CardsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardBlockButtonPrefab;
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
        CardBlockButtonPrefab = UIManager.Instance.Get("CardBlockButtonPrefab");
    }
    public void CreateCardsGallery(List<Cards> cards, Transform contentPanel)
    {
        foreach (var card in cards)
        {
            GameObject cardObject = Instantiate(CardBlockButtonPrefab, contentPanel);

            TextMeshProUGUI Title = cardObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = card.Name.Replace("_", " ");

            RawImage image = cardObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(card.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            image.texture = texture;

            // Set size 130x180
            RectTransform rect = image.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(130, 180);

            RawImage backgroundImage = cardObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.CARD_BUTTON_BACKGROUND_URL);

            Button button = cardObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(card, MainPanel);
            });

            RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.Rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = cardObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardObject.transform.Find("UnlockButton").GetComponent<Button>();
            if (card.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                image.color = Color.white;
            }
            else if (card.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (card.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await CardsGalleryService.Create().UpdateStatusCardGalleryAsync(card.Id);
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

            Button Upgrade = cardObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((card.CurrentStar < card.TempStar) && card.Status.Equals("available"))
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
                await CardsGalleryService.Create().UpdateCardGalleryPowerAsync(card.Id);
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
