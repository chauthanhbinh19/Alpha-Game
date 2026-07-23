using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardMilitariesService
{
    Task<List<CardMilitaries>> GetAllEquipmentPowerAsync(string userId, List<CardMilitaries> cardMilitaryList);
    Task<List<CardMilitaries>> GetAllRankPowerAsync(string userId, List<CardMilitaries> cardMilitaryList);
    Task<List<CardMilitaries>> GetAllMasterPowerAsync(string userId, List<CardMilitaries> cardMilitaryList);
    Task<List<CardMilitaries>> GetAllSpiritBeastPowerAsync(string userId, List<CardMilitaries> cardMilitaryList);
    Task<List<CardMilitaries>> GetSkillsAsync(string userId, List<CardMilitaries> cardMilitaryList);
    Task<List<CardMilitaries>> GetUserCardMilitariesAsync(string userId, string search, string type, int pageSize, int offset, string rare, UserStatsContextDTO sharedContext = null);
    Task<List<CardMilitaries>> GetUserCardMilitariesTeamAsync(string userId, string teamId, string position, UserStatsContextDTO sharedContext = null);
    Task<List<CardMilitaries>> GetUserCardMilitariesTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null);
    Task<Dictionary<string, int>> GetUniqueUserCardMilitariesTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardMilitaryAsync(string userId, string teamId, string position, string cardId);
    Task<int> GetUserCardMilitariesCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardMilitariesTeamsPositionCountAsync(string userId, string teamId, string position);
    Task<int> GetUserCardMilitariesTeamsCountAsync(string userId, string teamId);
    Task<bool> InsertUserCardMilitaryAsync(string userId, CardMilitaries cardMilitary);
    Task<bool> InsertOrUpdateUserCardMilitariesBatchAsync(string userId, List<CardMilitaries> cardMilitaries);
    Task<bool> UpdateUserCardMilitaryLevelAsync(string userId, CardMilitaries cardMilitary);
    Task<bool> UpdateUserCardMilitaryStarAsync(string userId, CardMilitaries cardMilitary);
    Task<bool> UpdateUserCardMilitaryBreakthroughAsync(string userId, CardMilitaries cardMilitary, int star, double quantity);
    Task<CardMilitaries> GetUserCardMilitaryByIdAsync(string userId, string Id, UserStatsContextDTO sharedContext = null);
    Task<List<CardMilitaries>> GetAllUserCardMilitariesInTeamAsync(string userId, UserStatsContextDTO sharedContext = null);
}