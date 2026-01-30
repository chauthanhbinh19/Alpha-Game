using System.Collections.Generic;
using System.Threading.Tasks;

public class CollaborationsService : ICollaborationsService
{
    private readonly ICollaborationsRepository _collaborationRepository;

    public CollaborationsService(ICollaborationsRepository collaborationRepository)
    {
        _collaborationRepository = collaborationRepository;
    }

    public static CollaborationsService Create()
    {
        return new CollaborationsService(new CollaborationsRepository());
    }

    public async Task<List<Collaborations>> GetCollaborationsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Collaborations> list = await _collaborationRepository.GetCollaborationsAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationsCountAsync(string search, string rare)
    {
        return await _collaborationRepository.GetCollaborationsCountAsync(search, rare);
    }

    public async Task<List<Collaborations>> GetCollaborationsWithPriceAsync(int pageSize, int offset)
    {
        List<Collaborations> list = await _collaborationRepository.GetCollaborationsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationsWithPriceCountAsync()
    {
        return await _collaborationRepository.GetCollaborationsWithPriceCountAsync();
    }

    public async Task<Collaborations> GetCollaborationByIdAsync(string Id)
    {
        return await _collaborationRepository.GetCollaborationByIdAsync(Id);
    }

    public async Task<Collaborations> SumPowerCollaborationsPercentAsync()
    {
        return await _collaborationRepository.SumPowerCollaborationsPercentAsync();
    }

    public async Task<List<string>> GetUniqueCollaborationsIdAsync()
    {
        return await _collaborationRepository.GetUniqueCollaborationsIdAsync();
    }
}
