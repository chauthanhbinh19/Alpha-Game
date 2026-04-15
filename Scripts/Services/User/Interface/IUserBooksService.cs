using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBooksService
{
    Task<List<Books>> GetAllEquipmentPowerAsync(string user_id, List<Books> BooksList);
    Task<List<Books>> GetAllRankPowerAsync(string user_id, List<Books> BooksList);
    Task<List<Books>> GetAllMasterPowerAsync(string user_id, List<Books> BooksList);
    Task<Books> GetNewLevelPowerAsync(Books c, double coefficient);
    Task<Books> GetNewBreakthroughPowerAsync(Books c, double coefficient);
    Task<List<Books>> GetUserBooksAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    // Task<List<Books>> GetUserBooksTeamAsync(string teamId);
    Task<Dictionary<string, int>> GetUniqueBooksTypesTeamAsync(string teamId);
    Task<int> GetUserBooksCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserBookAsync(Books books);
    Task<bool> UpdateBookLevelAsync(Books books, int cardLevel);
    Task<bool> UpdateBookBreakthroughAsync(Books books, int star, double quantity);
    Task<bool> UpdateTeamBookAsync(string team_id, string position, string book_id);
    Task<Books> GetUserBookByIdAsync(string user_id, string Id);
    Task<List<Books>> GetAllUserBooksInTeamAsync(string user_id);
}