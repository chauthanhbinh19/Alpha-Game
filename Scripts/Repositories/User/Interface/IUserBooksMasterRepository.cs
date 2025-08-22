public interface IUserBooksMasterRepository
{
    Master GetBooksMaster(string type, string card_id);
    void InsertOrUpdateBooksMaster(Master master, string type, string card_id);
    Master GetSumBooksMaster(string user_id, string card_id);
}
