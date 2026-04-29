using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardSpellsService
{
    Task<List<CardSpells>> GetAllEquipmentPowerAsync(string user_id, List<CardSpells> CardSpellList);
    Task<List<CardSpells>> GetAllRankPowerAsync(string user_id, List<CardSpells> CardSpellList);
    Task<List<CardSpells>> GetAllMasterPowerAsync(string user_id, List<CardSpells> CardSpellList);
    Task<List<CardSpells>> GetAllSpiritBeastPowerAsync(string user_id, List<CardSpells> cardSpells);
    Task<List<CardSpells>> GetSkillsAsync(string user_id, List<CardSpells> CardSpellList);
    Task<List<CardSpells>> GetUserCardSpellsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardSpells>> GetUserCardSpellsTeamAsync(string user_id, string teamId, string position);
    Task<List<CardSpells>> GetUserCardSpellsTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardSpellsTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardSpellAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardSpellsCountAsync(string user_id, string search, string type, string rare);
    Task<int> GetUserCardSpellsTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardSpellsTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardSpellAsync(CardSpells cardSpell);
    Task<bool> InsertOrUpdateUserCardSpellsBatchAsync(List<CardSpells> cardSpells);
    Task<bool> UpdateCardSpellLevelAsync(CardSpells cardSpell, int level);
    Task<bool> UpdateCardSpellBreakthroughAsync(CardSpells cardSpell, int star, double quantity);
    Task<CardSpells> GetUserCardSpellByIdAsync(string user_id, string Id);
    Task<List<CardSpells>> GetAllUserCardSpellsInTeamAsync(string user_id);
}