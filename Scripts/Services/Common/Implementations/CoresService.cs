using System.Collections.Generic;
using System.Threading.Tasks;

public class CoresService : ICoresService
{
    private static CoresService _instance;
    private readonly ICoresRepository _coresRepository;

    public CoresService(ICoresRepository coresRepository)
    {
        _coresRepository = coresRepository;
    }

    public static CoresService Create()
    {
        if (_instance == null)
        {
            _instance = new CoresService(new CoresRepository());
        }
        return _instance;
    }

    public async Task<List<Cores>> GetCoresAsync(string search, string rare,int pageSize, int offset)
    {
        List<Cores> list = await _coresRepository.GetCoresAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCoresCountAsync(string search, string rare)
    {
        return await _coresRepository.GetCoresCountAsync(search, rare);
    }

    public async Task<List<Cores>> GetCoresWithPriceAsync(int pageSize, int offset)
    {
        List<Cores> list = await _coresRepository.GetCoresWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCoresWithPriceCountAsync()
    {
        return await _coresRepository.GetCoresWithPriceCountAsync();
    }

    public async Task<Cores> GetCoreByIdAsync(string Id)
    {
        return await _coresRepository.GetCoreByIdAsync(Id);
    }

    public async Task<Cores> SumPowerCoresPercentAsync()
    {
        return await _coresRepository.SumPowerCoresPercentAsync();
    }

    public async Task<List<string>> GetUniqueCoresIdAsync()
    {
        return await _coresRepository.GetUniqueCoresIdAsync();
    }
}
