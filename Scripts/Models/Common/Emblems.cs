using System.Collections.Generic;

public class Emblems
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public List<EmblemThreshold> Thresholds;
    public Emblems()
    {
        
    }
}