using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBordersService
{
    Task<List<string>> GetUniqueBordersIdAsync();
    Task<List<Borders>> GetBordersAsync(int pageSize, int offset, string rare);
    Task<int> GetBordersCountAsync(string rare);
    Task<List<Borders>> GetBordersWithPriceAsync(int pageSize, int offset);
    Task<int> GetBordersWithPriceCountAsync();
    Task<Borders> GetBorderByIdAsync(string id);
    Task<Borders> SumPowerBordersPercentAsync();
}