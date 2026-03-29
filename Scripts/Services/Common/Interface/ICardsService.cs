using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtifactsService
{
    Task<List<string>> GetUniqueArtifactsIdAsync();
    Task<List<Artifacts>> GetArtifactsAsync(string search, string rare, int pageSize, int offset);
    Task<int> GetArtifactsCountAsync(string search, string rare);
    Task<List<Artifacts>> GetArtifactsWithPriceAsync(int pageSize, int offset);
    Task<int> GetArtifactsWithPriceCountAsync();
    Task<Artifacts> GetArtifactByIdAsync(string Id);
    Task<Artifacts> SumPowerArtifactsPercentAsync();
}
