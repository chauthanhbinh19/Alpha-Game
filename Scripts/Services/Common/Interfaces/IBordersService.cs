using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBordersService
{
    Task<List<string>> GetUniqueBordersIdAsync();
    Task<List<Borders>> GetBordersAsync(string search, string rare, int pageSize, int offset);
    Task<List<Borders>> GetBordersWithoutLimitAsync();
    Task<int> GetBordersCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertBorderAsync(Borders entity);
    Task<InsertOrUpdateResult<bool>> UpdateBorderAsync(Borders entity);
    Task<List<Borders>> GetBordersWithPriceAsync(int pageSize, int offset);
    Task<int> GetBordersWithPriceCountAsync();
    Task<Borders> GetBorderByIdAsync(string id);
    Task<Borders> SumPowerBordersPercentAsync(string userId);
}