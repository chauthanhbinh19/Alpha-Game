using System.Threading.Tasks;

public interface IUserPetsMasterService
{
    Task<Master> GetPetMasterAsync(string type, string card_id);
    Task InsertOrUpdatePetMasterAsync(Master Master, string type, string card_id);
    Task<Master> GetSumPetsMasterAsync(string user_id, string card_id);
}
