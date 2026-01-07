using System.Threading.Tasks;

public interface IUserPetsRankRepository
{
    Task<Rank> GetPetRankAsync(string id, string card_id);
    Task InsertOrUpdatePetRankAsync(Rank Rank, string card_id);
    Task<Rank> GetSumPetsRankAsync(string user_id, string card_id);
}
