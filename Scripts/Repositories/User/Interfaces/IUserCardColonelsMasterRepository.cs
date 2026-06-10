using System.Threading.Tasks;

public interface IUserCardColonelsMasterRepository
{
    Task<Master> GetCardColonelMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardColonelMasterAsync(string userId, UserMasters userMaster, string card_id);
    Task<Master> GetSumCardColonelsMasterAsync(string user_id, string card_id);
}
