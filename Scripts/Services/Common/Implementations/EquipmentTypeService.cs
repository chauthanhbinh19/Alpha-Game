using System.Collections.Generic;
using System.Threading.Tasks;

public class EquipmentTypeService : IEquipmentTypeService
{
    private readonly IEquipmentTypeRepository _equipmentTypeRepository;

    public EquipmentTypeService(IEquipmentTypeRepository equipmentTypeRepository)
    {
        _equipmentTypeRepository = equipmentTypeRepository;
    }

    public static IEquipmentTypeService Create() => ServiceContainer.GetService<IEquipmentTypeService>();

    public async Task<EquipmentType> GetEquipmentTypeByNameAsync(string type)
    {
        EquipmentType equipmentType = await _equipmentTypeRepository.GetEquipmentTypeByNameAsync(type);
        return equipmentType;
    }
}