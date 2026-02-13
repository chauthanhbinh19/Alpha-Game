using System.Collections.Generic;
using System.Threading.Tasks;
public class ResearchsService : IResearchsService
{
    private static ResearchsService _instance;
    private readonly IResearchsRepository _ResearchsRepository;

    public ResearchsService(IResearchsRepository ResearchsRepository)
    {
        _ResearchsRepository = ResearchsRepository;
    }

    public static ResearchsService Create()
    {
        if (_instance == null)
        {
            _instance = new ResearchsService(new ResearchsRepository());
        }
        return _instance;
    }

    public async Task<Researchs> GetResearchsAsync(string id)
    {
        return await _ResearchsRepository.GetResearchsAsync(id);
    }

    public async Task<Researchs> GetSumResearchsAsync(string user_id)
    {
        return await _ResearchsRepository.GetSumResearchsAsync(user_id);
    }

    public async Task InsertOrUpdateResearchsAsync(string userId, Researchs Researchs, string id)
    {
        await _ResearchsRepository.InsertOrUpdateResearchsAsync(userId, Researchs, id);
    }
}