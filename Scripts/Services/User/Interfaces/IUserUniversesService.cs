using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUniversesService
{ 
    Task<UserUniverses> GetUserUniversesAsync(string userId, string id);
    Task InsertOrUpdateUserUniversesAsync(string userId, UserUniverses Universes, string id);
    Task<UserUniverses> GetSumUserUniversesAsync(string userId);
}