using System.Collections.Generic;
using System.Threading.Tasks;

public class PuppetsService : IPuppetsService
{
    private static PuppetsService _instance;
    private readonly IPuppetsRepository _puppetsRepository;

    public PuppetsService(IPuppetsRepository puppetsRepository)
    {
        _puppetsRepository = puppetsRepository;
    }

    public static PuppetsService Create()
    {
        if (_instance == null)
        {
            _instance = new PuppetsService(new PuppetsRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniquePuppetsTypesAsync()
    {
        return await _puppetsRepository.GetUniquePuppetsTypesAsync();
    }

    public async Task<List<Puppets>> GetPuppetsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Puppets> list = await _puppetsRepository.GetPuppetsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPuppetsCountAsync(string search, string type, string rare)
    {
        return await _puppetsRepository.GetPuppetsCountAsync(search, type, rare);
    }

    public async Task<List<Puppets>> GetPuppetsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Puppets> list = await _puppetsRepository.GetPuppetsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPuppetsWithPriceCountAsync(string type)
    {
        return await _puppetsRepository.GetPuppetsWithPriceCountAsync(type);
    }

    public async Task<Puppets> GetPuppetByIdAsync(string Id)
    {
        return await _puppetsRepository.GetPuppetByIdAsync(Id);
    }

    public async Task<Puppets> SumPowerPuppetsPercentAsync()
    {
        return await _puppetsRepository.SumPowerPuppetsPercentAsync();
    }

    public async Task<List<string>> GetUniquePuppetsIdAsync()
    {
        return await _puppetsRepository.GetUniquePuppetsIdAsync();
    }
}
