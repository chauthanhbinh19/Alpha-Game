using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserArtifactsRepository
{
    Task<List<Artifacts>> GetUserArtifactsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArtifactsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserArtifactAsync(Artifacts artifact, string userId);
    Task<bool> InsertOrUpdateUserArtifactsBatchAsync(List<Artifacts> artifacts);
    Task<bool> UpdateArtifactLevelAsync(Artifacts artifact, int cardLevel);
    Task<bool> UpdateArtifactBreakthroughAsync(Artifacts artifact, int star, double quantity);
    Task<Artifacts> GetUserArtifactByIdAsync(string user_id, string Id);
    Task<Artifacts> SumPowerUserArtifactsAsync();
}