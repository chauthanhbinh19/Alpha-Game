using System.Collections.Generic;
using System.Threading.Tasks;

public class TalismansService : ITalismansService
{
    private readonly ITalismansRepository _talismansRepository;

    public TalismansService(ITalismansRepository talismansRepository)
    {
        _talismansRepository = talismansRepository;
    }

    public static ITalismansService Create() => ServiceContainer.GetService<ITalismansService>();

    public async Task<List<Talismans>> GetTalismansAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Talismans> list = await _talismansRepository.GetTalismansAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTalismansCountAsync(string search, string type, string rare)
    {
        return await _talismansRepository.GetTalismansCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertTalismanAsync(Talismans entity)
    {
        var result = await _talismansRepository.InsertTalismanAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTalismanAsync(Talismans entity)
    {
        var result = await _talismansRepository.UpdateTalismanAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<Talismans>> GetTalismansWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Talismans> list = await _talismansRepository.GetTalismansWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTalismansWithPriceCountAsync(string type)
    {
        return await _talismansRepository.GetTalismansWithPriceCountAsync(type);
    }

    public async Task<Talismans> GetTalismanByIdAsync(string Id)
    {
        return await _talismansRepository.GetTalismanByIdAsync(Id);
    }

    public async Task<Talismans> SumPowerTalismansPercentAsync(string userId)
    {
        return await _talismansRepository.SumPowerTalismansPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueTalismansTypesAsync()
    {
        return await _talismansRepository.GetUniqueTalismansTypesAsync();
    }

    public async Task<List<string>> GetUniqueTalismansIdAsync()
    {
        return await _talismansRepository.GetUniqueTalismansIdAsync();
    }

    public async Task<List<Talismans>> GetTalismansWithoutLimitAsync()
    {
        return await _talismansRepository.GetTalismansWithoutLimitAsync();
    }
}
