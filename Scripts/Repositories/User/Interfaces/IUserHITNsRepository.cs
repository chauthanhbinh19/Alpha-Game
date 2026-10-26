using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHITNsRepository
{
    Task<UserHITNs> GetUserHITNsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserHITNs>> InsertOrUpdateUserHITNsAsync(string userId, UserHITNs HITNs, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserHITNs> GetSumUserHITNsAsync(string userId);
}