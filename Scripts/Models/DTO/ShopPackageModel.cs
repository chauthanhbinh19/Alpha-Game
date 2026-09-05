public class ShopPackageModel
{
    public string PackageId { get; set; } = string.Empty;
    public string PackageName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // "COIN", "GEM", "STARTER", "EVENT"
    public decimal PriceUsd { get; set; } // Giá bán thực tế
    public decimal? OriginalPriceUsd { get; set; } // Giá gốc (có thể null nếu không giảm giá)
    public int DiscountPercent { get; set; } // % Giảm giá
    public string RewardCurrencyId { get; set; }
    public long RewardAmount { get; set; }
    public bool IsActive { get; set; }
    public bool IsDiscounted => DiscountPercent > 0 || (OriginalPriceUsd.HasValue && OriginalPriceUsd > PriceUsd);
}