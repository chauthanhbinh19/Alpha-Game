using System.Threading.Tasks;

public interface IUserEquipmentsRankRepository
{
    Task<Rank> GetEquipmentRankAsync(string id, string card_id);
    Task InsertOrUpdateEquipmentRankAsync(Rank Rank, string card_id);
    Task<Rank> GetSumEquipmentsRankAsync(string user_id, string card_id);
}
