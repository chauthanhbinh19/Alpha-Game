using System.Collections.Generic;

public interface IVehicleGalleryService
{
    List<Vehicles> GetVehicleCollection(string type, int pageSize, int offset, string rare);
    int GetVehicleCount(string type, string rare);
    void InsertVehicleGallery(string Id);
    void UpdateStatusVehicleGallery(string Id);
    void UpdateStarVehicleGallery(string Id, double star);
    void UpdateVehicleGalleryPower(string Id);
    Vehicles SumPowerVehicleGallery();
}