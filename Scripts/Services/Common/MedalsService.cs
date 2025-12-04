using System.Collections.Generic;
using System.Threading.Tasks;

public class MedalsService : IMedalsService
{
    private readonly IMedalsRepository _medalsRepository;

    public MedalsService(IMedalsRepository medalsRepository)
    {
        _medalsRepository = medalsRepository;
    }

    public static MedalsService Create()
    {
        return new MedalsService(new MedalsRepository());
    }

    public async Task<List<Medals>> GetMedalsAsync(int pageSize, int offset, string rare)
    {
        List<Medals> list = await _medalsRepository.GetMedalsAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMedalsCountAsync(string rare)
    {
        return await _medalsRepository.GetMedalsCountAsync(rare);
    }

    public async Task<List<Medals>> GetMedalsWithPriceAsync(int pageSize, int offset)
    {
        List<Medals> list = await _medalsRepository.GetMedalsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMedalsWithPriceCountAsync()
    {
        return await _medalsRepository.GetMedalsWithPriceCountAsync();
    }

    public async Task<Medals> GetMedalByIdAsync(string Id)
    {
        return await _medalsRepository.GetMedalByIdAsync(Id);
    }

    public async Task<Medals> SumPowerMedalsPercentAsync()
    {
        return await _medalsRepository.SumPowerMedalsPercentAsync();
    }

    public async Task<List<string>> GetUniqueMedalsIdAsync()
    {
        return await _medalsRepository.GetUniqueMedalsIdAsync();
    }
}
