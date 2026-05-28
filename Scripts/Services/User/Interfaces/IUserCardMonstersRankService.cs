using System.Threading.Tasks;

public interface IUserCardMonstersRankService
{
    Task<Rank> GetCardMonsterRankAsync(string id, string card_id);
    Task InsertOrUpdateCardMonsterRankAsync(Rank rank, string card_id);
    Task<Rank> GetSumCardMonstersRankAsync(string user_id, string card_id);

}
