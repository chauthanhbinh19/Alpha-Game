using System.Collections.Generic;
using System.Threading.Tasks;

public class ForgesService : IForgesService
{
    private readonly IForgesRepository _forgeRepository;

    public ForgesService(IForgesRepository forgeRepository)
    {
        _forgeRepository = forgeRepository;
    }

    public static ForgesService Create()
    {
        return new ForgesService(new ForgesRepository());
    }

    public async Task<List<string>> GetUniqueForgesTypesAsync()
    {
        return await _forgeRepository.GetUniqueForgesTypesAsync();
    }

    public async Task<List<Forges>> GetForgesAsync(string type, int pageSize, int offset, string rare)
    {
        List<Forges> list = await _forgeRepository.GetForgesAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetForgesCountAsync(string type, string rare)
    {
        return await _forgeRepository.GetForgesCountAsync(type, rare);
    }

    public async Task<List<Forges>> GetForgesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Forges> list = await _forgeRepository.GetForgesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetForgesWithPriceCountAsync(string type)
    {
        return await _forgeRepository.GetForgesWithPriceCountAsync(type);
    }

    public async Task<Forges> GetForgeByIdAsync(string Id)
    {
        return await _forgeRepository.GetForgeByIdAsync(Id);
    }

    public async Task<Forges> SumPowerForgesPercentAsync()
    {
        return await _forgeRepository.SumPowerForgesPercentAsync();
    }

    public async Task<List<string>> GetUniqueForgesIdAsync()
    {
        return await _forgeRepository.GetUniqueForgesIdAsync();
    }
}
