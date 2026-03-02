using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFeaturesRepository
{
    Task<Dictionary<string, Features>> GetFeaturesByTypeAsync(string type);
    Task<Dictionary<string, FeatureResearchDTO>> GetResearchFeaturesByTypeAsync(string type);
    Task<Dictionary<string, FeatureArchiveDTO>> GetArchiveFeaturesByTypeAsync(string type);
    Task<Dictionary<string, FeatureUniverseDTO>> GetUniverseFeaturesByTypeAsync(string type);
    Task<Dictionary<string, FeatureHIINDTO>> GetHIINFeaturesByTypeAsync(string type);
    Task<Dictionary<string, FeatureSSWNDTO>> GetSSWNFeaturesByTypeAsync(string type);
    Task<Dictionary<string, FeatureHITNDTO>> GetHITNFeaturesByTypeAsync(string type);
    Task<Dictionary<string, FeatureHIHNDTO>> GetHIHNFeaturesByTypeAsync(string type);
    Task<Dictionary<string, FeatureHIENDTO>> GetHIENFeaturesByTypeAsync(string type);
    Task<Dictionary<string, FeatureHICADTO>> GetHICAFeaturesByTypeAsync(string type);
    Task<Dictionary<string, FeatureHIRNDTO>> GetHIRNFeaturesByTypeAsync(string type);
    Task<Dictionary<string, Features>> GetAnimeFeaturesByTypeAsync(string type);
    // Add other potential methods related to Features data access here if needed
}
