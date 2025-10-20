using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalismanGalleryController : MonoBehaviour
{
    public static TalismanGalleryController Instance { get; private set; }
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

    // Update is called once per frame
    void Update()
    {

    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentSecondPrefab");
    }
    public void CreateTalismanGallery(List<Talismans> talismans, Transform DictionaryContentPanel)
    {
        foreach (var talisman in talismans)
        {
            GameObject talismanObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = talismanObject.transform.Find("Title").GetComponent<Text>();
            Title.text = talisman.Name.Replace("_", " ");

            RawImage Image = talismanObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(talisman.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // RawImage frameImage = talismanObject.transform.Find("FrameImage").GetComponent<RawImage>();
            // frameImage.gameObject.SetActive(true);

            Button button = talismanObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                PopupDetailsManager.Instance.PopupDetails(talisman, MainPanel);
            });

            RawImage rareImage = talismanObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{talisman.Rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = talismanObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = talismanObject.transform.Find("Unlock").GetComponent<Button>();
            if (talisman.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (talisman.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (talisman.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                var talismanGalleryService = TalismanGalleryService.Create();
                talismanGalleryService.UpdateStatusTalismanGallery(talisman.Id);
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;

                var powerManagerService = PowerManagerService.Create();
                var teamsService = TeamsService.Create();
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                powerManagerService.UpdateUserStats(User.CurrentUserId);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
            });

            Button Upgrade = talismanObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((talisman.CurrentStar < talisman.TempStar) && talisman.Status.Equals("available"))
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
                TalismanGalleryService.Create().UpdateTalismanGalleryPower(talisman.Id);
            });
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
        DictionaryContentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
