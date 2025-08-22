public interface IUserCardMilitaryMasterRepository
{
    Master GetCardMilitaryMaster(string type, string card_id);
    void InsertOrUpdateCardMilitaryMaster(Master master, string type, string card_id);
    Master GetSumCardMilitaryMaster(string user_id, string card_id);
}
