using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMonstersGalleryController : MonoBehaviour
{
    public static CardMonstersGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject CardMonsterBlockButtonPrefab;
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
        CardMonsterBlockButtonPrefab = UIManager.Instance.Get("CardMonsterBlockButtonPrefab");
    }
    public void CreateCardMonstersGallery(List<CardMonsters> cardMonsters, Transform contentPanel)
    {
        foreach (var cardMonster in cardMonsters)
        {
            GameObject cardMonstersObject = Instantiate(CardMonsterBlockButtonPrefab, contentPanel);

            TextMeshProUGUI Title = cardMonstersObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = cardMonster.Name.Replace("_", " ");

            RawImage Image = cardMonstersObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMonster.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI levelText = cardMonstersObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.text = cardMonster.Level.ToString().Replace("_", " ");

            TextMeshProUGUI cardText = cardMonstersObject.transform.Find("TagGroup/CardPanel/TitleText").GetComponent<TextMeshProUGUI>();
            cardText.text = "Card Monster";

            TextMeshProUGUI typePanel = cardMonstersObject.transform.Find("TagGroup/TypePanel/TitleText").GetComponent<TextMeshProUGUI>();
            typePanel.text = cardMonster.Type.ToString().Replace("_", " ");

            Image rareBackground = cardMonstersObject.transform.Find("RareBackground").GetComponent<Image>();
            rareBackground.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardMonster.Rare));

            RawImage backgroundImage = cardMonstersObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.CARD_MONSTER_BUTTON_BACKGROUND_URL);

            Button button = cardMonstersObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(cardMonster, MainPanel);
            });

            TextMeshProUGUI rareText = cardMonstersObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(cardMonster.Rare));
            rareText.text = cardMonster.Rare;

            RawImage blockImage = cardMonstersObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardMonstersObject.transform.Find("UnlockButton").GetComponent<Button>();
            if (cardMonster.Status.Equals(AppConstants.Status.AVAILABLE))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (cardMonster.Status.Equals(AppConstants.Status.PENDING))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (cardMonster.Status.Equals(AppConstants.Status.BLOCK))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }
            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var monsterGalleryService = CardMonstersGalleryService.Create();
                await monsterGalleryService.UpdateStatusCardMonsterGalleryAsync(cardMonster.Id);
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

            Button Upgrade = cardMonstersObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((cardMonster.CurrentStar < cardMonster.TempStar) && cardMonster.Status.Equals(AppConstants.Status.AVAILABLE))
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
                await CardMonstersGalleryService.Create().UpdateCardMonsterGalleryPowerAsync(cardMonster.Id);
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
