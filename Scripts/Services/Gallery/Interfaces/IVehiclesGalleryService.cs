using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVehiclesGalleryService
{
    Task<List<Vehicles>> GetVehiclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetVehiclesCountAsync(string search, string type, string rare);
    Task<bool> InsertVehicleGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusVehicleGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusVehiclesGalleryAsync(string userId);
    Task<bool> UpdateTempStarVehicleGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarVehicleGalleryAsync(string userId, string vehicleId);
    Task<bool> UpdateBatchCurrentStarVehiclesGalleryAsync(string userId);
    Task<bool> InsertBatchVehiclesGalleryAsync(string userId, List<Vehicles> vehicles);
    Task<Vehicles> GetVehicleCollectionByIdAsync(string userId, string vehicleId);
    Task UpdateVehicleGalleryPowerAsync(string userId, string Id);
    Task<Vehicles> SumPowerVehiclesGalleryAsync(string userId);
}