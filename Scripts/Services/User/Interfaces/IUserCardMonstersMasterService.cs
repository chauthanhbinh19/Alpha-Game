using System.Threading.Tasks;

public interface IUserCardMonstersMasterService
{
    Task<Master> GetCardMonsterMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardMonsterMasterAsync(string userId, UserMasters userMaster, string card_id);
    Task<Master> GetSumCardMonstersMasterAsync(string user_id, string card_id);

}
