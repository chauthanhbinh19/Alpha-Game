using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVehiclesRepository
{
    Task<List<string>> GetUniqueVehiclesTypesAsync();
    Task<List<string>> GetUniqueVehiclesIdAsync();
    Task<List<Vehicles>> GetVehiclesAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetVehiclesCountAsync(string type, string rare);
    Task<List<Vehicles>> GetVehiclesWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetVehiclesWithPriceCountAsync(string type);
    Task<Vehicles> GetVehicleByIdAsync(string Id);
    Task<Vehicles> SumPowerVehiclesPercentAsync();
}
