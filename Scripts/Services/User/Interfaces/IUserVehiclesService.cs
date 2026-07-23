using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserVehiclesService
{
    Task<List<Vehicles>> GetUserVehiclesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserVehiclesCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserVehicleAsync(Vehicles vehicle, string userId);
    Task<bool> InsertOrUpdateUserVehiclesBatchAsync(string userId, List<Vehicles> vehicles);
    Task<bool> UpdateUserVehicleLevelAsync(string userId, Vehicles vehicle);
    Task<bool> UpdateUserVehicleStarAsync(string userId, Vehicles vehicle);
    Task<bool> UpdateUserVehicleBreakthroughAsync(string userId, Vehicles vehicle, int star, double quantity);
    Task<Vehicles> GetUserVehicleByIdAsync(string userId, string Id);
    Task<Vehicles> SumPowerUserVehiclesAsync(string userId);
}