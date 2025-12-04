using System.Threading.Tasks;

public interface IUserCardCaptainsMasterService
{
    Task<Master> GetCardCaptainMasterAsync(string type, string card_id);
    Task InsertOrUpdateCardCaptainMasterAsync(Master Master, string type, string card_id);
    Task<Master> GetSumCardCaptainsMasterAsync(string user_id, string card_id);
}
