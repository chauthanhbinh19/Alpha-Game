using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBooksRepository
{
    Task<List<Books>> GetUserBooksAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<Books>> GetUserBooksTeamAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserBooksTypesTeamAsync(string userId, string teamId);
    Task<int> GetUserBooksCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserBookAsync(string userId, Books book);
    Task<bool> InsertOrUpdateUserBooksBatchAsync(string userId, List<Books> books);
    Task<bool> UpdateUserBookLevelAsync(string userId, Books book);
    Task<bool> UpdateUserBookStarAsync(string userId, Books book);
    Task<bool> UpdateUserBookBreakthroughAsync(string userId, Books book, int star, double quantity);
    Task<bool> UpdateTeamUserBookAsync(string userId, string team_id, string position, string book_id);
    Task<Books> GetUserBookByIdAsync(string userId, string Id);
    Task<List<Books>> GetAllUserBooksInTeamAsync(string userId);
}