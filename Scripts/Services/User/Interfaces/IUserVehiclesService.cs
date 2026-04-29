using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserVehiclesService
{
    Task<List<Vehicles>> GetUserVehiclesAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserVehiclesCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserVehicleAsync(Vehicles vehicle, string userId);
    Task<bool> InsertOrUpdateUserVehiclesBatchAsync(List<Vehicles> vehicles);
    Task<bool> UpdateVehicleLevelAsync(Vehicles vehicle, int level);
    Task<bool> UpdateVehicleBreakthroughAsync(Vehicles vehicle, int star, double quantity);
    Task<Vehicles> GetUserVehicleByIdAsync(string user_id, string Id);
    Task<Vehicles> SumPowerUserVehiclesAsync();
}