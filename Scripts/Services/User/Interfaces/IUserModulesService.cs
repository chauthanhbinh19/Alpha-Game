using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserModulesService
{ 
    Task<UserModules> GetUserModulesAsync(string id);
    Task InsertOrUpdateUserModulesAsync(string user_id, UserModules Modules, string id);
    Task<UserModules> GetSumUserModulesAsync(string user_id);
}