using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVehiclesGalleryRepository
{
    Task<List<Vehicles>> GetVehiclesCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetVehiclesCountAsync(string search, string type, string rare);
    Task InsertVehicleGalleryAsync(string Id, Vehicles VehicleFromDB);
    Task UpdateStatusVehicleGalleryAsync(string Id);
    Task UpdateStarVehicleGalleryAsync(string Id, double star);
    Task UpdateVehicleGalleryPowerAsync(string Id, Vehicles VehicleFromDB);
    Task<Vehicles> SumPowerVehiclesGalleryAsync();
}