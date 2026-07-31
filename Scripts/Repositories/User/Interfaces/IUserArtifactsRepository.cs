using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserArtifactsRepository
{
    Task<List<Artifacts>> GetUserArtifactsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArtifactsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Artifacts>> InsertOrUpdateUserArtifactAsync(string userId, Artifacts artifact);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Artifacts>>> InsertOrUpdateUserArtifactsBatchAsync(string userId, List<Artifacts> artifacts);
    Task<InsertOrUpdateResult<bool>> UpdateUserArtifactLevelAsync(string userId, Artifacts artifact);
    Task<InsertOrUpdateResult<bool>> UpdateUserArtifactStarAsync(string userId, Artifacts artifact);
    Task<Artifacts> GetUserArtifactByIdAsync(string userId, string Id);
    Task<Artifacts> SumPowerUserArtifactsAsync(string userId);
}