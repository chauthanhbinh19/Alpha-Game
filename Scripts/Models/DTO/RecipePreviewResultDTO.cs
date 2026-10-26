using System.Collections.Generic;
public class RecipePreviewResultDTO
{
    public int CurrentLevel { get; set; }
    public int TargetLevel { get; set; }           // Level sẽ đạt được sau khi nâng
    public int GainedLevels { get; set; }          // Số cấp tăng thêm
    public bool IsMaxMode { get; set; }             // True nếu đang xem ở chế độ Max
    public bool CanAffordAll { get; set; }          // True nếu đủ nguyên liệu nâng cấp này
    public List<RecipePreviewDetailDTO> RequiredMaterials { get; set; } = new List<RecipePreviewDetailDTO>();
}