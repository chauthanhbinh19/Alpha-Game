using System.Collections.Generic;

public interface IVehicleGalleryRepository
{
    List<Vehicles> GetVehicleCollection(string type, int pageSize, int offset, string rare);
    int GetVehicleCount(string type, string rare);
    void InsertVehicleGallery(string Id, Vehicles TitleFromDB);
    void UpdateStatusVehicleGallery(string Id);
    void UpdateStarVehicleGallery(string Id, double star);
    void UpdateVehicleGalleryPower(string Id, Vehicles VehicleFromDB);
    Vehicles SumPowerVehicleGallery();
}