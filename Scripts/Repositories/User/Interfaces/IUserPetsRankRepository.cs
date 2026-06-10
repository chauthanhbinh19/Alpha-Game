using System.Threading.Tasks;

public interface IUserPetsRankRepository
{
    Task<Rank> GetPetRankAsync(string id, string card_id);
    Task InsertOrUpdatePetRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumPetsRankAsync(string user_id, string card_id);
}
