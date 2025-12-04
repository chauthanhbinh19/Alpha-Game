using System.Collections.Generic;
using System.Threading.Tasks;

public class ArchitecturesService : IArchitecturesService
{
    private readonly IArchitecturesRepository _ArchitecturesRepository;

    public ArchitecturesService(IArchitecturesRepository titleRepository)
    {
        _ArchitecturesRepository = titleRepository;
    }

    public static ArchitecturesService Create()
    {
        return new ArchitecturesService(new ArchitecturesRepository());
    }

    public async Task<List<Architectures>> GetArchitecturesAsync(int pageSize, int offset, string rare)
    {
        List<Architectures> list = await _ArchitecturesRepository.GetArchitecturesAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArchitecturesCountAsync(string rare)
    {
        return await _ArchitecturesRepository.GetArchitecturesCountAsync(rare);
    }

    public async Task<List<Architectures>> GetArchitecturesWithPriceAsync(int pageSize, int offset)
    {
        List<Architectures> list = await _ArchitecturesRepository.GetArchitecturesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArchitecturesWithPriceCountAsync()
    {
        return await _ArchitecturesRepository.GetArchitecturesWithPriceCountAsync();
    }

    public async Task<Architectures> GetArchitectureByIdAsync(string Id)
    {
        return await _ArchitecturesRepository.GetArchitectureByIdAsync(Id);
    }

    public async Task<Architectures> SumPowerArchitecturesPercentAsync()
    {
        return await _ArchitecturesRepository.SumPowerArchitecturesPercentAsync();
    }

    public async Task<List<string>> GetUniqueArchitecturesIdAsync()
    {
        return await _ArchitecturesRepository.GetUniqueArchitecturesIdAsync();
    }
}
