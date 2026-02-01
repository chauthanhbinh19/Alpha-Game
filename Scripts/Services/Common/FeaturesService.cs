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

    // Implement other methods from IFeaturesService by calling the repository
}
