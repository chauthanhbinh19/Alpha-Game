using System.Collections.Generic;
using System.Threading.Tasks;
public class HICBsService : IHICBsService
{
    private static HICBsService _instance;
    private readonly IHICBsRepository _HICBsRepository;

    public HICBsService(IHICBsRepository HICBsRepository)
    {
        _HICBsRepository = HICBsRepository;
    }

    public static HICBsService Create()
    {
        if (_instance == null)
        {
            _instance = new HICBsService(new HICBsRepository());
        }
        return _instance;
    }

    public async Task<HICBs> GetHICBsAsync(string id)
    {
        return await _HICBsRepository.GetHICBsAsync(id);
    }

    public async Task<HICBs> GetSumHICBsAsync(string user_id)
    {
        return await _HICBsRepository.GetSumHICBsAsync(user_id);
    }

    public async Task InsertOrUpdateHICBsAsync(string userId, HICBs HICBs, string id)
    {
        await _HICBsRepository.InsertOrUpdateHICBsAsync(userId, HICBs, id);
    }

    
}