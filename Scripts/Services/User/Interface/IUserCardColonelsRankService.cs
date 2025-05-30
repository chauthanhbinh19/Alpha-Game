public interface IUserCardColonelsRankService
{
    Rank GetCardColonelsRank(string type, string card_id);
    void InsertOrUpdateCardColonelsRank(Rank rank, string type, string card_id);
    Rank GetSumCardColonelsRank(string user_id, string card_id);
}
