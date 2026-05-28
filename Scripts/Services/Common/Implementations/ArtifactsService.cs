using System.Collections.Generic;
using System.Threading.Tasks;

public class ArtifactsService : IArtifactsService
{
    private static ArtifactsService _instance;
    private readonly IArtifactsRepository _artifactsRepository;

    public ArtifactsService(IArtifactsRepository artifactsRepository)
    {
        _artifactsRepository = artifactsRepository;
    }

    public static ArtifactsService Create()
    {
        if (_instance == null)
        {
            _instance = new ArtifactsService(new ArtifactsRepository());
        }
        return _instance;
    }

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

    public async Task<Artifacts> SumPowerArtifactsPercentAsync()
    {
        return await _artifactsRepository.SumPowerArtifactsPercentAsync();
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
