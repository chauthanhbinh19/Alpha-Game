using System.Collections.Generic;
using System.Threading.Tasks;
public class HICAsService : IHICAsService
{
    private static HICAsService _instance;
    private readonly IHICAsRepository _HICAsRepository;

    public HICAsService(IHICAsRepository HICAsRepository)
    {
        _HICAsRepository = HICAsRepository;
    }

    public static HICAsService Create()
    {
        if (_instance == null)
        {
            _instance = new HICAsService(new HICAsRepository());
        }
        return _instance;
    }

    public async Task<HICAs> GetHICAsAsync(string id)
    {
        return await _HICAsRepository.GetHICAsAsync(id);
    }

    public async Task<HICAs> GetSumHICAsAsync(string user_id)
    {
        return await _HICAsRepository.GetSumHICAsAsync(user_id);
    }

    public async Task InsertOrUpdateHICAsAsync(string userId, HICAs HICAs, string id)
    {
        await _HICAsRepository.InsertOrUpdateHICAsAsync(userId, HICAs, id);
    }
}