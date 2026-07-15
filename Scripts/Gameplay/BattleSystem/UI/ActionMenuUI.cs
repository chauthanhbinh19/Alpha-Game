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
    private Button Active1TabButton;
    private Button Passive1TabButton;
    private Button Passive2TabButton;

    public Transform GamePlayPanel;
    public GameObject GamePlayPanelPrefab;
    public GameObject SkillPrefab;
    private CardBase CardData;
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
        InitializePhaseIconsFromConstants();
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
        SkillPrefab = UIManager.Instance.Get("SkillPrefab");

        // ĐOẠN KIỂM TRA BẮT LỖI NGAY TỪ ĐẦU LƯỢT:
        if (GamePlayPanel == null)
            Debug.LogError("[ActionMenuUI Error] Không tìm thấy Transform 'GamePlayPanel' từ UIManager!");

        if (GamePlayPanelPrefab == null)
            Debug.LogError("[ActionMenuUI Error] Không tìm thấy Prefab 'GamePlayPanelPrefab' từ UIManager! Hãy kiểm tra lại Key trong UIManager.");
    }

    private void InitializePhaseIconsFromConstants()
    {
        // Xóa dữ liệu cũ phòng hờ cấu hình lỗi trong Inspector
        PhaseIcons.Clear();

        // Nạp từng cặp (Loại Phase - Texture tương ứng từ đường dẫn ImageConstants)
        AddPhaseIcon(BattlePhaseType.Start, ImageConstants.Phase.START_PHASE_URL);
        AddPhaseIcon(BattlePhaseType.Preparation, ImageConstants.Phase.PREPARATION_PHASE_URL);
        AddPhaseIcon(BattlePhaseType.Shopping, ImageConstants.Phase.SHOPPING_PHASE_URL);
        AddPhaseIcon(BattlePhaseType.Battle, ImageConstants.Phase.BATTLE_PHASE_URL);
        AddPhaseIcon(BattlePhaseType.Boss, ImageConstants.Phase.BOSS_PHASE_URL);
        AddPhaseIcon(BattlePhaseType.Reward, ImageConstants.Phase.REWARD_PHASE_URL);
        AddPhaseIcon(BattlePhaseType.End, ImageConstants.Phase.END_PHASE_URL);
        AddPhaseIcon(BattlePhaseType.Custom, ImageConstants.Phase.CUSTOM_PHASE_URL);
    }

    private void AddPhaseIcon(BattlePhaseType type, string resourcePath)
    {
        Texture tex = Resources.Load<Texture>(resourcePath);
        if (tex != null)
        {
            PhaseIcons.Add(new BattlePhaseIcon
            {
                PhaseType = type,
                Texture = tex
            });
        }
        else
        {
            Debug.LogWarning($"[ActionMenuUI] Không tìm thấy ảnh Phase tại đường dẫn Resources: {resourcePath}");
        }
    }
    /// <summary>
    /// Hiển thị Menu lựa chọn tại vị trí của Cell vừa click
    /// </summary>
    public void ShowMenu(GridCell cell, int movementRange, int movementPoint, int attackRange, Vector3 screenPosition, CardBase cardData)
    {
        GameObject currentObject = CreateGamePlayPanelObject();
        if (currentObject == null) return;

        CardData = cardData;
        CurrentGamePlayPanelObject = currentObject;

        // 1. Ánh xạ các thành phần UI cơ bản
        MoveButton = currentObject.transform.Find("BottomPanel/Group1/MovementButton").GetComponent<Button>();
        AttackButton = currentObject.transform.Find("BottomPanel/Group1/AttackButton").GetComponent<Button>();
        PointText = currentObject.transform.Find("BottomPanel/Group2/PointText").GetComponent<TextMeshProUGUI>();
        MovementRangeText = currentObject.transform.Find("BottomPanel/Group2/MovementRangeText").GetComponent<TextMeshProUGUI>();

        // Điểm lưu ý: Hãy chắc chắn trên Hierarchy các Object Active1, Passive1 nằm trong Group3 như đường dẫn này
        Active1Transform = currentObject.transform.Find("BottomPanel/Group3/Active1");
        Passive1Transform = currentObject.transform.Find("BottomPanel/Group3/Passive1");
        Passive2Transform = currentObject.transform.Find("BottomPanel/Group3/Passive2");

        ClearSkillPrefabs(Active1Transform);
        ClearSkillPrefabs(Passive1Transform);
        ClearSkillPrefabs(Passive2Transform);

        // 2. Thiết lập cấu hình hệ thống Tab nút bấm
        SetupSkillTabs(currentObject.transform);

        BindPhaseTimeline(currentObject.transform);
        BuildPhaseTimelineFromCurrentTurn();

        PointText.text = CardData.CurrentMovementPoint.ToString();
        MovementRangeText.text = CardData.Class.MovementRange.ToString();

        this.TargetCell = cell;
        this.MovementRange = movementRange;
        this.MovementPoint = movementPoint;
        this.AttackRange = attackRange;

        transform.position = screenPosition;
        gameObject.SetActive(true);

        // Reset & Gán sự kiện cho nút Di chuyển / Tấn công
        MoveButton.onClick.RemoveAllListeners();
        AttackButton.onClick.RemoveAllListeners();
        MoveButton.onClick.AddListener(OnMoveClicked);
        AttackButton.onClick.AddListener(OnAttackClicked);

        // SỬA LỖI LOGIC: Mặc định tự động kích hoạt Tab Active 1 (Position = 1) khi vừa hiện Menu
        if (Active1TabButton != null)
        {
            OnSkillTabClicked(Active1TabButton, Active1Transform, 1);
        }
    }

    private void SetupSkillTabs(Transform root)
    {
        // SỬA LỖI ĐƯỜNG DẪN: Tìm chính xác Group1 nằm trong BottomPanel để treo SkillTabs vào
        Transform bottomPanel = root.Find("BottomPanel");
        Transform group1 = bottomPanel != null ? bottomPanel.Find("Group1") : null;
        if (group1 == null)
        {
            Debug.LogError("Không tìm thấy BottomPanel/Group1 để khởi tạo SkillTabs!");
            return;
        }

        Transform tabsParent = group1.Find("SkillTabs");
        if (tabsParent == null)
        {
            GameObject tabsObject = new GameObject("SkillTabs", typeof(RectTransform));
            tabsObject.transform.SetParent(group1, false);
            tabsParent = tabsObject.transform;
        }

        // Tạo hoặc tìm các nút dựa theo cấu trúc
        Active1TabButton = GetOrCreateTabButton(tabsParent, "Active1SkillButton", "Active 1");
        Passive1TabButton = GetOrCreateTabButton(tabsParent, "Passive1SkillButton", "Passive 1");
        Passive2TabButton = GetOrCreateTabButton(tabsParent, "Passive2SkillButton", "Passive 2");

        // Làm sạch sự kiện cũ để tránh trùng lặp khi gọi lại hàm nhiều lần
        Active1TabButton.onClick.RemoveAllListeners();
        Passive1TabButton.onClick.RemoveAllListeners();
        Passive2TabButton.onClick.RemoveAllListeners();

        // Gán sự kiện Click chuẩn hóa đúng Position theo yêu cầu của bạn
        Active1TabButton.onClick.AddListener(() => OnSkillTabClicked(Active1TabButton, Active1Transform, 1));
        Passive1TabButton.onClick.AddListener(() => OnSkillTabClicked(Passive1TabButton, Passive1Transform, 2));
        Passive2TabButton.onClick.AddListener(() => OnSkillTabClicked(Passive2TabButton, Passive2Transform, 3));
    }

    private void OnSkillTabClicked(Button clicked, Transform targetGroup, int position)
    {
        // 1. Cập nhật lại kích thước chiều rộng (Nút được chọn rộng 150, nút còn lại thu về 10)
        SetTabWidthsForClicked(clicked);

        // 2. SỬA LỖI LOGIC: Tắt toàn bộ các Group nội dung trước để tránh hiển thị đè đố chữ
        if (Active1Transform != null) Active1Transform.gameObject.SetActive(false);
        if (Passive1Transform != null) Passive1Transform.gameObject.SetActive(false);
        if (Passive2Transform != null) Passive2Transform.gameObject.SetActive(false);

        // 3. Làm sạch dữ liệu Prefab cũ trong các khay chứa dữ liệu
        ClearSkillPrefabs(Active1Transform);
        ClearSkillPrefabs(Passive1Transform);
        ClearSkillPrefabs(Passive2Transform);

        // 4. Kích hoạt và nạp dữ liệu mới cho khay chứa được lựa chọn
        if (targetGroup != null)
        {
            targetGroup.gameObject.SetActive(true);
            PopulateSkillPrefabs(targetGroup, position);
        }
    }

    private Button GetOrCreateTabButton(Transform parent, string name, string label)
    {
        Transform transform = parent.Find(name);
        if (transform != null)
        {
            Button existing = transform.GetComponent<Button>();
            if (existing != null) return existing;
        }

        GameObject buttonObject = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        buttonObject.transform.SetParent(parent, false);
        RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(10f, 30f);

        Image image = buttonObject.GetComponent<Image>();
        image.color = Color.white * 0.9f;

        GameObject txtObj = new GameObject("Text", typeof(RectTransform));
        txtObj.transform.SetParent(buttonObject.transform, false);
        RectTransform txtRt = txtObj.GetComponent<RectTransform>();
        txtRt.anchorMin = Vector2.zero; txtRt.anchorMax = Vector2.one; txtRt.offsetMin = Vector2.zero; txtRt.offsetMax = Vector2.zero;
        TextMeshProUGUI tmp = txtObj.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.fontSize = 18;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.black;

        return buttonObject.GetComponent<Button>();
    }

    private void SetTabWidthsForClicked(Button clicked)
    {
        float large = 150f;
        float small = 10f;

        SetRectWidth(Active1TabButton, clicked == Active1TabButton ? large : small);
        SetRectWidth(Passive1TabButton, clicked == Passive1TabButton ? large : small);
        SetRectWidth(Passive2TabButton, clicked == Passive2TabButton ? large : small);
    }

    private void SetRectWidth(Button button, float width)
    {
        if (button == null) return;
        RectTransform image = button.transform.Find("Background").GetComponent<RectTransform>();
        if (image != null)
        {
            image.sizeDelta = new Vector2(width, image.sizeDelta.y);
        }
    }

    private void SetTabWidths(float activeWidth, float passiveWidth)
    {
        SetRectWidth(Active1TabButton, activeWidth);
        SetRectWidth(Passive1TabButton, passiveWidth);
        SetRectWidth(Passive2TabButton, passiveWidth);
    }

    private void ClearSkillPrefabs(Transform group)
    {
        if (group == null) return;
        if (group != null)
        {
            foreach (Transform child in group)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void PopulateSkillPrefabs(Transform group, int position = 1)
    {
        if (group == null) return;

        // 1. Tạo một danh sách cục bộ để lưu nhanh các GameObject được sinh ra trong Group này
        List<GameObject> spawnedPrefabs = new List<GameObject>();

        foreach (Skills skill in CardData.Skills)
        {
            if (skill.Position == position)
            {
                GameObject iconObject = Instantiate(SkillPrefab, group);
                spawnedPrefabs.Add(iconObject); // Lưu lại để tí nữa reset chéo trạng thái

                // Set hình ảnh cho skill như cũ
                RawImage rawImage = iconObject.transform.Find("Image").GetComponent<RawImage>();
                if (rawImage != null)
                {
                    rawImage.texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(skill.Image));
                }

                // Trạng thái ban đầu khi mới load: Hiện Dec1, Ẩn Dec2
                Transform dec1 = iconObject.transform.Find("Decoration1");
                Transform dec2 = iconObject.transform.Find("Decoration2");
                if (dec1 != null) dec1.gameObject.SetActive(true);
                if (dec2 != null) dec2.gameObject.SetActive(false);

                // 2. Lấy component Button có sẵn của Prefab để bắt sự kiện click
                Button button = iconObject.GetComponent<Button>();
                if (button == null) button = iconObject.AddComponent<Button>(); // Đề phòng prefab chưa có sẵn Button

                // Xóa các sự kiện cũ nếu có để tránh kích hoạt trùng lặp
                button.onClick.RemoveAllListeners();

                // 3. Sử dụng Lambda diễn giải logic click trực tiếp tại đây luôn
                button.onClick.AddListener(() =>
                {
                    // 1. Cập nhật trạng thái đóng/mở chéo giữa các skill như cũ
                    foreach (GameObject item in spawnedPrefabs)
                    {
                        Transform itemDec1 = item.transform.Find("Decoration1");
                        Transform itemDec2 = item.transform.Find("Decoration2");

                        if (item == iconObject)
                        {
                            // Đối với thằng vừa được Click vào: Dec2 = true, Dec1 = false
                            if (itemDec1 != null) itemDec1.gameObject.SetActive(false);
                            if (itemDec2 != null) itemDec2.gameObject.SetActive(true);
                        }
                        else
                        {
                            // Đối với tất cả những thằng còn lại trong nhóm: Dec1 = true, Dec2 = false
                            if (itemDec1 != null) itemDec1.gameObject.SetActive(true);
                            if (itemDec2 != null) itemDec2.gameObject.SetActive(false);
                        }
                    }

                    // 2. TÍNH NĂNG MỚI: Gọi GridManager hiển thị vùng Target kĩ năng lên bàn cờ
                    if (GridManager.Instance != null)
                    {
                        // Giả sử 'currentCardCell' là GridCell mà quân cờ hiện tại của bạn đang đứng trên Grid.
                        // Giả sử 'skill' có biến '.Range' và '.SkillType' hoặc '.EffectAction.ActionCode' từ database gửi về.

                        // Ví dụ minh họa lệnh gọi:
                        GridCell currentRealCell = GridManager.Instance.GetCellOfCard(CardData);

                        if (currentRealCell != null)
                        {
                            GridManager.Instance.ShowSkillAttackRange(
                                currentRealCell.GridPosition,
                                AttackRange,
                                skill.SkillSubType.SubTypeCode,
                                CardData
                            );
                        }
                    }

                    // 3. Tìm CloseButton bên trong Decoration2 của chính thằng vừa click để dọn dẹp khi đóng
                    if (dec2 != null)
                    {
                        Transform closeButtonTransform = dec2.Find("CloseButton");
                        if (closeButtonTransform != null)
                        {
                            Button closeButton = closeButtonTransform.GetComponent<Button>();
                            if (closeButton == null) closeButton = closeButtonTransform.gameObject.AddComponent<Button>();

                            closeButton.onClick.RemoveAllListeners();
                            closeButton.onClick.AddListener(() =>
                            {
                                // Khi click CloseButton: Trả chính iconObject này về trạng thái mặc định
                                if (dec1 != null) dec1.gameObject.SetActive(true);
                                if (dec2 != null) dec2.gameObject.SetActive(false);

                                // ĐỒNG THỜI: Xóa sạch các vùng màu đỏ và TargetUI đang hiển thị trên bàn cờ
                                if (GridManager.Instance != null)
                                {
                                    GridManager.Instance.ClearAllMovementRanges();
                                }
                            });
                        }
                    }
                });
            }
        }
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
        GridCell currentRealCell = GridManager.Instance.GetCellOfCard(CardData);
        GridManager.Instance.ShowMovementRangeAt(currentRealCell.GridPosition, MovementRange, MovementPoint);

        HideMenu();
    }

    private void OnAttackClicked()
    {
        AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);

        // LẤY Ô CỜ THỰC TẾ MỚI NHẤT MÀ QUÂN CỜ ĐANG ĐỨNG
        GridCell currentRealCell = GridManager.Instance.GetCellOfCard(CardData);

        if (currentRealCell != null)
        {
            // RESET LẠI TargetCell THÀNH VỊ TRÍ MỚI NHẤT
            TargetCell = currentRealCell;

            // Xác định xem quân cờ đang chọn thuộc phe ta (Player) hay phe địch
            bool isPlayerCard = TargetCell.IsPlayerSpawnCell;

            // Gọi hàm hiện phạm vi tấn công dựa theo ô cờ thực tế mới cập nhật
            GridManager.Instance.ShowAttackRangeAt(TargetCell.GridPosition, AttackRange, isPlayerCard);

            Debug.Log($"[ActionMenu] Đang hiển thị tầm đánh ({AttackRange} ô) của quân cờ tại vị trí MỚI {TargetCell.GridPosition}. Phe Player: {isPlayerCard}");
        }
        else
        {
            Debug.LogWarning("[ActionMenu] Không tìm thấy ô cờ thực tế của quân cờ để hiển thị tầm đánh!");
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
        if (item == null || phase == null) return;

        RawImage image = item.GetComponent<RawImage>();
        if (image != null)
        {
            // SỬA LỖI: Truyền phase.PhaseType (Enum) thay vì truyền đối tượng phase
            image.texture = GetPhaseTexture(phase.PhaseType);
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
            // SỬA LỖI: Truyền phase.PhaseType (Enum) thay vì truyền đối tượng phase
            CurrentPhaseImage.texture = GetPhaseTexture(phase.PhaseType);
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

    // Hàm lấy Texture động từ Resources bằng cách sử dụng các hằng số trong ImageConstants
    private Texture GetPhaseTexture(BattlePhaseType type)
    {
        string resourcePath = string.Empty;

        switch (type)
        {
            case BattlePhaseType.Start:
                resourcePath = ImageConstants.Phase.START_PHASE_URL;
                break;
            case BattlePhaseType.Preparation:
                resourcePath = ImageConstants.Phase.PREPARATION_PHASE_URL;
                break;
            case BattlePhaseType.Shopping:
                resourcePath = ImageConstants.Phase.SHOPPING_PHASE_URL;
                break;
            case BattlePhaseType.Battle:
                resourcePath = ImageConstants.Phase.BATTLE_PHASE_URL;
                break;
            case BattlePhaseType.Boss:
                resourcePath = ImageConstants.Phase.BOSS_PHASE_URL;
                break;
            case BattlePhaseType.Reward:
                resourcePath = ImageConstants.Phase.REWARD_PHASE_URL;
                break;
            case BattlePhaseType.End:
                resourcePath = ImageConstants.Phase.END_PHASE_URL;
                break;
            case BattlePhaseType.Custom:
            default:
                resourcePath = ImageConstants.Phase.CUSTOM_PHASE_URL;
                break;
        }

        // Tiến hành Load Texture từ thư mục Resources bằng hàm Helper hoặc Resources.Load trực tiếp
        // Nếu bạn có class TextureHelper riêng thì xài: TextureHelper.LoadTexture2DCached(resourcePath)
        Texture loadedTexture = Resources.Load<Texture>(resourcePath);

        // Nếu không tìm thấy ảnh theo đường dẫn hằng số, trả về ảnh mặc định phòng hờ lỗi
        return loadedTexture != null ? loadedTexture : DefaultPhaseTexture;
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
