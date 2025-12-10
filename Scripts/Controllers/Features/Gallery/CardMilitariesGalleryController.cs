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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        CardMilitaryBlockButtonPrefab = UIManager.Instance.Get("CardMilitaryBlockButtonPrefab");
    }
    public void CreateCardMilitaryGallery(List<CardMilitaries> cardMilitaries, Transform contentPanel)
    {
        foreach (var cardMilitary in cardMilitaries)
        {
            GameObject cardMilitaryObject = Instantiate(CardMilitaryBlockButtonPrefab, contentPanel);

            TextMeshProUGUI Title = cardMilitaryObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = cardMilitary.Name.Replace("_", " ");

            RawImage Image = cardMilitaryObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMilitary.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = cardMilitaryObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardMilitary, MainPanel);
            });

            RawImage rareImage = cardMilitaryObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMilitary.Rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = cardMilitaryObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardMilitaryObject.transform.Find("UnlockButton").GetComponent<Button>();
            if (cardMilitary.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (cardMilitary.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (cardMilitary.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var militaryGalleryService = CardMilitariesGalleryService.Create();
                await militaryGalleryService.UpdateStatusCardMilitaryGalleryAsync(cardMilitary.Id);
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

            Button Upgrade = cardMilitaryObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((cardMilitary.CurrentStar < cardMilitary.TempStar) && cardMilitary.Status.Equals("available"))
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
                await CardMilitariesGalleryService.Create().UpdateCardMilitaryGalleryPowerAsync(cardMilitary.Id);
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
