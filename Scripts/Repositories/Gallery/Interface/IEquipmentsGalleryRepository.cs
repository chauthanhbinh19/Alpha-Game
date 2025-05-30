using System.Collections.Generic;

public interface IEquipmentsGalleryRepository
{
    List<Equipments> GetEquipmentsCollection(string type, int pageSize, int offset);
    int GetEquipmentsCount(string type);
    void InsertEquipmentsGallery(string Id, Equipments EquipmentFromDB);
    void UpdateStatusEquipmentsGallery(string Id);
    Equipments SumPowerEquipmentsGallery();
}