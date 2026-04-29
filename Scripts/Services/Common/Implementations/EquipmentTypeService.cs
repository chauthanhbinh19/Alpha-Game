using System.Collections.Generic;
using System.Threading.Tasks;

public class EquipmentTypeService : IEquipmentTypeService
{
    private static EquipmentTypeService _instance;
    private readonly IEquipmentTypeRepository _equipmentTypeRepository;

    public EquipmentTypeService(IEquipmentTypeRepository equipmentTypeRepository)
    {
        _equipmentTypeRepository = equipmentTypeRepository;
    }

    public static EquipmentTypeService Create()
    {
        if (_instance == null)
        {
            _instance = new EquipmentTypeService(new EquipmentTypeRepository());
        }
        return _instance;
    }

    public async Task<EquipmentType> GetEquipmentTypeByNameAsync(string type)
    {
        EquipmentType equipmentType = await _equipmentTypeRepository.GetEquipmentTypeByNameAsync(type);
        return equipmentType;
    }
}