using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserArtifactsRepository
{
    Task<List<Artifacts>> GetUserArtifactsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserArtifactsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserArtifactAsync(Artifacts Artifacts, string userId);
    Task<bool> UpdateArtifactLevelAsync(Artifacts Artifacts, int cardLevel);
    Task<bool> UpdateArtifactBreakthroughAsync(Artifacts Artifacts, int star, double quantity);
    Task<Artifacts> GetUserArtifactByIdAsync(string user_id, string Id);
    Task<Artifacts> SumPowerUserArtifactsAsync();
}