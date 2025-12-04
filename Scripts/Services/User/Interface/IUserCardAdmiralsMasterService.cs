using System.Threading.Tasks;

public interface IUserCardAdmiralsMasterService
{
    Task<Master> GetCardAdmiralMasterAsync(string type, string card_id);
    Task InsertOrUpdateCardAdmiralMasterAsync(Master Master, string type, string card_id);
    Task<Master> GetSumCardAdmiralsMasterAsync(string user_id, string card_id);
}
