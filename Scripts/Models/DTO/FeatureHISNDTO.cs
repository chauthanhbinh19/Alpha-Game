public class FeatureHISNDTO
{
    public string Id {get; set;}
    public string FeatureName { get; set; }
    public string CodeName { get; set; }
    public double BaseMultiplier { get; set; }
    public int RequiredLevel { get; set; }
    public int CurrentLevel {get; set;}
    public int MaxLevel {get; set;}
}