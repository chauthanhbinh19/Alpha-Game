public interface IUserCardAdmiralsMasterRepository
{
    Master GetCardAdmiralsMaster(string type, string card_id);
    void InsertOrUpdateCardAdmiralsMaster(Master master, string type, string card_id);
    Master GetSumCardAdmiralsMaster(string user_id, string card_id);
}
