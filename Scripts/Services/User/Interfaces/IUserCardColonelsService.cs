using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardColonelsService
{
    Task<List<CardColonels>> GetAllEquipmentPowerAsync(string userId, List<CardColonels> cardColonelList);
    Task<List<CardColonels>> GetAllRankPowerAsync(string userId, List<CardColonels> cardColonelList);
    Task<List<CardColonels>> GetAllMasterPowerAsync(string userId, List<CardColonels> cardColonelList);
    Task<List<CardColonels>> GetAllSpiritBeastPowerAsync(string userId, List<CardColonels> cardColonelList);
    Task<List<CardColonels>> GetSkillsAsync(string userId, List<CardColonels> cardColonelList);
    Task<List<CardColonels>> GetUserCardColonelsAsync(string userId, string search, string type, int pageSize, int offset, string rare, UserStatsContextDTO sharedContext = null);
    Task<List<CardColonels>> GetUserCardColonelsTeamAsync(string userId, string teamId, string position, UserStatsContextDTO sharedContext = null);
    Task<List<CardColonels>> GetUserCardColonelsTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null);
    Task<Dictionary<string, int>> GetUniqueUserCardColonelsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardColonelAsync(string userId, string teamId, string position, string cardId);
    Task<int> GetUserCardColonelsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardColonelsTeamsPositionCountAsync(string userId, string teamId, string position);
    Task<int> GetUserCardColonelsTeamsCountAsync(string userId, string teamId);
    Task<bool> InsertUserCardColonelAsync(string userId, CardColonels cardColonel);
    Task<bool> InsertOrUpdateUserCardColonelsBatchAsync(string userId, List<CardColonels> cardColonels);
    Task<bool> UpdateUserCardColonelLevelAsync(string userId, CardColonels cardColonel);
    Task<bool> UpdateUserCardColonelStarAsync(string userId, CardColonels cardColonel);
    Task<bool> UpdateUserCardColonelBreakthroughAsync(string userId, CardColonels cardColonel, int star, double quantity);
    Task<CardColonels> GetUserCardColonelByIdAsync(string userId, string Id, UserStatsContextDTO sharedContext = null);
    Task<List<CardColonels>> GetAllUserCardColonelsInTeamAsync(string userId, UserStatsContextDTO sharedContext = null);
}