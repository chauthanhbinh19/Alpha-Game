using System.Collections.Generic;

public class RecipeItemDTO
{
    public string RecipeId { get; set; }
    public string RecipeLevelId { get; set; }
    public string ItemId { get; set; }
    public string ItemImage { get; set; }
    public double RequiredQuantity { get; set; }
    public double ItemValue { get; set; }
    public double UserQuantity { get; set; }
    public int MinLevel { get; set; }   // thêm
    public int MaxLevel { get; set; }   // thêm
    public List<RecipeItemDetailDTO> RecipeItemDetails { get; set; } // thêm
}
