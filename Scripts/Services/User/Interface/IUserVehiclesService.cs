using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserVehiclesService
{
    Task<Vehicles> GetNewLevelPowerAsync(Vehicles c, double coefficient);
    Task<Vehicles> GetNewBreakthroughPowerAsync(Vehicles c, double coefficient);
    Task<List<Vehicles>> GetUserVehiclesAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserVehiclesCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserVehicleAsync(Vehicles Vehicle, string userId);
    Task<bool> UpdateVehicleLevelAsync(Vehicles Vehicle, int cardLevel);
    Task<bool> UpdateVehicleBreakthroughAsync(Vehicles Vehicle, int star, double quantity);
    Task<Vehicles> GetUserVehicleByIdAsync(string user_id, string Id);
    Task<Vehicles> SumPowerUserVehiclesAsync();
}