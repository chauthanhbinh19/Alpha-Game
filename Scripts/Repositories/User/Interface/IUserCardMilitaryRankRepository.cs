public interface IUserCardMilitaryRankRepository
{
    Rank GetCardMilitaryRank(string type, string card_id);
    void InsertOrUpdateCardMilitaryRank(Rank rank, string type, string card_id);
    Rank GetSumCardMilitaryRank(string user_id, string card_id);
}
