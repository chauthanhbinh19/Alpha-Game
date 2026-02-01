using System.Collections.Generic;
using System.Threading.Tasks;

public class MechaBeastsService : IMechaBeastsService
{
    private static MechaBeastsService _instance;
    private readonly IMechaBeastsRepository _mechaBeastsRepository;

    public MechaBeastsService(IMechaBeastsRepository mechaBeastsRepository)
    {
        _mechaBeastsRepository = mechaBeastsRepository;
    }

    public static MechaBeastsService Create()
    {
        if (_instance == null)
        {
            _instance = new MechaBeastsService(new MechaBeastsRepository());
        }
        return _instance;
    }

    public async Task<List<MechaBeasts>> GetMechaBeastsAsync(string search, string rare, int pageSize, int offset)
    {
        List<MechaBeasts> list = await _mechaBeastsRepository.GetMechaBeastsAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsCountAsync(string search, string rare)
    {
        return await _mechaBeastsRepository.GetMechaBeastsCountAsync(search, rare);
    }

    public async Task<List<MechaBeasts>> GetMechaBeastsWithPriceAsync(int pageSize, int offset)
    {
        List<MechaBeasts> list = await _mechaBeastsRepository.GetMechaBeastsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsWithPriceCountAsync()
    {
        return await _mechaBeastsRepository.GetMechaBeastsWithPriceCountAsync();
    }

    public async Task<MechaBeasts> GetMechaBeastByIdAsync(string Id)
    {
        return await _mechaBeastsRepository.GetMechaBeastByIdAsync(Id);
    }

    public async Task<MechaBeasts> SumPowerMechaBeastsPercentAsync()
    {
        return await _mechaBeastsRepository.SumPowerMechaBeastsPercentAsync();
    }

    public async Task<List<string>> GetUniqueMechaBeastsIdAsync()
    {
        return await _mechaBeastsRepository.GetUniqueMechaBeastsIdAsync();
    }
}
