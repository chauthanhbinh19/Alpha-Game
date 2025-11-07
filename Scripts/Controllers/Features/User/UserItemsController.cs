using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserItemsController : MonoBehaviour
{
    public static UserItemsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject equipmentsPrefab;
    private GameObject itemPrefab;
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
        equipmentsPrefab = UIManager.Instance.GetGameObject("EquipmentFirstPrefab");
        itemPrefab = UIManager.Instance.GetGameObject("itemSecondPrefab");
    }
    public void CreateUserItems(List<Items> items, Transform DictionaryContentPanel)
    {
        foreach (var item in items)
        {
            GameObject itemObject = Instantiate(itemPrefab, DictionaryContentPanel);

            TextMeshProUGUI Title = itemObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            Title.text = item.Name.Replace("_", " ");

            RawImage Image = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(item.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = itemObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                MainMenuDetailsManager.Instance.PopupDetails(item, MainPanel);
            });

            RawImage itemBackgroundImage = itemObject.transform.Find("ItemBackground").GetComponent<RawImage>();
            itemBackgroundImage.gameObject.SetActive(false);

            TextMeshProUGUI itemQuantityText = itemObject.transform.Find("ItemQuantity").GetComponent<TextMeshProUGUI>();
            itemQuantityText.text = item.Quantity.ToString();
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(130, 145);
        }
        DictionaryContentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
