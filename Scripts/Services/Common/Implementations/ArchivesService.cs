using System.Collections.Generic;
using System.Threading.Tasks;
public class ArchivesService : IArchivesService
{
    private static ArchivesService _instance;
    private readonly IArchivesRepository _ArchivesRepository;

    public ArchivesService(IArchivesRepository ArchivesRepository)
    {
        _ArchivesRepository = ArchivesRepository;
    }

    public static ArchivesService Create()
    {
        if (_instance == null)
        {
            _instance = new ArchivesService(new ArchivesRepository());
        }
        return _instance;
    }

    public async Task<Archives> GetArchivesAsync(string id)
    {
        return await _ArchivesRepository.GetArchivesAsync(id);
    }

    public async Task<Archives> GetSumArchivesAsync(string user_id)
    {
        return await _ArchivesRepository.GetSumArchivesAsync(user_id);
    }

    public async Task InsertOrUpdateArchivesAsync(string userId, Archives Archives, string id)
    {
        await _ArchivesRepository.InsertOrUpdateArchivesAsync(userId, Archives, id);
    }
}