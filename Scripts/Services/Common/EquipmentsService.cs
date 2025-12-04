using System.Collections.Generic;
using System.Threading.Tasks;

public class EquipmentsService : IEquipmentsService
{
    private readonly IEquipmentsRepository _equipmentsRepository;

    public EquipmentsService(IEquipmentsRepository equipmentsRepository)
    {
        _equipmentsRepository = equipmentsRepository;
    }

    public static EquipmentsService Create()
    {
        return new EquipmentsService(new EquipmentsRepository());
    }

    public async Task<List<string>> GetUniqueEquipmentsTypesAsync()
    {
        return await _equipmentsRepository.GetUniqueEquipmentsTypesAsync();
    }

    public async Task<List<Equipments>> GetEquipmentsAsync(string type, int pageSize, int offset, string rare)
    {
        List<Equipments> list = await _equipmentsRepository.GetEquipmentsAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEquipmentsCountAsync(string type, string rare)
    {
        return await _equipmentsRepository.GetEquipmentsCountAsync(type, rare);
    }

    public async Task<List<Equipments>> GetEquipmentsWithCurrencyAsync(string type, int pageSize, int offset)
    {
        return await _equipmentsRepository.GetEquipmentsWithCurrencyAsync(type, pageSize, offset);
    }

    public async Task<List<string>> GetEquipmentsSetAsync(string type)
    {
        return await _equipmentsRepository.GetEquipmentsSetAsync(type);
    }

    public async Task<Equipments> GetEquipmentByIdAsync(string Id)
    {
        return await _equipmentsRepository.GetEquipmentByIdAsync(Id);
    }

    public async Task<List<string>> GetUniqueEquipmentsIdAsync()
    {
        return await _equipmentsRepository.GetUniqueEquipmentsIdAsync();
    }
}
