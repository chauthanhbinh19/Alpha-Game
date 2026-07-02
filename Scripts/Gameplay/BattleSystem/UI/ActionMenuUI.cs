using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[DefaultExecutionOrder(-197)]
public class ActionMenuUI : MonoBehaviour
{
    public static ActionMenuUI Instance { get; private set; }

    private Button MoveButton;
    private Button AttackButton;
    private TextMeshProUGUI PointText;
    private TextMeshProUGUI MovementRangeText;
    private Transform Active1Transform;
    private Transform Passive1Transform;
    private Transform Passive2Transform;

    public Transform GamePlayPanel;
    public GameObject GamePlayPanelPrefab;
    private GridCell TargetCell;
    private int MovementRange;
    private int MovementPoint;
    private int AttackRange;

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
        MoveButton = currentObject.transform.Find("BottomPanel/Group1/MovementButton").GetComponent<Button>();
        AttackButton = currentObject.transform.Find("BottomPanel/Group1/AttackButton").GetComponent<Button>();
        PointText = currentObject.transform.Find("BottomPanel/Group2/PointText").GetComponent<TextMeshProUGUI>();
        MovementRangeText = currentObject.transform.Find("BottomPanel/Group2/MovementRangeText").GetComponent<TextMeshProUGUI>();

        Active1Transform = currentObject.transform.Find("BottomPanel/Group3/Active1");
        Passive1Transform = currentObject.transform.Find("BottomPanel/Group3/Passive1");
        Passive2Transform = currentObject.transform.Find("BottomPanel/Group3/Passive2");

        PointText.text = cardData.CurrentMovementPoint.ToString();
        MovementRangeText.text = cardData.Class.MovementRange.ToString();

        this.TargetCell = cell;
        this.MovementRange = movementRange;
        this.MovementPoint = movementPoint;
        this.AttackRange = attackRange;

        // Đặt vị trí UI hiển thị ngay tại vị trí thẻ bài trên màn hình
        transform.position = screenPosition;
        gameObject.SetActive(true);

        // Reset các sự kiện cũ trước khi gán sự kiện mới
        MoveButton.onClick.RemoveAllListeners();
        AttackButton.onClick.RemoveAllListeners();

        // Gán sự kiện khi click vào nút Move
        MoveButton.onClick.AddListener(OnMoveClicked);

        // Gán sự kiện khi click vào nút Attack
        AttackButton.onClick.AddListener(OnAttackClicked);
    }

    private void OnMoveClicked()
    {
        AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);

        GridManager.Instance.ShowMovementRangeAt(TargetCell.GridPosition, MovementRange, MovementPoint);

        HideMenu();
    }

    private void OnAttackClicked()
    {
        AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);

        if (TargetCell != null && TargetCell.OccupiedCard != null)
        {
            // Xác định xem quân cờ đang click chọn thuộc phe ta (Player) hay phe địch
            bool isPlayerCard = TargetCell.IsPlayerSpawnCell;

            // Gọi hàm hiện phạm vi tấn công mới dựng bên trên
            GridManager.Instance.ShowAttackRangeAt(TargetCell.GridPosition, AttackRange, isPlayerCard);

            Debug.Log($"[ActionMenu] Đang hiển thị tầm đánh ({AttackRange} ô) của quân cờ. Chặn chướng ngại vật & Đồng minh: {isPlayerCard}");
        }

        HideMenu();
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }

    public void UpdateMovementPoint(int currentPoint)
    {
        if (PointText != null)
        {
            PointText.text = currentPoint.ToString();
        }
    }
}