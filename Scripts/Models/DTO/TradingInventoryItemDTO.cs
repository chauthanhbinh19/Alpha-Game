public class TradingInventoryItemDTO
{
    public string Id { get; set; }
    public string ObjectType { get; set; }
    public string ObjectId { get; set; }
    public double AvailableQuantity { get; set; } // Số lượng đang có trong kho của item cụ thể này
    public double SelectedQuantity { get; set; } // Số lượng người chơi chọn từ item này để bù vào target
}