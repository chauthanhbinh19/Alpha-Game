using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICoresRepository
{
    Task<List<string>> GetUniqueCoresIdAsync();
    Task<List<Cores>> GetCoresAsync(string search, string rare, int pageSize, int offset);
    Task<int> GetCoresCountAsync(string search, string rare);
    Task<List<Cores>> GetCoresWithPriceAsync(int pageSize, int offset);
    Task<int> GetCoresWithPriceCountAsync();
    Task<Cores> GetCoreByIdAsync(string Id);
    Task<Cores> SumPowerCoresPercentAsync();
}
