using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpiritBeastsGalleryController : MonoBehaviour
{
    public static SpiritBeastsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject equipmentsPrefab;
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
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentSecondPrefab");
    }
    public void CreateSpiritBeastGallery(List<SpiritBeasts> SpiritBeastList, Transform DictionaryContentPanel)
    {
        foreach (var title in SpiritBeastList)
        {
            GameObject titleObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = titleObject.transform.Find("Title").GetComponent<Text>();
            Title.text = title.Name.Replace("_", " ");

            RawImage Image = titleObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(title.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            Image.SetNativeSize();
            Image.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            Button button = titleObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(title, MainPanel);
            });

            RawImage rareImage = titleObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{title.Rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = titleObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = titleObject.transform.Find("Unlock").GetComponent<Button>();
            if (title.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (title.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (title.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            RawImage rareBackgroundImage = titleObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareImage.gameObject.SetActive(false);
            rareBackgroundImage.gameObject.SetActive(false);

            Unlock.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var titleGalleryService = SpiritBeastGalleryService.Create();
                titleGalleryService.UpdateStatusSpiritBeastGallery(title.Id);
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

            Button Upgrade = titleObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((title.CurrentStar < title.TempStar) && title.Status.Equals("available"))
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
                TitlesGalleryService.Create().UpdateTitlesGalleryPower(title.Id);
            });
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 230);
        }
        DictionaryContentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
