using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRecipeService
{
    Task<List<RecipeItemDto>> GetRecipeItemsAsync(string featureName,int level,string userId);
}