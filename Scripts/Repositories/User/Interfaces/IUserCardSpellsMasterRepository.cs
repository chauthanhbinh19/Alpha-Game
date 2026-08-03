using System.Threading.Tasks;

public interface IUserCardSpellsMasterRepository
{
    Task<UserMasters> GetUserCardSpellMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardSpellMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<UserMasters> GetSumUserCardSpellsMasterAsync(string userId, string cardId);
}
