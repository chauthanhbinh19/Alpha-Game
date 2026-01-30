using System.Collections.Generic;
using System.Threading.Tasks;

public class CoresService : ICoresService
{
    private readonly ICoresRepository _CoresRepository;

    public CoresService(ICoresRepository titleRepository)
    {
        _CoresRepository = titleRepository;
    }

    public static CoresService Create()
    {
        return new CoresService(new CoresRepository());
    }

    public async Task<List<Cores>> GetCoresAsync(string search, string rare,int pageSize, int offset)
    {
        List<Cores> list = await _CoresRepository.GetCoresAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCoresCountAsync(string search, string rare)
    {
        return await _CoresRepository.GetCoresCountAsync(search, rare);
    }

    public async Task<List<Cores>> GetCoresWithPriceAsync(int pageSize, int offset)
    {
        List<Cores> list = await _CoresRepository.GetCoresWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCoresWithPriceCountAsync()
    {
        return await _CoresRepository.GetCoresWithPriceCountAsync();
    }

    public async Task<Cores> GetCoreByIdAsync(string Id)
    {
        return await _CoresRepository.GetCoreByIdAsync(Id);
    }

    public async Task<Cores> SumPowerCoresPercentAsync()
    {
        return await _CoresRepository.SumPowerCoresPercentAsync();
    }

    public async Task<List<string>> GetUniqueCoresIdAsync()
    {
        return await _CoresRepository.GetUniqueCoresIdAsync();
    }
}
