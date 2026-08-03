using System.Threading.Tasks;

public interface IUserBooksMasterService
{
    Task<UserMasters> GetUserBookMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserBookMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<UserMasters> GetSumUserBooksMasterAsync(string userId, string cardId);
}
