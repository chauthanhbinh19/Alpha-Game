using System.Collections.Generic;
using System.Threading.Tasks;

public class BeveragesService : IBeveragesService
{
    private static BeveragesService _instance;
    private readonly IBeveragesRepository _beveragesRepository;

    public BeveragesService(IBeveragesRepository beveragesRepository)
    {
        _beveragesRepository = beveragesRepository;
    }

    public static BeveragesService Create()
    {
        if (_instance == null)
        {
            _instance = new BeveragesService(new BeveragesRepository());
        }
        return _instance;
    }

    public async Task<List<Beverages>> GetBeveragesAsync(string search, string rare, int pageSize, int offset)
    {
        List<Beverages> list = await _beveragesRepository.GetBeveragesAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBeveragesCountAsync(string search, string rare)
    {
        return await _beveragesRepository.GetBeveragesCountAsync(search, rare);
    }

    public async Task<List<Beverages>> GetBeveragesWithPriceAsync(int pageSize, int offset)
    {
        List<Beverages> list = await _beveragesRepository.GetBeveragesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBeveragesWithPriceCountAsync()
    {
        return await _beveragesRepository.GetBeveragesWithPriceCountAsync();
    }

    public async Task<Beverages> GetBeverageByIdAsync(string Id)
    {
        return await _beveragesRepository.GetBeverageByIdAsync(Id);
    }

    public async Task<Beverages> SumPowerBeveragesPercentAsync()
    {
        return await _beveragesRepository.SumPowerBeveragesPercentAsync();
    }

    public async Task<List<string>> GetUniqueBeveragesIdAsync()
    {
        return await _beveragesRepository.GetUniqueBeveragesIdAsync();
    }
}
