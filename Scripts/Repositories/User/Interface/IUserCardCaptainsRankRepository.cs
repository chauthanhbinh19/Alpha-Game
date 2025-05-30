public interface IUserCardCaptainsRankRepository
{
    Rank GetCardCaptainsRank(string type, string card_id);
    void InsertOrUpdateCardCaptainsRank(Rank rank, string type, string card_id);
    Rank GetSumCardCaptainsRank(string user_id, string card_id);
}
