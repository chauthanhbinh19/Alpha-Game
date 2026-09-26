using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUniversesRepository
{
    Task<UserUniverses> GetUserUniversesAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserUniverses>> InsertOrUpdateUserUniversesAsync(string userId, UserUniverses Universes, string id);
    Task<UserUniverses> GetSumUserUniversesAsync(string userId);
}