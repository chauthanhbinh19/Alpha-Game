using System.Collections.Generic;
using System.Threading.Tasks;

public class TitlesService : ITitlesService
{
    private readonly ITitlesRepository _titlesRepository;

    public TitlesService(ITitlesRepository titlesRepository)
    {
        _titlesRepository = titlesRepository;
    }

    public static ITitlesService Create() => ServiceContainer.GetService<ITitlesService>();

    public async Task<List<Titles>> GetTitlesAsync(string search, string rare, int pageSize, int offset)
    {
        List<Titles> list = await _titlesRepository.GetTitlesAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTitlesCountAsync(string search, string rare)
    {
        return await _titlesRepository.GetTitlesCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertTitleAsync(Titles entity)
    {
        var result = await _titlesRepository.InsertTitleAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTitleAsync(Titles entity)
    {
        var result = await _titlesRepository.UpdateTitleAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<Titles>> GetTitlesWithPriceAsync(int pageSize, int offset)
    {
        List<Titles> list = await _titlesRepository.GetTitlesWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTitlesWithPriceCountAsync()
    {
        return await _titlesRepository.GetTitlesWithPriceCountAsync();
    }

    public async Task<Titles> GetTitleByIdAsync(string Id)
    {
        return await _titlesRepository.GetTitleByIdAsync(Id);
    }

    public async Task<Titles> SumPowerTitlesPercentAsync(string userId)
    {
        return await _titlesRepository.SumPowerTitlesPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueTitlesIdAsync()
    {
        return await _titlesRepository.GetUniqueTitlesIdAsync();
    }

    public async Task<List<Titles>> GetTitlesWithoutLimitAsync()
    {
        return await _titlesRepository.GetTitlesWithoutLimitAsync();
    }
}
