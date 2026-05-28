using System.Threading.Tasks;

public interface IUserCardCaptainsMasterService
{
    Task<Master> GetCardCaptainMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardCaptainMasterAsync(Master master, string card_id);
    Task<Master> GetSumCardCaptainsMasterAsync(string user_id, string card_id);
}
