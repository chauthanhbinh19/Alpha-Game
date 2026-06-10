using System.Threading.Tasks;

public interface IUserPetsMasterRepository
{
    Task<Master> GetPetMasterAsync(string id, string card_id);
    Task InsertOrUpdatePetMasterAsync(string userId, UserMasters userMaster, string card_id);
    Task<Master> GetSumPetsMasterAsync(string user_id, string card_id);
}
