using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserModulesService
{ 
    Task<UserModules> GetUserModulesAsync(string moduleId, IStats stat);
    Task InsertOrUpdateUserModulesAsync(string userId, UserModules module, IStats stat);
    Task<UserModules> GetSumUserModulesAsync(string userId, IStats stat);
}