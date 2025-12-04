using System.Threading.Tasks;

public interface IUserCardHeroesMasterRepository
{
    Task<Master> GetCardHeroMasterAsync(string type, string card_id);
    Task InsertOrUpdateCardHeroMasterAsync(Master Master, string type, string card_id);
    Task<Master> GetSumCardHeroesMasterAsync(string user_id, string card_id);
}
