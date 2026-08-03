using System.Threading.Tasks;

public interface IUserPetsMasterService
{
    Task<UserMasters> GetUserPetMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserPetMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<UserMasters> GetSumUserPetsMasterAsync(string userId, string cardId);
}
