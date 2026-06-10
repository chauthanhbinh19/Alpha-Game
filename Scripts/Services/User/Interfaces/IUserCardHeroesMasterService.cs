using System.Threading.Tasks;

public interface IUserCardHeroesMasterService
{
    Task<Master> GetCardHeroMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardHeroMasterAsync(string userId, UserMasters userMaster, string card_id);
    Task<Master> GetSumCardHeroesMasterAsync(string user_id, string card_id);
}
