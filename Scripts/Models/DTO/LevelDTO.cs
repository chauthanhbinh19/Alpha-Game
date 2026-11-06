using System.Collections.Generic;

public class LevelDTO
{
    public int Level { get; set; }
    public int Star { get; set; }
    public List<GameGrowthConfig> Configs { get; set; }
}