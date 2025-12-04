using System.Threading.Tasks;

public interface IUserPetsRankService
{
    Task<Rank> GetPetRankAsync(string type, string card_id);
    Task InsertOrUpdatePetRankAsync(Rank Rank, string type, string card_id);
    Task<Rank> GetSumPetsRankAsync(string user_id, string card_id);
}
