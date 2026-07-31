using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserArtifactsService
{
    Task<List<Artifacts>> GetUserArtifactsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArtifactsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtifactAsync(string userId, Artifacts artifact);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtifactsBatchAsync(string userId, List<Artifacts> artifacts);
    Task<bool> UpdateUserArtifactLevelAsync(string userId, Artifacts artifact);
    Task<bool> UpdateUserArtifactStarAsync(string userId, Artifacts artifact);
    Task<Artifacts> GetUserArtifactByIdAsync(string userId, string Id);
    Task<Artifacts> SumPowerUserArtifactsAsync(string userId);
}