using System.Threading.Tasks;

public interface IUserCardSoldiersMasterService
{
    Task<Master> GetCardSoldierMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardSoldierMasterAsync(Master master, string card_id);
    Task<Master> GetSumCardSoldiersMasterAsync(string user_id, string card_id);
}
