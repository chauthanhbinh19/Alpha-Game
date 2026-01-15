using System.Collections.Generic;
using System.Threading.Tasks;
public interface IEnergyService
{ 
    Task<Energy> GetEnergyAsync(string id);
    Task InsertOrUpdateEnergyAsync(string user_id, Energy energy, string id);
    Task<Energy> GetSumEnergyAsync(string user_id);
}