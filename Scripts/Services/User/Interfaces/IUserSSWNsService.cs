using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserSSWNsService
{ 
    Task<UserSSWNs> GetUserSSWNsAsync(string userId, string id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSSWNsAsync(string userId, UserSSWNs SSWNs, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserSSWNs> GetSumUserSSWNsAsync(string userId);
}