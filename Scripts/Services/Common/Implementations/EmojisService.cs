using System.Collections.Generic;
using System.Threading.Tasks;

public class EmojisService : IEmojisService
{
    private readonly IEmojisRepository _emojisRepository;

    public EmojisService(IEmojisRepository emojisRepository)
    {
        _emojisRepository = emojisRepository;
    }

    public static IEmojisService Create() => ServiceContainer.GetService<IEmojisService>();

    public async Task<List<Emojis>> GetEmojisAsync(string search, string rare,int pageSize, int offset)
    {
        List<Emojis> list = await _emojisRepository.GetEmojisAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmojisCountAsync(string search, string rare)
    {
        return await _emojisRepository.GetEmojisCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertEmojiAsync(Emojis entity)
    {
        var result = await _emojisRepository.InsertEmojiAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateEmojiAsync(Emojis entity)
    {
        var result = await _emojisRepository.UpdateEmojiAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<Emojis>> GetEmojisWithPriceAsync(int pageSize, int offset)
    {
        List<Emojis> list = await _emojisRepository.GetEmojisWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmojisWithPriceCountAsync()
    {
        return await _emojisRepository.GetEmojisWithPriceCountAsync();
    }

    public async Task<Emojis> GetEmojiByIdAsync(string Id)
    {
        return await _emojisRepository.GetEmojiByIdAsync(Id);
    }

    public async Task<Emojis> SumPowerEmojisPercentAsync(string userId)
    {
        return await _emojisRepository.SumPowerEmojisPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueEmojisIdAsync()
    {
        return await _emojisRepository.GetUniqueEmojisIdAsync();
    }

    public async Task<List<Emojis>> GetEmojisWithoutLimitAsync()
    {
        return await _emojisRepository.GetEmojisWithoutLimitAsync();
    }
}
