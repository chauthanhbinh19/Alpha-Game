using System.Threading.Tasks;

public interface IUserCardCaptainsMasterRepository
{
    Task<Master> GetCardCaptainMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardCaptainMasterAsync(Master Master, string card_id);
    Task<Master> GetSumCardCaptainsMasterAsync(string user_id, string card_id);
}
