using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ForgesGalleryController : MonoBehaviour
{
    public static ForgesGalleryController Instance { get; private set; }
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
    public void CreateForgeGallery(List<Forges> forges, Transform DictionaryContentPanel)
    {
        foreach (var forge in forges)
        {
            GameObject forgeObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = forgeObject.transform.Find("Title").GetComponent<Text>();
            Title.text = forge.Name.Replace("_", " ");

            RawImage Image = forgeObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(forge.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // RawImage frameImage = forgeObject.transform.Find("FrameImage").GetComponent<RawImage>();
            // frameImage.gameObject.SetActive(true);

            Button button = forgeObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(forge, MainPanel);
            });

            RawImage rareImage = forgeObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{forge.Rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = forgeObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = forgeObject.transform.Find("Unlock").GetComponent<Button>();
            if (forge.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (forge.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (forge.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var forgeGalleryService = ForgeGalleryService.Create();
                forgeGalleryService.UpdateStatusForgeGallery(forge.Id);
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

            Button Upgrade = forgeObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((forge.CurrentStar < forge.TempStar) && forge.Status.Equals("available"))
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
                ForgeGalleryService.Create().UpdateForgeGalleryPower(forge.Id);
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
