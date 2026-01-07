using System.Threading.Tasks;

public interface IUserCardSpellsMasterService
{
    Task<Master> GetCardSpellMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardSpellMasterAsync(Master master, string card_id);
    Task<Master> GetSumCardSpellsMasterAsync(string user_id, string card_id);
}
