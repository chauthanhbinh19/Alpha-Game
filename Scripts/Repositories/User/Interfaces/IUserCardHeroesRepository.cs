using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardHeroesRepository
{
    Task<List<CardHeroes>> GetUserCardHeroesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardHeroes>> GetUserCardHeroesTeamAsync(string userId, string teamId, string position);
    Task<List<CardHeroes>> GetUserCardHeroesTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardHeroesTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardHeroAsync(string userId, string team_id, string position, string cardId);
    Task<int> GetUserCardHeroesCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardHeroesTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardHeroesTeamsCountAsync(string userId, string team_id);
    Task<InsertOrUpdateResult<CardHeroes>> InsertOrUpdateUserCardHeroAsync(string userId, CardHeroes cardHero);
    Task<bool> InsertOrUpdateUserCardHeroesBatchAsync(string userId, List<CardHeroes> cardHeroes);
    Task<bool> UpdateUserCardHeroLevelAsync(string userId, CardHeroes cardHero);
    Task<bool> UpdateUserCardHeroStarAsync(string userId, CardHeroes cardHero);
    Task<bool> UpdateUserCardHeroBreakthroughAsync(string userId, CardHeroes cardHero, int star, double quantity);
    Task<CardHeroes> GetUserCardHeroByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId);
    Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId);
}