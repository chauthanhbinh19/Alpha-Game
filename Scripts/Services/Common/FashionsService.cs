using System.Collections.Generic;
using System.Threading.Tasks;

public class FashionsService : IFashionsService
{
    private static FashionsService _instance;
    private readonly IFashionsRepository _fashionsRepository;

    public FashionsService(IFashionsRepository fashionsRepository)
    {
        _fashionsRepository = fashionsRepository;
    }

    public static FashionsService Create()
    {
        if (_instance == null)
        {
            _instance = new FashionsService(new FashionsRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueFashionsTypesAsync()
    {
        return await _fashionsRepository.GetUniqueFashionsTypesAsync();
    }

    public async Task<List<Fashions>> GetFashionsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Fashions> list = await _fashionsRepository.GetFashionsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFashionsCountAsync(string search, string type, string rare)
    {
        return await _fashionsRepository.GetFashionsCountAsync(search, type, rare);
    }

    public async Task<List<Fashions>> GetFashionsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Fashions> list = await _fashionsRepository.GetFashionsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFashionsWithPriceCountAsync(string type)
    {
        return await _fashionsRepository.GetFashionsWithPriceCountAsync(type);
    }

    public async Task<Fashions> GetFashionByIdAsync(string Id)
    {
        return await _fashionsRepository.GetFashionByIdAsync(Id);
    }

    public async Task<Fashions> SumPowerFashionsPercentAsync()
    {
        return await _fashionsRepository.SumPowerFashionsPercentAsync();
    }

    public async Task<List<string>> GetUniqueFashionsIdAsync()
    {
        return await _fashionsRepository.GetUniqueFashionsIdAsync();
    }
}
