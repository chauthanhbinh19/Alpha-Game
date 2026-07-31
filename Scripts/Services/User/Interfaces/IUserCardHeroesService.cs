using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardHeroesService
{
    Task<List<CardHeroes>> GetAllEquipmentPowerAsync(string userId, List<CardHeroes> cardHeroList);
    Task<List<CardHeroes>> GetAllRankPowerAsync(string userId, List<CardHeroes> cardHeroList);
    Task<List<CardHeroes>> GetAllMasterPowerAsync(string userId, List<CardHeroes> cardHeroList);
    Task<List<CardHeroes>> GetUserCardHeroesAsync(string userId, string search, string type, int pageSize, int offset, string rare, UserStatsContextDTO sharedContext = null);
    Task<List<CardHeroes>> GetUserCardHeroesTeamAsync(string userId, string teamId, string position, UserStatsContextDTO sharedContext = null);
    Task<List<CardHeroes>> GetUserCardHeroesTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null);
    Task<Dictionary<string, int>> GetUniqueUserCardHeroesTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardHeroAsync(string userId, string teamId, string position, string cardId);
    Task<int> GetUserCardHeroesCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardHeroesTeamsPositionCountAsync(string userId, string teamId, string position);
    Task<int> GetUserCardHeroesTeamsCountAsync(string userId, string teamId);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardHeroAsync(string userId, CardHeroes cardHero);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardHeroesBatchAsync(string userId, List<CardHeroes> cardHeroes);
    Task<bool> UpdateUserCardHeroLevelAsync(string userId, CardHeroes cardHero);
    Task<bool> UpdateUserCardHeroStarAsync(string userId, CardHeroes cardHero);
    Task<CardHeroes> GetUserCardHeroByIdAsync(string userId, string Id, UserStatsContextDTO sharedContext = null);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId, UserStatsContextDTO sharedContext = null);
}