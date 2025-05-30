public interface IUserCardGeneralsRankService
{
    Rank GetCardGeneralsRank(string type, string card_id);
    void InsertOrUpdateCardGeneralsRank(Rank rank, string type, string card_id);
    Rank GetSumCardGeneralsRank(string user_id, string card_id);
}
