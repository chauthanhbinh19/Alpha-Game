using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserVehiclesRepository
{
    Task<List<Vehicles>> GetUserVehiclesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserVehiclesCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Vehicles>> InsertOrUpdateUserVehicleAsync(string userId, Vehicles vehicle);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Vehicles>>> InsertOrUpdateUserVehiclesBatchAsync(string userId, List<Vehicles> vehicles);
    Task<InsertOrUpdateResult<bool>> UpdateUserVehicleLevelAsync(string userId, Vehicles vehicle);
    Task<InsertOrUpdateResult<bool>> UpdateUserVehicleStarAsync(string userId, Vehicles vehicle);
    Task<Vehicles> GetUserVehicleByIdAsync(string userId, string Id);
    Task<Vehicles> SumPowerUserVehiclesAsync(string userId);
}