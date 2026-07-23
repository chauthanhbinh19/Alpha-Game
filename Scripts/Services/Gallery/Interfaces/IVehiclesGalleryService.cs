using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVehiclesGalleryService
{
    Task<List<Vehicles>> GetVehiclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetVehiclesCountAsync(string search, string type, string rare);
    Task InsertVehicleGalleryAsync(string userId, string Id);
    Task UpdateStatusVehicleGalleryAsync(string userId, string Id);
    Task UpdateStarVehicleGalleryAsync(string userId, string Id, double star);
    Task UpdateVehicleGalleryPowerAsync(string userId, string Id);
    Task<Vehicles> SumPowerVehiclesGalleryAsync(string userId);
}