using System.Threading.Tasks;

public interface IUserCardMonstersMasterService
{
    Task<Master> GetCardMonsterMasterAsync(string type, string card_id);
    Task InsertOrUpdateCardMonsterMasterAsync(Master Master, string type, string card_id);
    Task<Master> GetSumCardMonstersMasterAsync(string user_id, string card_id);

}
