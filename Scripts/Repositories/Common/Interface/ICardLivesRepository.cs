using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardLivesRepository
{
    Task<List<string>> GetUniqueCardLivesTypesAsync();
    Task<List<string>> GetUniqueCardLivesIdAsync();
    Task<List<CardLives>> GetCardLivesAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetCardLivesCountAsync(string type, string rare);
    Task<List<CardLives>> GetCardLivesWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardLivesWithPriceCountAsync(string type);
    Task<CardLives> GetCardLifeByIdAsync(string Id);
    Task<CardLives> SumPowerCardLivesPercentAsync();
}