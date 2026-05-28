using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardLivesService
{
    Task<List<string>> GetUniqueCardLivesTypesAsync();
    Task<List<string>> GetUniqueCardLivesIdAsync();
    Task<List<CardLives>> GetCardLivesAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<CardLives>> GetCardLivesWithoutLimitAsync();
    Task<int> GetCardLivesCountAsync(string search, string type, string rare);
    Task<List<CardLives>> GetCardLivesWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardLivesWithPriceCountAsync(string type);
    Task<CardLives> GetCardLifeByIdAsync(string Id);
    Task<CardLives> SumPowerCardLivesPercentAsync();
}