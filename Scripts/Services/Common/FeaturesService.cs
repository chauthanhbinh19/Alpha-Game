using System.Collections.Generic;
using System.Threading.Tasks;

public class FeaturesService : IFeaturesService
{
    private static FeaturesService _instance;
    private readonly IFeaturesRepository _featuresRepository;

    public FeaturesService(IFeaturesRepository featuresRepository)
    {
        _featuresRepository = featuresRepository;
    }

    public static FeaturesService Create()
    {
        if (_instance == null)
        {
            _instance = new FeaturesService(new FeaturesRepository());
        }
        return _instance;
    }

    public async Task<Dictionary<string, Features>> GetFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, Features>> GetAnimeFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetAnimeFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureResearchDTO>> GetResearchFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetResearchFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureArchiveDTO>> GetArchiveFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetArchiveFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureUniverseDTO>> GetUniverseFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetUniverseFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHIINDTO>> GetHIINFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHIINFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureSSWNDTO>> GetSSWNFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetSSWNFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHITNDTO>> GetHITNFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHITNFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHIHNDTO>> GetHIHNFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHIHNFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHIENDTO>> GetHIENFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHIENFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHICADTO>> GetHICAFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHICAFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHIRNDTO>> GetHIRNFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHIRNFeaturesByTypeAsync(type);
    }

    // Implement other methods from IFeaturesService by calling the repository
}
