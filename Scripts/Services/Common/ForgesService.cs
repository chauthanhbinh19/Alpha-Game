using System.Collections.Generic;
using System.Threading.Tasks;

public class ForgesService : IForgesService
{
    private static ForgesService _instance;
    private readonly IForgesRepository _forgesRepository;

    public ForgesService(IForgesRepository forgesRepository)
    {
        _forgesRepository = forgesRepository;
    }

    public static ForgesService Create()
    {
        if (_instance == null)
        {
            _instance = new ForgesService(new ForgesRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueForgesTypesAsync()
    {
        return await _forgesRepository.GetUniqueForgesTypesAsync();
    }

    public async Task<List<Forges>> GetForgesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Forges> list = await _forgesRepository.GetForgesAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetForgesCountAsync(string search, string type, string rare)
    {
        return await _forgesRepository.GetForgesCountAsync(search, type, rare);
    }

    public async Task<List<Forges>> GetForgesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Forges> list = await _forgesRepository.GetForgesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetForgesWithPriceCountAsync(string type)
    {
        return await _forgesRepository.GetForgesWithPriceCountAsync(type);
    }

    public async Task<Forges> GetForgeByIdAsync(string Id)
    {
        return await _forgesRepository.GetForgeByIdAsync(Id);
    }

    public async Task<Forges> SumPowerForgesPercentAsync()
    {
        return await _forgesRepository.SumPowerForgesPercentAsync();
    }

    public async Task<List<string>> GetUniqueForgesIdAsync()
    {
        return await _forgesRepository.GetUniqueForgesIdAsync();
    }
}
