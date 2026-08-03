using System.Threading.Tasks;

public interface IUserCardSoldiersMasterService
{
    Task<UserMasters> GetUserCardSoldierMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardSoldierMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<UserMasters> GetSumUserCardSoldiersMasterAsync(string userId, string cardId);
}
