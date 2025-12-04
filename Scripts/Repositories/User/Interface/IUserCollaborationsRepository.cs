using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCollaborationsRepository
{
    Task<List<Collaborations>> GetUserCollaborationsAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserCollaborationsCountAsync(string user_id, string rare);
    Task<bool> InsertUserCollaborationAsync(Collaborations collaboration, string userId);
    Task<bool> UpdateCollaborationLevelAsync(Collaborations collaboration, int cardLevel);
    Task<bool> UpdateCollaborationBreakthroughAsync(Collaborations collaboration, int star, double quantity);
    Task<Collaborations> GetUserCollaborationByIdAsync(string user_id, string Id);
    Task<Collaborations> SumPowerUserCollaborationsAsync();
}