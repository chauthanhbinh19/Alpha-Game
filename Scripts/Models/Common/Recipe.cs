using System.Collections.Generic;

public class Recipe
{
    public string Id { get; set; }
    public string Name { get; set; }

    // optional – nếu load full
    public List<RecipeLevelItem> LevelItems { get; set; } = new();
}
