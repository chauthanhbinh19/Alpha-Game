public interface IUserCardColonelsMasterService
{
    Master GetCardColonelsMaster(string type, string card_id);
    void InsertOrUpdateCardColonelsMaster(Master master, string type, string card_id);
    Master GetSumCardColonelsMaster(string user_id, string card_id);
}
