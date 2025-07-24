using System.Collections.Generic;

public interface IEquipmentsGalleryRepository
{
    List<Equipments> GetEquipmentsCollection(string type, int pageSize, int offset, string rare);
    int GetEquipmentsCount(string type, string rare);
    void InsertEquipmentsGallery(string Id, Equipments EquipmentFromDB);
    void UpdateStatusEquipmentsGallery(string Id);
    Equipments SumPowerEquipmentsGallery();
}