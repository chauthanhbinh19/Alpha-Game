using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardCaptainsService
{
    Task<List<CardCaptains>> GetAllEquipmentPowerAsync(string userId, List<CardCaptains> cardCaptainList);
    Task<List<CardCaptains>> GetAllRankPowerAsync(string userId, List<CardCaptains> cardCaptainList);
    Task<List<CardCaptains>> GetAllMasterPowerAsync(string userId, List<CardCaptains> cardCaptainList);
    Task<List<CardCaptains>> GetUserCardCaptainsAsync(string userId, string search, string type, int pageSize, int offset, string rare, UserStatsContextDTO sharedContext = null);
    Task<List<CardCaptains>> GetUserCardCaptainsTeamAsync(string userId, string teamId, string position, UserStatsContextDTO sharedContext = null);
    Task<List<CardCaptains>> GetUserCardCaptainsTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null);
    Task<List<CardCaptains>> GetUserCardCaptainsInTeamAsync(string userId, UserStatsContextDTO sharedContext = null);
    Task<List<CardCaptains>> GetUserCardCaptainsInTeamSimpleAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardCaptainsTypesTeamAsync(string userId, string teamId);
    Task<InsertOrUpdateResult<bool>> UpdateTeamUserCardCaptainAsync(string userId, string teamId, string position, string cardId);
    Task<int> GetUserCardCaptainsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardCaptainsTeamsPositionCountAsync(string userId, string teamId, string position);
    Task<int> GetUserCardCaptainsTeamsCountAsync(string userId, string teamId);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardCaptainAsync(string userId, CardCaptains cardCaptain);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardCaptainsBatchAsync(string userId, List<CardCaptains> cardCaptains);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardCaptainLevelAsync(string userId, CardCaptains cardCaptain);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardCaptainStarAsync(string userId, CardCaptains cardCaptain);
    Task<CardCaptains> GetUserCardCaptainByIdAsync(string userId, string Id, UserStatsContextDTO sharedContext = null);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId, UserStatsContextDTO sharedContext = null);
}