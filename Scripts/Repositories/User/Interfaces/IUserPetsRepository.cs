using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPetsRepository
{
    Task<List<Pets>> GetUserPetsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<Pets>> GetUserPetsTeamAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserPetsTypesTeamAsync(string userId, string teamId);
    Task<int> GetUserPetsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Pets>> InsertOrUpdateUserPetAsync(string userId, Pets pet);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Pets>>> InsertOrUpdateUserPetsBatchAsync(string userId, List<Pets> pets);
    Task<InsertOrUpdateResult<bool>> UpdateUserPetLevelAsync(string userId, Pets pet);
    Task<InsertOrUpdateResult<bool>> UpdateUserPetStarAsync(string userId, Pets pet);
    Task<bool> UpdateTeamUserPetAsync(string userId, string team_id, string cardId);
    Task<Pets> GetUserPetByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId);
    Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId);
}