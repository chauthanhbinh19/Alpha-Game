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

    public async Task<List<MechaBeasts>> GetMechaBeastsAsync(string search, string rare, int pageSize, int offset)
    {
        List<MechaBeasts> list = await _MechaBeastsRepository.GetMechaBeastsAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsCountAsync(string search, string rare)
    {
        return await _MechaBeastsRepository.GetMechaBeastsCountAsync(search, rare);
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
