using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserAnimesService
{ 
    Task<UserAnimes> GetUserAnimesAsync(string userId, string id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAnimesAsync(string userId, UserAnimes Animes, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserAnimes> GetSumUserAnimesAsync(string userId);
}