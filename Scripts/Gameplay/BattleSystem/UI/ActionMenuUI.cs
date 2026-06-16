using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[DefaultExecutionOrder(-197)]
public class ActionMenuUI : MonoBehaviour
{
    public static ActionMenuUI Instance { get; private set; }

    private Button moveButton;
    private Button attackButton;
    private TextMeshProUGUI pointText;
    private TextMeshProUGUI movementRangeText;

    public Transform GamePlayPanel;
    public GameObject GamePlayPanelPrefab;
    private GridCell targetCell;
    private int movementRange;
    private int movementPoint;
    private int attackRange;

    void Awake()
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
        if (UIManager.Instance == null)
        {
            Debug.LogError("[ActionMenuUI] UIManager.Instance chưa được khởi tạo!");
            return;
        }

        GamePlayPanel = UIManager.Instance.GetTransform("GamePlayPanel");
        GamePlayPanelPrefab = UIManager.Instance.Get("GamePlayPanelPrefab");

        // ĐOẠN KIỂM TRA BẮT LỖI NGAY TỪ ĐẦU LƯỢT:
        if (GamePlayPanel == null)
            Debug.LogError("[ActionMenuUI Error] Không tìm thấy Transform 'GamePlayPanel' từ UIManager!");

        if (GamePlayPanelPrefab == null)
            Debug.LogError("[ActionMenuUI Error] Không tìm thấy Prefab 'GamePlayPanelPrefab' từ UIManager! Hãy kiểm tra lại Key trong UIManager.");
    }
    /// <summary>
    /// Hiển thị Menu lựa chọn tại vị trí của Cell vừa click
    /// </summary>
    public void ShowMenu(GridCell cell, int movementRange, int movementPoint, int attackRange, Vector3 screenPosition, CardBase cardData)
    {
        ButtonEvent.Instance.Close(GamePlayPanel);
        GameObject currentObject = Instantiate(GamePlayPanelPrefab, GamePlayPanel);
        moveButton = currentObject.transform.Find("BottomPanel/Group1/MovementGroup/MovementButton").GetComponent<Button>();
        attackButton = currentObject.transform.Find("BottomPanel/Group1/AttackGroup/AttackButton").GetComponent<Button>();
        pointText = currentObject.transform.Find("BottomPanel/Group2/PointText").GetComponent<TextMeshProUGUI>();
        movementRangeText = currentObject.transform.Find("BottomPanel/Group2/MovementRangeText").GetComponent<TextMeshProUGUI>();

        pointText.text = cardData.CurrentMovementPoint.ToString();
        movementRangeText.text = cardData.Class.MovementRange.ToString();

        this.targetCell = cell;
        this.movementRange = movementRange;
        this.movementPoint = movementPoint;
        this.attackRange = attackRange;

        // Đặt vị trí UI hiển thị ngay tại vị trí thẻ bài trên màn hình
        transform.position = screenPosition;
        gameObject.SetActive(true);

        // Reset các sự kiện cũ trước khi gán sự kiện mới
        moveButton.onClick.RemoveAllListeners();
        attackButton.onClick.RemoveAllListeners();

        // Gán sự kiện khi click vào nút Move
        moveButton.onClick.AddListener(OnMoveClicked);

        // Gán sự kiện khi click vào nút Attack
        attackButton.onClick.AddListener(OnAttackClicked);
    }

    private void OnMoveClicked()
    {
        AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);

        GridManager.Instance.ShowMovementRangeAt(targetCell.GridPosition, movementRange, movementPoint);

        HideMenu();
    }

    private void OnAttackClicked()
    {
        AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);

        if (targetCell != null && targetCell.occupiedCard != null)
        {
            // Xác định xem quân cờ đang click chọn thuộc phe ta (Player) hay phe địch
            bool isPlayerCard = targetCell.IsPlayerSpawnCell;

            // Gọi hàm hiện phạm vi tấn công mới dựng bên trên
            GridManager.Instance.ShowAttackRangeAt(targetCell.GridPosition, attackRange, isPlayerCard);

            Debug.Log($"[ActionMenu] Đang hiển thị tầm đánh ({attackRange} ô) của quân cờ. Chặn chướng ngại vật & Đồng minh: {isPlayerCard}");
        }

        HideMenu();
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }

    public void UpdateMovementPoint(int currentPoint)
    {
        if (pointText != null)
        {
            pointText.text = currentPoint.ToString();
        }
    }
}