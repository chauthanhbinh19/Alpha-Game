using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserModulesRepository
{
    Task<UserModules> GetUserModulesAsync(string type);
    Task InsertOrUpdateUserModulesAsync(string user_id, UserModules Modules, string id);
    Task<UserModules> GetSumUserModulesAsync(string user_id);
}