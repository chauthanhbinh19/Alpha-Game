using System.Collections.Generic;
using System.Threading.Tasks;

public class FurnituresService : IFurnituresService
{
    private static FurnituresService _instance;
    private readonly IFurnituresRepository _furnituresRepository;

    public FurnituresService(IFurnituresRepository furnituresRepository)
    {
        _furnituresRepository = furnituresRepository;
    }

    public static FurnituresService Create()
    {
        if (_instance == null)
        {
            _instance = new FurnituresService(new FurnituresRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueFurnituresTypesAsync()
    {
        return await _furnituresRepository.GetUniqueFurnituresTypesAsync();
    }

    public async Task<List<Furnitures>> GetFurnituresAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Furnitures> list = await _furnituresRepository.GetFurnituresAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFurnituresCountAsync(string search, string type, string rare)
    {
        return await _furnituresRepository.GetFurnituresCountAsync(search, type, rare);
    }

    public async Task<List<Furnitures>> GetFurnituresWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Furnitures> list = await _furnituresRepository.GetFurnituresWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFurnituresWithPriceCountAsync(string type)
    {
        return await _furnituresRepository.GetFurnituresWithPriceCountAsync(type);
    }

    public async Task<Furnitures> GetFurnitureByIdAsync(string Id)
    {
        return await _furnituresRepository.GetFurnitureByIdAsync(Id);
    }

    public async Task<Furnitures> SumPowerFurnituresPercentAsync()
    {
        return await _furnituresRepository.SumPowerFurnituresPercentAsync();
    }

    public async Task<List<string>> GetUniqueFurnituresIdAsync()
    {
        return await _furnituresRepository.GetUniqueFurnituresIdAsync();
    }

    public async Task<List<Furnitures>> GetFurnituresWithoutLimitAsync()
    {
        return await _furnituresRepository.GetFurnituresWithoutLimitAsync();
    }
}
