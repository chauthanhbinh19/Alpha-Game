using System.Collections.Generic;
using System.Threading.Tasks;
public class HIENsService : IHIENsService
{
    private static HIENsService _instance;
    private readonly IHIENsRepository _HIENsRepository;

    public HIENsService(IHIENsRepository HIENsRepository)
    {
        _HIENsRepository = HIENsRepository;
    }

    public static HIENsService Create()
    {
        if (_instance == null)
        {
            _instance = new HIENsService(new HIENsRepository());
        }
        return _instance;
    }

    public async Task<HIENs> GetHIENsAsync(string id)
    {
        return await _HIENsRepository.GetHIENsAsync(id);
    }

    public async Task<HIENs> GetSumHIENsAsync(string user_id)
    {
        return await _HIENsRepository.GetSumHIENsAsync(user_id);
    }

    public async Task InsertOrUpdateHIENsAsync(string userId, HIENs HIENs, string id)
    {
        await _HIENsRepository.InsertOrUpdateHIENsAsync(userId, HIENs, id);
    }

    
}