using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardHeroesService
{
    Task<List<string>> GetUniqueCardHeroesTypesAsync();
    Task<List<string>> GetUniqueCardHeroesIdAsync();
    Task<List<CardHeroes>> GetCardHeroesAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetCardHeroesCountAsync(string type, string rare);
    Task<List<CardHeroes>> GetCardHeroesRandomAsync(string type, int pageSize);
    Task<List<CardHeroes>> GetAllCardHeroesAsync(string type);
    Task<int> GetMaxQuantityAsync(string Id);
    Task<CardHeroes> GetCardHeroByIdAsync(string Id);
    Task<List<CardHeroes>> GetCardHeroesWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardHeroesWithPriceCountAsync(string type);
    
}