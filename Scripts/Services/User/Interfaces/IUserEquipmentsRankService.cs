using System.Threading.Tasks;

public interface IUserEquipmentsRankService
{
    Task<UserRanks> GetUserEquipmentRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserEquipmentRankAsync(string userId, UserRanks rank, string cardId);
    Task<UserRanks> GetSumUserEquipmentsRankAsync(string userId, string cardId);
}
