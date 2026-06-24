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
        // Xóa bớt animation cũ nếu có để tránh lỗi chồng đè
        var oldAnim = contentPanel.GetComponent<StaggeredSlideAnimation>();
        if (oldAnim != null) Destroy(oldAnim);

        // Cache texture background dùng chung một lần duy nhất ngoài vòng lặp
        Texture bgTexture = TextureHelper.LoadTextureCached(ImageConstants.Background.EQUIPMENT_BUTTON_BACKGROUND_URL);

        foreach (var equipment in equipments)
        {
            try
            {
                GameObject equipmentObject = Instantiate(EquipmentButtonPrefab, contentPanel);
                Transform transform = equipmentObject.transform;

                TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                titleText.text = equipment.Name.Replace("_", " ");

                RawImage image = transform.Find("Image").GetComponent<RawImage>();
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(equipment.Image);
                Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
                image.texture = texture;

                ImageManager.Instance.ChangeSizeImageByTextureScale(image, texture);

                RawImage backgroundImage = transform.Find("RectMask2/Background").GetComponent<RawImage>();
                backgroundImage.texture = bgTexture;

                Button button = transform.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupDetailsManager.Instance.PopupDetails(equipment, MainPanel);
                });

                TextMeshProUGUI rareText = transform.Find("RareText").GetComponent<TextMeshProUGUI>();
                rareText.color = ColorHelper.HexToColor(QualityEvaluatorHelper.CheckRareColor(equipment.Rarity));
                rareText.text = equipment.Rarity;
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
