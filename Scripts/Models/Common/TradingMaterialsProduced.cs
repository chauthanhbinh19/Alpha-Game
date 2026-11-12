using System;

public class TradingMaterialsProduced
{
    public string Id { get; set; }
    public string TradingId { get; set; }
    public string ObjectType { get; set; }
    public string ObjectId { get; set; } // Có thể null nếu là dạng GENERAL
    public string SelectionType { get; set; }
    public double ObjectValue { get; set; }
    public double ProducedQuantity { get; set; }
    public string ObjectImage { get; set; }
    public double ObjectQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
}