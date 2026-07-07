using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

[Serializable]
public class BattlePhaseIcon
{
    public BattlePhaseType PhaseType;
    public Texture Texture;
}

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

    [Header("Phase Timeline")]
    public GameObject PhasePrefab;
    public List<BattlePhaseIcon> PhaseIcons = new List<BattlePhaseIcon>();

    private Transform NextPhaseGroup;
    private Transform PreviousPhaseGroup;
    private RawImage CurrentPhaseImage;
    private TextMeshProUGUI CurrentPhaseText;
    private Texture DefaultPhaseTexture;
    private GameObject CurrentGamePlayPanelObject;
    private readonly Dictionary<BattlePhaseDefinition, GameObject> NextPhaseItems = new Dictionary<BattlePhaseDefinition, GameObject>();

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
        SubscribeTurnManagerEvents();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (TurnManager.Instance != null)
            {
                TurnManager.Instance.AdvanceToNextPhase();

                if (CurrentGamePlayPanelObject == null || !gameObject.activeSelf)
                {
                    ShowBattleTimeline();
                }
            }
        }
    }

    private void OnDestroy()
    {
        UnsubscribeTurnManagerEvents();
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
        GameObject currentObject = CreateGamePlayPanelObject();
        if (currentObject == null)
        {
            return;
        }

        CurrentGamePlayPanelObject = currentObject;
        MoveButton = currentObject.transform.Find("BottomPanel/Group1/MovementButton").GetComponent<Button>();
        AttackButton = currentObject.transform.Find("BottomPanel/Group1/AttackButton").GetComponent<Button>();
        PointText = currentObject.transform.Find("BottomPanel/Group2/PointText").GetComponent<TextMeshProUGUI>();
        MovementRangeText = currentObject.transform.Find("BottomPanel/Group2/MovementRangeText").GetComponent<TextMeshProUGUI>();

        Active1Transform = currentObject.transform.Find("BottomPanel/Group3/Active1");
        Passive1Transform = currentObject.transform.Find("BottomPanel/Group3/Passive1");
        Passive2Transform = currentObject.transform.Find("BottomPanel/Group3/Passive2");

        BindPhaseTimeline(currentObject.transform);
        BuildPhaseTimelineFromCurrentTurn();

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

    public void ShowBattleTimeline()
    {
        GameObject currentObject = CreateGamePlayPanelObject();
        if (currentObject == null)
        {
            return;
        }

        CurrentGamePlayPanelObject = currentObject;
        BindPhaseTimeline(currentObject.transform);
        BuildPhaseTimelineFromCurrentTurn();

        Transform bottomPanel = currentObject.transform.Find("BottomPanel");
        if (bottomPanel != null)
        {
            bottomPanel.gameObject.SetActive(false);
        }

        currentObject.SetActive(true);
        gameObject.SetActive(true);
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

    private GameObject CreateGamePlayPanelObject()
    {
        if (GamePlayPanel == null || GamePlayPanelPrefab == null)
        {
            Initialize();
        }

        if (GamePlayPanel == null || GamePlayPanelPrefab == null)
        {
            Debug.LogError("[ActionMenuUI] Cannot create gameplay panel because GamePlayPanel or GamePlayPanelPrefab is missing.");
            return null;
        }

        if (ButtonEvent.Instance != null)
        {
            ButtonEvent.Instance.Close(GamePlayPanel);
        }
        else
        {
            for (int i = GamePlayPanel.childCount - 1; i >= 0; i--)
            {
                Destroy(GamePlayPanel.GetChild(i).gameObject);
            }
        }

        return Instantiate(GamePlayPanelPrefab, GamePlayPanel);
    }

    private void SubscribeTurnManagerEvents()
    {
        if (TurnManager.Instance == null)
        {
            return;
        }

        TurnManager.Instance.OnTurnStarted -= HandleTurnStarted;
        TurnManager.Instance.OnPhaseStarted -= HandlePhaseStarted;
        TurnManager.Instance.OnPhaseEnded -= HandlePhaseEnded;

        TurnManager.Instance.OnTurnStarted += HandleTurnStarted;
        TurnManager.Instance.OnPhaseStarted += HandlePhaseStarted;
        TurnManager.Instance.OnPhaseEnded += HandlePhaseEnded;
    }

    private void UnsubscribeTurnManagerEvents()
    {
        if (TurnManager.Instance == null)
        {
            return;
        }

        TurnManager.Instance.OnTurnStarted -= HandleTurnStarted;
        TurnManager.Instance.OnPhaseStarted -= HandlePhaseStarted;
        TurnManager.Instance.OnPhaseEnded -= HandlePhaseEnded;
    }

    private void BindPhaseTimeline(Transform root)
    {
        NextPhaseGroup = FindChildRecursive(root, "NextPhaseGroup");
        PreviousPhaseGroup = FindChildRecursive(root, "PreviousPhaseGroup");

        Transform currentPhaseImageTransform = FindChildRecursive(root, "CurrentPhaseImage");
        if (currentPhaseImageTransform != null)
        {
            CurrentPhaseImage = currentPhaseImageTransform.GetComponent<RawImage>();
        }

        Transform currentPhaseTextTransform = FindChildRecursive(root, "CurrentPhaseText");
        if (currentPhaseTextTransform != null)
        {
            CurrentPhaseText = currentPhaseTextTransform.GetComponent<TextMeshProUGUI>();
        }

        if (PhasePrefab == null)
        {
            PhasePrefab = Resources.Load<GameObject>("Main Feature/Prefabs/Component/PhasePrefab");
        }

        if (PhasePrefab != null)
        {
            RawImage prefabImage = PhasePrefab.GetComponent<RawImage>();
            if (prefabImage != null)
            {
                DefaultPhaseTexture = prefabImage.texture;
            }
        }

        ClearCurrentPhase();
    }

    private void BuildPhaseTimelineFromCurrentTurn()
    {
        ClearPhaseTimelineGroups();

        if (TurnManager.Instance == null || NextPhaseGroup == null || PreviousPhaseGroup == null)
        {
            return;
        }

        BattleTurnDefinition turnPlan = TurnManager.Instance.GetCurrentTurnPlan();
        if (turnPlan == null || turnPlan.Phases == null)
        {
            return;
        }

        BattlePhaseDefinition currentPhase = TurnManager.Instance.CurrentPhase;
        int currentPhaseIndex = turnPlan.Phases.IndexOf(currentPhase);

        for (int i = 0; i < turnPlan.Phases.Count; i++)
        {
            BattlePhaseDefinition phase = turnPlan.Phases[i];

            if (currentPhaseIndex >= 0 && i < currentPhaseIndex)
            {
                CreatePhaseItem(phase, PreviousPhaseGroup);
                continue;
            }

            if (currentPhaseIndex >= 0 && i == currentPhaseIndex)
            {
                ShowCurrentPhase(phase);
                continue;
            }

            GameObject nextItem = CreatePhaseItem(phase, NextPhaseGroup);
            if (nextItem != null)
            {
                NextPhaseItems[phase] = nextItem;
            }
        }
    }

    private void HandleTurnStarted(int turnNumber, BattleTurnDefinition turnPlan)
    {
        if (CurrentGamePlayPanelObject == null)
        {
            return;
        }

        ClearPhaseTimelineGroups();
        ClearCurrentPhase();

        if (turnPlan == null || turnPlan.Phases == null || NextPhaseGroup == null)
        {
            return;
        }

        foreach (BattlePhaseDefinition phase in turnPlan.Phases)
        {
            GameObject nextItem = CreatePhaseItem(phase, NextPhaseGroup);
            if (nextItem != null)
            {
                NextPhaseItems[phase] = nextItem;
            }
        }
    }

    private void HandlePhaseStarted(int turnNumber, BattlePhaseDefinition phase)
    {
        if (CurrentGamePlayPanelObject == null)
        {
            return;
        }

        RemoveNextPhaseItem(phase);
        ShowCurrentPhase(phase);
    }

    private void HandlePhaseEnded(int turnNumber, BattlePhaseDefinition phase)
    {
        if (CurrentGamePlayPanelObject == null)
        {
            return;
        }

        CreatePhaseItem(phase, PreviousPhaseGroup);
        ClearCurrentPhase();
    }

    private GameObject CreatePhaseItem(BattlePhaseDefinition phase, Transform parent)
    {
        if (PhasePrefab == null || parent == null || phase == null)
        {
            return null;
        }

        GameObject item = Instantiate(PhasePrefab, parent);
        item.name = $"Phase_{phase.PhaseType}";
        item.SetActive(true);
        SetupPhaseItemVisual(item, phase);
        return item;
    }

    private void SetupPhaseItemVisual(GameObject item, BattlePhaseDefinition phase)
    {
        RawImage image = item.GetComponent<RawImage>();
        if (image != null)
        {
            image.texture = GetPhaseTexture(phase);
        }

        TextMeshProUGUI text = item.GetComponentInChildren<TextMeshProUGUI>(true);
        if (text != null)
        {
            text.text = GetPhaseShortText(phase);
            text.gameObject.SetActive(true);
        }
    }

    private void ShowCurrentPhase(BattlePhaseDefinition phase)
    {
        if (phase == null)
        {
            ClearCurrentPhase();
            return;
        }

        if (CurrentPhaseImage != null)
        {
            CurrentPhaseImage.texture = GetPhaseTexture(phase);
            CurrentPhaseImage.enabled = true;
        }

        if (CurrentPhaseText != null)
        {
            CurrentPhaseText.text = GetPhaseDisplayText(phase);
            CurrentPhaseText.gameObject.SetActive(true);
        }
    }

    private void ClearCurrentPhase()
    {
        if (CurrentPhaseImage != null)
        {
            CurrentPhaseImage.texture = DefaultPhaseTexture;
            CurrentPhaseImage.enabled = DefaultPhaseTexture != null;
        }

        if (CurrentPhaseText != null)
        {
            CurrentPhaseText.text = string.Empty;
        }
    }

    private void RemoveNextPhaseItem(BattlePhaseDefinition phase)
    {
        if (phase == null)
        {
            return;
        }

        if (NextPhaseItems.TryGetValue(phase, out GameObject nextItem))
        {
            if (nextItem != null)
            {
                Destroy(nextItem);
            }

            NextPhaseItems.Remove(phase);
        }
    }

    private void ClearPhaseTimelineGroups()
    {
        NextPhaseItems.Clear();
        ClearChildren(NextPhaseGroup);
        ClearChildren(PreviousPhaseGroup);
    }

    private void ClearChildren(Transform parent)
    {
        if (parent == null)
        {
            return;
        }

        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    private Texture GetPhaseTexture(BattlePhaseDefinition phase)
    {
        if (phase != null)
        {
            foreach (BattlePhaseIcon icon in PhaseIcons)
            {
                if (icon != null && icon.PhaseType == phase.PhaseType && icon.Texture != null)
                {
                    return icon.Texture;
                }
            }
        }

        return DefaultPhaseTexture;
    }

    private string GetPhaseDisplayText(BattlePhaseDefinition phase)
    {
        if (TurnManager.Instance != null)
        {
            return TurnManager.Instance.GetPhaseDisplayName(phase);
        }

        if (phase == null)
        {
            return string.Empty;
        }

        return string.IsNullOrWhiteSpace(phase.PhaseName) ? phase.PhaseType.ToString() : phase.PhaseName;
    }

    private string GetPhaseShortText(BattlePhaseDefinition phase)
    {
        if (phase == null)
        {
            return string.Empty;
        }

        switch (phase.PhaseType)
        {
            case BattlePhaseType.Start:
                return "S";
            case BattlePhaseType.Preparation:
                return "P";
            case BattlePhaseType.Shopping:
                return "Shop";
            case BattlePhaseType.Battle:
                return "B";
            case BattlePhaseType.Boss:
                return "Boss";
            case BattlePhaseType.Reward:
                return "R";
            case BattlePhaseType.End:
                return "E";
            default:
                return string.IsNullOrWhiteSpace(phase.PhaseName) ? "?" : phase.PhaseName;
        }
    }

    private Transform FindChildRecursive(Transform parent, string childName)
    {
        if (parent == null)
        {
            return null;
        }

        if (parent.name == childName)
        {
            return parent;
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform found = FindChildRecursive(parent.GetChild(i), childName);
            if (found != null)
            {
                return found;
            }
        }

        return null;
    }
}
