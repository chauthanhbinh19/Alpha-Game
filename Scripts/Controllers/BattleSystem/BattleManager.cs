using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Transform PlayerCardsParent;
    public Transform EnemyCardsParent;
    public CardSlot[] AllySlots = new CardSlot[10]; // 5 vị trí cho phe mình
    public CardSlot[] EnemySlots = new CardSlot[10]; // 5 vị trí cho phe địch
    public GameObject CardModelPrefab;
    [SerializeField] private int maxTurn = 10;
    // Cần gán các GameObject Slot này vào Inspector
    private LoadTeams _loadTeamService;
    private TurnManager turnManager;
    private PlayerController attacker;
    private PlayerController defender;

    private void Awake()
    {
        // Khởi tạo service khi game bắt đầu
        _loadTeamService = new LoadTeams();
    }
    private void Start()
    {
        InitializeBattle();
        StartCoroutine(turnManager.RunTurns(attacker, defender));
    }

    // ==========================================================
    // CƠ CHẾ TỰ ĐỘNG ĐIỀN CHỈ TRONG EDITOR
    // ==========================================================

    // Hàm này chạy trong Editor (khi bạn thay đổi script, thay đổi giá trị...)
    // Hoặc khi bạn click vào nút "Reset" trong Inspector
    private void OnValidate()
    {
        // Tự động điền mảng khi script được cập nhật trong Editor
        AutoFillSlots();
    }

    private void AutoFillSlots()
    {
        if (PlayerCardsParent != null)
        {
            AllySlots = GetSlotsFromParent(PlayerCardsParent);
        }

        if (EnemyCardsParent != null)
        {
            EnemySlots = GetSlotsFromParent(EnemyCardsParent);
        }
    }

    private CardSlot[] GetSlotsFromParent(Transform parent)
    {
        // Lấy tất cả các con (CardPositionX)
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            children.Add(parent.GetChild(i));
        }

        // Tạo mảng CardSlotReference
        CardSlot[] slots = new CardSlot[children.Count];

        for (int i = 0; i < children.Count; i++)
        {
            slots[i] = new CardSlot
            {
                // Gán GameObject con vào tham chiếu
                positionObject = children[i].gameObject,
                // Gán Slot Index theo thứ tự (bắt đầu từ 1)
                slotIndex = i + 1
            };
        }
        return slots;
    }

    private void InitializeBattle()
    {
        attacker = new PlayerController();
        defender = new PlayerController();

        turnManager = new TurnManager(maxTurn);

        // var teams = TeamsService.Create().GetUserTeams(User.CurrentUserId);
        string userId = "638957884856698071";
        var teams = TeamsService.Create().GetUserTeams(userId);
        var firstTeam = teams.FirstOrDefault(t => t.TeamNumber == 1);
        SetupPlayerTeam(userId, firstTeam.TeamId);

        // Giả lập thêm vài lá bài vào field
        // attacker.AddCard(new DummyCard("🔥 Attacker Card A"));
        // attacker.AddCard(new DummyCard("⚔️ Attacker Card B"));
        // defender.AddCard(new DummyCard("🛡️ Defender Card X"));
        // defender.AddCard(new DummyCard("🧱 Defender Card Y"));

        Debug.Log("Battle initialized successfully!");
    }

    private List<CardBase> SelectUniquePositionCards(List<CardBase> allLoadedCards, int count)
    {
        List<CardBase> selectedCards = new List<CardBase>();
        // Sử dụng HashSet để theo dõi các giá trị MainPosition (int) đã được chọn.
        HashSet<int> usedPositions = new HashSet<int>();

        // 1. Sắp xếp danh sách. Việc sắp xếp theo MainPosition đảm bảo các vị trí
        //    nhỏ (ví dụ: 1, 2, 3) sẽ được ưu tiên chọn trước.
        List<CardBase> sortedCards = allLoadedCards
            .OrderBy(card => card.MainPosition)
            .ToList();

        foreach (var card in sortedCards)
        {
            // Kiểm tra xem đã đủ số lượng thẻ cần chọn chưa
            if (selectedCards.Count >= count)
            {
                break;
            }

            // Lấy MainPosition (Đã là int, không cần phân tích chuỗi)
            int mainPosition = card.MainPosition;

            // 2. Kiểm tra tính độc nhất của vị trí
            if (!usedPositions.Contains(mainPosition))
            {
                // Vị trí chưa được sử dụng:
                selectedCards.Add(card);       // Thêm thẻ bài vào danh sách
                usedPositions.Add(mainPosition); // Đánh dấu vị trí đã được sử dụng
            }
        }

        return selectedCards;
    }

    public void SetupPlayerTeam(string userId, string teamId)
    {
        // 1. Gọi Service để lấy các đối tượng CardBase đã được ánh xạ
        List<CardBase> allLoadedCards = _loadTeamService.LoadPlayerTeamCard(userId, teamId);

        // 2. Gán từng Card vào vị trí vật lý trên sân
        List<CardBase> selectedCards = SelectUniquePositionCards(allLoadedCards, 5);

        // 3. Gán thẻ vào vị trí trên sân
        AssignCardsToSlots(selectedCards, AllySlots);
    }

    // Logic gán và Instantiate
    private void AssignCardsToSlots(List<CardBase> cardsToPlace, CardSlot[] slots)
    {
        foreach (var card in cardsToPlace)
        {
            // Lấy vị trí MainPosition từ CardBase (đã được ánh xạ từ Entity Position "x-y")
            // *Lưu ý: Nếu MainPosition trong CardBase là 1-based (1, 2, 3...), 
            //         thì slotIndex của CardSlot cũng phải là 1-based (như hình bạn cung cấp)
            int cardMainPosition = card.MainPosition;

            // Tìm Slot tương ứng với MainPosition
            CardSlot targetSlot = slots
                .FirstOrDefault(slot => slot.slotIndex == cardMainPosition);

            if (targetSlot != null && targetSlot.positionObject != null)
            {
                // A. INSTANTIATE PREFAB MODEL CARD
                GameObject cardInstance = Instantiate(CardModelPrefab, targetSlot.positionObject.transform);

                // Đặt vị trí chính xác trong Slot (ví dụ: ở gốc (0,0,0) của Slot)
                cardInstance.transform.localPosition = new Vector3(-5f, 10f, -10f);
                cardInstance.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                cardInstance.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

                Transform mirrorTransform = cardInstance.transform.Find("Image");

                if (mirrorTransform != null)
                {
                    // 2. Lấy thành phần Renderer từ GameObject "Mirror"
                    Renderer mirrorRenderer = mirrorTransform.GetComponent<Renderer>();

                    if (mirrorRenderer != null)
                    {
                        // 3. Tải Texture từ đường dẫn (được lưu trong card.Image)
                        Texture newTexture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(card.Image)); // *LƯU Ý: Đảm bảo đường dẫn (path) trong card.Image là chính xác*

                        if (newTexture != null)
                        {
                            // 4. Gán Texture vào Material (sử dụng mainTexture hoặc một thuộc tính shader cụ thể)
                            // Lấy Material đầu tiên (hoặc Material chính)
                            Material targetMaterial = mirrorRenderer.material;

                            // Gán Texture vào thuộc tính chính của Shader (thường là "_MainTex")
                            targetMaterial.mainTexture = newTexture;

                            // HOẶC nếu bạn cần gán cho một thuộc tính shader khác (ví dụ: Texture ánh sáng):
                            // targetMaterial.SetTexture("_EmissionMap", newTexture);
                        }
                        else
                        {
                            Debug.LogError($"Không thể tải Texture từ đường dẫn: {card.Image}");
                        }
                    }
                    else
                    {
                        Debug.LogError("GameObject 'Mirror' không có component Renderer.");
                    }
                }
                else
                {
                    Debug.LogError("Không tìm thấy GameObject con tên là 'Mirror' trong CardModelPrefab.");
                }

                // B. GẮN ĐỐI TƯỢNG LOGIC VÀO GAMEOBJECT VỪA TẠO
                // *Giả sử Prefab CardModelPrefab có script CardVisual/CardController*

                // Lấy script quản lý model từ GameObject
                // CardVisualController visualController = cardInstance.GetComponent<CardVisualController>();
                // if (visualController != null)
                // {
                //     // Gán dữ liệu CardBase logic vào Model
                //     visualController.Initialize(card); 
                // }

                // C. Cập nhật trạng thái Slot (Nếu bạn có component CardSlot trên positionObject)
                // targetSlot.positionObject.GetComponent<CardSlotComponent>().PlaceCard(card);
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy Slot với index {cardMainPosition} hoặc Slot Object bị null.");
            }
        }
    }
}
