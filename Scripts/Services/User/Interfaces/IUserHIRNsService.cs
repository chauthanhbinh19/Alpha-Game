using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIRNsService
{ 
    Task<UserHIRNs> GetUserHIRNsAsync(string userId, string id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHIRNsAsync(string userId, UserHIRNs HIRNs, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserHIRNs> GetSumUserHIRNsAsync(string userId);
}