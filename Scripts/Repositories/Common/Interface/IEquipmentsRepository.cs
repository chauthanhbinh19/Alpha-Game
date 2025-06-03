using System.Collections.Generic;

public interface IEquipmentsRepository
{
    List<string> GetUniqueEquipmentsTypes();
    List<Equipments> GetEquipments(string type, int pageSize, int offset);
    int GetEquipmentsCount(string type);
    List<Equipments> GetEquipmentsWithCurrency(string type, int pageSize, int offset);
    List<string> GetEquipmentsSet(string type);
    Equipments GetEquipmentById(string Id);
}
