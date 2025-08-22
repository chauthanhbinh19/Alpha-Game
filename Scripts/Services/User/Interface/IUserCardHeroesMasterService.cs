public interface IUserCardHeroesMasterService
{
    Master GetCardHeroesMaster(string type, string card_id);
    void InsertOrUpdateCardHeroesMaster(Master master, string type, string card_id);
    Master GetSumCardHeroesMaster(string user_id, string card_id);
}
