using System.Collections.Generic;
using System.Threading.Tasks;

public class TalismansService : ITalismansService
{
    private static TalismansService _instance;
    private readonly ITalismansRepository _talismansRepository;

    public TalismansService(ITalismansRepository talismansRepository)
    {
        _talismansRepository = talismansRepository;
    }

    public static TalismansService Create()
    {
        if (_instance == null)
        {
            _instance = new TalismansService(new TalismansRepository());
        }
        return _instance;
    }

    public async Task<List<Talismans>> GetTalismansAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Talismans> list = await _talismansRepository.GetTalismansAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTalismansCountAsync(string search, string type, string rare)
    {
        return await _talismansRepository.GetTalismansCountAsync(search, type, rare);
    }

    public async Task<List<Talismans>> GetTalismansWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Talismans> list = await _talismansRepository.GetTalismansWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTalismansWithPriceCountAsync(string type)
    {
        return await _talismansRepository.GetTalismansWithPriceCountAsync(type);
    }

    public async Task<Talismans> GetTalismanByIdAsync(string Id)
    {
        return await _talismansRepository.GetTalismanByIdAsync(Id);
    }

    public async Task<Talismans> SumPowerTalismansPercentAsync()
    {
        return await _talismansRepository.SumPowerTalismansPercentAsync();
    }

    public async Task<List<string>> GetUniqueTalismansTypesAsync()
    {
        return await _talismansRepository.GetUniqueTalismansTypesAsync();
    }

    public async Task<List<string>> GetUniqueTalismansIdAsync()
    {
        return await _talismansRepository.GetUniqueTalismansIdAsync();
    }
}
