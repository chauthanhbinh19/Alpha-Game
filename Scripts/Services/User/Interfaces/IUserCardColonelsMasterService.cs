using System.Threading.Tasks;

public interface IUserCardColonelsMasterService
{
    Task<Master> GetUserCardColonelMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardColonelMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserCardColonelsMasterAsync(string userId, string cardId);
}
