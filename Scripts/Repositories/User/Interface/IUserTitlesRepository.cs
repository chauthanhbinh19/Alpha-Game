using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTitlesRepository
{
    Task<List<Titles>> GetUserTitlesAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserTitlesCountAsync(string user_id, string rare);
    Task<bool> InsertUserTitleAsync(Titles Titles, string userId);
    Task<bool> UpdateTitleLevelAsync(Titles Titles, int TitleLevel);
    Task<bool> UpdateTitleBreakthroughAsync(Titles Titles, int star, double quantity);
    Task<Titles> GetUserTitleByIdAsync(string user_id, string Id);
    Task<Titles> SumPowerUserTitlesAsync();
}