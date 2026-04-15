using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardHeroesService
{
    Task<List<CardHeroes>> GetAllEquipmentPowerAsync(string user_id, List<CardHeroes> CardHeroesList);
    Task<List<CardHeroes>> GetAllRankPowerAsync(string user_id, List<CardHeroes> CardHeroesList);
    Task<List<CardHeroes>> GetAllMasterPowerAsync(string user_id, List<CardHeroes> CardHeroesList);
    Task<List<CardHeroes>> GetAllSpiritBeastPowerAsync(string user_id, List<CardHeroes> cardHeroes);
    Task<CardHeroes> GetNewLevelPowerAsync(CardHeroes c, double coefficient);
    Task<CardHeroes> GetNewBreakthroughPowerAsync(CardHeroes c, double coefficient);
    Task<List<CardHeroes>> GetSkillsAsync(string user_id, List<CardHeroes> CardHeroesList);
    Task<List<CardHeroes>> GetUserCardHeroesAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardHeroes>> GetUserCardHeroesTeamAsync(string user_id, string teamId, string position);
    Task<List<CardHeroes>> GetUserCardHeroesTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardHeroesTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardHeroAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardHeroesCountAsync(string user_id, string search, string type, string rare);
    Task<int> GetUserCardHeroesTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardHeroesTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardHeroAsync(CardHeroes CardHeroes);
    Task<bool> UpdateCardHeroLevelAsync(CardHeroes CardHeroes, int cardLevel);
    Task<bool> UpdateCardHeroBreakthroughAsync(CardHeroes CardHeroes, int star, double quantity);
    Task<CardHeroes> GetUserCardHeroByIdAsync(string user_id, string Id);
    Task<List<CardHeroes>> GetAllUserCardHeroesInTeamAsync(string user_id);
}