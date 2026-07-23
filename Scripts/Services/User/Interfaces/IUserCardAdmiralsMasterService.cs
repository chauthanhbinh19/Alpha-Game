using System.Threading.Tasks;

public interface IUserCardAdmiralsMasterService
{
    Task<Master> GetUserCardAdmiralMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardAdmiralMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserCardAdmiralsMasterAsync(string userId, string cardId);
}
