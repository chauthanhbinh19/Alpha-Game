using System;
using System.Collections.Generic;
using System.Linq;
public class CardContract
{
    public string Name { get; set; }
    // List chứa 10 vị trí (Position 1 đến Position 10)
    public List<CardPosition> Positions { get; set; } = new List<CardPosition>();
}
public class CardPosition
{
    public int Index { get; set; } // Vị trí từ 1 đến 10
    public CardType Type { get; set; } // Loại thẻ ngẫu nhiên được gán cho vị trí này
    // Bạn có thể thêm các thuộc tính khác như CardId, UserId, v.v. nếu cần
}
public static class ContractRandomizer
{
    private static readonly Random Rng = new Random();

    /// <summary>
    /// Gán ngẫu nhiên loại thẻ cho 10 vị trí trong CardContract.
    /// </summary>
    /// <param name="contractName">Tên của hợp đồng.</param>
    /// <returns>Đối tượng CardContract đã được random 10 vị trí.</returns>
    public static CardContract RandomizePositions(string contractName)
    {
        var contract = new CardContract
        {
            Name = contractName
        };

        // Lấy danh sách tất cả các giá trị CardType có thể có
        Array cardTypes = Enum.GetValues(typeof(CardType));
        int numTypes = cardTypes.Length;

        // Tạo 10 vị trí và random loại thẻ cho từng vị trí
        for (int i = 1; i <= 10; i++)
        {
            // 1. Chọn một số ngẫu nhiên từ 0 đến numTypes - 1
            int randomIndex = Rng.Next(numTypes);

            // 2. Lấy CardType tương ứng
            CardType randomType = (CardType)cardTypes.GetValue(randomIndex);

            // 3. Tạo vị trí và thêm vào danh sách
            contract.Positions.Add(new CardPosition
            {
                Index = i,
                Type = randomType
            });
        }

        return contract;
    }
}