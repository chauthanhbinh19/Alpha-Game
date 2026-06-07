using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserAnimesService
{ 
    Task<UserAnimes> GetUserAnimesAsync(string id);
    Task InsertOrUpdateUserAnimesAsync(string user_id, UserAnimes Animes, string id);
    Task<UserAnimes> GetSumUserAnimesAsync(string user_id);
}