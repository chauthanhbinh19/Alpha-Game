using System.Collections.Generic;
using System.Linq;

public class TeamSetupService
{
    private LoadTeams _loadTeamService;
    // private ICardDisplayManager _displayManager;
    public TeamSetupService()
    {
        _loadTeamService = new LoadTeams();
        // _displayManager = displayManager;
    }
    private CardType GetCardBaseType(CardBase card)
    {
        if (card is CardHero) return CardType.CardHero;
        if (card is CardCaptain) return CardType.CardCaptain;
        if (card is CardColonel) return CardType.CardColonel;
        if (card is CardGeneral) return CardType.CardGeneral;
        if (card is CardAdmiral) return CardType.CardAdmiral;
        if (card is CardMonster) return CardType.CardMonster;
        if (card is CardMilitary) return CardType.CardMilitary;
        if (card is CardSpell) return CardType.CardSpell;

        throw new System.InvalidOperationException("Unknown card type.");
    }

    public static CardContract CreateRandomContract(string contractName)
    {
        return ContractRandomizer.RandomizePositions(contractName);
    }

    public static CardPenalty CreateRandomPenalty(string penaltyName)
    {
        return PenaltyRandomizer.GenerateRandomPenalties(penaltyName);
    } 
    
    /// <summary>
    /// Lấy và sắp xếp 10 thẻ bài đang sống dựa trên Contract và luật thay thế Penalty.
    /// </summary>
    /// <param name="allPlayerCards">Danh sách TẤT CẢ thẻ bài của người chơi.</param>
    /// <param name="contract">Luật Contract yêu cầu loại thẻ cho 10 vị trí.</param>
    /// <param name="penalty">Luật Penalty định nghĩa mức phạt theo loại thẻ.</param>
    /// <returns>Một List<CardBase> gồm 10 phần tử, đại diện cho 10 vị trí trên sân. Null nếu không có thẻ.</returns>
    public List<CardBase> GetAvailableCardsForAppearance(PlayerController player, CardContract contract, CardPenalty penalty)
    {
        List<CardBase> allPlayerCards = player.GetCards();
        // Danh sách cuối cùng sẽ được trả về (10 phần tử)
        List<CardBase> availableCardToAppear = new List<CardBase>();
        
        // 1. Lọc thẻ bài đang sống và sao chép để thao tác (tránh thay đổi danh sách gốc)
        // Chúng ta sẽ loại bỏ thẻ khỏi bản sao này khi chúng được sử dụng.
        List<CardBase> availableCards = allPlayerCards
            .Where(card => card.IsAlive) 
            .ToList();

        // 2. Sắp xếp danh sách Penalty theo PenaltyValue TĂNG DẦN
        List<CardPenaltyItem> orderedPenalties = penalty.Penalties
            .OrderBy(item => item.PenaltyValue)
            .ToList();

        // 3. Chuẩn bị Dictionary để dễ dàng tìm kiếm thẻ theo CardType
        // Sử dụng một Dictionary của Queue/Stack hoặc chỉ List để quản lý các thẻ chưa dùng
        Dictionary<CardType, List<CardBase>> cardPoolByType = availableCards
            .GroupBy(card => GetCardBaseType(card))
            .ToDictionary(g => g.Key, g => g.ToList());

        // Debug.Log($"Bắt đầu chọn thẻ: {availableCards.Count} thẻ đang sống.");

        // 4. Duyệt qua 10 vị trí (Position 1 đến 10) trong Contract
        foreach (var position in contract.Positions.OrderBy(p => p.Index))
        {
            CardType requiredType = position.Type;
            CardBase cardToPlace = null;
            bool cardFound = false;

            // A. Ưu tiên tìm thẻ theo Contract
            if (cardPoolByType.TryGetValue(requiredType, out List<CardBase> pool) && pool.Any())
            {
                cardToPlace = pool[0];
                pool.RemoveAt(0);
                cardFound = true;
                // Debug.Log($"[Pos {position.Index}] Đặt thẻ {cardToPlace.GetType().Name} theo Contract.");
            }
            
            // B. Nếu không tìm thấy, áp dụng luật thay thế Penalty (thẻ bị phạt ít nhất)
            if (!cardFound)
            {
                foreach (var penaltyItem in orderedPenalties)
                {
                    CardType substituteType = penaltyItem.Type;

                    if (cardPoolByType.TryGetValue(substituteType, out List<CardBase> substitutePool) && substitutePool.Any())
                    {
                        cardToPlace = substitutePool[0];
                        substitutePool.RemoveAt(0);
                        cardFound = true;
                        // Debug.Log($"[Pos {position.Index}] Thay thế bằng {cardToPlace.GetType().Name} (Penalty {penaltyItem.PenaltyValue}).");
                        break; 
                    }
                }
            }

            // 5. Thêm thẻ đã chọn (hoặc null) vào danh sách kết quả
            availableCardToAppear.Add(cardToPlace);
        }

        // Đảm bảo danh sách trả về luôn có 10 phần tử
        // while (availableCardToAppear.Count < 10)
        // {
        //     availableCardToAppear.Add(null);
        // }

        // Debug.Log($"Hoàn thành chọn thẻ. Tổng cộng {availableCardToAppear.Count(c => c != null)} thẻ đã được chọn để hiển thị.");
        
        return availableCardToAppear;
    }
}