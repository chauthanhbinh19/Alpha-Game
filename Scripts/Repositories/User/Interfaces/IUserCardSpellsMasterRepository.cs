using System.Threading.Tasks;

public interface IUserCardSpellsMasterRepository
{
    Task<Master> GetCardSpellMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardSpellMasterAsync(Master Master, string card_id);
    Task<Master> GetSumCardSpellsMasterAsync(string user_id, string card_id);
}
