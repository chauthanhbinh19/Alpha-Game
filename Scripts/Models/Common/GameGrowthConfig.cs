public class GameGrowthConfig
{
    public string Id { get; set; }
    public string ObjectType { get; set; } = string.Empty;
    public string GrowthType { get; set; } = string.Empty;
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public string FormulaType { get; set; } = string.Empty;
    public double BaseExp { get; set; }
    public double BaseMultiplier { get; set; }
    public double PowerFactor { get; set; }
    public double LinearFactor { get; set; }
    public int StepSize { get; set; }
}