using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardSpellsRepository
{
    Task<List<CardSpells>> GetUserCardSpellsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardSpells>> GetUserCardSpellsTeamAsync(string userId, string teamId, string position);
    Task<List<CardSpells>> GetUserCardSpellsTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardSpellsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardSpellAsync(string userId, string team_id, string position, string cardId);
    Task<int> GetUserCardSpellsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardSpellsTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardSpellsTeamsCountAsync(string userId, string team_id);
    Task<bool> InsertUserCardSpellAsync(string userId, CardSpells cardSpell);
    Task<bool> InsertOrUpdateUserCardSpellsBatchAsync(string userId, List<CardSpells> cardSpells);
    Task<bool> UpdateUserCardSpellLevelAsync(string userId, CardSpells cardSpell);
    Task<bool> UpdateUserCardSpellStarAsync(string userId, CardSpells cardSpell);
    Task<bool> UpdateUserCardSpellBreakthroughAsync(string userId, CardSpells cardSpell, int star, double quantity);
    Task<CardSpells> GetUserCardSpellByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId);
    Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId);
}