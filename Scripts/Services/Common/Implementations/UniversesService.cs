using System.Collections.Generic;
using System.Threading.Tasks;
public class UniversesService : IUniversesService
{
    private static UniversesService _instance;
    private readonly IUniversesRepository _UniversesRepository;

    public UniversesService(IUniversesRepository UniversesRepository)
    {
        _UniversesRepository = UniversesRepository;
    }

    public static UniversesService Create()
    {
        if (_instance == null)
        {
            _instance = new UniversesService(new UniversesRepository());
        }
        return _instance;
    }

    public async Task<Universes> GetUniversesAsync(string id)
    {
        return await _UniversesRepository.GetUniversesAsync(id);
    }

    public async Task<Universes> GetSumUniversesAsync(string user_id)
    {
        return await _UniversesRepository.GetSumUniversesAsync(user_id);
    }

    public async Task InsertOrUpdateUniversesAsync(string userId, Universes Universes, string id)
    {
        await _UniversesRepository.InsertOrUpdateUniversesAsync(userId, Universes, id);
    }

}