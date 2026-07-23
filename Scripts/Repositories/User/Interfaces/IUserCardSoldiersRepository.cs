using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardSoldiersRepository
{
    Task<List<CardSoldiers>> GetUserCardSoldiersAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardSoldiers>> GetUserCardSoldiersTeamAsync(string userId, string teamId, string position);
    Task<List<CardSoldiers>> GetUserCardSoldiersTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardSoldiersTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamCardSoldierAsync(string userId, string team_id, string position, string cardId);
    Task<int> GetUserCardSoldiersCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardSoldiersTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardSoldiersTeamsCountAsync(string userId, string team_id);
    Task<bool> InsertUserCardSoldierAsync(string userId, CardSoldiers cardSoldier);
    Task<bool> InsertOrUpdateUserCardSoldiersBatchAsync(string userId, List<CardSoldiers> cardSoldiers);
    Task<bool> UpdateUserCardSoldierLevelAsync(string userId, CardSoldiers cardSoldier);
    Task<bool> UpdateUserCardSoldierStarAsync(string userId, CardSoldiers cardSoldier);
    Task<bool> UpdateUserCardSoldierBreakthroughAsync(string userId, CardSoldiers cardSoldier, int star, double quantity);
    Task<CardSoldiers> GetUserCardSoldierByIdAsync(string userId, string Id);
    Task<List<CardSoldiers>> GetAllUserCardSoldiersInTeamAsync(string userId);
}