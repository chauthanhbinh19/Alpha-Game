using System.Threading.Tasks;

public interface IUserEquipmentsRankService
{
    Task<Rank> GetEquipmentRankAsync(string type, string card_id);
    Task InsertOrUpdateEquipmentRankAsync(Rank Rank, string type, string card_id);
    Task<Rank> GetSumEquipmentsRankAsync(string user_id, string card_id);
}
