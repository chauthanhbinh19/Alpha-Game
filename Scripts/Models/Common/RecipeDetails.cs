using System.Collections.Generic;

public class RecipeDetails
{
    public string RecipeId { get; set; }
    public string RecipeLevelId { get; set; }
    public List<RecipeLevelItem> RecipeLevelItems { get; set; }
}