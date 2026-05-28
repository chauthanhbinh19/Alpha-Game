using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardMonstersService
{
    Task<List<CardMonsters>> GetAllEquipmentPowerAsync(string user_id, List<CardMonsters> CardMonstersList);
    Task<List<CardMonsters>> GetAllRankPowerAsync(string user_id, List<CardMonsters> CardMonstersList);
    Task<List<CardMonsters>> GetAllMasterPowerAsync(string user_id, List<CardMonsters> CardMonstersList);
    Task<List<CardMonsters>> GetAllSpiritBeastPowerAsync(string user_id, List<CardMonsters> cardMonsters);
    Task<List<CardMonsters>> GetSkillsAsync(string user_id, List<CardMonsters> CardMonstersList);
    Task<List<CardMonsters>> GetUserCardMonstersAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardMonsters>> GetUserCardMonstersTeamAsync(string user_id, string teamId, string position);
    Task<List<CardMonsters>> GetUserCardMonstersTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardMonstersTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardMonsterAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardMonstersCountAsync(string user_id, string search, string type, string rare);
    Task<int> GetUserCardMonstersTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardMonstersTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardMonsterAsync(CardMonsters cardMonster);
    Task<bool> InsertOrUpdateUserCardMonstersBatchAsync(List<CardMonsters> cardMonsters);
    Task<bool> UpdateCardMonsterLevelAsync(CardMonsters cardMonster, int level);
    Task<bool> UpdateCardMonsterBreakthroughAsync(CardMonsters cardMonster, int star, double quantity);
    Task<CardMonsters> GetUserCardMonsterByIdAsync(string user_id, string Id);
    Task<List<CardMonsters>> GetAllUserCardMonstersInTeamAsync(string user_id);
}