public interface IUserCardGeneralsMasterRepository
{
    Master GetCardGeneralsMaster(string type, string card_id);
    void InsertOrUpdateCardGeneralsMaster(Master master, string type, string card_id);
    Master GetSumCardGeneralsMaster(string user_id, string card_id);
}
