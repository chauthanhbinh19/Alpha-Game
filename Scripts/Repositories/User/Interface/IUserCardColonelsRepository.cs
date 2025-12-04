using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardColonelsRepository
{
    Task<List<CardColonels>> GetUserCardColonelsAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<List<CardColonels>> GetUserCardColonelsTeamAsync(string user_id, string teamId, string position);
    Task<List<CardColonels>> GetUserCardColonelsTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardColonelsTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardColonelAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardColonelsCountAsync(string user_id, string type, string rare);
    Task<int> GetUserCardColonelsTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardColonelsTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardColonelAsync(CardColonels CardColonels);
    Task<bool> UpdateCardColonelLevelAsync(CardColonels CardColonels, int cardLevel);
    Task<bool> UpdateCardColonelBreakthroughAsync(CardColonels CardColonels, int star, double quantity);
    Task<CardColonels> GetUserCardColonelByIdAsync(string user_id, string Id);
    Task<List<CardColonels>> GetAllUserCardColonelsInTeamAsync(string user_id);
}