using System.Collections.Generic;
using System.Threading.Tasks;

public class OutfitsService : IOutfitsService
{
    private static OutfitsService _instance;
    private readonly IOutfitsRepository _weaponsRepository;

    public OutfitsService(IOutfitsRepository weaponsRepository)
    {
        _weaponsRepository = weaponsRepository;
    }

    public static OutfitsService Create()
    {
        if (_instance == null)
        {
            _instance = new OutfitsService(new OutfitsRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueOutfitsTypesAsync()
    {
        return await _weaponsRepository.GetUniqueOutfitsTypesAsync();
    }

    public async Task<List<Outfits>> GetOutfitsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Outfits> list = await _weaponsRepository.GetOutfitsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetOutfitsCountAsync(string search, string type, string rare)
    {
        return await _weaponsRepository.GetOutfitsCountAsync(search, type, rare);
    }

    public async Task<List<Outfits>> GetOutfitsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Outfits> list = await _weaponsRepository.GetOutfitsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetOutfitsWithPriceCountAsync(string type)
    {
        return await _weaponsRepository.GetOutfitsWithPriceCountAsync(type);
    }

    public async Task<Outfits> GetOutfitByIdAsync(string Id)
    {
        return await _weaponsRepository.GetOutfitByIdAsync(Id);
    }

    public async Task<Outfits> SumPowerOutfitsPercentAsync()
    {
        return await _weaponsRepository.SumPowerOutfitsPercentAsync();
    }

    public async Task<List<string>> GetUniqueOutfitsIdAsync()
    {
        return await _weaponsRepository.GetUniqueOutfitsIdAsync();
    }

    public async Task<List<Outfits>> GetOutfitsWithoutLimitAsync()
    {
        return await _weaponsRepository.GetOutfitsWithoutLimitAsync();
    }
}
