using System.Threading.Tasks;

public interface IUserCardSpellsMasterRepository
{
    Task<Master> GetUserCardSpellMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardSpellMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserCardSpellsMasterAsync(string userId, string cardId);
}
