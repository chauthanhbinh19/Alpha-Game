using System.Collections.Generic;
using System.Threading.Tasks;

public class AvatarsService : IAvatarsService
{
    private readonly IAvatarsRepository _avatarsRepository;

    public AvatarsService(IAvatarsRepository avatarsRepository)
    {
        _avatarsRepository = avatarsRepository;
    }

    public static AvatarsService Create()
    {
        return new AvatarsService(new AvatarsRepository());
    }

    public async Task<List<Avatars>> GetAvatarsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Avatars> list = await _avatarsRepository.GetAvatarsAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAvatarsCountAsync(string search, string rare)
    {
        return await _avatarsRepository.GetAvatarsCountAsync(search, rare);
    }

    public async Task<List<Avatars>> GetAvatarsWithPriceAsync(int pageSize, int offset)
    {
        List<Avatars> list = await _avatarsRepository.GetAvatarsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAvatarsWithPriceCountAsync()
    {
        return await _avatarsRepository.GetAvatarsWithPriceCountAsync();
    }

    public async Task<Avatars> GetAvatarByIdAsync(string Id)
    {
        return await _avatarsRepository.GetAvatarByIdAsync(Id);
    }

    public async Task<Avatars> SumPowerAvatarsPercentAsync()
    {
        return await _avatarsRepository.SumPowerAvatarsPercentAsync();
    }

    public async Task<List<string>> GetUniqueAvatarsIdAsync()
    {
        return await _avatarsRepository.GetUniqueAvatarsIdAsync();
    }
}