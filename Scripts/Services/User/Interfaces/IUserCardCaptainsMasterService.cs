using System.Threading.Tasks;

public interface IUserCardCaptainsMasterService
{
    Task<Master> GetUserCardCaptainMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardCaptainMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserCardCaptainsMasterAsync(string userId, string cardId);
}
