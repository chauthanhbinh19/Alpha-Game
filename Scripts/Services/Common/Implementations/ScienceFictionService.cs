using System.Collections.Generic;
using System.Threading.Tasks;
public class ScienceFictionService : IScienceFictionService
{
    private static ScienceFictionService _instance;
    private readonly IScienceFictionRepository _scienceFictionRepository;

    public ScienceFictionService(IScienceFictionRepository scienceFictionRepository)
    {
        _scienceFictionRepository = scienceFictionRepository;
    }

    public static ScienceFictionService Create()
    {
        if (_instance == null)
        {
            _instance = new ScienceFictionService(new ScienceFictionRepository());
        }
        return _instance;
    }

    public async Task<ScienceFiction> GetScienceFictionAsync(string id)
    {
        return await _scienceFictionRepository.GetScienceFictionAsync(id);
    }

    public async Task<ScienceFiction> GetSumScienceFictionAsync(string user_id)
    {
        return await _scienceFictionRepository.GetSumScienceFictionAsync(user_id);
    }

    public async Task InsertOrUpdateScienceFictionAsync(string userId, ScienceFiction scienceFiction, string id)
    {
        await _scienceFictionRepository.InsertOrUpdateScienceFictionAsync(userId, scienceFiction, id);
    }
}