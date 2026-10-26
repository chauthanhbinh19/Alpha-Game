using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRecipeService
{
    Task<RecipeResponseDTO> GetRecipeItemsAsync(string featureName, int level, string userId);
    Task<RecipeResponseDTO> GetRecipeItemsAsync(string featureName, string userId);
    Task<RecipeResponseDTO> GetRecipeItemsByItemIdAsync(string itemId, int level, string userId);
    Task DeductItemsAsync(string userId, List<RecipeItemDTO> items);
}