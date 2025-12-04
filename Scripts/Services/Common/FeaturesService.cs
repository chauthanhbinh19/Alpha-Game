using System.Collections.Generic;
using System.Threading.Tasks;

public class FeaturesService : IFeaturesService
{
    private readonly IFeaturesRepository _featuresRepository;

    public FeaturesService(IFeaturesRepository featuresRepository)
    {
        _featuresRepository = featuresRepository;
    }

    public static FeaturesService Create()
    {
        return new FeaturesService(new FeaturesRepository());
    }

    public async Task<Dictionary<string, int>> GetFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, int>> GetAnimeFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetAnimeFeaturesByTypeAsync(type);
    }

    // Implement other methods from IFeaturesService by calling the repository
}
