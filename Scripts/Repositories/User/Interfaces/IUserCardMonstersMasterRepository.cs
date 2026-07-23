using System.Threading.Tasks;

public interface IUserCardMonstersMasterRepository
{
    Task<Master> GetUserCardMonsterMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardMonsterMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserCardMonstersMasterAsync(string userId, string cardId);

}
