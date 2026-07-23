using System.Threading.Tasks;

public interface IUserCardMilitariesMasterService
{
    Task<Master> GetUserCardMilitaryMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardMilitaryMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserCardMilitariesMasterAsync(string userId, string cardId);
}
