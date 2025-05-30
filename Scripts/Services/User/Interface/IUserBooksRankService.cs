public interface IUserBooksRankService
{
    Rank GetBooksRank(string type, string card_id);
    void InsertOrUpdateBooksRank(Rank rank, string type, string card_id);
    Rank GetSumBooksRank(string user_id, string card_id);
}
