using System.Collections.Generic;
using System.Threading.Tasks;

public class SpiritBeastsService : ISpiritBeastsService
{
    private readonly ISpiritBeastsRepository _spiritBeastsRepository;

    public SpiritBeastsService(ISpiritBeastsRepository spiritBeastsRepository)
    {
        _spiritBeastsRepository = spiritBeastsRepository;
    }

    public static ISpiritBeastsService Create() => ServiceContainer.GetService<ISpiritBeastsService>();

    public async Task<List<SpiritBeasts>> GetSpiritBeastsAsync(string search, string rare, int pageSize, int offset)
    {
        List<SpiritBeasts> list = await _spiritBeastsRepository.GetSpiritBeastsAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritBeastsCountAsync(string search, string rare)
    {
        return await _spiritBeastsRepository.GetSpiritBeastCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertSpiritBeastAsync(SpiritBeasts entity)
    {
        var result = await _spiritBeastsRepository.InsertSpiritBeastAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateSpiritBeastAsync(SpiritBeasts entity)
    {
        var result = await _spiritBeastsRepository.UpdateSpiritBeastAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsWithPriceAsync(int pageSize, int offset)
    {
        List<SpiritBeasts> list = await _spiritBeastsRepository.GetSpiritBeastsWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritBeastsWithPriceCountAsync()
    {
        return await _spiritBeastsRepository.GetSpiritBeastsWithPriceCountAsync();
    }

    public async Task<SpiritBeasts> GetSpiritBeastByIdAsync(string Id)
    {
        return await _spiritBeastsRepository.GetSpiritBeastByIdAsync(Id);
    }

    public async Task<SpiritBeasts> SumPowerSpiritBeastsPercentAsync(string userId)
    {
        return await _spiritBeastsRepository.SumPowerSpiritBeastsPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueSpiritBeastsIdAsync()
    {
        return await _spiritBeastsRepository.GetUniqueSpiritBeastsIdAsync();
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsWithoutLimitAsync()
    {
        return await _spiritBeastsRepository.GetSpiritBeastsWithoutLimitAsync();
    }
}
