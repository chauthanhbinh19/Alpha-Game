using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMechaBeastsRepository
{
    Task<List<string>> GetUniqueMechaBeastsIdAsync();
    Task<List<MechaBeasts>> GetMechaBeastsAsync(int pageSize, int offset, string rare);
    Task<int> GetMechaBeastsCountAsync(string rare);
    Task<List<MechaBeasts>> GetMechaBeastsWithPriceAsync(int pageSize, int offset);
    Task<int> GetMechaBeastsWithPriceCountAsync();
    Task<MechaBeasts> GetMechaBeastByIdAsync(string Id);
    Task<MechaBeasts> SumPowerMechaBeastsPercentAsync();
}
