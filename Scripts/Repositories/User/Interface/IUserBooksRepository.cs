using System.Collections.Generic;

public interface IUserBooksRepository
{
    List<Books> GetUserBooks(string user_id, string type, int pageSize, int offset, string rare);
    List<Books> GetUserBooksTeam(string teamId);
    Dictionary<string, int> GetUniqueBookTypesTeam(string teamId);
    int GetUserBooksCount(string user_id, string type, string rare);
    bool InsertUserBooks(Books books);
    bool UpdateBooksLevel(Books books, int cardLevel);
    bool UpdateBooksBreakthrough(Books books, int star, double quantity);
    bool UpdateTeamBooks(string team_id, string position, string book_id);
    Books GetUserBooksById(string user_id, string Id);
    List<Books> GetAllUserBooksInTeam(string user_id);
}