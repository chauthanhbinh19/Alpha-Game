using System.Collections.Generic;
using System.Threading.Tasks;

public class CollaborationsService : ICollaborationsService
{
    private static CollaborationsService _instance;
    private readonly ICollaborationsRepository _collaborationsRepository;

    public CollaborationsService(ICollaborationsRepository collaborationsRepository)
    {
        _collaborationsRepository = collaborationsRepository;
    }

    public static CollaborationsService Create()
    {
        if (_instance == null)
        {
            _instance = new CollaborationsService(new CollaborationsRepository());
        }
        return _instance;
    }

    public async Task<List<Collaborations>> GetCollaborationsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Collaborations> list = await _collaborationsRepository.GetCollaborationsAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationsCountAsync(string search, string rare)
    {
        return await _collaborationsRepository.GetCollaborationsCountAsync(search, rare);
    }

    public async Task<List<Collaborations>> GetCollaborationsWithPriceAsync(int pageSize, int offset)
    {
        List<Collaborations> list = await _collaborationsRepository.GetCollaborationsWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationsWithPriceCountAsync()
    {
        return await _collaborationsRepository.GetCollaborationsWithPriceCountAsync();
    }

    public async Task<Collaborations> GetCollaborationByIdAsync(string Id)
    {
        return await _collaborationsRepository.GetCollaborationByIdAsync(Id);
    }

    public async Task<Collaborations> SumPowerCollaborationsPercentAsync()
    {
        return await _collaborationsRepository.SumPowerCollaborationsPercentAsync();
    }

    public async Task<List<string>> GetUniqueCollaborationsIdAsync()
    {
        return await _collaborationsRepository.GetUniqueCollaborationsIdAsync();
    }

    public async Task<List<Collaborations>> GetCollaborationsWithoutLimitAsync()
    {
        return await _collaborationsRepository.GetCollaborationsWithoutLimitAsync();
    }
}
