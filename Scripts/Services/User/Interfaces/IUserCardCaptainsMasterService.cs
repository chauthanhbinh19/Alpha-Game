using System.Threading.Tasks;

public interface IUserCardCaptainsMasterService
{
    Task<UserMasters> GetUserCardCaptainMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardCaptainMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<UserMasters> GetSumUserCardCaptainsMasterAsync(string userId, string cardId);
}
