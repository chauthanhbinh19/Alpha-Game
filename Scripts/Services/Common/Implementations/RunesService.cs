using System.Collections.Generic;
using System.Threading.Tasks;

public class RunesService : IRunesService
{
    private readonly IRunesRepository _runesRepository;

    public RunesService(IRunesRepository runesRepository)
    {
        _runesRepository = runesRepository;
    }

    public static IRunesService Create() => ServiceContainer.GetService<IRunesService>();

    public async Task<List<Runes>> GetRunesAsync(string search, string rare, int pageSize, int offset)
    {
        List<Runes> list = await _runesRepository.GetRunesAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRunesCountAsync(string search, string rare)
    {
        return await _runesRepository.GetRunesCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertRuneAsync(Runes entity)
    {
        var result = await _runesRepository.InsertRuneAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateRuneAsync(Runes entity)
    {
        var result = await _runesRepository.UpdateRuneAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<Runes>> GetRunesWithPriceAsync(int pageSize, int offset)
    {
        List<Runes> list = await _runesRepository.GetRunesWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRunesWithPriceCountAsync()
    {
        return await _runesRepository.GetRunesWithPriceCountAsync();
    }

    public async Task<Runes> GetRuneByIdAsync(string Id)
    {
        return await _runesRepository.GetRuneByIdAsync(Id);
    }

    public async Task<Runes> SumPowerRunesPercentAsync(string userId)
    {
        return await _runesRepository.SumPowerRunesPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueRunesIdAsync()
    {
        return await _runesRepository.GetUniqueRunesIdAsync();
    }

    public async Task<List<Runes>> GetRunesWithoutLimitAsync()
    {
        return await _runesRepository.GetRunesWithoutLimitAsync();
    }
}
