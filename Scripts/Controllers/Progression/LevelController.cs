using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    private Transform MainPanel;
    public GameObject LevelPanelPrefab;
    private GameObject currentPanel;
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
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        LevelPanelPrefab = UIManager.Instance.Get("LevelPanelPrefab");
    }
    public void CreateLevelPanel(IStats stat, ItemExperienceDTO itemExp, int maxLevel, Func<int, double> expRule)
    {
        currentPanel = Instantiate(LevelPanelPrefab, MainPanel);
        Transform panelTransform = currentPanel.transform;

        // Tìm các component trên UI
        TextMeshProUGUI currentLevelText = panelTransform.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nextLevelText = panelTransform.Find("NextLevel").GetComponent<TextMeshProUGUI>();
        Slider progressionSlider = panelTransform.Find("ProgressionSlider").GetComponent<Slider>();
        Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();
        TextMeshProUGUI experienceText = panelTransform.Find("ExperienceText").GetComponent<TextMeshProUGUI>();

        // Đã đổi tên quantityText thành userItemQuantityText hoặc itemUsedQuantityText tùy bạn muốn hiển thị ở đâu, 
        // ở đây mình cập nhật cả 2 để thể hiện rõ: "Sở hữu" và "Tiêu hao"
        TextMeshProUGUI userItemQuantityText = panelTransform.Find("UserItemQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemUsedQuantityText = panelTransform.Find("ItemUsedQuantityText").GetComponent<TextMeshProUGUI>();

        RawImage userItemImage = panelTransform.Find("UserItemImage").GetComponent<RawImage>();
        RawImage itemUsedImage = panelTransform.Find("ItemUsedImage").GetComponent<RawImage>();

        Button increaseOne = panelTransform.Find("IncreaseOneLevelButton").GetComponent<Button>();
        Button increaseTen = panelTransform.Find("IncreaseTenLevelButton").GetComponent<Button>();
        Button increaseMax = panelTransform.Find("IncreaseMaxLevelButton").GetComponent<Button>();
        Button decreaseOne = panelTransform.Find("DecreaseOneLevelButton").GetComponent<Button>();
        Button decreaseTen = panelTransform.Find("DecreaseTenLevelButton").GetComponent<Button>();
        Button decreaseMax = panelTransform.Find("DecreaseMaxLevelButton").GetComponent<Button>();
        Button confirm = panelTransform.Find("ConfirmButton").GetComponent<Button>();
        Button close = panelTransform.Find("CloseButton").GetComponent<Button>();

        // Khởi tạo các giá trị ban đầu từ nhân vật/vật phẩm
        int currentLevel = stat.Level;
        double currentExp = stat.Experience;
        int targetLevel = currentLevel;

        // Giả định mỗi 1 itemExp cung cấp một lượng EXP nhất định (Ví dụ: itemExp.ExperienceValue)
        // Nếu DTO của bạn không có, bạn hãy thay 'itemExp.ExperienceValue' bằng giá trị EXP của 1 cuốn sách/viên đá kinh nghiệm.
        double expPerItem = itemExp.ExperienceValue > 0 ? itemExp.ExperienceValue : 100;

        // Cấu hình hình ảnh
        string texturePath = ImageHelper.RemoveImageExtension(itemExp.Image);
        userItemImage.texture = TextureHelper.LoadTexture2DCached(texturePath);
        itemUsedImage.texture = TextureHelper.LoadTexture2DCached(texturePath);

        // Cấu hình Slider số lượng vật phẩm
        quantitySlider.minValue = 0;
        // Max slider có thể là số lượng tối đa cần để up lên Max Level hoặc số lượng user đang có tùy bạn chọn.
        quantitySlider.maxValue = Mathf.Max((float)itemExp.Quantity, 1);

        // Lắng nghe sự kiện kéo Slider vật phẩm
        quantitySlider.onValueChanged.AddListener((value) =>
        {
            int itemsToUse = Mathf.RoundToInt(value);
            CalculateLevelFromItems(itemsToUse);
            RefreshUI(itemsToUse);
        });

        // Gọi Refresh lần đầu
        RefreshUI(0);

        #region Local Functions

        // Hàm tính toán xem với số lượng item dùng thì sẽ lên được cấp bao nhiêu
        void CalculateLevelFromItems(int itemsToUse)
        {
            double totalExpGained = itemsToUse * expPerItem;
            double tempExp = currentExp + totalExpGained;
            int tempLevel = currentLevel;

            while (tempLevel < maxLevel)
            {
                double requiredExpForNext = expRule(tempLevel);
                if (tempExp >= requiredExpForNext)
                {
                    tempExp -= requiredExpForNext;
                    tempLevel++;
                }
                else
                {
                    break;
                }
            }
            targetLevel = tempLevel;
        }

        // Hàm tính ngược lại: Từ targetLevel mong muốn cần bao nhiêu items
        int CalculateItemsNeededForTarget()
        {
            if (targetLevel == currentLevel) return 0;

            double totalExpNeeded = 0;
            // Cộng dồn lượng EXP cần từ cấp hiện tại đến targetLevel
            for (int i = currentLevel; i < targetLevel; i++)
            {
                totalExpNeeded += expRule(i);
            }
            // Trừ đi số EXP hiện có sẵn của nhân vật
            totalExpNeeded -= currentExp;
            if (totalExpNeeded < 0) totalExpNeeded = 0;

            // Tính ra số lượng item cần (Làm tròn lên)
            return Mathf.CeilToInt((float)(totalExpNeeded / expPerItem));
        }

        void RefreshUI(int itemsToUse)
        {
            // 1. Cập nhật chữ hiển thị Level
            currentLevelText.text = currentLevel.ToString();
            nextLevelText.text = (targetLevel >= maxLevel) ? "MAX" : targetLevel.ToString();

            // 2. Cập nhật Slider và Text tiến trình EXP dựa trên TARGET LEVEL mới
            double requiredExpForTarget = expRule(targetLevel);

            // Nếu dùng slider kéo trực tiếp, tính toán lại lượng EXP hiển thị tại target level đó
            double totalExpGained = itemsToUse * expPerItem;
            double remainingExpInTargetLevel = currentExp + totalExpGained;
            for (int i = currentLevel; i < targetLevel; i++)
            {
                remainingExpInTargetLevel -= expRule(i);
            }

            if (targetLevel >= maxLevel)
            {
                progressionSlider.value = 1f;
                experienceText.text = "MAX";
            }
            else
            {
                progressionSlider.value = (float)(remainingExpInTargetLevel / requiredExpForTarget);
                experienceText.text = $"{remainingExpInTargetLevel:N0}/{requiredExpForTarget:N0}";
            }

            // 3. Hiển thị số lượng sở hữu và số lượng tiêu hao
            userItemQuantityText.text = NumberFormatterHelper.FormatNumber(itemExp.Quantity, true);
            itemUsedQuantityText.text = NumberFormatterHelper.FormatNumber(itemsToUse, true);

            // Đổi màu chữ nếu vượt quá số lượng đang có
            if (itemsToUse > itemExp.Quantity)
            {
                itemUsedQuantityText.color = Color.red;
            }
            else
            {
                itemUsedQuantityText.color = Color.white; // Hoặc màu mặc định của bạn
            }

            // Cập nhật giá trị Slider (Chỉ áp dụng khi bấm nút, tránh loop sự kiện kéo slider)
            quantitySlider.SetValueWithoutNotify(itemsToUse);
        }

        void OnButtonClickRefresh()
        {
            int itemsNeeded = CalculateItemsNeededForTarget();
            RefreshUI(itemsNeeded);
        }

        #endregion

        #region Button Events listeners

        increaseOne.onClick.AddListener(() =>
        {
            targetLevel = Mathf.Min(targetLevel + 1, maxLevel);
            OnButtonClickRefresh();
        });

        increaseTen.onClick.AddListener(() =>
        {
            targetLevel = Mathf.Min(targetLevel + 10, maxLevel);
            OnButtonClickRefresh();
        });

        increaseMax.onClick.AddListener(() =>
        {
            targetLevel = maxLevel;
            OnButtonClickRefresh();
        });

        decreaseOne.onClick.AddListener(() =>
        {
            targetLevel = Mathf.Max(targetLevel - 1, currentLevel);
            OnButtonClickRefresh();
        });

        decreaseTen.onClick.AddListener(() =>
        {
            targetLevel = Mathf.Max(targetLevel - 10, currentLevel);
            OnButtonClickRefresh();
        });

        decreaseMax.onClick.AddListener(() =>
        {
            targetLevel = currentLevel;
            OnButtonClickRefresh();
        });

        close.onClick.AddListener(() =>
        {
            Destroy(currentPanel); // Sửa lỗi: Hủy toàn bộ panel thay vì chỉ hủy Transform
        });

        confirm.onClick.AddListener(() =>
        {
            int finalItemsUsed = CalculateItemsNeededForTarget();
            if (finalItemsUsed > itemExp.Quantity)
            {
                Debug.LogError("Không đủ vật phẩm nâng cấp!");
                return; // Ngăn không cho nâng cấp nếu thiếu item
            }

            Debug.Log($"Xác nhận nâng cấp: Level {currentLevel} -> {targetLevel}. Tiêu hao: {finalItemsUsed} items.");

            // Thực hiện logic trừ item và cộng cấp độ thực tế của bạn tại đây...

            Destroy(currentPanel);
        });

        #endregion
    }
}