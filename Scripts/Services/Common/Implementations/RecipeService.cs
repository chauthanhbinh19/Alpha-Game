using System.Collections.Generic;
using System.Threading.Tasks;
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;

    public RecipeService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public static IRecipeService Create() => ServiceContainer.GetService<IRecipeService>();

    public async Task<RecipeResponseDTO> GetRecipeItemsAsync(string featureName, int level, string userId)
    {
        return await _recipeRepository.GetRecipeItemsAsync(featureName, level, userId);
    }

    public async Task<RecipeResponseDTO> GetRecipeItemsAsync(string featureName, string userId)
    {
        return await _recipeRepository.GetRecipeItemsAsync(featureName, userId);
    }

    public async Task<RecipeResponseDTO> GetRecipeItemsByItemIdAsync(string itemId, int level, string userId)
    {
        return await _recipeRepository.GetRecipeItemsByItemIdAsync(itemId, level, userId);
    }

    public async Task DeductItemsAsync(string userId, List<RecipeItemDTO> items)
    {
        await _recipeRepository.DeductItemsAsync(userId, items);
    }
}
