using System.Collections.Generic;
public class RecipeCalculateDTO
{
    public int TargetLevel { get; set; } // Level sau khi nâng (CurrentLevel + GainedLevels)
    public int GainedLevels { get; set; } // Số cấp tăng được
    public List<RecipeItemDetailDTO> TotalDeductions { get; set; } = new List<RecipeItemDetailDTO>(); // Tổng nguyên liệu tiêu tốn
}