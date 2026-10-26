using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHICAsService
{ 
    Task<UserHICAs> GetUserHICAsAsync(string userId, string id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHICAsAsync(string userId, UserHICAs HICAs, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserHICAs> GetSumUserHICAsAsync(string userId);
}