using System.Threading.Tasks;

public interface IUserEquipmentsRankService
{
    Task<Rank> GetUserEquipmentRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserEquipmentRankAsync(string userId, Rank rank, string cardId);
    Task<Rank> GetSumUserEquipmentsRankAsync(string userId, string cardId);
}
