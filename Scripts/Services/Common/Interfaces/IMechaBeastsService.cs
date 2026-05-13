using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMechaBeastsService
{
    Task<List<string>> GetUniqueMechaBeastsIdAsync();
    Task<List<MechaBeasts>> GetMechaBeastsAsync(string search, string rare, int pageSize, int offset);
    Task<List<MechaBeasts>> GetMechaBeastsWithoutLimitAsync();
    Task<int> GetMechaBeastsCountAsync(string search, string rare);
    Task<List<MechaBeasts>> GetMechaBeastsWithPriceAsync(int pageSize, int offset);
    Task<int> GetMechaBeastsWithPriceCountAsync();
    Task<MechaBeasts> GetMechaBeastByIdAsync(string Id);
    Task<MechaBeasts> SumPowerMechaBeastsPercentAsync();
}
