using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AvatarsGalleryController : MonoBehaviour
{
    public static AvatarsGalleryController Instance { get; private set; }
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
    public void CreateAvatarsGallery(List<Avatars> Avatars, Transform DictionaryContentPanel)
    {
        foreach (var Avatar in Avatars)
        {
            GameObject AvatarObject = Instantiate(equipmentsPrefab, DictionaryContentPanel);

            Text Title = AvatarObject.transform.Find("Title").GetComponent<Text>();
            Title.text = Avatar.name.Replace("_", " ");

            RawImage Image = AvatarObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Avatar.image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            Image.SetNativeSize();
            Image.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);

            Button button = AvatarObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                PopupDetailsManager.Instance.PopupDetails(Avatar, MainPanel);
            });

            RawImage rareImage = AvatarObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Avatar.rare}");
            rareImage.texture = rareTexture;

            RawImage rareBackgroundImage = AvatarObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareImage.gameObject.SetActive(false);
            rareBackgroundImage.gameObject.SetActive(false);

            RawImage blockImage = AvatarObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = AvatarObject.transform.Find("Unlock").GetComponent<Button>();
            if (Avatar.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
                Image.color = Color.white;
            }
            else if (Avatar.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (Avatar.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            Unlock.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                var AvatarGalleryService = AvatarsGalleryService.Create();
                AvatarGalleryService.UpdateStatusAvatarsGallery(Avatar.id);
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

            Button Upgrade = AvatarObject.transform.Find("UpgradeButton").GetComponent<Button>();
            if ((Avatar.current_star < Avatar.temp_star) && Avatar.status.Equals("available"))
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
                AvatarsGalleryService.Create().UpdateAvatarsGalleryPower(Avatar.id);
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
