using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardAdmiralsService
{
    Task<List<CardAdmirals>> GetAllEquipmentPowerAsync(string userId, List<CardAdmirals> cardAdmiralList);
    Task<List<CardAdmirals>> GetAllRankPowerAsync(string userId, List<CardAdmirals> cardAdmiralList);
    Task<List<CardAdmirals>> GetAllMasterPowerAsync(string userId, List<CardAdmirals> cardAdmiralList);
    Task<List<CardAdmirals>> GetAllSpiritBeastPowerAsync(string userId, List<CardAdmirals> cardAdmiralList);
    Task<List<CardAdmirals>> GetSkillsAsync(string userId, List<CardAdmirals> cardAdmiralList);
    Task<List<CardAdmirals>> GetUserCardAdmiralsAsync(string userId, string search, string type, int pageSize, int offset, string rare, UserStatsContextDTO sharedContext = null);
    Task<List<CardAdmirals>> GetUserCardAdmiralsTeamAsync(string userId, string teamId, string position, UserStatsContextDTO sharedContext = null);
    Task<List<CardAdmirals>> GetUserCardAdmiralsTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null);
    Task<Dictionary<string, int>> GetUniqueUserCardAdmiralsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardAdmiralAsync(string userId, string teamId, string position, string cardId);
    Task<int> GetUserCardAdmiralsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardAdmiralsTeamsPositionCountAsync(string userId, string teamId, string position);
    Task<int> GetUserCardAdmiralsTeamsCountAsync(string userId, string teamId);
    Task<bool> InsertUserCardAdmiralAsync(string userId, CardAdmirals cardAdmiral);
    Task<bool> InsertOrUpdateUserCardAdmiralsBatchAsync(string userId, List<CardAdmirals> cardAdmirals);
    Task<bool> UpdateUserCardAdmiralLevelAsync(string userId, CardAdmirals cardAdmiral);
    Task<bool> UpdateUserCardAdmiralStarAsync(string userId, CardAdmirals cardAdmiral);
    Task<bool> UpdateUserCardAdmiralBreakthroughAsync(string userId, CardAdmirals cardAdmiral, int star, double quantity);
    Task<CardAdmirals> GetUserCardAdmiralByIdAsync(string userId, string Id, UserStatsContextDTO sharedContext = null);
    Task<List<CardAdmirals>> GetAllUserCardAdmiralsInTeamAsync(string userId, UserStatsContextDTO sharedContext = null);
}