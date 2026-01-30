using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFashionsService
{
    Task<List<string>> GetUniqueFashionsTypesAsync();
    Task<List<string>> GetUniqueFashionsIdAsync();
    Task<List<Fashions>> GetFashionsAsync(string search, string rare, string type, int pageSize, int offset);
    Task<int> GetFashionsCountAsync(string search, string type, string rare);
    Task<List<Fashions>> GetFashionsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetFashionsWithPriceCountAsync(string type);
    Task<Fashions> GetFashionByIdAsync(string Id);
    Task<Fashions> SumPowerFashionsPercentAsync();
}
