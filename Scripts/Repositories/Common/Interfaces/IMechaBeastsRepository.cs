using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMechaBeastsRepository
{
    Task<List<string>> GetUniqueMechaBeastsIdAsync();
    Task<List<MechaBeasts>> GetMechaBeastsAsync(string search, string rare, int pageSize, int offset);
    Task<List<MechaBeasts>> GetMechaBeastsWithoutLimitAsync();
    Task<int> GetMechaBeastsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<MechaBeasts>> InsertMechaBeastAsync(MechaBeasts entity);
    Task<InsertOrUpdateResult<MechaBeasts>> UpdateMechaBeastAsync(MechaBeasts entity);
    Task<List<MechaBeasts>> GetMechaBeastsWithPriceAsync(int pageSize, int offset);
    Task<int> GetMechaBeastsWithPriceCountAsync();
    Task<MechaBeasts> GetMechaBeastByIdAsync(string Id);
    Task<MechaBeasts> SumPowerMechaBeastsPercentAsync(string userId);
}
