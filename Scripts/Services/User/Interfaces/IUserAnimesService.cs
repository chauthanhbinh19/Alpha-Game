using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserAnimesService
{ 
    Task<UserAnimes> GetUserAnimesAsync(string userId, string id);
    Task InsertOrUpdateUserAnimesAsync(string userId, UserAnimes Animes, string id);
    Task<UserAnimes> GetSumUserAnimesAsync(string userId);
}