using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserVehicleService
{
    Task<Vehicles> GetNewLevelPowerAsync(Vehicles c, double coefficient);
    Task<Vehicles> GetNewBreakthroughPowerAsync(Vehicles c, double coefficient);
    Task<List<Vehicles>> GetUserVehiclesAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<int> GetUserVehiclesCountAsync(string user_id, string type, string rare);
    Task<bool> InsertUserVehicleAsync(Vehicles Vehicle, string userId);
    Task<bool> UpdateVehicleLevelAsync(Vehicles Vehicle, int cardLevel);
    Task<bool> UpdateVehicleBreakthroughAsync(Vehicles Vehicle, int star, double quantity);
    Task<Vehicles> GetUserVehicleByIdAsync(string user_id, string Id);
    Task<Vehicles> SumPowerUserVehiclesAsync();
}