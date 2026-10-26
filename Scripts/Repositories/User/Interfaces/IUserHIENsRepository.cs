using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIENsRepository
{
    Task<UserHIENs> GetUserHIENsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserHIENs>> InsertOrUpdateUserHIENsAsync(string userId, UserHIENs HIENs, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserHIENs> GetSumUserHIENsAsync(string userId);
}