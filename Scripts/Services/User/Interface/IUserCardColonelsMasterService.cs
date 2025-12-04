using System.Threading.Tasks;

public interface IUserCardColonelsMasterService
{
    Task<Master> GetCardColonelMasterAsync(string type, string card_id);
    Task InsertOrUpdateCardColonelMasterAsync(Master Master, string type, string card_id);
    Task<Master> GetSumCardColonelsMasterAsync(string user_id, string card_id);
}
