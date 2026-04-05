using System;
using System.Collections.Generic;
using System.Linq;
public class CardPenalty
{
    public string Name { get; set; }
    // Danh sách lưu trữ loại thẻ và giá trị bị phạt tương ứng
    public List<CardPenaltyItem> Penalties { get; set; } = new List<CardPenaltyItem>();
}
public class CardPenaltyItem
{
    public CardType Type { get; set; }
    public int PenaltyValue { get; set; } // Giá trị bị phạt (0, 5, 10, ...)
}
public static class PenaltyRandomizer
{
    private static readonly Random Rng = new Random();

    /// <summary>
    /// Gán ngẫu nhiên giá trị phạt (bội số của 5), trong đó một loại thẻ ngẫu nhiên sẽ có giá trị phạt là 0.
    /// </summary>
    /// <param name="penaltyName">Tên cơ chế phạt.</param>
    /// <returns>Đối tượng CardPenalty đã random giá trị.</returns>
    public static CardPenalty GenerateRandomPenalties(string penaltyName)
    {
        var penalty = new CardPenalty { Name = penaltyName };

        // 1. Lấy tất cả CardType và số lượng
        List<CardType> allCardTypes = Enum.GetValues(typeof(CardType)).Cast<CardType>().ToList();
        int numTypes = allCardTypes.Count;

        // 2. Tạo danh sách các giá trị phạt (0, 5, 10, 15, ...)
        List<int> penaltyValues = new List<int>();
        for (int i = 0; i < numTypes; i++)
        {
            penaltyValues.Add(i * 5);
        }

        // 3. SHUFFLE CẢ HAI DANH SÁCH (CardType và PenaltyValue) cùng lúc
        // Đây là cách đơn giản và hiệu quả nhất để đảm bảo tính ngẫu nhiên 1-1.

        // Sao chép danh sách CardType để xáo trộn
        List<CardType> shuffledTypes = new List<CardType>(allCardTypes);
        
        // Xáo trộn danh sách CardType (Fisher-Yates shuffle)
        for (int i = shuffledTypes.Count - 1; i > 0; i--)
        {
            int j = Rng.Next(i + 1);
            
            // Xáo trộn CardType
            CardType typeTemp = shuffledTypes[i];
            shuffledTypes[i] = shuffledTypes[j];
            shuffledTypes[j] = typeTemp;

            // Xáo trộn PenaltyValue
            int valueTemp = penaltyValues[i];
            penaltyValues[i] = penaltyValues[j];
            penaltyValues[j] = valueTemp;
        }

        // 4. Gán giá trị phạt đã được shuffle cho loại thẻ đã được shuffle tương ứng
        for (int i = 0; i < numTypes; i++)
        {
            penalty.Penalties.Add(new CardPenaltyItem
            {
                Type = shuffledTypes[i],
                PenaltyValue = penaltyValues[i]
            });
        }

        return penalty;
    }
}