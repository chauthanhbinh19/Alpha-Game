using System.Collections.Generic;
using System.Threading.Tasks;

public class FashionsService : IFashionsService
{
    private readonly IFashionsRepository _FashionsRepository;

    public FashionsService(IFashionsRepository FashionsRepository)
    {
        _FashionsRepository = FashionsRepository;
    }

    public static FashionsService Create()
    {
        return new FashionsService(new FashionsRepository());
    }

    public async Task<List<string>> GetUniqueFashionsTypesAsync()
    {
        return await _FashionsRepository.GetUniqueFashionsTypesAsync();
    }

    public async Task<List<Fashions>> GetFashionsAsync(string type, int pageSize, int offset, string rare)
    {
        List<Fashions> list = await _FashionsRepository.GetFashionsAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFashionsCountAsync(string type, string rare)
    {
        return await _FashionsRepository.GetFashionsCountAsync(type, rare);
    }

    public async Task<List<Fashions>> GetFashionsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Fashions> list = await _FashionsRepository.GetFashionsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFashionsWithPriceCountAsync(string type)
    {
        return await _FashionsRepository.GetFashionsWithPriceCountAsync(type);
    }

    public async Task<Fashions> GetFashionByIdAsync(string Id)
    {
        return await _FashionsRepository.GetFashionByIdAsync(Id);
    }

    public async Task<Fashions> SumPowerFashionsPercentAsync()
    {
        return await _FashionsRepository.SumPowerFashionsPercentAsync();
    }

    public async Task<List<string>> GetUniqueFashionsIdAsync()
    {
        return await _FashionsRepository.GetUniqueFashionsIdAsync();
    }
}
