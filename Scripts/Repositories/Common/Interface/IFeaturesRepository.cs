using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFeaturesRepository
{
    Task<Dictionary<string, Features>> GetFeaturesByTypeAsync(string type);
    Task<Dictionary<string, Features>> GetAnimeFeaturesByTypeAsync(string type);
    // Add other potential methods related to Features data access here if needed
}
