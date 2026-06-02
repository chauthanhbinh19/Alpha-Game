using System.Collections.Generic;

public class UpgradePreviewDTO
{
    public bool Success { get; set; }

    public int CurrentLevel { get; set; }

    public int TargetLevel { get; set; }

    public int UpgradedLevels { get; set; }

    public Dictionary<string, double> RequiredItems { get; set; } = new();

    public string Message { get; set; }
}