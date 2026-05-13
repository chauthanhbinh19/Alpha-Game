using System.Collections.Generic;
using System.Threading.Tasks;
public class HISNsService : IHISNsService
{
    private static HISNsService _instance;
    private readonly IHISNsRepository _HISNsRepository;

    public HISNsService(IHISNsRepository HISNsRepository)
    {
        _HISNsRepository = HISNsRepository;
    }

    public static HISNsService Create()
    {
        if (_instance == null)
        {
            _instance = new HISNsService(new HISNsRepository());
        }
        return _instance;
    }

    public async Task<HISNs> GetHISNsAsync(string id)
    {
        return await _HISNsRepository.GetHISNsAsync(id);
    }

    public async Task<HISNs> GetSumHISNsAsync(string user_id)
    {
        return await _HISNsRepository.GetSumHISNsAsync(user_id);
    }

    public async Task InsertOrUpdateHISNsAsync(string userId, HISNs HISNs, string id)
    {
        await _HISNsRepository.InsertOrUpdateHISNsAsync(userId, HISNs, id);
    }
}