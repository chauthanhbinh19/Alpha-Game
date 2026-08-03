using System.Collections.Generic;
using System.Threading.Tasks;

public class ArtifactsService : IArtifactsService
{
    private readonly IArtifactsRepository _artifactsRepository;

    public ArtifactsService(IArtifactsRepository artifactsRepository)
    {
        _artifactsRepository = artifactsRepository;
    }

    public static IArtifactsService Create() => ServiceContainer.GetService<IArtifactsService>();

    public async Task<List<Artifacts>> GetArtifactsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Artifacts> list = await _artifactsRepository.GetArtifactsAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtifactsCountAsync(string search, string rare)
    {
        return await _artifactsRepository.GetArtifactsCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertArtifactAsync(Artifacts entity)
    {
        var result = await _artifactsRepository.InsertArtifactAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateArtifactAsync(Artifacts entity)
    {
        var result = await _artifactsRepository.UpdateArtifactAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<Artifacts>> GetArtifactsWithPriceAsync(int pageSize, int offset)
    {
        List<Artifacts> list = await _artifactsRepository.GetArtifactsWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtifactsWithPriceCountAsync()
    {
        return await _artifactsRepository.GetArtifactsWithPriceCountAsync();
    }

    public async Task<Artifacts> GetArtifactByIdAsync(string Id)
    {
        return await _artifactsRepository.GetArtifactByIdAsync(Id);
    }

    public async Task<Artifacts> SumPowerArtifactsPercentAsync(string userId)
    {
        return await _artifactsRepository.SumPowerArtifactsPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueArtifactsIdAsync()
    {
        return await _artifactsRepository.GetUniqueArtifactsIdAsync();
    }

    public async Task<List<Artifacts>> GetArtifactsWithoutLimitAsync()
    {
        return await _artifactsRepository.GetArtifactsWithoutLimitAsync();
    }
}
