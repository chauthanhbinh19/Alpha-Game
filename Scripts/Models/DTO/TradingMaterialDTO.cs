using System;
using System.Collections.Generic;
public class TradingMaterialDTO
{
    public string Id { get; set; } 
    public string ObjectType { get; set; }     // VD: "CARD_HEROES", "ITEM"
    public string ObjectId { get; set; }       // Null hoặc rỗng nếu là GENERAL
    public string SelectionType { get; set; }  // "SPECIFIC" hoặc "GENERAL"
    public double ObjectValue { get; set; }    // Giá trị quy đổi (nếu có)
    public double TargetQuantity { get; set; } // Số lượng cần đạt (Required) hoặc nhận được (Produced)
    
    // Dành riêng cho phần UI chọn items (GENERAL)
    public List<TradingInventoryItemDTO> ChosenInventoryItems { get; set; } = new List<TradingInventoryItemDTO>();
}