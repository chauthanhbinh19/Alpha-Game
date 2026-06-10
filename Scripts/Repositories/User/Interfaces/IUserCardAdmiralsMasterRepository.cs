using System.Threading.Tasks;

public interface IUserCardAdmiralsMasterRepository
{
    Task<Master> GetCardAdmiralMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardAdmiralMasterAsync(string userId, UserMasters userMaster, string card_id);
    Task<Master> GetSumCardAdmiralsMasterAsync(string user_id, string card_id);
}
