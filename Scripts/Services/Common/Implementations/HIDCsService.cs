using System.Collections.Generic;
using System.Threading.Tasks;
public class HIDCsService : IHIDCsService
{
    private static HIDCsService _instance;
    private readonly IHIDCsRepository _HIDCsRepository;

    public HIDCsService(IHIDCsRepository HIDCsRepository)
    {
        _HIDCsRepository = HIDCsRepository;
    }

    public static HIDCsService Create()
    {
        if (_instance == null)
        {
            _instance = new HIDCsService(new HIDCsRepository());
        }
        return _instance;
    }

    public async Task<HIDCs> GetHIDCsAsync(string id)
    {
        return await _HIDCsRepository.GetHIDCsAsync(id);
    }

    public async Task<HIDCs> GetSumHIDCsAsync(string user_id)
    {
        return await _HIDCsRepository.GetSumHIDCsAsync(user_id);
    }

    public async Task InsertOrUpdateHIDCsAsync(string userId, HIDCs HIDCs, string id)
    {
        await _HIDCsRepository.InsertOrUpdateHIDCsAsync(userId, HIDCs, id);
    }

    
}