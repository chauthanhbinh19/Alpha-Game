using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardMonstersRepository
{
    Task<List<CardMonsters>> GetUserCardMonstersAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<List<CardMonsters>> GetUserCardMonstersTeamAsync(string user_id, string teamId, string position);
    Task<List<CardMonsters>> GetUserCardMonstersTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardMonstersTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardMonsterAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardMonstersCountAsync(string user_id, string type, string rare);
    Task<int> GetUserCardMonstersTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardMonstersTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardMonsterAsync(CardMonsters CardMonsters);
    Task<bool> UpdateCardMonsterLevelAsync(CardMonsters CardMonsters, int cardLevel);
    Task<bool> UpdateCardMonsterBreakthroughAsync(CardMonsters CardMonsters, int star, double quantity);
    Task<CardMonsters> GetUserCardMonsterByIdAsync(string user_id, string Id);
    Task<List<CardMonsters>> GetAllUserCardMonstersInTeamAsync(string user_id);
}