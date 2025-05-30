public interface IUserCardHeroesRankService
{
    Rank GetCardHeroesRank(string type, string card_id);
    void InsertOrUpdateCardHeroesRank(Rank rank, string type, string card_id);
    Rank GetSumCardHeroesRank(string user_id, string card_id);
}
