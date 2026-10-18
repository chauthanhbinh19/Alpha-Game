public class RecipeLevelItem
{
    public string RecipeId { get; set; }
    public string RecipeLevelId { get; set; }
    public string ItemId { get; set; }
    public double Quantity { get; set; }
    public double ItemValue { get; set; }

    // runtime only (không lưu DB)
    public double UserQuantity { get; set; }
}
