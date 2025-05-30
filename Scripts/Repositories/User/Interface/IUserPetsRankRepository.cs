public interface IUserPetsRankRepository
{
    Rank GetPetsRank(string type, string card_id);
    void InsertOrUpdatePetsRank(Rank rank, string type, string card_id);
    Rank GetSumPetsRank(string user_id, string card_id);
}
