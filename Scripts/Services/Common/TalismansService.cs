using System.Collections.Generic;
using System.Threading.Tasks;

public class TalismansService : ITalismansService
{
    private readonly ITalismansRepository _talismanRepository;

    public TalismansService(ITalismansRepository talismanRepository)
    {
        _talismanRepository = talismanRepository;
    }

    public static TalismansService Create()
    {
        return new TalismansService(new TalismansRepository());
    }

    public async Task<List<Talismans>> GetTalismansAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Talismans> list = await _talismanRepository.GetTalismansAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTalismansCountAsync(string search, string type, string rare)
    {
        return await _talismanRepository.GetTalismansCountAsync(search, type, rare);
    }

    public async Task<List<Talismans>> GetTalismansWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Talismans> list = await _talismanRepository.GetTalismansWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTalismansWithPriceCountAsync(string type)
    {
        return await _talismanRepository.GetTalismansWithPriceCountAsync(type);
    }

    public async Task<Talismans> GetTalismanByIdAsync(string Id)
    {
        return await _talismanRepository.GetTalismanByIdAsync(Id);
    }

    public async Task<Talismans> SumPowerTalismansPercentAsync()
    {
        return await _talismanRepository.SumPowerTalismansPercentAsync();
    }

    public async Task<List<string>> GetUniqueTalismansTypesAsync()
    {
        return await _talismanRepository.GetUniqueTalismansTypesAsync();
    }

    public async Task<List<string>> GetUniqueTalismansIdAsync()
    {
        return await _talismanRepository.GetUniqueTalismansIdAsync();
    }
}
