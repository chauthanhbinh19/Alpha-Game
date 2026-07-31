using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardSoldiersService
{
    Task<List<CardSoldiers>> GetAllEquipmentPowerAsync(string userId, List<CardSoldiers> cardSoldierList);
    Task<List<CardSoldiers>> GetAllRankPowerAsync(string userId, List<CardSoldiers> cardSoldierList);
    Task<List<CardSoldiers>> GetAllMasterPowerAsync(string userId, List<CardSoldiers> cardSoldierList);
    Task<List<CardSoldiers>> GetUserCardSoldiersAsync(string userId, string search, string type, int pageSize, int offset, string rare, UserStatsContextDTO sharedContext = null);
    Task<List<CardSoldiers>> GetUserCardSoldiersTeamAsync(string userId, string teamId, string position, UserStatsContextDTO sharedContext = null);
    Task<List<CardSoldiers>> GetUserCardSoldiersTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null);
    Task<Dictionary<string, int>> GetUniqueUserCardSoldiersTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardSoldierAsync(string userId, string teamId, string position, string cardId);
    Task<int> GetUserCardSoldiersCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardSoldiersTeamsPositionCountAsync(string userId, string teamId, string position);
    Task<int> GetUserCardSoldiersTeamsCountAsync(string userId, string teamId);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardSoldierAsync(string userId, CardSoldiers cardSoldier);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardSoldiersBatchAsync(string userId, List<CardSoldiers> cardSolders);
    Task<bool> UpdateUserCardSoldierLevelAsync(string userId, CardSoldiers cardSoldier);
    Task<bool> UpdateUserCardSoldierStarAsync(string userId, CardSoldiers cardSoldier);
    Task<CardSoldiers> GetUserCardSoldierByIdAsync(string userId, string Id, UserStatsContextDTO sharedContext = null);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId, UserStatsContextDTO sharedContext = null);
}