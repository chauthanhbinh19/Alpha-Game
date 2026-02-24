using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEquipmentTypeService
{
    Task<EquipmentType> GetEquipmentTypeByNameAsync(string type);
}