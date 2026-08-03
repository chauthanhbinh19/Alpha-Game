using System.Threading.Tasks;

public interface IUserCardAdmiralsMasterService
{
    Task<UserMasters> GetUserCardAdmiralMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardAdmiralMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<UserMasters> GetSumUserCardAdmiralsMasterAsync(string userId, string cardId);
}
