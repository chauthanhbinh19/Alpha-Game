using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCollaborationsService
{
    Task<Collaborations> GetNewLevelPowerAsync(Collaborations c, double coefficient);
    Task<Collaborations> GetNewBreakthroughPowerAsync(Collaborations c, double coefficient);
    Task<List<Collaborations>> GetUserCollaborationsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserCollaborationsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserCollaborationAsync(Collaborations collaboration, string userId);
    Task<bool> UpdateCollaborationLevelAsync(Collaborations collaboration, int cardLevel);
    Task<bool> UpdateCollaborationBreakthroughAsync(Collaborations collaboration, int star, double quantity);
    Task<Collaborations> GetUserCollaborationByIdAsync(string user_id, string Id);
    Task<Collaborations> SumPowerUserCollaborationsAsync();
}