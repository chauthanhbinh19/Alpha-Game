using System;
using System.Collections.Generic;

public class Tradings
{
    public string Id { get; set; }
    public string CodeName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string TradingType { get; set; }
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Danh sách vật liệu yêu cầu (Đầu vào)
    public List<TradingMaterialRequired> RequiredMaterials { get; set; } = new List<TradingMaterialRequiredModel>();

    // Danh sách vật liệu thành phẩm (Đầu ra)
    public List<TradingMaterialProduced> ProducedMaterials { get; set; } = new List<TradingMaterialProducedModel>();
}