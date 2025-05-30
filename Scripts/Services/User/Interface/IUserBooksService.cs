using System.Collections.Generic;

public interface IUserBooksService
{
    List<Books> GetFinalPower(string user_id, List<Books> BooksList);
    List<Books> GetAllEquipmentPower(string user_id, List<Books> BooksList);
    List<Books> GetAllRankPower(string user_id, List<Books> BooksList);
    List<Books> GetAllAnimeStatsPower(string user_id, List<Books> BooksList);
    Books GetNewLevelPower(Books c, double coefficient);
    Books GetNewBreakthroughPower(Books c, double coefficient);
    List<Books> GetUserBooks(string user_id, string type, int pageSize, int offset);
    List<Books> GetUserBooksTeam(string user_id, string teamId, string position);
    Dictionary<string, int> GetUniqueBookTypesTeam(string teamId);
    int GetUserBooksCount(string user_id, string type);
    bool InsertUserBooks(Books books);
    bool UpdateBooksLevel(Books books, int cardLevel);
    bool UpdateBooksBreakthrough(Books books, int star, int quantity);
    bool InsertFactBooks(Books books);
    bool UpdateFactBooks(Books books);
    bool UpdateTeamFactBooks(string team_id, string position, string book_id);
    Books GetUserBooksById(string user_id, string Id);
    List<Books> GetAllUserBooksInTeam(string user_id);
}