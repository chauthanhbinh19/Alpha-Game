public class RecipePreviewDetailDTO
{
    public string ObjectId { get; set; }
    public string ObjectType { get; set; }
    public string ObjectImage { get; set; }
    public double ObjectValue { get; set; }
    public double TotalRequiredQuantity { get; set; } // Tổng nguyên liệu CẦN cho N cấp
    public double CurrentUserQuantity { get; set; }   // Số lượng người chơi ĐANG CÓ
    public bool IsEnough { get; set; }                // True nếu đủ (CurrentUserQuantity >= TotalRequiredQuantity)
}