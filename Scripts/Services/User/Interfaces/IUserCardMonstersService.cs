using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardMonstersService
{
    Task<List<CardMonsters>> GetAllEquipmentPowerAsync(string userId, List<CardMonsters> cardMonsterList);
    Task<List<CardMonsters>> GetAllRankPowerAsync(string userId, List<CardMonsters> cardMonsterList);
    Task<List<CardMonsters>> GetAllMasterPowerAsync(string userId, List<CardMonsters> cardMonsterList);
    Task<List<CardMonsters>> GetAllSpiritBeastPowerAsync(string userId, List<CardMonsters> cardMonsterList);
    Task<List<CardMonsters>> GetSkillsAsync(string userId, List<CardMonsters> cardMonsterList);
    Task<List<CardMonsters>> GetUserCardMonstersAsync(string userId, string search, string type, int pageSize, int offset, string rare, UserStatsContextDTO sharedContext = null);
    Task<List<CardMonsters>> GetUserCardMonstersTeamAsync(string userId, string teamId, string position, UserStatsContextDTO sharedContext = null);
    Task<List<CardMonsters>> GetUserCardMonstersTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null);
    Task<Dictionary<string, int>> GetUniqueUserCardMonstersTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardMonsterAsync(string userId, string teamId, string position, string cardId);
    Task<int> GetUserCardMonstersCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardMonstersTeamsPositionCountAsync(string userId, string teamId, string position);
    Task<int> GetUserCardMonstersTeamsCountAsync(string userId, string teamId);
    Task<bool> InsertUserCardMonsterAsync(string userId, CardMonsters cardMonster);
    Task<bool> InsertOrUpdateUserCardMonstersBatchAsync(string userId, List<CardMonsters> cardMonsters);
    Task<bool> UpdateUserCardMonsterLevelAsync(string userId, CardMonsters cardMonster);
    Task<bool> UpdateUserCardMonsterStarAsync(string userId, CardMonsters cardMonster);
    Task<bool> UpdateUserCardMonsterBreakthroughAsync(string userId, CardMonsters cardMonster, int star, double quantity);
    Task<CardMonsters> GetUserCardMonsterByIdAsync(string userId, string Id, UserStatsContextDTO sharedContext = null);
    Task<List<CardMonsters>> GetAllUserCardMonstersInTeamAsync(string userId, UserStatsContextDTO sharedContext = null);
}