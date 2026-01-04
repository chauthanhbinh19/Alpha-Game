using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSpellsGalleryController : MonoBehaviour
{
    public static CardSpellsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardSpellBlockButtonPrefab;
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
        CardSpellBlockButtonPrefab = UIManager.Instance.Get("CardSpellBlockButtonPrefab");
    }
    public void CreateCardSpellsGallery(List<CardSpells> cardSpells, Transform contentPanel)
    {
        foreach (var cardSpell in cardSpells)
        {
            GameObject cardSpellObject = Instantiate(CardSpellBlockButtonPrefab, contentPanel);

            TextMeshProUGUI Title = cardSpellObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = cardSpell.Name.Replace("_", " ");

            RawImage Image = cardSpellObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardSpell.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage backgroundImage = cardSpellObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.CARD_SPELL_BUTTON_BACKGROUND_URL);

            Button button = cardSpellObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardSpell, MainPanel);
            });

            TextMeshProUGUI rareText = cardSpellObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardSpell.Rare));
            rareText.text = cardSpell.Rare;

            RawImage blockImage = cardSpellObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardSpellObject.transform.Find("UnlockButton").GetComponent<Button>();
            if (cardSpell.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (cardSpell.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (cardSpell.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var spellGalleryService = CardSpellsGalleryService.Create();
                await spellGalleryService.UpdateStatusCardSpellGalleryAsync(cardSpell.Id);
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

            Button Upgrade = cardSpellObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((cardSpell.CurrentStar < cardSpell.TempStar) && cardSpell.Status.Equals("available"))
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
                await CardSpellsGalleryService.Create().UpdateCardSpellGalleryPowerAsync(cardSpell.Id);
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
