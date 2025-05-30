using System.Collections.Generic;

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

    public Dictionary<string, int> GetFeaturesByType(string type)
    {
        return _featuresRepository.GetFeaturesByType(type);
    }

    public Dictionary<string, int> GetAnimeFeaturesByType(string type)
    {
        return _featuresRepository.GetAnimeFeaturesByType(type);
    }

    // Implement other methods from IFeaturesService by calling the repository
}
