using System.Collections.Generic;
using System.Threading.Tasks;
public class HIRNsService : IHIRNsService
{
    private static HIRNsService _instance;
    private readonly IHIRNsRepository _HIRNsRepository;

    public HIRNsService(IHIRNsRepository HIRNsRepository)
    {
        _HIRNsRepository = HIRNsRepository;
    }

    public static HIRNsService Create()
    {
        if (_instance == null)
        {
            _instance = new HIRNsService(new HIRNsRepository());
        }
        return _instance;
    }

    public async Task<HIRNs> GetHIRNsAsync(string id)
    {
        return await _HIRNsRepository.GetHIRNsAsync(id);
    }

    public async Task<HIRNs> GetSumHIRNsAsync(string user_id)
    {
        return await _HIRNsRepository.GetSumHIRNsAsync(user_id);
    }

    public async Task InsertOrUpdateHIRNsAsync(string userId, HIRNs HIRNs, string id)
    {
        await _HIRNsRepository.InsertOrUpdateHIRNsAsync(userId, HIRNs, id);
    }
}