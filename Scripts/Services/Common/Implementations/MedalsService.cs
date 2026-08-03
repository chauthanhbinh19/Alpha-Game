using System.Collections.Generic;
using System.Threading.Tasks;

public class MedalsService : IMedalsService
{
    private readonly IMedalsRepository _medalsRepository;

    public MedalsService(IMedalsRepository medalsRepository)
    {
        _medalsRepository = medalsRepository;
    }

    public static IMedalsService Create() => ServiceContainer.GetService<IMedalsService>();

    public async Task<List<Medals>> GetMedalsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Medals> list = await _medalsRepository.GetMedalsAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMedalsCountAsync(string search, string rare)
    {
        return await _medalsRepository.GetMedalsCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertMedalAsync(Medals entity)
    {
        var result = await _medalsRepository.InsertMedalAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateMedalAsync(Medals entity)
    {
        var result = await _medalsRepository.UpdateMedalAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<Medals>> GetMedalsWithPriceAsync(int pageSize, int offset)
    {
        List<Medals> list = await _medalsRepository.GetMedalsWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMedalsWithPriceCountAsync()
    {
        return await _medalsRepository.GetMedalsWithPriceCountAsync();
    }

    public async Task<Medals> GetMedalByIdAsync(string Id)
    {
        return await _medalsRepository.GetMedalByIdAsync(Id);
    }

    public async Task<Medals> SumPowerMedalsPercentAsync(string userId)
    {
        return await _medalsRepository.SumPowerMedalsPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueMedalsIdAsync()
    {
        return await _medalsRepository.GetUniqueMedalsIdAsync();
    }

    public async Task<List<Medals>> GetMedalsWithoutLimitAsync()
    {
        return await _medalsRepository.GetMedalsWithoutLimitAsync();
    }
}
