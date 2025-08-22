public interface IUserPetsMasterService
{
    Master GetPetsMaster(string type, string card_id);
    void InsertOrUpdatePetsMaster(Master master, string type, string card_id);
    Master GetSumPetsMaster(string user_id, string card_id);
}
