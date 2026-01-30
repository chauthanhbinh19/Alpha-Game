using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BooksGalleryController : MonoBehaviour
{
    public static BooksGalleryController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject BookBlockButtonPrefab;
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
        BookBlockButtonPrefab = UIManager.Instance.Get("BookBlockButtonPrefab");
    }
    public void CreateBooksGallery(List<Books> books, Transform contentPanel)
    {
        foreach (var book in books)
        {
            try
            {
                GameObject bookObject = Instantiate(BookBlockButtonPrefab, contentPanel);

                TextMeshProUGUI Title = bookObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                Title.text = book.Name.Replace("_", " ");

                RawImage image = bookObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(book.Image);
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
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

                RawImage backgroundImage = bookObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = Resources.Load<Texture>(ImageConstants.Background.BOOK_BUTTON_BACKGROUND_URL);

                Button button = bookObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(book, MainPanel);
                });

                TextMeshProUGUI rareText = bookObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
                rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(book.Rare));
                rareText.text = book.Rare;

                RawImage blockImage = bookObject.transform.Find("Block").GetComponent<RawImage>();
                Button Unlock = bookObject.transform.Find("UnlockButton").GetComponent<Button>();
                if (book.Status.Equals(AppConstants.Status.AVAILABLE))
                {
                    blockImage.gameObject.SetActive(false);
                    Unlock.gameObject.SetActive(false);
                    image.color = Color.white;
                }
                else if (book.Status.Equals(AppConstants.Status.PENDING))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(true);
                }
                else if (book.Status.Equals(AppConstants.Status.BLOCK))
                {
                    blockImage.gameObject.SetActive(true);
                    Unlock.gameObject.SetActive(false);
                }

                Unlock.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    var booksGalleryService = BooksGalleryService.Create();
                    await booksGalleryService.UpdateStatusBookGalleryAsync(book.Id);
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

                Button Upgrade = bookObject.transform.Find("UpgradeButton").GetComponent<Button>();
                if ((book.CurrentStar < book.TempStar) && book.Status.Equals(AppConstants.Status.AVAILABLE))
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
                    await BooksGalleryService.Create().UpdateBookGalleryPowerAsync(book.Id);
                });
            }
            catch (Exception ex)
            {
                Debug.Log("Error:" + ex.Message);
            }
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(260, 320);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
