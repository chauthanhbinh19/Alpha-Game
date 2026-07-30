using System.Collections.Generic;
using System.Threading.Tasks;

public class ArchitecturesService : IArchitecturesService
{
    private readonly IArchitecturesRepository _architecturesRepository;

    public ArchitecturesService(IArchitecturesRepository architecturesRepository)
    {
        _architecturesRepository = architecturesRepository;
    }

    public static IArchitecturesService Create() => ServiceContainer.GetService<IArchitecturesService>();

    public async Task<List<Architectures>> GetArchitecturesAsync(string search, string rare, int pageSize, int offset)
    {
        List<Architectures> list = await _architecturesRepository.GetArchitecturesAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArchitecturesCountAsync(string search, string rare)
    {
        return await _architecturesRepository.GetArchitecturesCountAsync(search, rare);
    }

    public async Task<List<Architectures>> GetArchitecturesWithPriceAsync(int pageSize, int offset)
    {
        List<Architectures> list = await _architecturesRepository.GetArchitecturesWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArchitecturesWithPriceCountAsync()
    {
        return await _architecturesRepository.GetArchitecturesWithPriceCountAsync();
    }

    public async Task<Architectures> GetArchitectureByIdAsync(string Id)
    {
        return await _architecturesRepository.GetArchitectureByIdAsync(Id);
    }

    public async Task<Architectures> SumPowerArchitecturesPercentAsync(string userId)
    {
        return await _architecturesRepository.SumPowerArchitecturesPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueArchitecturesIdAsync()
    {
        return await _architecturesRepository.GetUniqueArchitecturesIdAsync();
    }

    public async Task<List<Architectures>> GetArchitecturesWithoutLimitAsync()
    {
        return await _architecturesRepository.GetArchitecturesWithoutLimitAsync();
    }
}
