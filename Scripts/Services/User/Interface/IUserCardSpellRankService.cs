public interface IUserCardSpellRankService
{
    Rank GetCardSpellRank(string type, string card_id);
    void InsertOrUpdateCardSpellRank(Rank rank, string type, string card_id);
    Rank GetSumCardSpellRank(string user_id, string card_id);
}
