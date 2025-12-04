using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    [SerializeField] private int maxTurn = 10;
    // Cần gán các GameObject Slot này vào Inspector
    // private TeamSetupService teamSetupService;
    [SerializeField] private CardDisplayManager cardDisplayManager;
    [SerializeField] private Transform openingPanel;
    [SerializeField] private Transform displayPanel;
    private TurnManager turnManager;
    private PlayerController attacker;
    private PlayerController defender;
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

    private void Start()
    {
        StartCoroutine(BattleSequence());
    }

    private IEnumerator BattleSequence()
    {
        Debug.Log("--- Bắt đầu Battle Sequence ---");

        // 1. GỌI INTIALIZE OPENING (Thực hiện animation)
        InitializeOpening();

        Debug.Log("Đang chạy Opening Animation...");

        // 2. CHỜ ĐỢI 3 GIÂY
        // Dòng này tạm dừng Coroutine trong 3 giây.
        yield return new WaitForSeconds(3.0f);
        openingPanel.gameObject.SetActive(false);
        displayPanel.gameObject.SetActive(true);

        Debug.Log("Opening Animation hoàn tất. Bắt đầu Battle.");

        // 3. KHỞI TẠO VÀ BẮT ĐẦU TRẬN ĐẤU
        // await InitializeBattleAsync();

        // 4. BẮT ĐẦU VÒNG CHƠI (chỉ bắt đầu sau khi Battle được Initialize)
        StartCoroutine(turnManager.RunTurns(attacker, defender));
    }
    private void InitializeOpening()
    {
        Transform circleGroup = openingPanel.Find("CircleGroup");
        circleGroup.AddComponent<RotateAnimation>();
    }
    private async Task InitializeBattleAsync()
    {
        string userId = "638957884856698071";
        var teams = await TeamsService.Create().GetUserTeamsAsync(userId);
        var firstTeam = teams.FirstOrDefault(t => t.TeamNumber == 1);

        attacker = new PlayerController();
        await attacker.GetPlayerCardAsync(userId, firstTeam.TeamId);
        defender = new PlayerController();

        turnManager = new TurnManager(attacker, defender, maxTurn);

        // var teams = TeamsService.Create().GetUserTeams(User.CurrentUserId);

        // Giả lập thêm vài lá bài vào field
        // attacker.AddCard(new DummyCard("🔥 Attacker Card A"));
        // attacker.AddCard(new DummyCard("⚔️ Attacker Card B"));
        // defender.AddCard(new DummyCard("🛡️ Defender Card X"));
        // defender.AddCard(new DummyCard("🧱 Defender Card Y"));

        Debug.Log("Battle initialized successfully!");
    }
    public void ToggleDisplayManager()
    {
        if (displayPanel != null)
        {
            bool currentState = displayPanel.gameObject.activeSelf;
            displayPanel.gameObject.SetActive(!currentState);

            // Debug.Log($"DisplayManager state changed to {!currentState}");
        }
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



    // Logic gán và Instantiate

}
