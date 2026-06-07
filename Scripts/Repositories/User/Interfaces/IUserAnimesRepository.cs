using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserAnimesRepository
{
    Task<UserAnimes> GetUserAnimesAsync(string type);
    Task InsertOrUpdateUserAnimesAsync(string user_id, UserAnimes Animes, string id);
    Task<UserAnimes> GetSumUserAnimesAsync(string user_id);
}