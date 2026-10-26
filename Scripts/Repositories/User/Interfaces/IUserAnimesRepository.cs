using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserAnimesRepository
{
    Task<UserAnimes> GetUserAnimesAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserAnimes>> InsertOrUpdateUserAnimesAsync(string userId, UserAnimes Animes, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserAnimes> GetSumUserAnimesAsync(string userId);
}