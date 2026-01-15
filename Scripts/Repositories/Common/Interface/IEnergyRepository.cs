using System.Collections.Generic;
using System.Threading.Tasks;
public interface IEnergyRepository
{
    Task<Energy> GetEnergyAsync(string type);
    Task InsertOrUpdateEnergyAsync(string user_id, Energy energy, string id);
    Task<Energy> GetSumEnergyAsync(string user_id);
}