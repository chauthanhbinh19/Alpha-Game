using System.Collections.Generic;
using System.Threading.Tasks;
public class HIINsService : IHIINsService
{
    private static HIINsService _instance;
    private readonly IHIINsRepository _HIINsRepository;

    public HIINsService(IHIINsRepository HIINsRepository)
    {
        _HIINsRepository = HIINsRepository;
    }

    public static HIINsService Create()
    {
        if (_instance == null)
        {
            _instance = new HIINsService(new HIINsRepository());
        }
        return _instance;
    }

    public async Task<HIINs> GetHIINsAsync(string id)
    {
        return await _HIINsRepository.GetHIINsAsync(id);
    }

    public async Task<HIINs> GetSumHIINsAsync(string user_id)
    {
        return await _HIINsRepository.GetSumHIINsAsync(user_id);
    }

    public async Task InsertOrUpdateHIINsAsync(string userId, HIINs HIINs, string id)
    {
        await _HIINsRepository.InsertOrUpdateHIINsAsync(userId, HIINs, id);
    }

    
}