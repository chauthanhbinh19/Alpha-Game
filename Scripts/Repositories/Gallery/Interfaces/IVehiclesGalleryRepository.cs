using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVehiclesGalleryRepository
{
    Task<List<Vehicles>> GetVehiclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetVehiclesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Vehicles>> InsertVehicleGalleryAsync(string userId, string Id, Vehicles VehicleFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusVehicleGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusVehiclesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarVehicleGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarVehicleGalleryAsync(string userId, string vehicleId);
    Task<InsertOrUpdateResult<List<(string VehicleId, double CurrentStar)>>> UpdateBatchCurrentStarVehiclesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Vehicles>>> InsertBatchVehiclesGalleryAsync(string userId, List<Vehicles> vehicles);
    Task<Vehicles> GetVehicleCollectionByIdAsync(string userId, string objectId);
    Task UpdateVehicleGalleryPowerAsync(string userId, string Id, Vehicles VehicleFromDB);
    Task<Vehicles> SumPowerVehiclesGalleryAsync(string userId);
}