using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserBooksController : MonoBehaviour
{
    public static UserBooksController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject cardsPrefab;
    private GameObject PositionPrefab;
    private GameObject MainMenuDetailPanel2Prefab;
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
        cardsPrefab = UIManager.Instance.Get("CardsPrefab");
        PositionPrefab = UIManager.Instance.Get("PositionPrefab");
        MainMenuDetailPanel2Prefab = UIManager.Instance.Get("MainMenuDetailPanel2Prefab");
    }
    public void CreateUserBooks(List<Books> books, Transform contentPanel)
    {
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        // Cache texture background dùng chung một lần duy nhất ngoài vòng lặp
        Texture bgTexture = TextureHelper.LoadTextureCached(ImageConstants.Background.BOOK_BUTTON_BACKGROUND_URL);

        foreach (var book in books)
        {
            GameObject bookObject = Instantiate(cardsPrefab, contentPanel);
            Transform transform = bookObject.transform;

            Text titleText = transform.Find("Title").GetComponent<Text>();
            titleText.text = book.Name.Replace("_", " ");

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(book.Image);
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

            RawImage backgroundImage = transform.Find("RectMask2/Background").GetComponent<RawImage>();
            backgroundImage.texture = bgTexture;

            TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
            rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(book.Rare));
            rareText.text = book.Rare;

            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                ShowBookDetails(book);
            });

            GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(260, 320);
            }
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateUserBooksForSummon(List<Books> books, Transform PositionPanel)
    {
        foreach (var book in books)
        {
            GameObject bookObject = Instantiate(PositionPrefab, PositionPanel);
            Transform transform = bookObject.transform;

            RawImage image = transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(book.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            image.texture = texture;
            image.SetNativeSize();

            // Chỉnh width và height
            RectTransform rectTransform = image.rectTransform;
            // rectTransform.sizeDelta = new Vector2(300f, 375f);
            if (texture.width < 1400 && texture.height < 1400 && texture.width > 700 && texture.height > 700)
            {
                image.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else if (texture.width > 1000 && texture.height <= 2100 && texture.width < 2000 && texture.height > 1000)
            {
                image.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            }
            else if (texture.width <= 700 && texture.height <= 700)
            {
                image.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            else if (texture.width <= 700 && texture.height <= 1100)
            {
                image.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else if (texture.width > 700 && texture.height <= 700)
            {
                image.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else
            {
                image.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
            GridLayoutGroup gridLayout = PositionPanel.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.spacing = new Vector2(100, 0);
            }
        }
    }
    public void ShowBookDetails(Books book, int buttonType = 1)
    {
        GameObject currentObject = Instantiate(MainMenuDetailPanel2Prefab, MainPanel);
        Transform transform = currentObject.transform;
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Transform setButtonGroupPanel = transform.Find("DictionaryCards/SetButtonGroup/Viewport/Content");
        RawImage cardBackground = transform.Find("DictionaryCards/Background").GetComponent<RawImage>();
        RawImage backgroundCircle1Image = transform.Find("DictionaryCards/CircleImage1").GetComponent<RawImage>();
        ButtonLoader.Instance.CreateSetButtonGroup(book, setButtonGroupPanel);
        backgroundCircle1Image.gameObject.AddComponent<RotateAnimation>();

        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BOOK);
        Texture texture = TextureHelper.LoadTextureCached(ImageConstants.Background.BOOK_BACKGROUND_URL);
        cardBackground.texture = texture;
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
            MainMenuManager.Instance.GetType(AppConstants.MainType.BOOK);
        });
        CreateDetailsUI(book, currentObject);
    }
    public void CreateDetailsUI(Books book, GameObject currentObject)
    {
        RawImage image = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(book.Image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");

        ImageManager.Instance.ChangeSizeImage(image, texture, 700f);
        image.texture = texture;

        TextMeshProUGUI nameText = transform.Find("DictionaryCards/Name/NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = book.Name;

        TextMeshProUGUI powerText = transform.Find("DictionaryCards/Power/PowerText").GetComponent<TextMeshProUGUI>();
        powerText.text = NumberFormatterHelper.FormatNumber(book.Power, false);

        TextMeshProUGUI levelText = transform.Find("DictionaryCards/DetailsPanel/Group1/Level/LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = book.Level.ToString();

        RawImage rareImage = transform.Find("DictionaryCards/DetailsPanel/Group2/Rare/RareImage").GetComponent<RawImage>();
        Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{book.Rare}");
        rareImage.texture = rareTexture;

        Transform starGridLayout = transform.Find("DictionaryCards/DetailsPanel/Group2/Star/GridLayout");
        TextureHelper.SetupStars(starGridLayout, book.Star);

        // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

        SetupStat(transform, "Power", AppConstants.StatFields.POWER, AppDisplayConstants.StatFieldsShort.POWER, book.Power);
        SetupStat(transform, "Health", AppConstants.StatFields.HEALTH, AppDisplayConstants.StatFieldsShort.HEALTH, book.Health);
        SetupStat(transform, "PhysicalAttack", AppConstants.StatFields.PHYSICAL_ATTACK, AppDisplayConstants.StatFieldsShort.PHYSICAL_ATTACK, book.PhysicalAttack);
        SetupStat(transform, "PhysicalDefense", AppConstants.StatFields.PHYSICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.PHYSICAL_DEFENSE, book.PhysicalDefense);
        SetupStat(transform, "MagicalAttack", AppConstants.StatFields.MAGICAL_ATTACK, AppDisplayConstants.StatFieldsShort.MAGICAL_ATTACK, book.MagicalAttack);
        SetupStat(transform, "MagicalDefense", AppConstants.StatFields.MAGICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MAGICAL_DEFENSE, book.MagicalDefense);
        SetupStat(transform, "ChemicalAttack", AppConstants.StatFields.CHEMICAL_ATTACK, AppDisplayConstants.StatFieldsShort.CHEMICAL_ATTACK, book.ChemicalAttack);
        SetupStat(transform, "ChemicalDefense", AppConstants.StatFields.CHEMICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.CHEMICAL_DEFENSE, book.ChemicalDefense);
        SetupStat(transform, "AtomicAttack", AppConstants.StatFields.ATOMIC_ATTACK, AppDisplayConstants.StatFieldsShort.ATOMIC_ATTACK, book.AtomicAttack);
        SetupStat(transform, "AtomicDefense", AppConstants.StatFields.ATOMIC_DEFENSE, AppDisplayConstants.StatFieldsShort.ATOMIC_DEFENSE, book.AtomicDefense);
        SetupStat(transform, "MentalAttack", AppConstants.StatFields.MENTAL_ATTACK, AppDisplayConstants.StatFieldsShort.MENTAL_ATTACK, book.MentalAttack);
        SetupStat(transform, "MentalDefense", AppConstants.StatFields.MENTAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MENTAL_DEFENSE, book.MentalDefense);
        SetupStat(transform, "Speed", AppConstants.StatFields.SPEED, AppDisplayConstants.StatFieldsShort.SPEED, book.Speed);

        Button detailButton = transform.Find("DictionaryCards/DetailsPanel/Group4/Stats/DetailButton").GetComponent<Button>();
        detailButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            StatsManager.Instance.CreateStatsManager(book);
        });
    }
    private void SetupStat(Transform root, string statObjectName, string statField, string statDisplayName, double value, bool isPercent = false)
    {
        Transform statTransform = root.Find($"DictionaryCards/DetailsPanel/Group4/Stats/GridLayout/{statObjectName}");

        RawImage iconImage = statTransform.Find("IconImage").GetComponent<RawImage>();
        TextMeshProUGUI titleText = statTransform.Find("StatTitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI valueText = statTransform.Find("StatText").GetComponent<TextMeshProUGUI>();

        TextureHelper.CreatePropertyRuneUI(statField, iconImage);

        titleText.text = LocalizationManager.Get(statDisplayName);
        titleText.enableWordWrapping = false;

        if (isPercent)
        {
            valueText.text = NumberFormatterHelper.FormatNumberExtended(value, true) + " %";
        }
        else
        {
            valueText.text = NumberFormatterHelper.FormatNumberExtended(value, true);
        }
    }
}
