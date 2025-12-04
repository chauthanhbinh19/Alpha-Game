using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpiritCardsGalleryController : MonoBehaviour
{
    public static SpiritCardsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject cardsPrefab;
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
        cardsPrefab = UIManager.Instance.GetGameObject("CardsSecondPrefab");
    }
    public void CreateSpiritCardGallery(List<SpiritCards> SpiritCardList, Transform contentPanel)
    {
        foreach (var spiritCard in SpiritCardList)
        {
            GameObject spiritCardObject = Instantiate(cardsPrefab, contentPanel);

            Text Title = spiritCardObject.transform.Find("Title").GetComponent<Text>();
            Title.text = spiritCard.Name.Replace("_", " ");

            RawImage Image = spiritCardObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(spiritCard.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            Image.SetNativeSize();
            RectTransform rect = Image.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(200f, 400f);

            Button button = spiritCardObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(spiritCard, MainPanel);
            });

            RawImage rareImage = spiritCardObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spiritCard.Rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = spiritCardObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = spiritCardObject.transform.Find("Unlock").GetComponent<Button>();
            if (spiritCard.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (spiritCard.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (spiritCard.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            RawImage rareBackgroundImage = spiritCardObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareImage.gameObject.SetActive(false);
            rareBackgroundImage.gameObject.SetActive(false);

            Unlock.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var spiritCardGalleryService = SpiritCardsGalleryService.Create();
                await spiritCardGalleryService.UpdateStatusSpiritCardGalleryAsync(spiritCard.Id);
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

            Button Upgrade = spiritCardObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((spiritCard.CurrentStar < spiritCard.TempStar) && spiritCard.Status.Equals("available"))
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
                await SpiritCardsGalleryService.Create().UpdateSpiritCardGalleryPowerAsync(spiritCard.Id);
            });
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 400);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
