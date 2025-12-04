using System.Collections.Generic;
using System.Threading.Tasks;
public class ScienceFictionService : IScienceFictionService
{
    private readonly IScienceFictionRepository _scienceFictionRepository;

    public ScienceFictionService(IScienceFictionRepository scienceFictionRepository)
    {
        _scienceFictionRepository = scienceFictionRepository;
    }

    public static ScienceFictionService Create()
    {
        return new ScienceFictionService(new ScienceFictionRepository());
    }

    public async Task<ScienceFiction> GetScienceFictionAsync(string type)
    {
        return await _scienceFictionRepository.GetScienceFictionAsync(type);
    }

    public async Task<ScienceFiction> GetSumScienceFictionAsync(string user_id)
    {
        return await _scienceFictionRepository.GetSumScienceFictionAsync(user_id);
    }

    public async Task InsertOrUpdateScienceFictionAsync(string userId, ScienceFiction scienceFiction, string type)
    {
        await _scienceFictionRepository.InsertOrUpdateScienceFictionAsync(userId, scienceFiction, type);
    }
}