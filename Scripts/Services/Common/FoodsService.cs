using System.Collections.Generic;
using System.Threading.Tasks;

public class FoodsService : IFoodsService
{
    private readonly IFoodsRepository _FoodsRepository;

    public FoodsService(IFoodsRepository FoodRepository)
    {
        _FoodsRepository = FoodRepository;
    }

    public static FoodsService Create()
    {
        return new FoodsService(new FoodsRepository());
    }

    public async Task<List<Foods>> GetFoodsAsync(int pageSize, int offset, string rare)
    {
        List<Foods> list = await _FoodsRepository.GetFoodsAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFoodsCountAsync(string rare)
    {
        return await _FoodsRepository.GetFoodsCountAsync(rare);
    }

    public async Task<List<Foods>> GetFoodsWithPriceAsync(int pageSize, int offset)
    {
        List<Foods> list = await _FoodsRepository.GetFoodsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFoodsWithPriceCountAsync()
    {
        return await _FoodsRepository.GetFoodsWithPriceCountAsync();
    }

    public async Task<Foods> GetFoodByIdAsync(string Id)
    {
        return await _FoodsRepository.GetFoodByIdAsync(Id);
    }

    public async Task<Foods> SumPowerFoodsPercentAsync()
    {
        return await _FoodsRepository.SumPowerFoodsPercentAsync();
    }

    public async Task<List<string>> GetUniqueFoodsIdAsync()
    {
        return await _FoodsRepository.GetUniqueFoodsIdAsync();
    }
}
