public interface IUserCardSpellMasterService
{
    Master GetCardSpellMaster(string type, string card_id);
    void InsertOrUpdateCardSpellMaster(Master master, string type, string card_id);
    Master GetSumCardSpellMaster(string user_id, string card_id);
}
