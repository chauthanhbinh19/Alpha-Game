using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardsService
{
    Task<List<string>> GetUniqueCardsIdAsync();
    Task<List<Cards>> GetCardsAsync(string search, string rare, int pageSize, int offset);
    Task<int> GetCardsCountAsync(string search, string rare);
    Task<List<Cards>> GetCardsWithPriceAsync(int pageSize, int offset);
    Task<int> GetCardsWithPriceCountAsync();
    Task<Cards> GetCardByIdAsync(string Id);
    Task<Cards> SumPowerCardsPercentAsync();
}
