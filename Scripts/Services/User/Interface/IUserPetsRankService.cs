using System.Threading.Tasks;

public interface IUserPetsRankService
{
    Task<Rank> GetPetRankAsync(string id, string card_id);
    Task InsertOrUpdatePetRankAsync(Rank rank, string card_id);
    Task<Rank> GetSumPetsRankAsync(string user_id, string card_id);
}
