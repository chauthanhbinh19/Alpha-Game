using System.Collections.Generic;
using System.Threading.Tasks;

public class TitlesService : ITitlesService
{
    private readonly ITitlesRepository _titlesRepository;

    public TitlesService(ITitlesRepository titleRepository)
    {
        _titlesRepository = titleRepository;
    }

    public static TitlesService Create()
    {
        return new TitlesService(new TitlesRepository());
    }

    public async Task<List<Titles>> GetTitlesAsync(string search, string rare, int pageSize, int offset)
    {
        List<Titles> list = await _titlesRepository.GetTitlesAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTitlesCountAsync(string search, string rare)
    {
        return await _titlesRepository.GetTitlesCountAsync(search, rare);
    }

    public async Task<List<Titles>> GetTitlesWithPriceAsync(int pageSize, int offset)
    {
        List<Titles> list = await _titlesRepository.GetTitlesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTitlesWithPriceCountAsync()
    {
        return await _titlesRepository.GetTitlesWithPriceCountAsync();
    }

    public async Task<Titles> GetTitleByIdAsync(string Id)
    {
        return await _titlesRepository.GetTitleByIdAsync(Id);
    }

    public async Task<Titles> SumPowerTitlesPercentAsync()
    {
        return await _titlesRepository.SumPowerTitlesPercentAsync();
    }

    public async Task<List<string>> GetUniqueTitlesIdAsync()
    {
        return await _titlesRepository.GetUniqueTitlesIdAsync();
    }
}
