using System.Threading.Tasks;

public interface IUserCardGeneralsMasterService
{
    Task<Master> GetUserCardGeneralMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardGeneralMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserCardGeneralsMasterAsync(string userId, string cardId);
}
