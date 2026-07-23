using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCollaborationsService
{
    Task<List<Collaborations>> GetUserCollaborationsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserCollaborationsCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserCollaborationAsync(Collaborations collaboration, string userId);
    Task<bool> InsertOrUpdateUserCollaborationsBatchAsync(string userId, List<Collaborations> collaborations);
    Task<bool> UpdateUserCollaborationLevelAsync(string userId, Collaborations collaboration);
    Task<bool> UpdateUserCollaborationStarAsync(string userId, Collaborations collaboration);
    Task<bool> UpdateUserCollaborationBreakthroughAsync(string userId, Collaborations collaboration, int star, double quantity);
    Task<Collaborations> GetUserCollaborationByIdAsync(string userId, string Id);
    Task<Collaborations> SumPowerUserCollaborationsAsync(string userId);
}