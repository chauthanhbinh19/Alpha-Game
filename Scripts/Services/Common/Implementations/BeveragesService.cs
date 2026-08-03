using System.Collections.Generic;
using System.Threading.Tasks;

public class BeveragesService : IBeveragesService
{
    private readonly IBeveragesRepository _beveragesRepository;

    public BeveragesService(IBeveragesRepository beveragesRepository)
    {
        _beveragesRepository = beveragesRepository;
    }

    public static IBeveragesService Create() => ServiceContainer.GetService<IBeveragesService>();

    public async Task<List<Beverages>> GetBeveragesAsync(string search, string rare, int pageSize, int offset)
    {
        List<Beverages> list = await _beveragesRepository.GetBeveragesAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBeveragesCountAsync(string search, string rare)
    {
        return await _beveragesRepository.GetBeveragesCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBeverageAsync(Beverages entity)
    {
        var result = await _beveragesRepository.InsertBeverageAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBeverageAsync(Beverages entity)
    {
        var result = await _beveragesRepository.UpdateBeverageAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<Beverages>> GetBeveragesWithPriceAsync(int pageSize, int offset)
    {
        List<Beverages> list = await _beveragesRepository.GetBeveragesWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
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

    public async Task<Beverages> SumPowerBeveragesPercentAsync(string userId)
    {
        return await _beveragesRepository.SumPowerBeveragesPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueBeveragesIdAsync()
    {
        return await _beveragesRepository.GetUniqueBeveragesIdAsync();
    }

    public async Task<List<Beverages>> GetBeveragesWithoutLimitAsync()
    {
        return await _beveragesRepository.GetBeveragesWithoutLimitAsync();
    }
}
