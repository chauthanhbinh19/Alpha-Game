using System.Collections.Generic;

public class Patterns
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<PatternCells> Cells { get; set; } = new List<PatternCells>();
    public Patterns()
    {
        
    }
}