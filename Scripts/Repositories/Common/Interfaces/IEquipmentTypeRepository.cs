using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEquipmentTypeRepository
{
    Task<EquipmentType> GetEquipmentTypeByNameAsync(string type);
}