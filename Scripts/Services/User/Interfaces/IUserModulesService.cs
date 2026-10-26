using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserModulesService
{ 
    Task<UserModules> GetUserModulesAsync(string userId, string moduleId, IStats stat);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserModulesAsync(string userId, UserModules module, IStats stat, List<RecipeItemDetailDTO> totalDeductions = null);
    Task<UserModules> GetSumUserModulesAsync(string userId, IStats stat);
}