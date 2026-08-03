using System.Threading.Tasks;

public interface IUserEquipmentsRankRepository
{
    Task<UserRanks> GetEquipmentRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateEquipmentRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumEquipmentsRankAsync(string userId, string cardId);
}
