public interface IUserEquipmentsRankService
{
    Rank GetEquipmentsRank(string type, string card_id);
    void InsertOrUpdateEquipmentsRank(Rank rank, string type, string card_id);
    Rank GetSumEquipmentsRank(string user_id, string card_id);
}
