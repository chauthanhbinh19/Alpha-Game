using System.Threading.Tasks;

public interface IUserBooksMasterRepository
{
    Task<Master> GetBookMasterAsync(string id, string card_id);
    Task InsertOrUpdateBookMasterAsync(string userId, UserMasters userMaster, string card_id);
    Task<Master> GetSumBooksMasterAsync(string user_id, string card_id);
}
