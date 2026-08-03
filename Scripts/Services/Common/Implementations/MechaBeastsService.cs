using System.Collections.Generic;
using System.Threading.Tasks;

public class MechaBeastsService : IMechaBeastsService
{
    private readonly IMechaBeastsRepository _mechaBeastsRepository;

    public MechaBeastsService(IMechaBeastsRepository mechaBeastsRepository)
    {
        _mechaBeastsRepository = mechaBeastsRepository;
    }

    public static IMechaBeastsService Create() => ServiceContainer.GetService<IMechaBeastsService>();

    public async Task<List<MechaBeasts>> GetMechaBeastsAsync(string search, string rare, int pageSize, int offset)
    {
        List<MechaBeasts> list = await _mechaBeastsRepository.GetMechaBeastsAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsCountAsync(string search, string rare)
    {
        return await _mechaBeastsRepository.GetMechaBeastsCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertMechaBeastAsync(MechaBeasts entity)
    {
        var result = await _mechaBeastsRepository.InsertMechaBeastAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateMechaBeastAsync(MechaBeasts entity)
    {
        var result = await _mechaBeastsRepository.UpdateMechaBeastAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<MechaBeasts>> GetMechaBeastsWithPriceAsync(int pageSize, int offset)
    {
        List<MechaBeasts> list = await _mechaBeastsRepository.GetMechaBeastsWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsWithPriceCountAsync()
    {
        return await _mechaBeastsRepository.GetMechaBeastsWithPriceCountAsync();
    }

    public async Task<MechaBeasts> GetMechaBeastByIdAsync(string Id)
    {
        return await _mechaBeastsRepository.GetMechaBeastByIdAsync(Id);
    }

    public async Task<MechaBeasts> SumPowerMechaBeastsPercentAsync(string userId)
    {
        return await _mechaBeastsRepository.SumPowerMechaBeastsPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueMechaBeastsIdAsync()
    {
        return await _mechaBeastsRepository.GetUniqueMechaBeastsIdAsync();
    }

    public async Task<List<MechaBeasts>> GetMechaBeastsWithoutLimitAsync()
    {
        return await _mechaBeastsRepository.GetMechaBeastsWithoutLimitAsync();
    }
}
