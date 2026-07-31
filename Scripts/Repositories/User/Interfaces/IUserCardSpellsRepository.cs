using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardSpellsRepository
{
    Task<List<CardSpells>> GetUserCardSpellsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardSpells>> GetUserCardSpellsTeamAsync(string userId, string teamId, string position);
    Task<List<CardSpells>> GetUserCardSpellsTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardSpellsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardSpellAsync(string userId, string team_id, string position, string cardId);
    Task<bool> IsCardInTeamAsync(string userId, string cardId);
    Task<int> GetUserCardSpellsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardSpellsTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardSpellsTeamsCountAsync(string userId, string team_id);
    Task<InsertOrUpdateResult<CardSpells>> InsertOrUpdateUserCardSpellAsync(string userId, CardSpells cardSpell);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<CardSpells>>> InsertOrUpdateUserCardSpellsBatchAsync(string userId, List<CardSpells> cardSpells);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardSpellLevelAsync(string userId, CardSpells cardSpell);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardSpellStarAsync(string userId, CardSpells cardSpell);
    Task<CardSpells> GetUserCardSpellByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId);
    Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId);
}