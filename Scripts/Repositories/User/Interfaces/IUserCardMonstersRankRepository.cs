using System.Threading.Tasks;

public interface IUserCardMonstersRankRepository
{
    Task<Rank> GetCardMonsterRankAsync(string id, string card_id);
    Task InsertOrUpdateCardMonsterRankAsync(Rank Rank, string card_id);
    Task<Rank> GetSumCardMonstersRankAsync(string user_id, string card_id);

}
