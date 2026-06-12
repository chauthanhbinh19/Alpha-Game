using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum BattleState { Start, Battle, End }

public class TurnManager : MonoBehaviour
{
    public List<CardBase> allCards = new List<CardBase>();
    private Queue<CardBase> actionQueue = new Queue<CardBase>();

    private BattleState currentState;
    private CardBase currentActiveCard;

    void Start()
    {
        ChangeState(BattleState.Start);
    }
    
    private void ChangeState(BattleState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case BattleState.Start:
                StartCoroutine(HandleTurnStart());
                break;
            case BattleState.Battle:
                HandleTurnBattle();
                break;
            case BattleState.End:
                StartCoroutine(HandleTurnEnd());
                break;
        }
    }

    // --- TURN START ---
    private IEnumerator HandleTurnStart()
    {
        Debug.Log("=== TRẬN ĐẤU BẮT ĐẦU: TURN START ===");
        
        // Reset trạng thái hành động của tất cả tướng
        foreach (var card in allCards)
        {
            card.hasActedThisturn = false;
        }

        // SẮP XẾP: Lực chiến cao hơn xếp trước. 
        // Nếu bằng lực chiến, bạn có thể thêm điều kiện phụ (ví dụ: Phe A ưu tiên)
        List<CardBase> sortedList = allCards
            .OrderByDescending(c => c.Power)
            .ThenBy(c => c.Team) 
            .ToList();

        // Nạp vào Hàng đợi (Queue)
        actionQueue.Clear();
        foreach (var card in sortedList)
        {
            actionQueue.Enqueue(card);
        }

        yield return new WaitForSeconds(1f); // Chờ 1 giây để hiển thị UI hiệu ứng Turn Start
        ChangeState(BattleState.Battle);
    }

    // --- TURN BATTLE ---
    private void HandleTurnBattle()
    {
        // Kiểm tra xem còn tướng nào chưa đi không
        if (actionQueue.Count > 0)
        {
            currentActiveCard = actionQueue.Dequeue();
            Debug.Log($"---> Lượt của: {currentActiveCard.Name} (Phe {currentActiveCard.Team}) - Lực chiến: {currentActiveCard.Power}");
            
            // Kích hoạt quyền điều khiển cho tướng này
            StartCoroutine(PlayerOrAIControl(currentActiveCard));
        }
        else
        {
            // Tất cả các tướng đều đã đi xong lượt
            ChangeState(BattleState.End);
        }
    }

    // Mô phỏng quá trình điều khiển tướng (Di chuyển -> Tấn công/Bỏ qua)
    private IEnumerator PlayerOrAIControl(CardBase character)
    {
        // 1. Giai đoạn di chuyển (Ví dụ: click chọn vị trí cụ thể)
        // Trong game thật, bạn sẽ đợi người chơi bấm chuột hoặc AI tính toán. Ở đây giả lập chờ 1.5s
        yield return new WaitForSeconds(1f);
        character.MoveTo(new Vector3(Random.Range(0, 5), 0, Random.Range(0, 5)));

        // 2. Giai đoạn Tấn công hoặc Không tấn công
        yield return new WaitForSeconds(1f);
        bool choseToAttack = Random.value > 0.3f; // Giả lập tỷ lệ 70% tấn công, 30% bỏ qua
        
        if (choseToAttack)
        {
            // character.Attack();
        }
        else
        {
            Debug.Log($"{character.Name} chọn Đứng Yên (Không tấn công).");
        }

        // 3. Kết thúc hành động của tướng này
        character.EndAction();
        Debug.Log($"{character.Name} kết thúc lượt cá nhân.");

        // Tiếp tục vòng lặp lấy tướng tiếp theo trong Queue
        HandleTurnBattle(); 
    }

    // --- TURN END ---
    private IEnumerator HandleTurnEnd()
    {
        Debug.Log("=== KẾT THÚC TURN: TURN END ===");
        // Kiểm tra điều kiện thắng/thua ở đây (ví dụ: một phe chết hết chưa)
        
        yield return new WaitForSeconds(1.5f);

        // Chuyển sang Turn mới, vòng lặp lại từ đầu
        ChangeState(BattleState.Start);
    }
}