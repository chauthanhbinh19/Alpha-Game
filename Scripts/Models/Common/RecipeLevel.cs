public class RecipeLevel
{
    public string Id { get; set; }
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }

    public bool IsInRange(int level)
        => level >= MinLevel && level <= MaxLevel;
}
