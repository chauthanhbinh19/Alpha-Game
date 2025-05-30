using System.Collections.Generic;

public interface IFeaturesService
{
    Dictionary<string, int> GetFeaturesByType(string type);
    Dictionary<string, int> GetAnimeFeaturesByType(string type);
    // Add other potential methods related to Features data access here if needed
}
