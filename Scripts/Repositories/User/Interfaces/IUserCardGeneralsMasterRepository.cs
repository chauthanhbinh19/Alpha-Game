using System.Threading.Tasks;

public interface IUserCardGeneralsMasterRepository
{
    Task<UserMasters> GetUserCardGeneralMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardGeneralMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<UserMasters> GetSumUserCardGeneralsMasterAsync(string userId, string cardId);
}
