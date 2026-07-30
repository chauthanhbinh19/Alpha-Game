using System.Collections.Generic;
using System.Threading.Tasks;

public class ForgesService : IForgesService
{
    private readonly IForgesRepository _forgesRepository;

    public ForgesService(IForgesRepository forgesRepository)
    {
        _forgesRepository = forgesRepository;
    }

    public static IForgesService Create() => ServiceContainer.GetService<IForgesService>();

    public async Task<List<string>> GetUniqueForgesTypesAsync()
    {
        return await _forgesRepository.GetUniqueForgesTypesAsync();
    }

    public async Task<List<Forges>> GetForgesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Forges> list = await _forgesRepository.GetForgesAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetForgesCountAsync(string search, string type, string rare)
    {
        return await _forgesRepository.GetForgesCountAsync(search, type, rare);
    }

    public async Task<List<Forges>> GetForgesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Forges> list = await _forgesRepository.GetForgesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
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

    public async Task<Forges> SumPowerForgesPercentAsync(string userId)
    {
        return await _forgesRepository.SumPowerForgesPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueForgesIdAsync()
    {
        return await _forgesRepository.GetUniqueForgesIdAsync();
    }

    public async Task<List<Forges>> GetForgesWithoutLimitAsync()
    {
        return await _forgesRepository.GetForgesWithoutLimitAsync();
    }
}
