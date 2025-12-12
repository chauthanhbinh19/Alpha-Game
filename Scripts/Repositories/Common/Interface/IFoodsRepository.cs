using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFoodsRepository
{
    Task<List<string>> GetUniqueFoodsIdAsync();
    Task<List<Foods>> GetFoodsAsync(int pageSize, int offset, string rare);
    Task<int> GetFoodsCountAsync(string rare);
    Task<List<Foods>> GetFoodsWithPriceAsync(int pageSize, int offset);
    Task<int> GetFoodsWithPriceCountAsync();
    Task<Foods> GetFoodByIdAsync(string id);
    Task<Foods> SumPowerFoodsPercentAsync();
}
