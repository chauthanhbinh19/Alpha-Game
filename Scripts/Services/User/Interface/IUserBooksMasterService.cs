using System.Threading.Tasks;

public interface IUserBooksMasterService
{
    Task<Master> GetBookMasterAsync(string type, string card_id);
    Task InsertOrUpdateBookMasterAsync(Master master, string type, string card_id);
    Task<Master> GetSumBooksMasterAsync(string user_id, string card_id);
}
