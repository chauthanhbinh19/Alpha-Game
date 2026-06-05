using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserModulesRepository
{
    Task<UserModules> GetUserModulesAsync(string moduleId, string userTable, string objectColumn);
    Task InsertOrUpdateUserModulesAsync(string userId, UserModules module, string objectId, string userTable, string objectColumn);
    Task<UserModules> GetSumUserModulesAsync(string userId, string objectId, string userTable, string objectColumn);
}