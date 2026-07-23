using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserArtifactsRepository
{
    Task<List<Artifacts>> GetUserArtifactsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArtifactsCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserArtifactAsync(Artifacts artifact, string userId);
    Task<bool> InsertOrUpdateUserArtifactsBatchAsync(string userId, List<Artifacts> artifacts);
    Task<bool> UpdateUserArtifactLevelAsync(string userId, Artifacts artifact);
    Task<bool> UpdateUserArtifactStarAsync(string userId, Artifacts artifact);
    Task<bool> UpdateUserArtifactBreakthroughAsync(string userId, Artifacts artifact, int star, double quantity);
    Task<Artifacts> GetUserArtifactByIdAsync(string userId, string Id);
    Task<Artifacts> SumPowerUserArtifactsAsync(string userId);
}