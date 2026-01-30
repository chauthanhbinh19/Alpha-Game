using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFoodsService
{
    Task<List<string>> GetUniqueFoodsIdAsync();
    Task<List<Foods>> GetFoodsAsync(string search, string rare, int pageSize, int offset);
    Task<int> GetFoodsCountAsync(string search, string rare);
    Task<List<Foods>> GetFoodsWithPriceAsync(int pageSize, int offset);
    Task<int> GetFoodsWithPriceCountAsync();
    Task<Foods> GetFoodByIdAsync(string id);
    Task<Foods> SumPowerFoodsPercentAsync();
}
