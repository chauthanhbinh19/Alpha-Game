using System.Threading.Tasks;

public interface IUserCardColonelsMasterService
{
    Task<UserMasters> GetUserCardColonelMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardColonelMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<UserMasters> GetSumUserCardColonelsMasterAsync(string userId, string cardId);
}
