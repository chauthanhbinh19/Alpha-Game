using System.Threading.Tasks;

public interface IUserCardSpellsMasterService
{
    Task<Master> GetCardSpellMasterAsync(string type, string card_id);
    Task InsertOrUpdateCardSpellMasterAsync(Master Master, string type, string card_id);
    Task<Master> GetSumCardSpellsMasterAsync(string user_id, string card_id);
}
