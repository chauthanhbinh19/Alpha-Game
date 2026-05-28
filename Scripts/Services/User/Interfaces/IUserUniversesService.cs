using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUniversesService
{ 
    Task<UserUniverses> GetUserUniversesAsync(string id);
    Task InsertOrUpdateUserUniversesAsync(string user_id, UserUniverses Universes, string id);
    Task<UserUniverses> GetSumUserUniversesAsync(string user_id);
}