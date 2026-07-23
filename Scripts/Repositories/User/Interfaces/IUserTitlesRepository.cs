using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTitlesRepository
{
    Task<List<Titles>> GetUserTitlesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserTitlesCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserTitleAsync(Titles title, string userId);
    Task<bool> InsertOrUpdateUserTitlesBatchAsync(string userId, List<Titles> titles);
    Task<bool> UpdateUserTitleLevelAsync(string userId, Titles title);
    Task<bool> UpdateUserTitleStarAsync(string userId, Titles title);
    Task<bool> UpdateUserTitleBreakthroughAsync(string userId, Titles title, int star, double quantity);
    Task<Titles> GetUserTitleByIdAsync(string userId, string Id);
    Task<Titles> SumPowerUserTitlesAsync(string userId);
}