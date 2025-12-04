using System.Collections.Generic;
using System.Threading.Tasks;

public class MechaBeastsService : IMechaBeastsService
{
    private readonly IMechaBeastsRepository _MechaBeastsRepository;

    public MechaBeastsService(IMechaBeastsRepository titleRepository)
    {
        _MechaBeastsRepository = titleRepository;
    }

    public static MechaBeastsService Create()
    {
        return new MechaBeastsService(new MechaBeastsRepository());
    }

    public async Task<List<MechaBeasts>> GetMechaBeastsAsync(int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = await _MechaBeastsRepository.GetMechaBeastsAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsCountAsync(string rare)
    {
        return await _MechaBeastsRepository.GetMechaBeastsCountAsync(rare);
    }

    public async Task<List<MechaBeasts>> GetMechaBeastsWithPriceAsync(int pageSize, int offset)
    {
        List<MechaBeasts> list = await _MechaBeastsRepository.GetMechaBeastsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsWithPriceCountAsync()
    {
        return await _MechaBeastsRepository.GetMechaBeastsWithPriceCountAsync();
    }

    public async Task<MechaBeasts> GetMechaBeastByIdAsync(string Id)
    {
        return await _MechaBeastsRepository.GetMechaBeastByIdAsync(Id);
    }

    public async Task<MechaBeasts> SumPowerMechaBeastsPercentAsync()
    {
        return await _MechaBeastsRepository.SumPowerMechaBeastsPercentAsync();
    }

    public async Task<List<string>> GetUniqueMechaBeastsIdAsync()
    {
        return await _MechaBeastsRepository.GetUniqueMechaBeastsIdAsync();
    }
}
