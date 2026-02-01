using System.Collections.Generic;
using System.Threading.Tasks;

public class BasesService : IBasesService
{
    private static BasesService _instance;
    private readonly IBasesRepository _basesRepository;

    public BasesService(IBasesRepository basesRepository)
    {
        _basesRepository = basesRepository;
    }

    public static BasesService Create()
    {
        if (_instance == null)
        {
            _instance = new BasesService(new BasesRepository());
        }
        return _instance;
    }

    public async Task<List<Bases>> GetBasesAsync(string userId, int pageSize, int offset)
    {
        List<Bases> list = await _basesRepository.GetBasesAsync(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBasesCountAsync(string rare)
    {
        return await _basesRepository.GetBasesCountAsync(rare);
    }

    public async Task<List<Bases>> GetBasesWithPriceAsync(int pageSize, int offset)
    {
        List<Bases> list = await _basesRepository.GetBasesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBasesWithPriceCountAsync()
    {
        return await _basesRepository.GetBasesWithPriceCountAsync();
    }

    public async Task<Bases> GetBaseByIdAsync(string Id)
    {
        return await _basesRepository.GetBaseByIdAsync(Id);
    }

    public async Task<Bases> SumPowerBasesPercentAsync()
    {
        return await _basesRepository.SumPowerBasesPercentAsync();
    }

    public async Task<List<string>> GetUniqueBasesIdAsync()
    {
        return await _basesRepository.GetUniqueBasesIdAsync();
    }
}
