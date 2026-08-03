using System.Threading.Tasks;

public interface IUserPetsRankService
{
    Task<UserRanks> GetUserPetRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserPetRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserPetsRankAsync(string userId, string cardId);
}
