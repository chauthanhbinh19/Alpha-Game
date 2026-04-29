using System.Threading.Tasks;

public interface IUserPetsMasterService
{
    Task<Master> GetPetMasterAsync(string id, string card_id);
    Task InsertOrUpdatePetMasterAsync(Master master, string card_id);
    Task<Master> GetSumPetsMasterAsync(string user_id, string card_id);
}
