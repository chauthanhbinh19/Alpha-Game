public interface IUserCardMonstersMasterService
{
    Master GetCardMonstersMaster(string type, string card_id);
    void InsertOrUpdateCardMonstersMaster(Master master, string type, string card_id);
    Master GetSumCardMonstersMaster(string user_id, string card_id);

}
