using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardMilitariesRepository
{
    Task<List<CardMilitaries>> GetUserCardMilitariesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardMilitaries>> GetUserCardMilitariesTeamAsync(string userId, string teamId, string position);
    Task<List<CardMilitaries>> GetUserCardMilitariesTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardMilitariesTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardMilitaryAsync(string userId, string team_id, string position, string cardId);
    Task<int> GetUserCardMilitariesCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardMilitariesTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardMilitariesTeamsCountAsync(string userId, string team_id);
    Task<bool> InsertUserCardMilitaryAsync(string userId, CardMilitaries cardMilitary);
    Task<bool> InsertOrUpdateUserCardMilitariesBatchAsync(string userId, List<CardMilitaries> cardMilitaries);
    Task<bool> UpdateUserCardMilitaryLevelAsync(string userId, CardMilitaries cardMilitary);
    Task<bool> UpdateUserCardMilitaryStarAsync(string userId, CardMilitaries cardMilitary);
    Task<bool> UpdateUserCardMilitaryBreakthroughAsync(string userId, CardMilitaries cardMilitary, int star, double quantity);
    Task<CardMilitaries> GetUserCardMilitaryByIdAsync(string userId, string Id);
    Task<List<CardMilitaries>> GetAllUserCardMilitariesInTeamAsync(string userId);
}