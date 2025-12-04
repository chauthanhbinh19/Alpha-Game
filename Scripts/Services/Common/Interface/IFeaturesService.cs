using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFeaturesService
{
    Task<Dictionary<string, int>> GetFeaturesByTypeAsync(string type);
    Task<Dictionary<string, int>> GetAnimeFeaturesByTypeAsync(string type);
    // Add other potential methods related to Features data access here if needed
}
