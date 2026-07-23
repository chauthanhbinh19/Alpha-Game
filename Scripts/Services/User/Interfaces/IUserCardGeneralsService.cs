using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardGeneralsService
{
    Task<List<CardGenerals>> GetAllEquipmentPowerAsync(string userId, List<CardGenerals> cardGeneralList);
    Task<List<CardGenerals>> GetAllRankPowerAsync(string userId, List<CardGenerals> cardGeneralList);
    Task<List<CardGenerals>> GetAllMasterPowerAsync(string userId, List<CardGenerals> cardGeneralList);
    Task<List<CardGenerals>> GetAllSpiritBeastPowerAsync(string userId, List<CardGenerals> cardGeneralList);
    Task<List<CardGenerals>> GetSkillsAsync(string userId, List<CardGenerals> cardGeneralList);
    Task<List<CardGenerals>> GetUserCardGeneralsAsync(string userId, string search, string type, int pageSize, int offset, string rare, UserStatsContextDTO sharedContext = null);
    Task<List<CardGenerals>> GetUserCardGeneralsTeamAsync(string userId, string teamId, string position, UserStatsContextDTO sharedContext = null);
    Task<List<CardGenerals>> GetUserCardGeneralsTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null);
    Task<Dictionary<string, int>> GetUniqueUserCardGeneralsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardGeneralAsync(string userId, string teamId, string position, string cardId);
    Task<int> GetUserCardGeneralsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardGeneralsTeamsPositionCountAsync(string userId, string teamId, string position);
    Task<int> GetUserCardGeneralsTeamsCountAsync(string userId, string teamId);
    Task<bool> InsertUserCardGeneralAsync(string userId, CardGenerals cardGeneral);
    Task<bool> InsertOrUpdateUserCardGeneralsBatchAsync(string userId, List<CardGenerals> cardGenerals);
    Task<bool> UpdateUserCardGeneralLevelAsync(string userId, CardGenerals cardGeneral);
    Task<bool> UpdateUserCardGeneralStarAsync(string userId, CardGenerals cardGeneral);
    Task<bool> UpdateUserCardGeneralBreakthroughAsync(string userId, CardGenerals cardGeneral, int star, double quantity);
    Task<CardGenerals> GetUserCardGeneralByIdAsync(string userId, string Id, UserStatsContextDTO sharedContext = null);
    Task<List<CardGenerals>> GetAllUserCardGeneralsInTeamAsync(string userId, UserStatsContextDTO sharedContext = null);
}