using System.Collections.Generic;
using System.Threading.Tasks;

public class OutfitsService : IOutfitsService
{
    private readonly IOutfitsRepository _outfitsRepository;

    public OutfitsService(IOutfitsRepository outfitsRepository)
    {
        _outfitsRepository = outfitsRepository;
    }

    public static IOutfitsService Create() => ServiceContainer.GetService<IOutfitsService>();

    public async Task<List<string>> GetUniqueOutfitsTypesAsync()
    {
        return await _outfitsRepository.GetUniqueOutfitsTypesAsync();
    }

    public async Task<List<Outfits>> GetOutfitsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Outfits> list = await _outfitsRepository.GetOutfitsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetOutfitsCountAsync(string search, string type, string rare)
    {
        return await _outfitsRepository.GetOutfitsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOutfitAsync(Outfits entity)
    {
        var result = await _outfitsRepository.InsertOutfitAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateOutfitAsync(Outfits entity)
    {
        var result = await _outfitsRepository.UpdateOutfitAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<Outfits>> GetOutfitsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Outfits> list = await _outfitsRepository.GetOutfitsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetOutfitsWithPriceCountAsync(string type)
    {
        return await _outfitsRepository.GetOutfitsWithPriceCountAsync(type);
    }

    public async Task<Outfits> GetOutfitByIdAsync(string Id)
    {
        return await _outfitsRepository.GetOutfitByIdAsync(Id);
    }

    public async Task<Outfits> SumPowerOutfitsPercentAsync(string userId)
    {
        return await _outfitsRepository.SumPowerOutfitsPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueOutfitsIdAsync()
    {
        return await _outfitsRepository.GetUniqueOutfitsIdAsync();
    }

    public async Task<List<Outfits>> GetOutfitsWithoutLimitAsync()
    {
        return await _outfitsRepository.GetOutfitsWithoutLimitAsync();
    }
}
