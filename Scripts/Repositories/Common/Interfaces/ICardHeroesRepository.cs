using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardHeroesRepository
{
    Task<List<string>> GetUniqueCardHeroesTypesAsync();
    Task<List<string>> GetUniqueCardHeroesIdAsync();
    Task<List<CardHeroes>> GetCardHeroesAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<CardHeroes>> GetCardHeroesWithoutLimitAsync();
    Task<int> GetCardHeroesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CardHeroes>> InsertCardHeroAsync(CardHeroes entity);
    Task<InsertOrUpdateResult<CardHeroes>> UpdateCardHeroAsync(CardHeroes entity);
    Task<List<CardHeroes>> GetCardHeroesRandomAsync(string type, int pageSize);
    Task<List<CardHeroes>> GetAllCardHeroesAsync(string type);
    Task<int> GetMaxQuantityAsync(string Id);
    Task<CardHeroes> GetCardHeroByIdAsync(string Id);
    Task<List<CardHeroes>> GetCardHeroesWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardHeroesWithPriceCountAsync(string type);
    
}