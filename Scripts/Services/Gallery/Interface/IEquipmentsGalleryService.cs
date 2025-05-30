using System.Collections.Generic;

public interface IEquipmentsGalleryService
{
    List<Equipments> GetEquipmentsCollection(string type, int pageSize, int offset);
    int GetEquipmentsCount(string type);
    void InsertEquipmentsGallery(string Id);
    void UpdateStatusEquipmentsGallery(string Id);
    Equipments SumPowerEquipmentsGallery();
}