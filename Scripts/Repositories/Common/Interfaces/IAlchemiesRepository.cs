using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAlchemiesRepository
{
    Task<List<string>> GetUniqueAlchemiesTypesAsync();
    Task<List<string>> GetUniqueAlchemiesIdAsync();
    Task<List<Alchemies>> GetAlchemiesAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<Alchemies>> GetAlchemiesWithoutLimitAsync();
    Task<int> GetAlchemiesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Alchemies>> InsertAlchemyAsync(Alchemies entity);
    Task<InsertOrUpdateResult<Alchemies>> UpdateAlchemyAsync(Alchemies entity);
    Task<List<Alchemies>> GetAlchemiesWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetAlchemiesWithPriceCountAsync(string type);
    Task<Alchemies> GetAlchemyByIdAsync(string id);
    Task<Alchemies> SumPowerAlchemiesPercentAsync(string userId);
}