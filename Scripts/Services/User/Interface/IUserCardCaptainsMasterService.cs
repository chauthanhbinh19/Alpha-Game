public interface IUserCardCaptainsMasterService
{
    Master GetCardCaptainsMaster(string type, string card_id);
    void InsertOrUpdateCardCaptainsMaster(Master master, string type, string card_id);
    Master GetSumCardCaptainsMaster(string user_id, string card_id);
}
