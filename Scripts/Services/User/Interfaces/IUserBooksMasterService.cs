using System.Threading.Tasks;

public interface IUserBooksMasterService
{
    Task<Master> GetUserBookMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserBookMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserBooksMasterAsync(string userId, string cardId);
}
