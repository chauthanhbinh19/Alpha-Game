using System;
using System.Collections.Generic;
public class TradingDTO
{
    public int Id { get; set; }
    public string CodeName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string TradingType { get; set; } // "SPECIFIC_TO_SPECIFIC", "GENERAL_TO_SPECIFIC", v.v.
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public bool IsActive { get; set; }

    // Danh sách nguyên liệu đầu vào
    public List<TradingMaterialDTO> RequiredMaterials { get; set; } = new List<TradingMaterialDTO>();

    // Danh sách thành phẩm đầu ra
    public List<TradingMaterialDTO> ProducedMaterials { get; set; } = new List<TradingMaterialDTO>();
}