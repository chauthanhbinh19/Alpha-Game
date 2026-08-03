using System.Threading.Tasks;

public interface IUserCardHeroesMasterRepository
{
    Task<UserMasters> GetUserCardHeroMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardHeroMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<UserMasters> GetSumUserCardHeroesMasterAsync(string userId, string cardId);
}
