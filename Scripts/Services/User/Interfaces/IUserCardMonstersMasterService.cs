using System.Threading.Tasks;

public interface IUserCardMonstersMasterService
{
    Task<Master> GetUserCardMonsterMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardMonsterMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserCardMonstersMasterAsync(string userId, string cardId);

}
