using System.Collections.Generic;
using System.Threading.Tasks;

public class BeveragesService : IBeveragesService
{
    private readonly IBeveragesRepository _BeveragesRepository;

    public BeveragesService(IBeveragesRepository BeverageRepository)
    {
        _BeveragesRepository = BeverageRepository;
    }

    public static BeveragesService Create()
    {
        return new BeveragesService(new BeveragesRepository());
    }

    public async Task<List<Beverages>> GetBeveragesAsync(int pageSize, int offset, string rare)
    {
        List<Beverages> list = await _BeveragesRepository.GetBeveragesAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBeveragesCountAsync(string rare)
    {
        return await _BeveragesRepository.GetBeveragesCountAsync(rare);
    }

    public async Task<List<Beverages>> GetBeveragesWithPriceAsync(int pageSize, int offset)
    {
        List<Beverages> list = await _BeveragesRepository.GetBeveragesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBeveragesWithPriceCountAsync()
    {
        return await _BeveragesRepository.GetBeveragesWithPriceCountAsync();
    }

    public async Task<Beverages> GetBeverageByIdAsync(string Id)
    {
        return await _BeveragesRepository.GetBeverageByIdAsync(Id);
    }

    public async Task<Beverages> SumPowerBeveragesPercentAsync()
    {
        return await _BeveragesRepository.SumPowerBeveragesPercentAsync();
    }

    public async Task<List<string>> GetUniqueBeveragesIdAsync()
    {
        return await _BeveragesRepository.GetUniqueBeveragesIdAsync();
    }
}
