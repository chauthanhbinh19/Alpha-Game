using System.Collections.Generic;
using System.Threading.Tasks;
public class RecipeService : IRecipeService
{
    private static RecipeService _instance;
    private readonly IRecipeRepository _recipeRepository;

    public RecipeService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public static RecipeService Create()
    {
        if (_instance == null)
        {
            _instance = new RecipeService(new RecipeRepository());
        }
        return _instance;
    }
    public Task<List<RecipeItemDto>> GetRecipeItemsAsync(string featureName,int level,string userId)
    {
        return _recipeRepository.GetRecipeItemsAsync(featureName,level,userId);
    }
}
