using System.Threading.Tasks;

public interface IUserCardHeroesMasterRepository
{
    Task<Master> GetCardHeroMasterAsync(string id, string card_id);
    Task InsertOrUpdateCardHeroMasterAsync(Master Master, string card_id);
    Task<Master> GetSumCardHeroesMasterAsync(string user_id, string card_id);
}
