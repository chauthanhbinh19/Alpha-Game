using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVehicleGalleryRepository
{
    Task<List<Vehicles>> GetVehiclesCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetVehiclesCountAsync(string type, string rare);
    Task InsertVehicleGalleryAsync(string Id, Vehicles VehicleFromDB);
    Task UpdateStatusVehicleGalleryAsync(string Id);
    Task UpdateStarVehicleGalleryAsync(string Id, double star);
    Task UpdateVehicleGalleryPowerAsync(string Id, Vehicles VehicleFromDB);
    Task<Vehicles> SumPowerVehiclesGalleryAsync();
}