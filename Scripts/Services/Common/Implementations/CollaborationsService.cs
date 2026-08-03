using System.Collections.Generic;
using System.Threading.Tasks;

public class CollaborationsService : ICollaborationsService
{
    private readonly ICollaborationsRepository _collaborationsRepository;

    public CollaborationsService(ICollaborationsRepository collaborationsRepository)
    {
        _collaborationsRepository = collaborationsRepository;
    }

    public static ICollaborationsService Create() => ServiceContainer.GetService<ICollaborationsService>();

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

    public async Task<InsertOrUpdateResult<bool>> InsertCollaborationAsync(Collaborations entity)
    {
        var result = await _collaborationsRepository.InsertCollaborationAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCollaborationAsync(Collaborations entity)
    {
        var result = await _collaborationsRepository.UpdateCollaborationAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
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

    public async Task<Collaborations> SumPowerCollaborationsPercentAsync(string userId)
    {
        return await _collaborationsRepository.SumPowerCollaborationsPercentAsync(userId);
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
