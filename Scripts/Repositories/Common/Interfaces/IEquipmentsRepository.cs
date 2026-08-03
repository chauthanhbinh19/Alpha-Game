using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEquipmentsRepository
{
    Task<List<string>> GetUniqueEquipmentsTypesAsync();
    Task<List<string>> GetUniqueEquipmentsIdAsync();
    Task<List<Equipments>> GetEquipmentsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<Equipments>> GetEquipmentsWithoutLimitAsync();
    Task<int> GetEquipmentsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Equipments>> InsertEquipmentAsync(Equipments entity);
    Task<InsertOrUpdateResult<Equipments>> UpdateEquipmentAsync(Equipments entity);
    Task<List<Equipments>> GetEquipmentsWithCurrencyAsync(string type, int pageSize, int offset);
    Task<List<string>> GetEquipmentsSetAsync(string type);
    Task<Equipments> GetEquipmentByIdAsync(string id);
}
