using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBooksService
{
    Task<List<Books>> GetAllEquipmentPowerAsync(string userId, List<Books> bookList);
    Task<List<Books>> GetAllRankPowerAsync(string userId, List<Books> bookList);
    Task<List<Books>> GetAllMasterPowerAsync(string userId, List<Books> bookList);
    Task<List<Books>> GetUserBooksAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    // Task<List<Books>> GetUserBooksTeamAsync(string teamId);
    Task<Dictionary<string, int>> GetUniqueUserBooksTypesTeamAsync(string userId, string teamId);
    Task<int> GetUserBooksCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBookAsync(string userId, Books book);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBooksBatchAsync(string userId, List<Books> books);
    Task<bool> UpdateUserBookLevelAsync(string userId, Books book);
    Task<bool> UpdateUserBookStarAsync(string userId, Books book);
    Task<bool> UpdateTeamUserBookAsync(string userId, string teamId, string position, string bookId);
    Task<Books> GetUserBookByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId, UserStatsContextDTO sharedContext = null);
}