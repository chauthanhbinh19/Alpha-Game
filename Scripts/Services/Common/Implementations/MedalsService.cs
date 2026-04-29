using System.Collections.Generic;
using System.Threading.Tasks;

public class MedalsService : IMedalsService
{
    private static MedalsService _instance;
    private readonly IMedalsRepository _medalsRepository;

    public MedalsService(IMedalsRepository medalsRepository)
    {
        _medalsRepository = medalsRepository;
    }

    public static MedalsService Create()
    {
        if (_instance == null)
        {
            _instance = new MedalsService(new MedalsRepository());
        }
        return _instance;
    }

    public async Task<List<Medals>> GetMedalsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Medals> list = await _medalsRepository.GetMedalsAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMedalsCountAsync(string search, string rare)
    {
        return await _medalsRepository.GetMedalsCountAsync(search, rare);
    }

    public async Task<List<Medals>> GetMedalsWithPriceAsync(int pageSize, int offset)
    {
        List<Medals> list = await _medalsRepository.GetMedalsWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
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
