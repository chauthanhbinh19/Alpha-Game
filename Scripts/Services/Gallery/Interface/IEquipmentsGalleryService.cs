using System.Collections.Generic;

public interface IEquipmentsGalleryService
{
    List<Equipments> GetEquipmentsCollection(string type, int pageSize, int offset, string rare);
    int GetEquipmentsCount(string type, string rare);
    void InsertEquipmentsGallery(string Id);
    void UpdateStatusEquipmentsGallery(string Id);
    Equipments SumPowerEquipmentsGallery();
}