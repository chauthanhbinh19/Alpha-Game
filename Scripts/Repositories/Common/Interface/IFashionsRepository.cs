using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFashionsRepository
{
    Task<List<string>> GetUniqueFashionsTypesAsync();
    Task<List<string>> GetUniqueFashionsIdAsync();
    Task<List<Fashions>> GetFashionsAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetFashionsCountAsync(string type, string rare);
    Task<List<Fashions>> GetFashionsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetFashionsWithPriceCountAsync(string type);
    Task<Fashions> GetFashionByIdAsync(string Id);
    Task<Fashions> SumPowerFashionsPercentAsync();
}
