using System.Threading.Tasks;

public interface IUserCardGeneralsMasterRepository
{
    Task<Master> GetCardGeneralMasterAsync(string type, string card_id);
    Task InsertOrUpdateCardGeneralMasterAsync(Master Master, string type, string card_id);
    Task<Master> GetSumCardGeneralsMasterAsync(string user_id, string card_id);
}
