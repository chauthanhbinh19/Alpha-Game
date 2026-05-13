using System.Collections.Generic;
using System.Threading.Tasks;

public class EmojisService : IEmojisService
{
    private static EmojisService _instance;
    private readonly IEmojisRepository _coresRepository;

    public EmojisService(IEmojisRepository coresRepository)
    {
        _coresRepository = coresRepository;
    }

    public static EmojisService Create()
    {
        if (_instance == null)
        {
            _instance = new EmojisService(new EmojisRepository());
        }
        return _instance;
    }

    public async Task<List<Emojis>> GetEmojisAsync(string search, string rare,int pageSize, int offset)
    {
        List<Emojis> list = await _coresRepository.GetEmojisAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmojisCountAsync(string search, string rare)
    {
        return await _coresRepository.GetEmojisCountAsync(search, rare);
    }

    public async Task<List<Emojis>> GetEmojisWithPriceAsync(int pageSize, int offset)
    {
        List<Emojis> list = await _coresRepository.GetEmojisWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmojisWithPriceCountAsync()
    {
        return await _coresRepository.GetEmojisWithPriceCountAsync();
    }

    public async Task<Emojis> GetEmojiByIdAsync(string Id)
    {
        return await _coresRepository.GetEmojiByIdAsync(Id);
    }

    public async Task<Emojis> SumPowerEmojisPercentAsync()
    {
        return await _coresRepository.SumPowerEmojisPercentAsync();
    }

    public async Task<List<string>> GetUniqueEmojisIdAsync()
    {
        return await _coresRepository.GetUniqueEmojisIdAsync();
    }

    public async Task<List<Emojis>> GetEmojisWithoutLimitAsync()
    {
        return await _coresRepository.GetEmojisWithoutLimitAsync();
    }
}
