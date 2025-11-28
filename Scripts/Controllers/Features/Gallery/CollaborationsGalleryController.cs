using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollaborationsGalleryController : MonoBehaviour
{
    public static CollaborationsGalleryController Instance { get; private set; }
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
    public void CreateCollaborationGallery(List<Collaborations> collaborationList, Transform DictionaryContentPanel)
    {
        foreach (var collaboration in collaborationList)
        {
            GameObject collaborationObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = collaborationObject.transform.Find("Title").GetComponent<Text>();
            Title.text = collaboration.Name.Replace("_", " ");

            RawImage Image = collaborationObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(collaboration.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = collaborationObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                PopupDetailsManager.Instance.PopupDetails(collaboration, MainPanel);
            });

            RawImage rareImage = collaborationObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            rareImage.texture = rareTexture;

            Image.SetNativeSize();
            Image.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);

            RawImage blockImage = collaborationObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = collaborationObject.transform.Find("Unlock").GetComponent<Button>();
            if (collaboration.Status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (collaboration.Status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (collaboration.Status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }
            Unlock.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                var collaborationGalleryService = CollaborationGalleryService.Create();
                collaborationGalleryService.UpdateStatusCollaborationsGallery(collaboration.Id);
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

            Button Upgrade = collaborationObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((collaboration.CurrentStar < collaboration.TempStar) && collaboration.Status.Equals("available"))
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
                CollaborationGalleryService.Create().UpdateCollaborationsGalleryPower(collaboration.Id);
            });
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(280, 230);
        }
        DictionaryContentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
