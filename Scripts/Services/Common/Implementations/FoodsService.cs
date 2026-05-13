using System.Collections.Generic;
using System.Threading.Tasks;

public class FoodsService : IFoodsService
{
    private static FoodsService _instance;
    private readonly IFoodsRepository _foodsRepository;

    public FoodsService(IFoodsRepository foodRepository)
    {
        _foodsRepository = foodRepository;
    }

    public static FoodsService Create()
    {
        if (_instance == null)
        {
            _instance = new FoodsService(new FoodsRepository());
        }
        return _instance;
    }

    public async Task<List<Foods>> GetFoodsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Foods> list = await _foodsRepository.GetFoodsAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFoodsCountAsync(string search, string rare)
    {
        return await _foodsRepository.GetFoodsCountAsync(search, rare);
    }

    public async Task<List<Foods>> GetFoodsWithPriceAsync(int pageSize, int offset)
    {
        List<Foods> list = await _foodsRepository.GetFoodsWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFoodsWithPriceCountAsync()
    {
        return await _foodsRepository.GetFoodsWithPriceCountAsync();
    }

    public async Task<Foods> GetFoodByIdAsync(string Id)
    {
        return await _foodsRepository.GetFoodByIdAsync(Id);
    }

    public async Task<Foods> SumPowerFoodsPercentAsync()
    {
        return await _foodsRepository.SumPowerFoodsPercentAsync();
    }

    public async Task<List<string>> GetUniqueFoodsIdAsync()
    {
        return await _foodsRepository.GetUniqueFoodsIdAsync();
    }

    public async Task<List<Foods>> GetFoodsWithoutLimitAsync()
    {
        return await _foodsRepository.GetFoodsWithoutLimitAsync();
    }
}
