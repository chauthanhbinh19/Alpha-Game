using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIENsService
{ 
    Task<UserHIENs> GetUserHIENsAsync(string userId, string id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHIENsAsync(string userId, UserHIENs HIENs, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserHIENs> GetSumUserHIENsAsync(string userId);
}