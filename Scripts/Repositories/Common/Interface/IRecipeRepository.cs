using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRecipeRepository
{
    Task<List<RecipeItemDto>> GetRecipeItemsAsync(string featureName,int level,string userId);
    Task DeductItemsAsync(string userId, List<RecipeItemDto> items);
}