using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardSpellsRepository
{
    Task<List<CardSpells>> GetUserCardSpellsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardSpells>> GetUserCardSpellsTeamAsync(string user_id, string teamId, string position);
    Task<List<CardSpells>> GetUserCardSpellsTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardSpellsTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardSpellAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardSpellsCountAsync(string user_id, string search, string type, string rare);
    Task<int> GetUserCardSpellsTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardSpellsTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardSpellAsync(CardSpells CardSpells);
    Task<bool> UpdateCardSpellLevelAsync(CardSpells CardSpells, int cardLevel);
    Task<bool> UpdateCardSpellBreakthroughAsync(CardSpells CardSpells, int star, double quantity);
    Task<CardSpells> GetUserCardSpellByIdAsync(string user_id, string Id);
    Task<List<CardSpells>> GetAllUserCardSpellsInTeamAsync(string user_id);
}