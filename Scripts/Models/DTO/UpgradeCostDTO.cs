public class UpgradeCostDTO
{
    public double UserQuantity { get; set; }

    public double RequiredQuantity { get; set; }

    public bool CanUpgrade =>
        UserQuantity >= RequiredQuantity;
}