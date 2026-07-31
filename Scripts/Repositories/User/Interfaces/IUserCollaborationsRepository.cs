using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCollaborationsRepository
{
    Task<List<Collaborations>> GetUserCollaborationsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserCollaborationsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Collaborations>> InsertOrUpdateUserCollaborationAsync(string userId, Collaborations collaboration);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Collaborations>>> InsertOrUpdateUserCollaborationsBatchAsync(string userId, List<Collaborations> collaborations);
    Task<InsertOrUpdateResult<bool>> UpdateUserCollaborationLevelAsync(string userId, Collaborations collaboration);
    Task<InsertOrUpdateResult<bool>> UpdateUserCollaborationStarAsync(string userId, Collaborations collaboration);
    Task<Collaborations> GetUserCollaborationByIdAsync(string userId, string Id);
    Task<Collaborations> SumPowerUserCollaborationsAsync(string userId);
}