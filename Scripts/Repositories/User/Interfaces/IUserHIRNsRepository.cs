using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIRNsRepository
{
    Task<UserHIRNs> GetUserHIRNsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserHIRNs>> InsertOrUpdateUserHIRNsAsync(string userId, UserHIRNs HIRNs, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserHIRNs> GetSumUserHIRNsAsync(string userId);
}