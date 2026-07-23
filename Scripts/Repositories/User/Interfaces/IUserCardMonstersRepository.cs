using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardMonstersRepository
{
    Task<List<CardMonsters>> GetUserCardMonstersAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardMonsters>> GetUserCardMonstersTeamAsync(string userId, string teamId, string position);
    Task<List<CardMonsters>> GetUserCardMonstersTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardMonstersTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardMonsterAsync(string userId, string team_id, string position, string cardId);
    Task<int> GetUserCardMonstersCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardMonstersTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardMonstersTeamsCountAsync(string userId, string team_id);
    Task<bool> InsertUserCardMonsterAsync(string userId, CardMonsters cardMonster);
    Task<bool> InsertOrUpdateUserCardMonstersBatchAsync(string userId, List<CardMonsters> cardMonsters);
    Task<bool> UpdateUserCardMonsterLevelAsync(string userId, CardMonsters cardMonster);
    Task<bool> UpdateUserCardMonsterStarAsync(string userId, CardMonsters cardMonster);
    Task<bool> UpdateUserCardMonsterBreakthroughAsync(string userId, CardMonsters cardMonster, int star, double quantity);
    Task<CardMonsters> GetUserCardMonsterByIdAsync(string userId, string Id);
    Task<List<CardMonsters>> GetAllUserCardMonstersInTeamAsync(string userId);
}