using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCollaborationsService
{
    Task<List<Collaborations>> GetUserCollaborationsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserCollaborationsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationAsync(string userId, Collaborations collaboration);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationsBatchAsync(string userId, List<Collaborations> collaborations);
    Task<bool> UpdateUserCollaborationLevelAsync(string userId, Collaborations collaboration);
    Task<bool> UpdateUserCollaborationStarAsync(string userId, Collaborations collaboration);
    Task<Collaborations> GetUserCollaborationByIdAsync(string userId, string Id);
    Task<Collaborations> SumPowerUserCollaborationsAsync(string userId);
}