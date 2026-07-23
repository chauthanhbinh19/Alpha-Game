using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardColonelsRepository
{
    Task<List<CardColonels>> GetUserCardColonelsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardColonels>> GetUserCardColonelsTeamAsync(string userId, string teamId, string position);
    Task<List<CardColonels>> GetUserCardColonelsTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardColonelsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardColonelAsync(string userId, string team_id, string position, string cardId);
    Task<int> GetUserCardColonelsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardColonelsTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardColonelsTeamsCountAsync(string userId, string team_id);
    Task<bool> InsertUserCardColonelAsync(string userId, CardColonels cardColonel);
    Task<bool> InsertOrUpdateUserCardColonelsBatchAsync(string userId, List<CardColonels> cardColonels);
    Task<bool> UpdateUserCardColonelLevelAsync(string userId, CardColonels cardColonel);
    Task<bool> UpdateUserCardColonelStarAsync(string userId, CardColonels cardColonel);
    Task<bool> UpdateUserCardColonelBreakthroughAsync(string userId, CardColonels cardColonel, int star, double quantity);
    Task<CardColonels> GetUserCardColonelByIdAsync(string userId, string Id);
    Task<List<CardColonels>> GetAllUserCardColonelsInTeamAsync(string userId);
}