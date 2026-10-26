using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIHNsRepository
{
    Task<UserHIHNs> GetUserHIHNsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserHIHNs>> InsertOrUpdateUserHIHNsAsync(string userId, UserHIHNs HIHNs, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserHIHNs> GetSumUserHIHNsAsync(string userId);
}