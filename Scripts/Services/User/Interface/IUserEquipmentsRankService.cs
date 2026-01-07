using System.Threading.Tasks;

public interface IUserEquipmentsRankService
{
    Task<Rank> GetEquipmentRankAsync(string id, string card_id);
    Task InsertOrUpdateEquipmentRankAsync(Rank rank, string card_id);
    Task<Rank> GetSumEquipmentsRankAsync(string user_id, string card_id);
}
