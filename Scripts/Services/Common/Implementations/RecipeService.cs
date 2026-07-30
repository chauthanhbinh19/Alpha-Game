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

    public Task<List<RecipeItemDto>> GetRecipeItemsAsync(string featureName,int level,string userId)
    {
        return _recipeRepository.GetRecipeItemsAsync(featureName, level, userId);
    }

    public Task DeductItemsAsync(string userId, List<RecipeItemDto> items)
    {
        return _recipeRepository.DeductItemsAsync(userId, items);
    }
}
