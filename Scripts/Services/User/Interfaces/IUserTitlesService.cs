using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTitlesService
{
    Task<List<Titles>> GetUserTitlesAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserTitlesCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserTitleAsync(Titles title, string userId);
    Task<bool> InsertOrUpdateUserTitlesBatchAsync(List<Titles> titles);
    Task<bool> UpdateTitleLevelAsync(Titles title, int level);
    Task<bool> UpdateTitleBreakthroughAsync(Titles title, int star, double quantity);
    Task<Titles> GetUserTitleByIdAsync(string user_id, string Id);
    Task<Titles> SumPowerUserTitlesAsync();
}