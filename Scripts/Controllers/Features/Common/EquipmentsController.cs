using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentsController : MonoBehaviour
{
    public static EquipmentsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject EquipmentButtonPrefab;
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
        EquipmentButtonPrefab = UIManager.Instance.Get("EquipmentButtonPrefab");
    }
    public void CreateEquipmentsGallery(List<Equipments> equipments, Transform contentPanel)
    {
        foreach (var equipment in equipments)
        {
            try
            {
                GameObject equipmentObject = Instantiate(EquipmentButtonPrefab, contentPanel);

                TextMeshProUGUI titleText = equipmentObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                titleText.text = equipment.Name.Replace("_", " ");

                RawImage image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(equipment.Image);
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

                RawImage backgroundImage = equipmentObject.transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.EQUIPMENT_BUTTON_BACKGROUND_URL);

                Button button = equipmentObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(equipment, MainPanel);
                });

                TextMeshProUGUI rareText = equipmentObject.transform.Find("RareText").GetComponent<TextMeshProUGUI>();
                rareText.color = ColorHelper.ToColor(QualityEvaluator.CheckRareColor(equipment.Rare));
                rareText.text = equipment.Rare;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        GridLayoutGroup gridLayout = contentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 230);
        }
        contentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
}
