using System.Collections.Generic;

public interface IVehicleGalleryRepository
{
    List<Vehicles> GetVehiclesCollection(string type, int pageSize, int offset, string rare);
    int GetVehiclesCount(string type, string rare);
    void InsertVehiclesGallery(string Id, Vehicles TitleFromDB);
    void UpdateStatusVehiclesGallery(string Id);
    void UpdateStarVehiclesGallery(string Id, double star);
    void UpdateVehiclesGalleryPower(string Id, Vehicles VehicleFromDB);
    Vehicles SumPowerVehiclesGallery();
}