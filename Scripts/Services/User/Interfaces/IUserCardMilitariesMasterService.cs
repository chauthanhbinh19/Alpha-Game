using System.Threading.Tasks;

public interface IUserCardMilitariesMasterService
{
    Task<UserMasters> GetUserCardMilitaryMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardMilitaryMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<UserMasters> GetSumUserCardMilitariesMasterAsync(string userId, string cardId);
}
