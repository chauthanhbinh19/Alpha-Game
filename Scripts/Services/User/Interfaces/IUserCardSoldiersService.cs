using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardSoldiersService
{
    Task<List<CardSoldiers>> GetAllEquipmentPowerAsync(string user_id, List<CardSoldiers> CardSoldiersList);
    Task<List<CardSoldiers>> GetAllRankPowerAsync(string user_id, List<CardSoldiers> CardSoldiersList);
    Task<List<CardSoldiers>> GetAllMasterPowerAsync(string user_id, List<CardSoldiers> CardSoldiersList);
    Task<List<CardSoldiers>> GetSkillsAsync(string user_id, List<CardSoldiers> CardSoldiersList);
    Task<List<CardSoldiers>> GetUserCardSoldiersAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardSoldiers>> GetUserCardSoldiersTeamAsync(string user_id, string teamId, string position);
    Task<List<CardSoldiers>> GetUserCardSoldiersTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardSoldiersTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardSoldierAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardSoldiersCountAsync(string user_id, string search, string type, string rare);
    Task<int> GetUserCardSoldiersTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardSoldiersTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardSoldierAsync(CardSoldiers cardSoldier);
    Task<bool> InsertOrUpdateUserCardSoldiersBatchAsync(List<CardSoldiers> cardSolders);
    Task<bool> UpdateCardSoldierLevelAsync(CardSoldiers cardSoldier, int level);
    Task<bool> UpdateCardSoldierBreakthroughAsync(CardSoldiers cardSoldier, int star, double quantity);
    Task<CardSoldiers> GetUserCardSoldierByIdAsync(string user_id, string Id);
    Task<List<CardSoldiers>> GetAllUserCardSoldiersInTeamAsync(string user_id);
}