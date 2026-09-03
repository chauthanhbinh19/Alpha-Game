public class ShopPackageModel
{
    public string PackageId { get; set; } = string.Empty;
    public string PackageName { get; set; } = string.Empty;
    public decimal PriceUsd { get; set; }
    public string RewardCurrencyType { get; set; } = string.Empty;
    public long RewardAmount { get; set; }
    public bool IsActive { get; set; }
}