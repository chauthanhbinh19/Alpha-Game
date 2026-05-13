using System.Collections.Generic;
using System.Threading.Tasks;
public class SSWNsService : ISSWNsService
{
    private static SSWNsService _instance;
    private readonly ISSWNsRepository _SSWNsRepository;

    public SSWNsService(ISSWNsRepository SSWNsRepository)
    {
        _SSWNsRepository = SSWNsRepository;
    }

    public static SSWNsService Create()
    {
        if (_instance == null)
        {
            _instance = new SSWNsService(new SSWNsRepository());
        }
        return _instance;
    }

    public async Task<SSWNs> GetSSWNsAsync(string id)
    {
        return await _SSWNsRepository.GetSSWNsAsync(id);
    }

    public async Task<SSWNs> GetSumSSWNsAsync(string user_id)
    {
        return await _SSWNsRepository.GetSumSSWNsAsync(user_id);
    }

    public async Task InsertOrUpdateSSWNsAsync(string userId, SSWNs SSWNs, string id)
    {
        await _SSWNsRepository.InsertOrUpdateSSWNsAsync(userId, SSWNs, id);
    }
}