using System.Threading.Tasks;

public interface IUserPetsRankService
{
    Task<Rank> GetUserPetRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserPetRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumUserPetsRankAsync(string userId, string cardId);
}
