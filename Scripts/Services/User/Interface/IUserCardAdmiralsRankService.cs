public interface IUserCardAdmiralsRankService
{
    Rank GetCardAdmiralsRank(string type, string card_id);
    void InsertOrUpdateCardAdmiralsRank(Rank rank, string type, string card_id);
    Rank GetSumCardAdmiralsRank(string user_id, string card_id);
}
