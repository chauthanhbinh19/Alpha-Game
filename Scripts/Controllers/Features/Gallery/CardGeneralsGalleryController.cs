using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardGeneralsGalleryController : MonoBehaviour
{
    public static CardGeneralsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardGeneralBlockButtonPrefab;
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
        CardGeneralBlockButtonPrefab = UIManager.Instance.Get("CardGeneralBlockButtonPrefab");
    }
    public void CreateCardGeneralsGallery(List<CardGenerals> cardGenerals, Transform contentPanel)
    {
        foreach (var cardGeneral in cardGenerals)
        {
            GameObject cardGeneralObject = Instantiate(CardGeneralBlockButtonPrefab, contentPanel);

            TextMeshProUGUI Title = cardGeneralObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = cardGeneral.Name.Replace("_", " ");

            RawImage Image = cardGeneralObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardGeneral.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage backgroundImage = cardGeneralObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.CARD_GENERAL_BUTTON_BACKGROUND_URL);

            Button button = cardGeneralObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardGeneral, MainPanel);
            });

            RawImage rareImage = cardGeneralObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardGeneral.Rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = cardGeneralObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardGeneralObject.transform.Find("UnlockButton").GetComponent<Button>();
            if (cardGeneral.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (cardGeneral.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (cardGeneral.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var generalGalleryService = CardGeneralsGalleryService.Create();
                await generalGalleryService.UpdateStatusCardGeneralGalleryAsync(cardGeneral.Id);
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

            Button Upgrade = cardGeneralObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((cardGeneral.CurrentStar < cardGeneral.TempStar) && cardGeneral.Status.Equals("available"))
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
                await CardGeneralsGalleryService.Create().UpdateCardGeneralGalleryPowerAsync(cardGeneral.Id);
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
