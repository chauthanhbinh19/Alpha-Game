using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardSpellsService
{
    Task<List<CardSpells>> GetAllEquipmentPowerAsync(string userId, List<CardSpells> cardSpellList);
    Task<List<CardSpells>> GetAllRankPowerAsync(string userId, List<CardSpells> cardSpellList);
    Task<List<CardSpells>> GetAllMasterPowerAsync(string userId, List<CardSpells> cardSpellList);
    Task<List<CardSpells>> GetAllSpiritBeastPowerAsync(string userId, List<CardSpells> cardSpellList);
    Task<List<CardSpells>> GetUserCardSpellsAsync(string userId, string search, string type, int pageSize, int offset, string rare, UserStatsContextDTO sharedContext = null);
    Task<List<CardSpells>> GetUserCardSpellsTeamAsync(string userId, string teamId, string position, UserStatsContextDTO sharedContext = null);
    Task<List<CardSpells>> GetUserCardSpellsTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null);
    Task<Dictionary<string, int>> GetUniqueUserCardSpellsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardSpellAsync(string userId, string teamId, string position, string cardId);
    Task<int> GetUserCardSpellsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardSpellsTeamsPositionCountAsync(string userId, string teamId, string position);
    Task<int> GetUserCardSpellsTeamsCountAsync(string userId, string teamId);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardSpellAsync(string userId, CardSpells cardSpell);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardSpellsBatchAsync(string userId, List<CardSpells> cardSpells);
    Task<bool> UpdateUserCardSpellLevelAsync(string userId, CardSpells cardSpell);
    Task<bool> UpdateUserCardSpellStarAsync(string userId, CardSpells cardSpell);
    Task<CardSpells> GetUserCardSpellByIdAsync(string userId, string Id, UserStatsContextDTO sharedContext = null);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId, UserStatsContextDTO sharedContext = null);
}