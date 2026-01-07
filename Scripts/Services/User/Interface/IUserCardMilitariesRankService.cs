using System.Threading.Tasks;

public interface IUserCardMilitariesRankService
{
    Task<Rank> GetCardMilitaryRankAsync(string id, string card_id);
    Task InsertOrUpdateCardMilitaryRankAsync(Rank rank, string card_id);
    Task<Rank> GetSumCardMilitariesRankAsync(string user_id, string card_id);
}
