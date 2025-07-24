using System.Collections.Generic;

public interface IEquipmentsRepository
{
    List<string> GetUniqueEquipmentsTypes();
    List<string> GetUniqueEquipmentsId();
    List<Equipments> GetEquipments(string type, int pageSize, int offset, string rare);
    int GetEquipmentsCount(string type, string rare);
    List<Equipments> GetEquipmentsWithCurrency(string type, int pageSize, int offset);
    List<string> GetEquipmentsSet(string type);
    Equipments GetEquipmentById(string Id);
}
