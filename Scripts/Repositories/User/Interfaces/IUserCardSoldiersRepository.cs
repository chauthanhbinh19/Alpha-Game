using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardSoldiersRepository
{
    Task<List<CardSoldiers>> GetUserCardSoldiersAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardSoldiers>> GetUserCardSoldiersTeamAsync(string user_id, string teamId, string position);
    Task<List<CardSoldiers>> GetUserCardSoldiersTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardSoldiersTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardSoldierAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardSoldiersCountAsync(string user_id, string search, string type, string rare);
    Task<int> GetUserCardSoldiersTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardSoldiersTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardSoldierAsync(CardSoldiers cardSoldier);
    Task<bool> InsertOrUpdateUserCardSoldiersBatchAsync(List<CardSoldiers> cardSoldiers);
    Task<bool> UpdateCardSoldierLevelAsync(CardSoldiers cardSoldier, int cardLevel);
    Task<bool> UpdateCardSoldierBreakthroughAsync(CardSoldiers cardSoldier, int star, double quantity);
    Task<CardSoldiers> GetUserCardSoldierByIdAsync(string user_id, string Id);
    Task<List<CardSoldiers>> GetAllUserCardSoldiersInTeamAsync(string user_id);
}