using System;
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
    private GameObject ItemSecondButtonPrefab;
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
        equipmentsPrefab = UIManager.Instance.Get("EquipmentFirstPrefab");
        ItemSecondButtonPrefab = UIManager.Instance.Get("ItemSecondButtonPrefab");
    }
    public void CreateUserItems(List<Items> items, Transform contentPanel)
    {
        foreach (var item in items)
        {
            try
            {
                GameObject itemObject = Instantiate(ItemSecondButtonPrefab, contentPanel);

                TextMeshProUGUI Title = itemObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                Title.text = item.Name.Replace("_", " ");

                RawImage image = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(item.Image);
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

                Button button = itemObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    MainMenuDetailsManager.Instance.PopupDetails(item, MainPanel);
                });

                Image itemBackgroundImage = itemObject.transform.Find("ItemBackground").GetComponent<Image>();
                itemBackgroundImage.gameObject.SetActive(true);

                TextMeshProUGUI itemQuantityText = itemObject.transform.Find("ItemQuantity").GetComponent<TextMeshProUGUI>();
                itemQuantityText.text = NumberFormatter.FormatNumber(item.Quantity, true).ToString();
            }
            catch (Exception ex)
            {
                Debug.Log("Error:" + ex.Message);
            }
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(123, 150);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
