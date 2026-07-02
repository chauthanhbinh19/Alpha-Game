using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum BattleState
{
    GameStart,  // Chuẩn bị đầu trận (Chọn map, chia bài ban đầu)
    Playing,    // Trận đấu đang diễn ra (Vòng lặp các Phase)
    GameOver    // Trận đấu kết thúc (Thắng / Thua)
}

public enum TurnPhase
{
    StartPhase,    // Đầu Turn: Hồi năng lượng, kích hoạt hiệu ứng đầu trận
    ShoppingPhase, // Giai đoạn mua sắm: Mua thẻ, sắp xếp đội hình lên bàn cờ 12x12
    BattlePhase,   // Giai đoạn chiến đấu: Các thẻ bài di chuyển và băm nhau (Code cũ của bạn)
    EndPhase       // Cuối Turn: Kiểm tra sát thương người chơi, dọn dẹp bàn cờ, tăng số Turn
}

public class TurnManager : MonoBehaviour
{
    [Header("Game Loop Settings")]
    public int CurrentTurnNumber = 1;
    private BattleState CurrentBattleState;
    private TurnPhase CurrentPhase;

    [Header("Card & Battle Reference")]
    public List<CardBase> AllCards = new List<CardBase>();
    private Queue<CardBase> ActionQueue = new Queue<CardBase>();
    private CardBase CurrentActiveCard;

    void Start()
    {
        // Khởi động trận đấu
        CurrentBattleState = BattleState.GameStart;
        HandleGameStart();
    }

    private void HandleGameStart()
    {
        Debug.Log("=== TRẬN ĐẤU KHỞI TRANH ===");
        // Khởi tạo bàn cờ 12x12, nạp dữ liệu thẻ bài...

        CurrentBattleState = BattleState.Playing;
        // Bắt đầu Phase đầu tiên của Turn 1
        ChangePhase(TurnPhase.StartPhase);
    }

    // Bộ điều hướng Phase (Central Phase Controller)
    public void ChangePhase(TurnPhase newPhase)
    {
        if (CurrentBattleState == BattleState.GameOver) return;

        CurrentPhase = newPhase;
        Debug.Log($"<color=yellow>=== TURN {CurrentTurnNumber} | PHASE: {CurrentPhase} ===</color>");

        switch (CurrentPhase)
        {
            case TurnPhase.StartPhase:
                StartCoroutine(HandleStartPhase());
                break;
            case TurnPhase.ShoppingPhase:
                StartCoroutine(HandleShoppingPhase());
                break;
            case TurnPhase.BattlePhase:
                HandleBattlePhaseInit();
                break;
            case TurnPhase.EndPhase:
                StartCoroutine(HandleEndPhase());
                break;
        }
    }

    // =========================================================================
    // 1. START PHASE: Đầu Turn
    // =========================================================================
    private IEnumerator HandleStartPhase()
    {
        // Logic: Cộng tiền thụ động, hồi MovementPoint tối đa cho tướng từ DB
        foreach (var card in AllCards)
        {
            card.hasActedThisturn = false;
            // Ví dụ hồi điểm di chuyển: card.currentRuntimeMovePoint = card.classData.movement_point;
        }

        yield return new WaitForSeconds(1f); // Hiển thị UI "Turn X"

        // Tự động chuyển sang Phase mua sắm
        ChangePhase(TurnPhase.ShoppingPhase);
    }

    // =========================================================================
    // 2. SHOPPING PHASE: Mua sắm & Sắp xếp đội hình
    // =========================================================================
    private IEnumerator HandleShoppingPhase()
    {
        Debug.Log("Cửa hàng mở cửa! Người chơi có 30 giây để mua bài và xếp quân.");

        // Bật UI Shop, cho phép kéo thả thẻ bài lên bàn cờ 12x12
        // Thường đoạn này bạn sẽ làm một bộ đếm thời gian (Countdown Timer)

        float shoppingTime = 5f; // Giả lập cho 5 giây mua sắm nhanh
        while (shoppingTime > 0)
        {
            // Debug.Log($"Thời gian mua sắm còn: {Mathf.CeilToInt(shoppingTime)}s");
            yield return new WaitForSeconds(1f);
            shoppingTime -= 1f;
        }

        // Đóng UI Shop, khóa vị trí không cho xếp quân nữa
        Debug.Log("Hết giờ mua sắm! Chuẩn bị chiến đấu.");

        ChangePhase(TurnPhase.BattlePhase);
    }

    // =========================================================================
    // 3. BATTLE PHASE: Giai đoạn chiến đấu (Logic cũ của bạn được đem vào đây)
    // =========================================================================
    private void HandleBattlePhaseInit()
    {
        Debug.Log("--- BẮT ĐẦU GIAO TRANH ---");

        // Lọc lấy những tướng thực sự đang có mặt trên bàn cờ để chiến đấu
        // Sắp xếp: Lực chiến cao xếp trước
        List<CardBase> sortedList = AllCards
            .OrderByDescending(c => c.Power)
            .ThenBy(c => c.Team)
            .ToList();

        ActionQueue.Clear();
        foreach (var card in sortedList)
        {
            ActionQueue.Enqueue(card);
        }

        // Chạy vòng lặp hành động cho hàng đợi
        ExecuteNextBattleAction();
    }

    private void ExecuteNextBattleAction()
    {
        if (ActionQueue.Count > 0)
        {
            CurrentActiveCard = ActionQueue.Dequeue();
            Debug.Log($"-> Lượt hành động: {CurrentActiveCard.Name} (Phe {CurrentActiveCard.Team})");

            StartCoroutine(CharacterControlRoutine(CurrentActiveCard));
        }
        else
        {
            // Tất cả các tướng trên bàn cờ đã đi xong lượt hành động
            ChangePhase(TurnPhase.EndPhase);
        }
    }

    private IEnumerator CharacterControlRoutine(CardBase character)
    {
        // Giả lập Tướng di chuyển dựa trên movement_range
        yield return new WaitForSeconds(0.8f);
        character.MoveTo(new Vector3(Random.Range(0, 5), 0, Random.Range(0, 5)));

        // Giả lập Tướng tấn công
        yield return new WaitForSeconds(0.8f);
        // character.Attack();

        character.EndAction();

        // Đệ quy lấy tướng tiếp theo trong Queue chiến đấu
        ExecuteNextBattleAction();
    }

    // =========================================================================
    // 4. END PHASE: Kết thúc Turn, dọn dẹp dữ liệu
    // =========================================================================
    private IEnumerator HandleEndPhase()
    {
        Debug.Log("--- KẾT THÚC LƯỢT (CLEANUP) ---");

        // Logic: Tính toán trừ máu Linh Hồn của Người chơi nếu bị thua trong Battle Phase
        // Kiểm tra xem có người chơi nào máu về 0 chưa để chuyển sang BattleState.GameOver

        yield return new WaitForSeconds(1.5f);

        // Tăng số Turn lên và lặp lại vòng tuần hoàn mới
        CurrentTurnNumber++;
        ChangePhase(TurnPhase.StartPhase);
    }

    // Hàm để các class khác gọi từ ngoài vào khi game kết thúc đột ngột
    public void TriggerGameOver()
    {
        CurrentBattleState = BattleState.GameOver;
        Debug.Log("<color=red>=== TRẬN ĐẤU KẾT THÚC ===</color>");
    }
}