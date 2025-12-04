using System.Collections.Generic;
using System.Threading.Tasks;

public class BasesService : IBasesService
{
    private readonly IBasesRepository _BasesRepository;

    public BasesService(IBasesRepository titleRepository)
    {
        _BasesRepository = titleRepository;
    }

    public static BasesService Create()
    {
        return new BasesService(new BasesRepository());
    }

    public async Task<List<Bases>> GetBasesAsync(string userId, int pageSize, int offset)
    {
        List<Bases> list = await _BasesRepository.GetBasesAsync(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBasesCountAsync(string rare)
    {
        return await _BasesRepository.GetBasesCountAsync(rare);
    }

    public async Task<List<Bases>> GetBasesWithPriceAsync(int pageSize, int offset)
    {
        List<Bases> list = await _BasesRepository.GetBasesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBasesWithPriceCountAsync()
    {
        return await _BasesRepository.GetBasesWithPriceCountAsync();
    }

    public async Task<Bases> GetBaseByIdAsync(string Id)
    {
        return await _BasesRepository.GetBaseByIdAsync(Id);
    }

    public async Task<Bases> SumPowerBasesPercentAsync()
    {
        return await _BasesRepository.SumPowerBasesPercentAsync();
    }

    public async Task<List<string>> GetUniqueBasesIdAsync()
    {
        return await _BasesRepository.GetUniqueBasesIdAsync();
    }
}
