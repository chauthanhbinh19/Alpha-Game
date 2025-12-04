using System.Threading.Tasks;

public interface IUserCardMilitariesRankRepository
{
    Task<Rank> GetCardMilitaryRankAsync(string type, string card_id);
    Task InsertOrUpdateCardMilitaryRankAsync(Rank Rank, string type, string card_id);
    Task<Rank> GetSumCardMilitariesRankAsync(string user_id, string card_id);
}
