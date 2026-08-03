using System.Threading.Tasks;

public interface IUserCardMonstersMasterRepository
{
    Task<UserMasters> GetUserCardMonsterMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardMonsterMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<UserMasters> GetSumUserCardMonstersMasterAsync(string userId, string cardId);
}
