using System.Threading.Tasks;

public interface IUserCardSoldiersMasterRepository
{
    Task<Master> GetUserCardSoldierMasterAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardSoldierMasterAsync(string userId, UserMasters userMaster, string cardId);
    Task<Master> GetSumUserCardSoldiersMasterAsync(string userId, string cardId);
}
