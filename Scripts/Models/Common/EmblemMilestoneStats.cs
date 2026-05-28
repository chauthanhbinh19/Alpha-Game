using System.Collections.Generic;

public class EmblemMilestoneStats
{
    public string Id { get; set; }
    public string MilestoneId { get; set; }
    public string StatName { get; set; }
    public float StatValue { get; set; }
    public bool IsPercent {get; set; }
    public EmblemMilestoneStats()
    {
        
    }
}