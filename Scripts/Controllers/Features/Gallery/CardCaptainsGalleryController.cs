using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardCaptainsGalleryController : MonoBehaviour
{
    public static CardCaptainsGalleryController Instance { get; private set; }
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

    // Update is called once per frame
    void Update()
    {

    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        cardsPrefab = UIManager.Instance.GetGameObject("CardsSecondPrefab");
    }
    public void CreateCardCaptainsGallery(List<CardCaptains> captainsList, Transform DictionaryContentPanel)
    {
        foreach (var captain in captainsList)
        {
            GameObject captainsObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = captainsObject.transform.Find("Title").GetComponent<Text>();
            Title.text = captain.Name.Replace("_", " ");

            RawImage Image = captainsObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(captain.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = captainsObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                PopupDetailsManager.Instance.PopupDetails(captain, MainPanel);
            });

            RawImage rareImage = captainsObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{captain.Rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = captainsObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = captainsObject.transform.Find("Unlock").GetComponent<Button>();
            if (captain.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (captain.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (captain.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }
            Unlock.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                var cardCaptainsGalleryService = CardCaptainsGalleryService.Create();
                cardCaptainsGalleryService.UpdateStatusCardCaptainsGallery(captain.Id);
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;

                var powerManagerService = PowerManagerService.Create();
                var teamsService = TeamsService.Create();

                powerManagerService.UpdateUserStats(User.CurrentUserId);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
            });

            Button Upgrade = captainsObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((captain.CurrentStar < captain.TempStar) && captain.Status.Equals("available"))
            {
                Upgrade.gameObject.SetActive(true);
            }
            else
            {
                Upgrade.gameObject.SetActive(false);
            }

            Upgrade.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                CardCaptainsGalleryService.Create().UpdateCardCaptainsGalleryPower(captain.Id);
            });
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(280, 350);
        }
        DictionaryContentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
