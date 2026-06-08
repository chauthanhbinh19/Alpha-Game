using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUniversesRepository
{
    Task<UserUniverses> GetUserUniversesAsync(string type);
    Task InsertOrUpdateUserUniversesAsync(string userId, UserUniverses Universes, string id);
    Task<UserUniverses> GetSumUserUniversesAsync(string userId);
}