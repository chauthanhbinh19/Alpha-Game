using System.Threading.Tasks;

public interface IUserCardHeroesMasterService
{
    Task<Master> GetUserCardHeroMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardHeroMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserCardHeroesMasterAsync(string userId, string cardId);
}
