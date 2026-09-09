using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVehiclesGalleryService
{
    Task<List<Vehicles>> GetVehiclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetVehiclesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertVehicleGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusVehicleGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusVehiclesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarVehicleGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarVehicleGalleryAsync(string userId, string vehicleId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarVehiclesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchVehiclesGalleryAsync(string userId, List<Vehicles> vehicles);
    Task<Vehicles> GetVehicleCollectionByIdAsync(string userId, string vehicleId);
    Task<InsertOrUpdateResult<bool>> UpdateVehicleGalleryPowerAsync(string userId, string Id);
    Task<Vehicles> SumPowerVehiclesGalleryAsync(string userId);
}