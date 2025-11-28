using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMonstersGalleryController : MonoBehaviour
{
    public static CardMonstersGalleryController Instance { get; private set; }
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
    public void CreateCardMonstersGallery(List<CardMonsters> monstersList, Transform DictionaryContentPanel)
    {
        foreach (var monster in monstersList)
        {
            GameObject monstersObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = monstersObject.transform.Find("Title").GetComponent<Text>();
            Title.text = monster.Name.Replace("_", " ");

            RawImage Image = monstersObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(monster.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = monstersObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(monster, MainPanel);
            });

            RawImage rareImage = monstersObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{monster.Rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = monstersObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = monstersObject.transform.Find("Unlock").GetComponent<Button>();
            if (monster.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (monster.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (monster.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }
            Unlock.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var monsterGalleryService = CardMonstersGalleryService.Create();
                monsterGalleryService.UpdateStatusCardMonstersGallery(monster.Id);
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;

                var powerManagerService = PowerManagerService.Create();
                var teamsService = TeamsService.Create();

                powerManagerService.UpdateUserStats(User.CurrentUserId);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
            });

            Button Upgrade = monstersObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((monster.CurrentStar < monster.TempStar) && monster.Status.Equals("available"))
            {
                Upgrade.gameObject.SetActive(true);
            }
            else
            {
                Upgrade.gameObject.SetActive(false);
            }

            Upgrade.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                CardMonstersGalleryService.Create().UpdateCardMonstersGalleryPower(monster.Id);
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
