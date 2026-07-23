using System.Threading.Tasks;

public interface IUserEquipmentsRankRepository
{
    Task<Rank> GetEquipmentRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateEquipmentRankAsync(string userId, Rank Rank, string cardId);
    Task<Rank> GetSumEquipmentsRankAsync(string userId, string cardId);
}
