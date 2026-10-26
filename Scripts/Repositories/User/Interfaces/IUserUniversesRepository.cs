using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUniversesRepository
{
    Task<UserUniverses> GetUserUniversesAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserUniverses>> InsertOrUpdateUserUniversesAsync(string userId, UserUniverses userUniverse, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserUniverses> GetSumUserUniversesAsync(string userId);
}