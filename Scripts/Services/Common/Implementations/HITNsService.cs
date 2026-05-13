using System.Collections.Generic;
using System.Threading.Tasks;
public class HITNsService : IHITNsService
{
    private static HITNsService _instance;
    private readonly IHITNsRepository _HITNsRepository;

    public HITNsService(IHITNsRepository HITNsRepository)
    {
        _HITNsRepository = HITNsRepository;
    }

    public static HITNsService Create()
    {
        if (_instance == null)
        {
            _instance = new HITNsService(new HITNsRepository());
        }
        return _instance;
    }

    public async Task<HITNs> GetHITNsAsync(string id)
    {
        return await _HITNsRepository.GetHITNsAsync(id);
    }

    public async Task<HITNs> GetSumHITNsAsync(string user_id)
    {
        return await _HITNsRepository.GetSumHITNsAsync(user_id);
    }

    public async Task InsertOrUpdateHITNsAsync(string userId, HITNs HITNs, string id)
    {
        await _HITNsRepository.InsertOrUpdateHITNsAsync(userId, HITNs, id);
    }
}