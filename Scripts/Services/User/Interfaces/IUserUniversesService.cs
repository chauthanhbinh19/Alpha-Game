using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUniversesService
{ 
    Task<UserUniverses> GetUserUniversesAsync(string userId, string id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserUniversesAsync(string userId, UserUniverses userUniverse, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserUniverses> GetSumUserUniversesAsync(string userId);
}