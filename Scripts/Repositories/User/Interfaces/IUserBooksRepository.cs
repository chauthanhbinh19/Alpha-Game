using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBooksRepository
{
    Task<List<Books>> GetUserBooksAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<Books>> GetUserBooksTeamAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserBooksTypesTeamAsync(string userId, string teamId);
    Task<int> GetUserBooksCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Books>> InsertOrUpdateUserBookAsync(string userId, Books book);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Books>>> InsertOrUpdateUserBooksBatchAsync(string userId, List<Books> books);
    Task<InsertOrUpdateResult<bool>> UpdateUserBookLevelAsync(string userId, Books book);
    Task<InsertOrUpdateResult<bool>> UpdateUserBookStarAsync(string userId, Books book);
    Task<bool> UpdateTeamUserBookAsync(string userId, string team_id, string position, string book_id);
    Task<Books> GetUserBookByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId);
    Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId);
}