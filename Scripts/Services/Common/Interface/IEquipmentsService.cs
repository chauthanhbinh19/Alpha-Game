using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEquipmentsService
{
    Task<List<string>> GetUniqueEquipmentsTypesAsync();
    Task<List<string>> GetUniqueEquipmentsIdAsync();
    Task<List<Equipments>> GetEquipmentsAsync(string search, string rare, string type, int pageSize, int offset);
    Task<int> GetEquipmentsCountAsync(string search, string type, string rare);
    Task<List<Equipments>> GetEquipmentsWithCurrencyAsync(string type, int pageSize, int offset);
    Task<List<string>> GetEquipmentsSetAsync(string type);
    Task<Equipments> GetEquipmentByIdAsync(string id);
}
