using System.Threading.Tasks;

public interface IUserPetsMasterRepository
{
    Task<Master> GetUserPetMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserPetMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserPetsMasterAsync(string userId, string cardId);
}
