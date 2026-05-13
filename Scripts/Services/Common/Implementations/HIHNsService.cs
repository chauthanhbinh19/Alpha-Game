using System.Collections.Generic;
using System.Threading.Tasks;
public class HIHNsService : IHIHNsService
{
    private static HIHNsService _instance;
    private readonly IHIHNsRepository _HIHNsRepository;

    public HIHNsService(IHIHNsRepository HIHNsRepository)
    {
        _HIHNsRepository = HIHNsRepository;
    }

    public static HIHNsService Create()
    {
        if (_instance == null)
        {
            _instance = new HIHNsService(new HIHNsRepository());
        }
        return _instance;
    }

    public async Task<HIHNs> GetHIHNsAsync(string id)
    {
        return await _HIHNsRepository.GetHIHNsAsync(id);
    }

    public async Task<HIHNs> GetSumHIHNsAsync(string user_id)
    {
        return await _HIHNsRepository.GetSumHIHNsAsync(user_id);
    }

    public async Task InsertOrUpdateHIHNsAsync(string userId, HIHNs HIHNs, string id)
    {
        await _HIHNsRepository.InsertOrUpdateHIHNsAsync(userId, HIHNs, id);
    }

    
}