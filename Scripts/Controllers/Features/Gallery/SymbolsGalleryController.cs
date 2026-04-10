using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SymbolsGalleryController : MonoBehaviour
{
    public static SymbolsGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject SymbolBlockButtonPrefab;
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
        SymbolBlockButtonPrefab = UIManager.Instance.Get("SymbolBlockButtonPrefab");
    }
    public void CreateSymbolsGallery(List<Symbols> symbols, Transform contentPanel)
    {
        foreach (var symbol in symbols)
        {
            try
            {
                GameObject symbolObject = Instantiate(SymbolBlockButtonPrefab, contentPanel);

                TextMeshProUGUI titleText = symbolObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                titleText.text = symbol.Name.Replace("_", " ");

                RawImage image = symbolObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(symbol.Image);
                Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                image.texture = texture;

                // Kích thước của RawImage (khung hiển thị)
                RectTransform rect = image.GetComponent<RectTransform>();
                float maxWidth = rect.rect.width;
                float maxHeight = rect.rect.height;

                // Kích thước thật của texture
                float texWidth = texture.width;
                float texHeight = texture.height;

                // Tính scale để texture nằm gọn trong khung
                float widthRatio = maxWidth / texWidth;
                float heightRatio = maxHeight / texHeight;
                float finalScale = Mathf.Min(widthRatio, heightRatio);  // scale nhỏ nhất

                // Áp dụng scale theo tỉ lệ đúng
                image.SetNativeSize();
                image.transform.localScale = new Vector3(finalScale, finalScale, 1f);

                RawImage backgroundImage = symbolObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SYMBOL_BUTTON_BACKGROUND_URL);

                Button button = symbolObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(symbol, MainPanel);
                });
                // cardImage.SetNativeSize();
                // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

                TextMeshProUGUI rareText = symbolObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
                rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(symbol.Rare));
                rareText.text = symbol.Rare;

                RawImage blockImage = symbolObject.transform.Find("Block").GetComponent<RawImage>();
                Button Unlock = symbolObject.transform.Find("UnlockButton").GetComponent<Button>();
                if (symbol.Status.Equals(AppConstants.Status.AVAILABLE))
                {
                    blockImage.gameObject.SetActive(false);
                    Unlock.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (symbol.Status.Equals(AppConstants.Status.PENDING))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(true);
                }
                else if (symbol.Status.Equals(AppConstants.Status.BLOCK))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(false);
                }

                Unlock.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    var symbolGalleryService = SymbolsGalleryService.Create();
                    await symbolGalleryService.UpdateStatusSymbolGalleryAsync(symbol.Id);
                    blockImage.gameObject.SetActive(false);
                    Unlock.gameObject.SetActive(false);
                    image.color = Color.white;

                    var powerManagerService = PowerManagerService.Create();
                    var teamsService = TeamsService.Create();

                    await powerManagerService.UpdateUserStatsAsync(User.CurrentUserId);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                });

                Button Upgrade = symbolObject.transform.Find("UpgradeButton").GetComponent<Button>();
                if ((symbol.CurrentStar < symbol.TempStar) && symbol.Status.Equals(AppConstants.Status.AVAILABLE))
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
                    await SymbolsGalleryService.Create().UpdateSymbolGalleryPowerAsync(symbol.Id);
                });
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 240);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
