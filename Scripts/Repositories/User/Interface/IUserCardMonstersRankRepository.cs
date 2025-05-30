public interface IUserCardMonstersRankRepository
{
    Rank GetCardMonstersRank(string type, string card_id);
    void InsertOrUpdateCardMonstersRank(Rank rank, string type, string card_id);
    Rank GetSumCardMonstersRank(string user_id, string card_id);

}
