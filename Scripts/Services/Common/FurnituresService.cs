using System.Collections.Generic;
using System.Threading.Tasks;

public class FurnituresService : IFurnituresService
{
    private readonly IFurnituresRepository _FurnituresRepository;

    public FurnituresService(IFurnituresRepository FurnituresRepository)
    {
        _FurnituresRepository = FurnituresRepository;
    }

    public static FurnituresService Create()
    {
        return new FurnituresService(new FurnituresRepository());
    }

    public async Task<List<string>> GetUniqueFurnituresTypesAsync()
    {
        return await _FurnituresRepository.GetUniqueFurnituresTypesAsync();
    }

    public async Task<List<Furnitures>> GetFurnituresAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Furnitures> list = await _FurnituresRepository.GetFurnituresAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFurnituresCountAsync(string search, string type, string rare)
    {
        return await _FurnituresRepository.GetFurnituresCountAsync(search, type, rare);
    }

    public async Task<List<Furnitures>> GetFurnituresWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Furnitures> list = await _FurnituresRepository.GetFurnituresWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFurnituresWithPriceCountAsync(string type)
    {
        return await _FurnituresRepository.GetFurnituresWithPriceCountAsync(type);
    }

    public async Task<Furnitures> GetFurnitureByIdAsync(string Id)
    {
        return await _FurnituresRepository.GetFurnitureByIdAsync(Id);
    }

    public async Task<Furnitures> SumPowerFurnituresPercentAsync()
    {
        return await _FurnituresRepository.SumPowerFurnituresPercentAsync();
    }

    public async Task<List<string>> GetUniqueFurnituresIdAsync()
    {
        return await _FurnituresRepository.GetUniqueFurnituresIdAsync();
    }
}
